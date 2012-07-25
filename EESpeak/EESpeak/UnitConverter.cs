using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EESpeak
{
    public static class UnitConverter
    {
        public static string ToEngineeringNotation(this double d)
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

        public static string ToEngineeringNotationSpeech(this double d)
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

        public static string GetUnit(string command)
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

        public static string GetSpokenUnit(string unit)
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
    }
}
