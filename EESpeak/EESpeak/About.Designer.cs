namespace EESpeak
{
	partial class About
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.aboutOK = new System.Windows.Forms.Button();
			this.versionLabel = new System.Windows.Forms.Label();
			this.githubLink = new System.Windows.Forms.LinkLabel();
			this.siteLabel = new System.Windows.Forms.LinkLabel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.authorLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// aboutOK
			// 
			this.aboutOK.Location = new System.Drawing.Point(479, 232);
			this.aboutOK.Name = "aboutOK";
			this.aboutOK.Size = new System.Drawing.Size(176, 35);
			this.aboutOK.TabIndex = 0;
			this.aboutOK.Text = "OK";
			this.aboutOK.UseVisualStyleBackColor = true;
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(613, 46);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(42, 13);
			this.versionLabel.TabIndex = 1;
			this.versionLabel.Text = "Version";
			// 
			// githubLink
			// 
			this.githubLink.AutoSize = true;
			this.githubLink.Location = new System.Drawing.Point(276, 245);
			this.githubLink.Name = "githubLink";
			this.githubLink.Size = new System.Drawing.Size(197, 13);
			this.githubLink.TabIndex = 2;
			this.githubLink.TabStop = true;
			this.githubLink.Text = "https://github.com/zarthcode/EESpeak";
			// 
			// siteLabel
			// 
			this.siteLabel.AutoSize = true;
			this.siteLabel.Location = new System.Drawing.Point(276, 12);
			this.siteLabel.Name = "siteLabel";
			this.siteLabel.Size = new System.Drawing.Size(330, 13);
			this.siteLabel.TabIndex = 3;
			this.siteLabel.TabStop = true;
			this.siteLabel.Text = "http://zarthcode.com/products/eespeak-a-voice-based-lookup-tool/";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(258, 260);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(276, 232);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(192, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "EESpeak is Licensed under the GPLv3";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(279, 62);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(376, 154);
			this.richTextBox1.TabIndex = 6;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// authorLabel
			// 
			this.authorLabel.AutoSize = true;
			this.authorLabel.Location = new System.Drawing.Point(276, 46);
			this.authorLabel.Name = "authorLabel";
			this.authorLabel.Size = new System.Drawing.Size(198, 13);
			this.authorLabel.TabIndex = 7;
			this.authorLabel.Text = "Written by Anthony Clay, ZarthCode LLC";
			// 
			// About
			// 
			this.AcceptButton = this.aboutOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(667, 281);
			this.ControlBox = false;
			this.Controls.Add(this.authorLabel);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.siteLabel);
			this.Controls.Add(this.githubLink);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.aboutOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About EESpeak";
			this.Load += new System.EventHandler(this.About_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button aboutOK;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.LinkLabel githubLink;
		private System.Windows.Forms.LinkLabel siteLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label authorLabel;
	}
}