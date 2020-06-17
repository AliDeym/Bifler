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

using System;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel;

namespace libBifler.UI
{
	partial class BiflerController
	{
		#region Controls
		private NumericUpDown labelCurrentValue;
		private TrackBar slider;
		private Label labelMin;
		private Label labelMax;
		private Label labelName;
		#endregion


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.labelCurrentValue = new System.Windows.Forms.NumericUpDown();
			this.slider = new System.Windows.Forms.TrackBar();
			this.labelMin = new System.Windows.Forms.Label();
			this.labelMax = new System.Windows.Forms.Label();
			this.labelName = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.labelCurrentValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
			this.SuspendLayout();
			// 
			// labelCurrentValue
			// 
			this.labelCurrentValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelCurrentValue.AutoSize = true;
			this.labelCurrentValue.Location = new System.Drawing.Point(134, 30);
			this.labelCurrentValue.Name = "labelCurrentValue";
			this.labelCurrentValue.Size = new System.Drawing.Size(41, 20);
			this.labelCurrentValue.TabIndex = 1;
			this.labelCurrentValue.ValueChanged += new System.EventHandler(this.OnValueChanged);
			// 
			// slider
			// 
			this.slider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slider.Location = new System.Drawing.Point(44, 0);
			this.slider.Name = "slider";
			this.slider.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.slider.Size = new System.Drawing.Size(135, 45);
			this.slider.TabIndex = 0;
			this.slider.Scroll += new System.EventHandler(this.slider_Scroll);
			// 
			// labelMin
			// 
			this.labelMin.AutoSize = true;
			this.labelMin.Location = new System.Drawing.Point(3, 17);
			this.labelMin.Name = "labelMin";
			this.labelMin.Size = new System.Drawing.Size(24, 13);
			this.labelMin.TabIndex = 1;
			this.labelMin.Text = "Min";
			this.labelMin.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelMax
			// 
			this.labelMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMax.AutoSize = true;
			this.labelMax.Location = new System.Drawing.Point(198, 17);
			this.labelMax.Name = "labelMax";
			this.labelMax.Size = new System.Drawing.Size(27, 13);
			this.labelMax.TabIndex = 1;
			this.labelMax.Text = "Max";
			// 
			// labelName
			// 
			this.labelName.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelName.Location = new System.Drawing.Point(0, 30);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(128, 13);
			this.labelName.TabIndex = 1;
			this.labelName.Text = "ValueName:";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.labelName.Visible = false;
			// 
			// BiflerController
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.labelCurrentValue);
			this.Controls.Add(this.labelMax);
			this.Controls.Add(this.labelMin);
			this.Controls.Add(this.slider);
			this.MinimumSize = new System.Drawing.Size(225, 50);
			this.Name = "BiflerController";
			this.Size = new System.Drawing.Size(225, 50);
			((System.ComponentModel.ISupportInitialize)(this.labelCurrentValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		
	}
}
