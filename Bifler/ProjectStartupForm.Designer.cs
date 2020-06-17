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
	partial class ProjectStartupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectStartupForm));
			this.nameLabel = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.startLabel = new System.Windows.Forms.Label();
			this.startBox = new System.Windows.Forms.TextBox();
			this.finishLabel = new System.Windows.Forms.Label();
			this.finishBox = new System.Windows.Forms.TextBox();
			this.startButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.codeLabel = new System.Windows.Forms.Label();
			this.codeBox = new System.Windows.Forms.TextBox();
			this.provinceCodeLabel = new System.Windows.Forms.Label();
			this.provinceCodeBox = new System.Windows.Forms.TextBox();
			this.laneLabel = new System.Windows.Forms.Label();
			this.laneBox = new System.Windows.Forms.NumericUpDown();
			this.provinceLabel = new System.Windows.Forms.Label();
			this.provinceBox = new System.Windows.Forms.TextBox();
			this.cityLabel = new System.Windows.Forms.Label();
			this.cityBox = new System.Windows.Forms.TextBox();
			this.startKilometerNumber = new System.Windows.Forms.NumericUpDown();
			this.startKilometerLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pathBox = new System.Windows.Forms.TextBox();
			this.pathChooseButton = new System.Windows.Forms.Button();
			this.directionLabel = new System.Windows.Forms.Label();
			this.directionBox = new System.Windows.Forms.ComboBox();
			this.projectLaneLabel = new System.Windows.Forms.Label();
			this.projectLane = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.laneBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startKilometerNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.projectLane)).BeginInit();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nameLabel.Location = new System.Drawing.Point(12, 18);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(70, 19);
			this.nameLabel.TabIndex = 255;
			this.nameLabel.Text = "نام پروژه:";
			// 
			// nameBox
			// 
			this.nameBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nameBox.Location = new System.Drawing.Point(110, 15);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(495, 27);
			this.nameBox.TabIndex = 0;
			// 
			// startLabel
			// 
			this.startLabel.AutoSize = true;
			this.startLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startLabel.Location = new System.Drawing.Point(12, 113);
			this.startLabel.Name = "startLabel";
			this.startLabel.Size = new System.Drawing.Size(92, 19);
			this.startLabel.TabIndex = 256;
			this.startLabel.Text = "محور شروع:";
			// 
			// startBox
			// 
			this.startBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startBox.Location = new System.Drawing.Point(124, 110);
			this.startBox.Name = "startBox";
			this.startBox.Size = new System.Drawing.Size(189, 27);
			this.startBox.TabIndex = 3;
			// 
			// finishLabel
			// 
			this.finishLabel.AutoSize = true;
			this.finishLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.finishLabel.Location = new System.Drawing.Point(329, 113);
			this.finishLabel.Name = "finishLabel";
			this.finishLabel.Size = new System.Drawing.Size(81, 19);
			this.finishLabel.TabIndex = 257;
			this.finishLabel.Text = "محور پایان:";
			// 
			// finishBox
			// 
			this.finishBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.finishBox.Location = new System.Drawing.Point(416, 110);
			this.finishBox.Name = "finishBox";
			this.finishBox.Size = new System.Drawing.Size(189, 27);
			this.finishBox.TabIndex = 4;
			// 
			// startButton
			// 
			this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
			this.startButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.startButton.Location = new System.Drawing.Point(481, 331);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(124, 42);
			this.startButton.TabIndex = 12;
			this.startButton.Text = "تنظیم پروژه";
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.OnStartReaction);
			// 
			// cancelButton
			// 
			this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cancelButton.Location = new System.Drawing.Point(12, 331);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(124, 42);
			this.cancelButton.TabIndex = 13;
			this.cancelButton.Text = "انصراف";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.OnCancellation);
			// 
			// codeLabel
			// 
			this.codeLabel.AutoSize = true;
			this.codeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.codeLabel.Location = new System.Drawing.Point(329, 64);
			this.codeLabel.Name = "codeLabel";
			this.codeLabel.Size = new System.Drawing.Size(69, 19);
			this.codeLabel.TabIndex = 258;
			this.codeLabel.Text = "کد پروژه:";
			// 
			// codeBox
			// 
			this.codeBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.codeBox.Location = new System.Drawing.Point(416, 61);
			this.codeBox.Name = "codeBox";
			this.codeBox.Size = new System.Drawing.Size(189, 27);
			this.codeBox.TabIndex = 2;
			// 
			// provinceCodeLabel
			// 
			this.provinceCodeLabel.AutoSize = true;
			this.provinceCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.provinceCodeLabel.Location = new System.Drawing.Point(25, 64);
			this.provinceCodeLabel.Name = "provinceCodeLabel";
			this.provinceCodeLabel.Size = new System.Drawing.Size(79, 19);
			this.provinceCodeLabel.TabIndex = 260;
			this.provinceCodeLabel.Text = "کد استان:";
			// 
			// provinceCodeBox
			// 
			this.provinceCodeBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.provinceCodeBox.Location = new System.Drawing.Point(124, 64);
			this.provinceCodeBox.Name = "provinceCodeBox";
			this.provinceCodeBox.Size = new System.Drawing.Size(189, 27);
			this.provinceCodeBox.TabIndex = 1;
			// 
			// laneLabel
			// 
			this.laneLabel.AutoSize = true;
			this.laneLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.laneLabel.Location = new System.Drawing.Point(325, 205);
			this.laneLabel.Name = "laneLabel";
			this.laneLabel.Size = new System.Drawing.Size(78, 19);
			this.laneLabel.TabIndex = 262;
			this.laneLabel.Text = "تعداد لاین:";
			// 
			// laneBox
			// 
			this.laneBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.laneBox.Location = new System.Drawing.Point(557, 201);
			this.laneBox.Name = "laneBox";
			this.laneBox.Size = new System.Drawing.Size(48, 27);
			this.laneBox.TabIndex = 8;
			// 
			// provinceLabel
			// 
			this.provinceLabel.AutoSize = true;
			this.provinceLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.provinceLabel.Location = new System.Drawing.Point(12, 158);
			this.provinceLabel.Name = "provinceLabel";
			this.provinceLabel.Size = new System.Drawing.Size(57, 19);
			this.provinceLabel.TabIndex = 265;
			this.provinceLabel.Text = "استان:";
			// 
			// provinceBox
			// 
			this.provinceBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.provinceBox.Location = new System.Drawing.Point(124, 155);
			this.provinceBox.Name = "provinceBox";
			this.provinceBox.Size = new System.Drawing.Size(189, 27);
			this.provinceBox.TabIndex = 5;
			// 
			// cityLabel
			// 
			this.cityLabel.AutoSize = true;
			this.cityLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cityLabel.Location = new System.Drawing.Point(325, 158);
			this.cityLabel.Name = "cityLabel";
			this.cityLabel.Size = new System.Drawing.Size(85, 19);
			this.cityLabel.TabIndex = 267;
			this.cityLabel.Text = "شهرستان:";
			// 
			// cityBox
			// 
			this.cityBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cityBox.Location = new System.Drawing.Point(416, 155);
			this.cityBox.Name = "cityBox";
			this.cityBox.Size = new System.Drawing.Size(189, 27);
			this.cityBox.TabIndex = 6;
			// 
			// startKilometerNumber
			// 
			this.startKilometerNumber.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startKilometerNumber.Location = new System.Drawing.Point(124, 201);
			this.startKilometerNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.startKilometerNumber.Name = "startKilometerNumber";
			this.startKilometerNumber.Size = new System.Drawing.Size(189, 27);
			this.startKilometerNumber.TabIndex = 7;
			// 
			// startKilometerLabel
			// 
			this.startKilometerLabel.AutoSize = true;
			this.startKilometerLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startKilometerLabel.Location = new System.Drawing.Point(12, 203);
			this.startKilometerLabel.Name = "startKilometerLabel";
			this.startKilometerLabel.Size = new System.Drawing.Size(108, 19);
			this.startKilometerLabel.TabIndex = 269;
			this.startKilometerLabel.Text = "کیلومتر شروع:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 298);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 19);
			this.label1.TabIndex = 270;
			this.label1.Text = "پوشه پروژه:";
			// 
			// pathBox
			// 
			this.pathBox.Enabled = false;
			this.pathBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pathBox.Location = new System.Drawing.Point(126, 295);
			this.pathBox.Name = "pathBox";
			this.pathBox.Size = new System.Drawing.Size(351, 27);
			this.pathBox.TabIndex = 14;
			// 
			// pathChooseButton
			// 
			this.pathChooseButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pathChooseButton.Location = new System.Drawing.Point(483, 295);
			this.pathChooseButton.Name = "pathChooseButton";
			this.pathChooseButton.Size = new System.Drawing.Size(124, 27);
			this.pathChooseButton.TabIndex = 11;
			this.pathChooseButton.Text = "انتخاب";
			this.pathChooseButton.UseVisualStyleBackColor = true;
			this.pathChooseButton.Click += new System.EventHandler(this.ChooseButtonClick);
			// 
			// directionLabel
			// 
			this.directionLabel.AutoSize = true;
			this.directionLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.directionLabel.Location = new System.Drawing.Point(8, 252);
			this.directionLabel.Name = "directionLabel";
			this.directionLabel.Size = new System.Drawing.Size(103, 19);
			this.directionLabel.TabIndex = 271;
			this.directionLabel.Text = "جهت جغرافیا:";
			// 
			// directionBox
			// 
			this.directionBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.directionBox.FormattingEnabled = true;
			this.directionBox.Items.AddRange(new object[] {
            "هیچکدام",
            "شمال به جنوب",
            "شمال به شرق",
            "شمال به غرب",
            "جنوب به شمال",
            "جنوب به شرق",
            "جنوب به غرب",
            "شرق به شمال",
            "شرق به جنوب",
            "شرق به غرب",
            "غرب به شمال",
            "غرب به جنوب",
            "غرب به شرق"});
			this.directionBox.Location = new System.Drawing.Point(124, 249);
			this.directionBox.Name = "directionBox";
			this.directionBox.Size = new System.Drawing.Size(189, 27);
			this.directionBox.TabIndex = 9;
			this.directionBox.Text = "هیچکدام";
			// 
			// projectLaneLabel
			// 
			this.projectLaneLabel.AutoSize = true;
			this.projectLaneLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectLaneLabel.Location = new System.Drawing.Point(325, 249);
			this.projectLaneLabel.Name = "projectLaneLabel";
			this.projectLaneLabel.Size = new System.Drawing.Size(78, 19);
			this.projectLaneLabel.TabIndex = 272;
			this.projectLaneLabel.Text = "لاین پروژه:";
			// 
			// projectLane
			// 
			this.projectLane.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectLane.Location = new System.Drawing.Point(557, 249);
			this.projectLane.Name = "projectLane";
			this.projectLane.Size = new System.Drawing.Size(48, 27);
			this.projectLane.TabIndex = 10;
			// 
			// ProjectStartupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(617, 382);
			this.Controls.Add(this.projectLane);
			this.Controls.Add(this.projectLaneLabel);
			this.Controls.Add(this.directionBox);
			this.Controls.Add(this.directionLabel);
			this.Controls.Add(this.pathChooseButton);
			this.Controls.Add(this.pathBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.startKilometerNumber);
			this.Controls.Add(this.startKilometerLabel);
			this.Controls.Add(this.cityBox);
			this.Controls.Add(this.cityLabel);
			this.Controls.Add(this.provinceBox);
			this.Controls.Add(this.provinceLabel);
			this.Controls.Add(this.laneBox);
			this.Controls.Add(this.laneLabel);
			this.Controls.Add(this.provinceCodeBox);
			this.Controls.Add(this.provinceCodeLabel);
			this.Controls.Add(this.codeBox);
			this.Controls.Add(this.codeLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.finishBox);
			this.Controls.Add(this.finishLabel);
			this.Controls.Add(this.startBox);
			this.Controls.Add(this.startLabel);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.nameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ProjectStartupForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.Text = "شروع پروژه";
			this.Load += new System.EventHandler(this.FormLoaded);
			((System.ComponentModel.ISupportInitialize)(this.laneBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startKilometerNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.projectLane)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label startLabel;
		private System.Windows.Forms.TextBox startBox;
		private System.Windows.Forms.Label finishLabel;
		private System.Windows.Forms.TextBox finishBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label codeLabel;
		private System.Windows.Forms.TextBox codeBox;
		private System.Windows.Forms.Label provinceCodeLabel;
		private System.Windows.Forms.TextBox provinceCodeBox;
		private System.Windows.Forms.Label laneLabel;
		private System.Windows.Forms.NumericUpDown laneBox;
		private System.Windows.Forms.Label provinceLabel;
		private System.Windows.Forms.TextBox provinceBox;
		private System.Windows.Forms.Label cityLabel;
		private System.Windows.Forms.TextBox cityBox;
		private System.Windows.Forms.NumericUpDown startKilometerNumber;
		private System.Windows.Forms.Label startKilometerLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox pathBox;
		private System.Windows.Forms.Button pathChooseButton;
		private System.Windows.Forms.Label directionLabel;
		private System.Windows.Forms.ComboBox directionBox;
		private System.Windows.Forms.Label projectLaneLabel;
		private System.Windows.Forms.NumericUpDown projectLane;
	}
}