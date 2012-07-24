EESpeak - ©2012 ZarthCode LLC
www.zarthcode.com/EESpeak

---

Version History:

1.2

Released onto github GPL - https://github.com/zarthcode/EESpeak
*Fix: Recognition is now disabled during speech synthesis/playback.


1.1

*Fixed: "4 band resistor Yellow Violet Gold Gold" gives 0R instead of 4.7R.
*Fixed: If you have speech disabled and say "metric prefixes on" the program crashes.
*Added: Proper installer, program icons, not to mention this readme.

1.0 - Initial release

---
HOW TO USE:

Upon program start, the default windows speech recognizer will be initialized/and started.
(Microsoft SAPI is the default. If you haven't trained it, it would help to do so.)

Simply say any of the supported commands:

"Exit EE Speak"

"4-band {resistor|capacitor|inductor|lookup} {color}{color}{color}{color}"

"5-band {resistor|capacitor|inductor|lookup} {color}{color}{color}{color}{color}"

"EIA {resistor|capacitor|inductor|lookup} {number}{number}{letter}"

"Speech {On|Off}"

"Metric prefixes {On|Off}"

---
KNOWN ISSUES:

1. 	The program is still quite sensitive to background speech,
	especially the exit command.

2.	EIA lookups currently only work for E96 series values.

3. 	EIA lookups must be read aloud as digits. (e.g. "EIA lookup seventeen A" fails,
	while "EIA lookup one seven A" works.)

4.	Metric prefixes don't work consistently for all values.

5.	Metric prefixes for capacitors don't follow standard conventions, should
	prefer µF over mF.


---

If find this software to be useful, and/or would like to see it extended/improved,
or discover any problems, contact me:

Contact Info:

Anthony Clay
ZarthCode LLC
anthony.clay@zarthcode.com