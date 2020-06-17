/*
Copyright (C) 2017-2018 Ali Deym
This file is part of Bifler <https://github.com/alideym/bifler>.

Bifler is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Bifler is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Bifler.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace Bifler
{
	partial class BaslerSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaslerSettings));
			this.camTextLabel = new System.Windows.Forms.Label();
			this.cameraLabel = new System.Windows.Forms.Label();
			this.gainTextLabel = new System.Windows.Forms.Label();
			this.exposureTextLabel = new System.Windows.Forms.Label();
			this.GainSlider = new libBifler.UI.BiflerController();
			this.ExposureSlider = new libBifler.UI.BiflerController();
			this.closeButton = new System.Windows.Forms.Button();
			this.widthLabel = new System.Windows.Forms.Label();
			this.WidthSlider = new libBifler.UI.BiflerController();
			this.HeightSlider = new libBifler.UI.BiflerController();
			this.heightLabel = new System.Windows.Forms.Label();
			this.xPosLabel = new System.Windows.Forms.Label();
			this.xPosBar = new libBifler.UI.BiflerController();
			this.yPosLabel = new System.Windows.Forms.Label();
			this.yPosBar = new libBifler.UI.BiflerController();
			this.autoBrightnessLabel = new System.Windows.Forms.Label();
			this.autoBrightnessCheckbox = new System.Windows.Forms.CheckBox();
			this.brightnessLevelLabel = new System.Windows.Forms.Label();
			this.BrightnessBar = new libBifler.UI.BiflerFloatController();
			this.SuspendLayout();
			// 
			// camTextLabel
			// 
			this.camTextLabel.AutoSize = true;
			this.camTextLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camTextLabel.Location = new System.Drawing.Point(12, 9);
			this.camTextLabel.Name = "camTextLabel";
			this.camTextLabel.Size = new System.Drawing.Size(57, 19);
			this.camTextLabel.TabIndex = 255;
			this.camTextLabel.Text = "دوربین:";
			// 
			// cameraLabel
			// 
			this.cameraLabel.AutoSize = true;
			this.cameraLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cameraLabel.Location = new System.Drawing.Point(75, 9);
			this.cameraLabel.Name = "cameraLabel";
			this.cameraLabel.Size = new System.Drawing.Size(0, 19);
			this.cameraLabel.TabIndex = 256;
			// 
			// gainTextLabel
			// 
			this.gainTextLabel.AutoSize = true;
			this.gainTextLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gainTextLabel.Location = new System.Drawing.Point(12, 81);
			this.gainTextLabel.Name = "gainTextLabel";
			this.gainTextLabel.Size = new System.Drawing.Size(84, 19);
			this.gainTextLabel.TabIndex = 257;
			this.gainTextLabel.Text = "افزایش نور:";
			// 
			// exposureTextLabel
			// 
			this.exposureTextLabel.AutoSize = true;
			this.exposureTextLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.exposureTextLabel.Location = new System.Drawing.Point(12, 148);
			this.exposureTextLabel.Name = "exposureTextLabel";
			this.exposureTextLabel.Size = new System.Drawing.Size(99, 19);
			this.exposureTextLabel.TabIndex = 258;
			this.exposureTextLabel.Text = "سرعت شاتر:";
			// 
			// GainSlider
			// 
			this.GainSlider.Location = new System.Drawing.Point(140, 71);
			this.GainSlider.MinimumSize = new System.Drawing.Size(225, 50);
			this.GainSlider.Name = "GainSlider";
			this.GainSlider.NodeName = "GainRaw";
			this.GainSlider.Size = new System.Drawing.Size(622, 50);
			this.GainSlider.TabIndex = 1;
			// 
			// ExposureSlider
			// 
			this.ExposureSlider.Location = new System.Drawing.Point(140, 132);
			this.ExposureSlider.MinimumSize = new System.Drawing.Size(225, 50);
			this.ExposureSlider.Name = "ExposureSlider";
			this.ExposureSlider.NodeName = "ExposureTimeRaw";
			this.ExposureSlider.Size = new System.Drawing.Size(622, 50);
			this.ExposureSlider.TabIndex = 2;
			// 
			// closeButton
			// 
			this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.closeButton.Location = new System.Drawing.Point(327, 544);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(129, 35);
			this.closeButton.TabIndex = 8;
			this.closeButton.Text = "ذخیره";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.OnCloseClick);
			// 
			// widthLabel
			// 
			this.widthLabel.AutoSize = true;
			this.widthLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.widthLabel.Location = new System.Drawing.Point(12, 206);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(82, 19);
			this.widthLabel.TabIndex = 262;
			this.widthLabel.Text = "طول تصویر:";
			// 
			// WidthSlider
			// 
			this.WidthSlider.Location = new System.Drawing.Point(140, 193);
			this.WidthSlider.MinimumSize = new System.Drawing.Size(225, 50);
			this.WidthSlider.Name = "WidthSlider";
			this.WidthSlider.NodeName = "Width";
			this.WidthSlider.Size = new System.Drawing.Size(622, 50);
			this.WidthSlider.TabIndex = 3;
			// 
			// HeightSlider
			// 
			this.HeightSlider.Location = new System.Drawing.Point(140, 252);
			this.HeightSlider.MinimumSize = new System.Drawing.Size(225, 50);
			this.HeightSlider.Name = "HeightSlider";
			this.HeightSlider.NodeName = "Height";
			this.HeightSlider.Size = new System.Drawing.Size(622, 50);
			this.HeightSlider.TabIndex = 4;
			// 
			// heightLabel
			// 
			this.heightLabel.AutoSize = true;
			this.heightLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.heightLabel.Location = new System.Drawing.Point(12, 263);
			this.heightLabel.Name = "heightLabel";
			this.heightLabel.Size = new System.Drawing.Size(88, 19);
			this.heightLabel.TabIndex = 265;
			this.heightLabel.Text = "عرض تصویر:";
			// 
			// xPosLabel
			// 
			this.xPosLabel.AutoSize = true;
			this.xPosLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xPosLabel.Location = new System.Drawing.Point(12, 322);
			this.xPosLabel.Name = "xPosLabel";
			this.xPosLabel.Size = new System.Drawing.Size(110, 19);
			this.xPosLabel.TabIndex = 267;
			this.xPosLabel.Text = "موقعیت طولی:";
			// 
			// xPosBar
			// 
			this.xPosBar.Location = new System.Drawing.Point(140, 311);
			this.xPosBar.MinimumSize = new System.Drawing.Size(225, 50);
			this.xPosBar.Name = "xPosBar";
			this.xPosBar.NodeName = "OffsetX";
			this.xPosBar.Size = new System.Drawing.Size(622, 50);
			this.xPosBar.TabIndex = 5;
			// 
			// yPosLabel
			// 
			this.yPosLabel.AutoSize = true;
			this.yPosLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.yPosLabel.Location = new System.Drawing.Point(12, 380);
			this.yPosLabel.Name = "yPosLabel";
			this.yPosLabel.Size = new System.Drawing.Size(115, 19);
			this.yPosLabel.TabIndex = 269;
			this.yPosLabel.Text = "موقعیت عرضی:";
			// 
			// yPosBar
			// 
			this.yPosBar.Location = new System.Drawing.Point(140, 369);
			this.yPosBar.MinimumSize = new System.Drawing.Size(225, 50);
			this.yPosBar.Name = "yPosBar";
			this.yPosBar.NodeName = "OffsetY";
			this.yPosBar.Size = new System.Drawing.Size(622, 50);
			this.yPosBar.TabIndex = 6;
			// 
			// autoBrightnessLabel
			// 
			this.autoBrightnessLabel.AutoSize = true;
			this.autoBrightnessLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.autoBrightnessLabel.Location = new System.Drawing.Point(645, 9);
			this.autoBrightnessLabel.Name = "autoBrightnessLabel";
			this.autoBrightnessLabel.Size = new System.Drawing.Size(117, 19);
			this.autoBrightnessLabel.TabIndex = 270;
			this.autoBrightnessLabel.Text = "نورپردازی خودکار";
			// 
			// autoBrightnessCheckbox
			// 
			this.autoBrightnessCheckbox.AutoSize = true;
			this.autoBrightnessCheckbox.Location = new System.Drawing.Point(624, 14);
			this.autoBrightnessCheckbox.Name = "autoBrightnessCheckbox";
			this.autoBrightnessCheckbox.Size = new System.Drawing.Size(15, 14);
			this.autoBrightnessCheckbox.TabIndex = 0;
			this.autoBrightnessCheckbox.UseVisualStyleBackColor = true;
			this.autoBrightnessCheckbox.CheckedChanged += new System.EventHandler(this.AutoBrightnessChanged);
			// 
			// brightnessLevelLabel
			// 
			this.brightnessLevelLabel.AutoSize = true;
			this.brightnessLevelLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.brightnessLevelLabel.Location = new System.Drawing.Point(12, 438);
			this.brightnessLevelLabel.Name = "brightnessLevelLabel";
			this.brightnessLevelLabel.Size = new System.Drawing.Size(122, 19);
			this.brightnessLevelLabel.TabIndex = 273;
			this.brightnessLevelLabel.Text = "تفکیک پذیری نور:";
			// 
			// BrightnessBar
			// 
			this.BrightnessBar.Location = new System.Drawing.Point(140, 427);
			this.BrightnessBar.MinimumSize = new System.Drawing.Size(225, 50);
			this.BrightnessBar.Name = "BrightnessBar";
			this.BrightnessBar.NodeName = "AutoTargetBrightness";
			this.BrightnessBar.Size = new System.Drawing.Size(622, 50);
			this.BrightnessBar.TabIndex = 7;
			// 
			// BaslerSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(783, 591);
			this.Controls.Add(this.brightnessLevelLabel);
			this.Controls.Add(this.BrightnessBar);
			this.Controls.Add(this.autoBrightnessCheckbox);
			this.Controls.Add(this.autoBrightnessLabel);
			this.Controls.Add(this.yPosLabel);
			this.Controls.Add(this.yPosBar);
			this.Controls.Add(this.xPosLabel);
			this.Controls.Add(this.xPosBar);
			this.Controls.Add(this.heightLabel);
			this.Controls.Add(this.HeightSlider);
			this.Controls.Add(this.WidthSlider);
			this.Controls.Add(this.widthLabel);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.ExposureSlider);
			this.Controls.Add(this.GainSlider);
			this.Controls.Add(this.exposureTextLabel);
			this.Controls.Add(this.gainTextLabel);
			this.Controls.Add(this.cameraLabel);
			this.Controls.Add(this.camTextLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "BaslerSettings";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.Text = "تنظیمات دوربین";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosingForm);
			this.Load += new System.EventHandler(this.OnInit);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label camTextLabel;
		private System.Windows.Forms.Label cameraLabel;
		private System.Windows.Forms.Label gainTextLabel;
		private System.Windows.Forms.Label exposureTextLabel;
		private libBifler.UI.BiflerController GainSlider;
		private libBifler.UI.BiflerController ExposureSlider;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label widthLabel;
		private libBifler.UI.BiflerController WidthSlider;
		private libBifler.UI.BiflerController HeightSlider;
		private System.Windows.Forms.Label heightLabel;
		private System.Windows.Forms.Label xPosLabel;
		private libBifler.UI.BiflerController xPosBar;
		private System.Windows.Forms.Label yPosLabel;
		private libBifler.UI.BiflerController yPosBar;
		private System.Windows.Forms.Label autoBrightnessLabel;
		private System.Windows.Forms.CheckBox autoBrightnessCheckbox;
		private System.Windows.Forms.Label brightnessLevelLabel;
		private libBifler.UI.BiflerFloatController BrightnessBar;
	}
}