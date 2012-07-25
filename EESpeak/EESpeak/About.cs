/*
	EESpeak
	
	Copyright (C) Anthony Clay, ZarthCode LLC 2012.

	anthony.clay [at] zarthcode [dot] com

	http://zarthcode.com/products/eespeak-a-voice-based-lookup-tool/
 
	https://github.com/zarthcode/EESpeak
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EESpeak
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
		}

		private void About_Load(object sender, EventArgs e)
		{
			versionLabel.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		}

        private void aboutOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
