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
	partial class SettingsPanel
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPanel));
			this.comPortLabel = new System.Windows.Forms.Label();
			this.comBox = new System.Windows.Forms.ComboBox();
			this.gpsStatusLabel = new System.Windows.Forms.Label();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.DeviceRefresher = new System.Windows.Forms.Timer(this.components);
			this.trigPortLab = new System.Windows.Forms.Label();
			this.triggerStatusLabel = new System.Windows.Forms.Label();
			this.caliberButton = new System.Windows.Forms.Button();
			this.triggerCaliberLabel = new System.Windows.Forms.Label();
			this.triggerCaliberCount = new System.Windows.Forms.NumericUpDown();
			this.onLabel = new System.Windows.Forms.Label();
			this.meterCount = new System.Windows.Forms.NumericUpDown();
			this.meterLabel = new System.Windows.Forms.Label();
			this.trigBox = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.triggerCaliberCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.meterCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trigBox)).BeginInit();
			this.SuspendLayout();
			// 
			// comPortLabel
			// 
			this.comPortLabel.AutoSize = true;
			this.comPortLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comPortLabel.Location = new System.Drawing.Point(12, 30);
			this.comPortLabel.Name = "comPortLabel";
			this.comPortLabel.Size = new System.Drawing.Size(213, 18);
			this.comPortLabel.TabIndex = 255;
			this.comPortLabel.Text = "درگاه ورودی دستگاه موقعیت یاب:";
			// 
			// comBox
			// 
			this.comBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comBox.FormattingEnabled = true;
			this.comBox.Location = new System.Drawing.Point(231, 26);
			this.comBox.Name = "comBox";
			this.comBox.Size = new System.Drawing.Size(154, 27);
			this.comBox.TabIndex = 0;
			this.comBox.SelectedValueChanged += new System.EventHandler(this.OnValueChanged);
			// 
			// gpsStatusLabel
			// 
			this.gpsStatusLabel.AutoSize = true;
			this.gpsStatusLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gpsStatusLabel.ForeColor = System.Drawing.Color.Red;
			this.gpsStatusLabel.Location = new System.Drawing.Point(12, 74);
			this.gpsStatusLabel.Name = "gpsStatusLabel";
			this.gpsStatusLabel.Size = new System.Drawing.Size(365, 36);
			this.gpsStatusLabel.TabIndex = 256;
			this.gpsStatusLabel.Text = "خطا در اتصال دستگاه موقعیت یاب در درگاه خواسته شده.\r\nلطفا درگاه دیگری را امتحان ک" +
    "نید.";
			// 
			// saveButton
			// 
			this.saveButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveButton.Location = new System.Drawing.Point(297, 309);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(88, 37);
			this.saveButton.TabIndex = 4;
			this.saveButton.Text = "ذخیره";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.OnSaveClick);
			// 
			// cancelButton
			// 
			this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cancelButton.Location = new System.Drawing.Point(12, 309);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 37);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "انصراف";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// DeviceRefresher
			// 
			this.DeviceRefresher.Interval = 1000;
			this.DeviceRefresher.Tick += new System.EventHandler(this.OnInterval);
			// 
			// trigPortLab
			// 
			this.trigPortLab.AutoSize = true;
			this.trigPortLab.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.trigPortLab.Location = new System.Drawing.Point(12, 144);
			this.trigPortLab.Name = "trigPortLab";
			this.trigPortLab.Size = new System.Drawing.Size(167, 18);
			this.trigPortLab.TabIndex = 257;
			this.trigPortLab.Text = "پورت شبکه دستگاه تریگر:";
			// 
			// triggerStatusLabel
			// 
			this.triggerStatusLabel.AutoSize = true;
			this.triggerStatusLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.triggerStatusLabel.ForeColor = System.Drawing.Color.Red;
			this.triggerStatusLabel.Location = new System.Drawing.Point(12, 189);
			this.triggerStatusLabel.Name = "triggerStatusLabel";
			this.triggerStatusLabel.Size = new System.Drawing.Size(363, 36);
			this.triggerStatusLabel.TabIndex = 259;
			this.triggerStatusLabel.Text = "خطا در اتصال دستگاه موقعیت یاب در پورت خواسته شده.\r\nلطفا پورت دیگری را امتحان کنی" +
    "د.";
			// 
			// caliberButton
			// 
			this.caliberButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.caliberButton.Location = new System.Drawing.Point(124, 309);
			this.caliberButton.Name = "caliberButton";
			this.caliberButton.Size = new System.Drawing.Size(145, 37);
			this.caliberButton.TabIndex = 6;
			this.caliberButton.Text = "شروع کالیبریشن";
			this.caliberButton.UseVisualStyleBackColor = true;
			this.caliberButton.Click += new System.EventHandler(this.OnCaliberClick);
			// 
			// triggerCaliberLabel
			// 
			this.triggerCaliberLabel.AutoSize = true;
			this.triggerCaliberLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.triggerCaliberLabel.Location = new System.Drawing.Point(12, 260);
			this.triggerCaliberLabel.Name = "triggerCaliberLabel";
			this.triggerCaliberLabel.Size = new System.Drawing.Size(166, 18);
			this.triggerCaliberLabel.TabIndex = 261;
			this.triggerCaliberLabel.Text = "مقدار کالیبر دستگاه تریگر:\r\n";
			// 
			// triggerCaliberCount
			// 
			this.triggerCaliberCount.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.triggerCaliberCount.Location = new System.Drawing.Point(184, 257);
			this.triggerCaliberCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.triggerCaliberCount.Name = "triggerCaliberCount";
			this.triggerCaliberCount.Size = new System.Drawing.Size(67, 27);
			this.triggerCaliberCount.TabIndex = 2;
			// 
			// onLabel
			// 
			this.onLabel.AutoSize = true;
			this.onLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.onLabel.Location = new System.Drawing.Point(257, 260);
			this.onLabel.Name = "onLabel";
			this.onLabel.Size = new System.Drawing.Size(28, 18);
			this.onLabel.TabIndex = 263;
			this.onLabel.Text = "بازه";
			// 
			// meterCount
			// 
			this.meterCount.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.meterCount.Location = new System.Drawing.Point(291, 257);
			this.meterCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.meterCount.Name = "meterCount";
			this.meterCount.Size = new System.Drawing.Size(67, 27);
			this.meterCount.TabIndex = 3;
			this.meterCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// meterLabel
			// 
			this.meterLabel.AutoSize = true;
			this.meterLabel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.meterLabel.Location = new System.Drawing.Point(364, 260);
			this.meterLabel.Name = "meterLabel";
			this.meterLabel.Size = new System.Drawing.Size(27, 18);
			this.meterLabel.TabIndex = 265;
			this.meterLabel.Text = "متر";
			// 
			// trigBox
			// 
			this.trigBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.trigBox.Location = new System.Drawing.Point(318, 141);
			this.trigBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.trigBox.Name = "trigBox";
			this.trigBox.Size = new System.Drawing.Size(67, 27);
			this.trigBox.TabIndex = 1;
			this.trigBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// SettingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 355);
			this.Controls.Add(this.trigBox);
			this.Controls.Add(this.meterLabel);
			this.Controls.Add(this.meterCount);
			this.Controls.Add(this.onLabel);
			this.Controls.Add(this.triggerCaliberCount);
			this.Controls.Add(this.triggerCaliberLabel);
			this.Controls.Add(this.caliberButton);
			this.Controls.Add(this.triggerStatusLabel);
			this.Controls.Add(this.trigPortLab);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.gpsStatusLabel);
			this.Controls.Add(this.comBox);
			this.Controls.Add(this.comPortLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsPanel";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "تنظیمات";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			this.Load += new System.EventHandler(this.Initialization);
			((System.ComponentModel.ISupportInitialize)(this.triggerCaliberCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.meterCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trigBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label comPortLabel;
		private System.Windows.Forms.ComboBox comBox;
		private System.Windows.Forms.Label gpsStatusLabel;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Timer DeviceRefresher;
		private System.Windows.Forms.Label trigPortLab;
		private System.Windows.Forms.Label triggerStatusLabel;
		private System.Windows.Forms.Button caliberButton;
		private System.Windows.Forms.Label triggerCaliberLabel;
		private System.Windows.Forms.NumericUpDown triggerCaliberCount;
		private System.Windows.Forms.Label onLabel;
		private System.Windows.Forms.NumericUpDown meterCount;
		private System.Windows.Forms.Label meterLabel;
		private System.Windows.Forms.NumericUpDown trigBox;
	}
}