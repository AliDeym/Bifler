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
	partial class LoadingForm
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
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.statusText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 12);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(462, 23);
			this.progressBar.TabIndex = 0;
			// 
			// statusText
			// 
			this.statusText.AutoSize = true;
			this.statusText.Location = new System.Drawing.Point(12, 47);
			this.statusText.Name = "statusText";
			this.statusText.Size = new System.Drawing.Size(93, 13);
			this.statusText.TabIndex = 1;
			this.statusText.Text = "در حال بارگذاری...";
			this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LoadingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(486, 69);
			this.ControlBox = false;
			this.Controls.Add(this.statusText);
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoadingForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "در حال بارگزاری نرم افزار";
			this.Load += new System.EventHandler(this.OnLoading);
			this.Shown += new System.EventHandler(this.OnInitialization);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label statusText;
	}
}