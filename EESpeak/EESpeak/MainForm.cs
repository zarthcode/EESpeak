/*
	EESpeak

	Copyright (C) Anthony Clay, ZarthCode LLC 2012.

	anthony.clay [at] zarthcode [dot] com

 * Github Repository:
	https://github.com/zarthcode/EESpeak
 * Product Page:
	http://zarthcode.com/products/eespeak-a-voice-based-lookup-tool/

*/

/*
  This file is part of EESpeak.

	EESpeak is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	EESpeak is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with EESpeak.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace EESpeak
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeRecognition();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Quit program.
            Application.Exit();
        }

        private SpeechRecognitionEngine speechRecognizer;
        private Grammar appGrammar;

        private SpeechSynthesizer speechEngine;
        private bool speechEnabled = false;
        private bool verboseSpeech = false;
        private bool sayMetricPrefixes = false;

        private void InitializeSynthesis()
        {
            speechEngine = new SpeechSynthesizer();
            speechEnabled = true;

            speechEngine.Volume = 100;
            speechEngine.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);
            speechEngine.Rate = -4;
            speechRecognizer.RecognizeAsyncStop();
            speechEngine.Speak("Speech Enabled.");
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void InitializeRecognition()
        {
            speechRecognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            if (speechRecognizer != null)
            {
                appGrammar = CreateGrammar();
                speechRecognizer.LoadGrammar(appGrammar);

                speechRecognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognizer_SpeechRecognized);
                speechRecognizer.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(speechRecognizer_SpeechRecognitionRejected);
                speechRecognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(speechRecognizer_SpeechDetected);

                speechRecognizer.SetInputToDefaultAudioDevice();
                speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
                toolStripStatusLabel1.Text = "Recognition started.";
            }
        }

        private void speechRecognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Listening...";
        }

        private void speechRecognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Rejected: " + e.Result.Text + " (" + (e.Result.Confidence * 100) + "%)";
        }

        private void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Command: " + e.Result.Text + " (" + (e.Result.Confidence * 100) + "%)";
            speechRecognizer.RecognizeAsyncStop();

            // Exit command
            if (Matches(e.Result.Text, exitStrings))
            {
                if (speechEnabled)
                {
                    speechEngine.Speak("Goodbye!");
                }
                Application.Exit();
            }

            // enable/disable speech
            else if (e.Result.Text == enableSpeechCommand)
            {
                if (!speechEnabled)
                {
                    InitializeSynthesis();
                }
            }

            else if (e.Result.Text == disableSpeechCommand)
            {
                if (speechEnabled)
                {
                    speechEngine.Pause();
                    speechEngine.Dispose();
                    speechEngine = null;
                    speechEnabled = false;
                }
            }

            // Four band lookup
            if (Matches(e.Result.Text, fourBandStrings))
            {
                // Determine the unit
                string unit = GetUnit(e.Result.Text);

                // Evaluate the four bands.
                string[] result = ParseBands(unit,
                    (string)e.Result.Semantics["first_band"].Value,
                    (string)e.Result.Semantics["second_band"].Value,
                    (string)e.Result.Semantics["third_band"].Value,
                    (string)e.Result.Semantics["fourth_band"].Value);

                outputLabel.Text = result[0];

                if (speechEnabled)
                {
                    speechEngine.SpeakAsync(e.Result.Text + " is " + result[1]);
                    speechEngine.Resume();
                }
            }

            // Five band lookup
            else if (Matches(e.Result.Text, fiveBandStrings))
            {
                // Determine the unit
                string unit = GetUnit(e.Result.Text);

                // Evaluate the four bands.
                string[] result = ParseBands(unit,
                    (string)e.Result.Semantics["first_band"].Value,
                    (string)e.Result.Semantics["second_band"].Value,
                    (string)e.Result.Semantics["third_band"].Value,
                    (string)e.Result.Semantics["fourth_band"].Value,
                    (string)e.Result.Semantics["fifth_band"].Value);

                outputLabel.Text = result[0];
                if (speechEnabled)
                {
                    int i = (sayMetricPrefixes) ? 2 : 1;
                    speechEngine.SpeakAsync(e.Result.Text + " is " + result[i]);
                }
            }

            // EIA lookup
            else if (Matches(e.Result.Text, eiaCommandStrings))
            {
                // Determine the unit
                string unit = GetUnit(e.Result.Text);

                // Evaluate
                string[] result = ParseEIA(unit, (int)e.Result.Semantics["first_number"].Value,
                                            (int)e.Result.Semantics["second_number"].Value,
                                            (string)e.Result.Semantics["letter"].Value);

                outputLabel.Text = result[0];

                if (speechEnabled)
                {
                    int i = (sayMetricPrefixes) ? 2 : 1;
                    speechEngine.SpeakAsync(e.Result.Text + " is " + result[i]);
                }
            }

            // Toggle metric prefixes
            else if (Matches(e.Result.Text, toggleMetricPrefixes))
            {
                if (e.Result.Text == toggleMetricPrefixes[0])
                {
                    sayMetricPrefixes = true;

                    if (speechEnabled)
                        speechEngine.Speak("Prefixes on.");
                }
                else
                {
                    sayMetricPrefixes = false;

                    if (speechEnabled)
                        speechEngine.Speak("Prefixes off.");
                }
            }

            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        // Strings

        private string enableSpeechCommand = "Enable speech";
        private string disableSpeechCommand = "Disable speech";
        private string[] toggleMetricPrefixes = { "Metric Prefixes On", "Metric Prefixes Off" };
        private string[] fourBandStrings = { "Four band lookup", "Four band resistor", "Four band inductor", "Four band capacitor" };
        private string[] fiveBandStrings = { "Five band lookup", "Five band resistor", "Five band inductor", "Five band capacitor" };
        private string[] eiaCommandStrings = { "EIA lookup", "EIA", "EIA resistor", "EIA capacitor", "EIA inductor" };

        // Code numbers
        private string[] numberString = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        // Resistor color wildcards
        private string[] resistorColors = { "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "purple", "gray", "white", "gold", "silver", "none" };

        private int[] resistorDigits = { 0, 1, 2, 3, 4, 5, 6, 7, 7, 8, 9, -1, -2, 0 };
        private string[] resistorTolerances = { "20", "1", "2", "3", "4", "0.5", "0.25", "0.1", "0.1", "0.05", "", "5", "10", "20" };

        // Exit command
        private string[] exitStrings = { "Exit EE Speak" };

        // EIA wildcards
        private string[] eiaNumbers = { "zero", "one", "two", "three", "four", "five",
							"six", "seven", "eight", "nine" };

        private string[] eiaLetters = { "Z", "Y", "R", "X", "S", "A", "B", "H", "C", "D", "E", "F" };
        private string[] eiaPhonemes = { "Zulu", "Yankee", "Romeo", "X-ray", "Sierra", "Alfa", "Bravo", "Henry", "Charlie", "Delta", "Echo", "Foxtrot" };

        /// <summary>
        /// Creates a grammar for all the recognized commands and values.
        /// </summary>
        /// <returns>A grammar for the EESpeak application</returns>
        private Grammar CreateGrammar()
        {
            // Exit grammar
            GrammarBuilder exitGrammar = new GrammarBuilder(new Choices(exitStrings));

            // Enable speech
            GrammarBuilder enableSpeechGrammar = new GrammarBuilder(enableSpeechCommand);

            // Disable speech
            GrammarBuilder disableSpeechGrammar = new GrammarBuilder(disableSpeechCommand);

            // Metric Prefixes
            GrammarBuilder toggleMetricPrefixGrammar = new GrammarBuilder(new Choices(toggleMetricPrefixes));

            // 4-band lookup

            // Create color band semantics
            Choices resistorColorChoices = new Choices();
            GrammarBuilder resistorColorValues = new GrammarBuilder();

            foreach (string s in resistorColors)
            {
                SemanticResultValue temp = new SemanticResultValue(s, s);
                resistorColorChoices.Add(temp);
                resistorColorValues.Append(temp);
            }

            GrammarBuilder fourBandGrammar = new GrammarBuilder(new Choices(fourBandStrings));

            fourBandGrammar.Append(new SemanticResultKey("first_band", resistorColorChoices));
            fourBandGrammar.Append(new SemanticResultKey("second_band", resistorColorChoices));
            fourBandGrammar.Append(new SemanticResultKey("third_band", resistorColorChoices));
            fourBandGrammar.Append(new SemanticResultKey("fourth_band", resistorColorChoices));

            // 5-band lookup

            GrammarBuilder fiveBandGrammar = new GrammarBuilder(new Choices(fiveBandStrings));

            fiveBandGrammar.Append(new SemanticResultKey("first_band", resistorColorChoices));
            fiveBandGrammar.Append(new SemanticResultKey("second_band", resistorColorChoices));
            fiveBandGrammar.Append(new SemanticResultKey("third_band", resistorColorChoices));
            fiveBandGrammar.Append(new SemanticResultKey("fourth_band", resistorColorChoices));
            fiveBandGrammar.Append(new SemanticResultKey("fifth_band", resistorColorChoices));

            // EIA (2 digits, one letter) lookup

            // Create EIA semantics
            Choices eiaNumberChoices = new Choices();
            GrammarBuilder eiaNumberValues = new GrammarBuilder();

            for (int i = 0; i < eiaNumbers.Length; i++)
            {
                SemanticResultValue temp = new SemanticResultValue(eiaNumbers[i], i);
                eiaNumberChoices.Add(temp);
                eiaNumberValues.Append(temp);
            }

            Choices eiaLetterChoices = new Choices();
            GrammarBuilder eiaLetterValues = new GrammarBuilder();
            foreach (string s in eiaLetters)
            {
                SemanticResultValue temp = new SemanticResultValue(s, s);
                eiaLetterChoices.Add(temp);
                eiaLetterValues.Append(temp);
            }

            Choices eiaPhonemeChoices = new Choices();
            GrammarBuilder eiaLetterPhonemes = new GrammarBuilder();
            foreach (string s in eiaPhonemes)
            {
                SemanticResultValue temp = new SemanticResultValue(s, s);
                eiaPhonemeChoices.Add(temp);
                eiaLetterPhonemes.Append(temp);
            }

            GrammarBuilder eiaGrammar = new GrammarBuilder(new Choices(eiaCommandStrings));

            eiaGrammar.Append(new SemanticResultKey("first_number", eiaNumberChoices));
            eiaGrammar.Append(new SemanticResultKey("second_number", eiaNumberChoices));
            eiaGrammar.Append(new SemanticResultKey("letter", new Choices(eiaLetterChoices, eiaPhonemeChoices)));

            // SMD lookup

            // Final grammar
            Choices commandChoices = new Choices(new GrammarBuilder[] { exitGrammar, toggleMetricPrefixGrammar, enableSpeechGrammar, disableSpeechGrammar, fourBandGrammar, fiveBandGrammar, eiaGrammar });
            Grammar appGrammar = new Grammar((GrammarBuilder)commandChoices);
            appGrammar.Name = "commands";

            return appGrammar;
        }

        private string[] ParseEIA(string unit, int firstdigit, int seconddigit, string letter, int decade = 96)
        {
            int index = ((firstdigit * 10) + seconddigit) - 1;
            double Renard = Math.Round(Math.Pow(10, ((double)index / (double)decade)) * 100);

            double Multiplier = 0;
            switch (letter)
            {
                case "Z":
                    Multiplier = -3;
                    break;

                case "Y":
                case "R":
                    Multiplier = -2;
                    break;

                case "X":
                case "S":
                    Multiplier = -1;
                    break;

                case "A":
                    Multiplier = 0;
                    break;

                case "B":
                case "H":
                    Multiplier = 1;
                    break;

                case "C":
                    Multiplier = 2;
                    break;

                case "D":
                    Multiplier = 3;
                    break;

                case "E":
                    Multiplier = 4;
                    break;

                case "F":
                    Multiplier = 5;
                    break;
            }

            Renard *= Math.Pow(10, Multiplier);

            string[] result = { ToEngineeringNotation(Renard) + unit, Renard + "," + GetSpokenUnit(unit), ToEngineeringNotationSpeech(Renard) + "-" + GetSpokenUnit(unit) };

            return result;
        }

        private string[] ParseBands(string unit, string first, string second, string third, string fourth, string fifth = "none")
        {
            int radix = 1;
            double retValue = 0;
            // Digit 3 (5 band only)
            if (fifth != "none")
            {
                // Find color in array
                retValue += (GetDigit(third) * radix);
                radix *= 10;
            }

            // Digit 2
            retValue += (GetDigit(second) * radix);
            radix *= 10;

            // Digit 1
            retValue += (GetDigit(first) * radix);

            // Multiplier
            if (fifth != "none")
            {
                retValue *= Math.Pow(10, GetDigit(fourth));
            }
            else
            {
                retValue *= Math.Pow(10, GetDigit(third));
            }

            // Unit

            // Tolerance
            string tolerance = "±";
            if (fifth != "none")
            {
                tolerance += GetTolerance(fifth) + "%";
            }
            else
            {
                tolerance += GetTolerance(fourth) + "%";
            }

            string[] result = { ToEngineeringNotation(retValue) + unit + tolerance, retValue + GetSpokenUnit(unit) + "," + tolerance, ToEngineeringNotationSpeech(retValue) + "-" + GetSpokenUnit(unit) + "," + tolerance };

            return result;
        }

        private int GetDigit(string color)
        {
            for (int i = 0; i < resistorDigits.Length; i++)
            {
                if (resistorColors[i] == color)
                {
                    return resistorDigits[i];
                }
            }

            throw new KeyNotFoundException();
        }

        private string GetTolerance(string color)
        {
            for (int i = 0; i < resistorDigits.Length; i++)
            {
                if (resistorColors[i] == color)
                {
                    return resistorTolerances[i];
                }
            }

            throw new KeyNotFoundException();
        }

        private string GetUnit(string command)
        {
            if (command.Contains("capacitor"))
            {
                return "F";
            }

            if (command.Contains("resistor"))
            {
                return "Ω";
            }

            if (command.Contains("inductor"))
            {
                return "H";
            }

            return "";
        }

        private string GetSpokenUnit(string unit)
        {
            if (unit == "F")
            {
                return "Farad";
            }

            if (unit == "Ω")
            {
                return "Ohm";
            }

            if (unit == "H")
            {
                return "Henry";
            }

            return "";
        }

        private bool Matches(string command, string[] phrases)
        {
            foreach (string s in phrases)
            {
                if (command.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }

        private string ToEngineeringNotation(double d)
        {
            double exp = Math.Log10(Math.Abs(d));
            if (Math.Abs(d) >= 1)
            {
                switch ((int)Math.Floor(exp))
                {
                    case 0:
                    case 1:
                    case 2:
                        return d.ToString();
                    case 3:
                    case 4:
                    case 5:
                        return (d / 1e3).ToString() + "k";
                    case 6:
                    case 7:
                    case 8:
                        return (d / 1e6).ToString() + "M";
                    case 9:
                    case 10:
                    case 11:
                        return (d / 1e9).ToString() + "G";
                    case 12:
                    case 13:
                    case 14:
                        return (d / 1e12).ToString() + "T";
                    case 15:
                    case 16:
                    case 17:
                        return (d / 1e15).ToString() + "P";
                    case 18:
                    case 19:
                    case 20:
                        return (d / 1e18).ToString() + "E";
                    case 21:
                    case 22:
                    case 23:
                        return (d / 1e21).ToString() + "Z";
                    default:
                        return (d / 1e24).ToString() + "Y";
                }
            }
            else if (Math.Abs(d) > 0)
            {
                switch ((int)Math.Floor(exp))
                {
                    case -1:
                    case -2:
                    case -3:
                        return (d * 1e3).ToString() + "m";
                    case -4:
                    case -5:
                    case -6:
                        return (d * 1e6).ToString() + "μ";
                    case -7:
                    case -8:
                    case -9:
                        return (d * 1e9).ToString() + "n";
                    case -10:
                    case -11:
                    case -12:
                        return (d * 1e12).ToString() + "p";
                    case -13:
                    case -14:
                    case -15:
                        return (d * 1e15).ToString() + "f";
                    case -16:
                    case -17:
                    case -18:
                        return (d * 1e15).ToString() + "a";
                    case -19:
                    case -20:
                    case -21:
                        return (d * 1e15).ToString() + "z";
                    default:
                        return (d * 1e15).ToString() + "y";
                }
            }
            else
            {
                return "0";
            }
        }

        private string ToEngineeringNotationSpeech(double d)
        {
            double exp = Math.Log10(Math.Abs(d));
            if (Math.Abs(d) >= 1)
            {
                switch ((int)Math.Floor(exp))
                {
                    case 0:
                    case 1:
                    case 2:
                        return d.ToString();
                    case 3:
                    case 4:
                    case 5:
                        return (d / 1e3).ToString() + "kilo";
                    case 6:
                    case 7:
                    case 8:
                        return (d / 1e6).ToString() + "Mega";
                    case 9:
                    case 10:
                    case 11:
                        return (d / 1e9).ToString() + "Giga";
                    case 12:
                    case 13:
                    case 14:
                        return (d / 1e12).ToString() + "Tera";
                    case 15:
                    case 16:
                    case 17:
                        return (d / 1e15).ToString() + "Peta";
                    case 18:
                    case 19:
                    case 20:
                        return (d / 1e18).ToString() + "Exa";
                    case 21:
                    case 22:
                    case 23:
                        return (d / 1e21).ToString() + "Z";
                    default:
                        return (d / 1e24).ToString() + "Y";
                }
            }
            else if (Math.Abs(d) > 0)
            {
                switch ((int)Math.Floor(exp))
                {
                    case -1:
                    case -2:
                    case -3:
                        return (d * 1e3).ToString() + "milli";
                    case -4:
                    case -5:
                    case -6:
                        return (d * 1e6).ToString() + "micro";
                    case -7:
                    case -8:
                    case -9:
                        return (d * 1e9).ToString() + "nano";
                    case -10:
                    case -11:
                    case -12:
                        return (d * 1e12).ToString() + "pico";
                    case -13:
                    case -14:
                    case -15:
                        return (d * 1e15).ToString() + "femto";
                    case -16:
                    case -17:
                    case -18:
                        return (d * 1e15).ToString() + "atto";
                    case -19:
                    case -20:
                    case -21:
                        return (d * 1e15).ToString() + "z";
                    default:
                        return (d * 1e15).ToString() + "y";
                }
            }
            else
            {
                return "0";
            }
        }

        private void eESpeakHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string targetURL = "http://zarthcode.com/products/eespeak-a-voice-based-lookup-tool/";

            System.Diagnostics.Process.Start(targetURL);
        }

        private void aboutEESpeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutDialog = new About();
            aboutDialog.ShowDialog(this);
        }
    }
}