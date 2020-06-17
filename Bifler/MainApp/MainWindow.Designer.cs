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
	partial class MainWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.cameraSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.portsSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.projectToolstrip = new System.Windows.Forms.ToolStripMenuItem();
			this.createProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.startProjectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.endProjectMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.timeStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.triggerCountStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.projectStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.gpsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.cameraBox6 = new libBifler.UI.CameraBox();
			this.cameraBox5 = new libBifler.UI.CameraBox();
			this.cameraBox4 = new libBifler.UI.CameraBox();
			this.cameraBox3 = new libBifler.UI.CameraBox();
			this.cameraBox1 = new libBifler.UI.CameraBox();
			this.cameraBox2 = new libBifler.UI.CameraBox();
			this.projectTimerUpdater = new System.Windows.Forms.Timer(this.components);
			this.statusStrip2 = new System.Windows.Forms.StatusStrip();
			this.camera1Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.camera2Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.camera3Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.camera4Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.camera5Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.camera6Label = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tableLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox2)).BeginInit();
			this.statusStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraSettingsMenuItem,
            this.portsSettingsMenuItem,
            this.projectToolstrip,
            this.mapMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1195, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// cameraSettingsMenuItem
			// 
			this.cameraSettingsMenuItem.Name = "cameraSettingsMenuItem";
			this.cameraSettingsMenuItem.Size = new System.Drawing.Size(98, 20);
			this.cameraSettingsMenuItem.Text = "تنظیمات دوربین";
			this.cameraSettingsMenuItem.Click += new System.EventHandler(this.OnCameraSettingsMenuClick);
			// 
			// portsSettingsMenuItem
			// 
			this.portsSettingsMenuItem.Name = "portsSettingsMenuItem";
			this.portsSettingsMenuItem.Size = new System.Drawing.Size(90, 20);
			this.portsSettingsMenuItem.Text = "تنظیمات پورت";
			this.portsSettingsMenuItem.Click += new System.EventHandler(this.SettingsPanelClick);
			// 
			// projectToolstrip
			// 
			this.projectToolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createProjectMenu,
            this.toolStripSeparator1,
            this.startProjectMenuItem,
            this.endProjectMenu,
            this.toolStripMenuItem1});
			this.projectToolstrip.Name = "projectToolstrip";
			this.projectToolstrip.Size = new System.Drawing.Size(43, 20);
			this.projectToolstrip.Text = "پروژه";
			// 
			// createProjectMenu
			// 
			this.createProjectMenu.Enabled = false;
			this.createProjectMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
			this.createProjectMenu.Image = global::Bifler.Properties.Resources.icons8_Plus_Filled_50;
			this.createProjectMenu.Name = "createProjectMenu";
			this.createProjectMenu.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.createProjectMenu.Size = new System.Drawing.Size(231, 22);
			this.createProjectMenu.Text = "تنظیم پروژه جدید";
			this.createProjectMenu.Click += new System.EventHandler(this.OnCreateProjectMenuItemClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
			// 
			// startProjectMenuItem
			// 
			this.startProjectMenuItem.Enabled = false;
			this.startProjectMenuItem.ForeColor = System.Drawing.Color.DarkGreen;
			this.startProjectMenuItem.Image = global::Bifler.Properties.Resources.icons8_Play_Filled_50;
			this.startProjectMenuItem.Name = "startProjectMenuItem";
			this.startProjectMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.startProjectMenuItem.Size = new System.Drawing.Size(231, 22);
			this.startProjectMenuItem.Text = "شروع پروژه";
			this.startProjectMenuItem.Click += new System.EventHandler(this.OnStartProjectClick);
			// 
			// endProjectMenu
			// 
			this.endProjectMenu.Enabled = false;
			this.endProjectMenu.ForeColor = System.Drawing.Color.Red;
			this.endProjectMenu.Image = global::Bifler.Properties.Resources.icons8_Stop_Filled_50;
			this.endProjectMenu.Name = "endProjectMenu";
			this.endProjectMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
			this.endProjectMenu.Size = new System.Drawing.Size(231, 22);
			this.endProjectMenu.Text = "اتمام پروژه";
			this.endProjectMenu.Click += new System.EventHandler(this.OnEndProjectMenuItemClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(228, 6);
			// 
			// mapMenuItem
			// 
			this.mapMenuItem.Image = global::Bifler.Properties.Resources.icons8_globe_filled_50;
			this.mapMenuItem.Name = "mapMenuItem";
			this.mapMenuItem.Size = new System.Drawing.Size(61, 20);
			this.mapMenuItem.Text = "نقشه";
			this.mapMenuItem.Click += new System.EventHandler(this.onMapButtonClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeStripLabel,
            this.triggerCountStripLabel,
            this.projectStatusStripLabel,
            this.gpsStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 691);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1195, 28);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// timeStripLabel
			// 
			this.timeStripLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.timeStripLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.timeStripLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeStripLabel.Name = "timeStripLabel";
			this.timeStripLabel.Size = new System.Drawing.Size(295, 23);
			this.timeStripLabel.Spring = true;
			this.timeStripLabel.Text = "زمان پروژه : 00:00:00s";
			this.timeStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// triggerCountStripLabel
			// 
			this.triggerCountStripLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.triggerCountStripLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.triggerCountStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.triggerCountStripLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.triggerCountStripLabel.Name = "triggerCountStripLabel";
			this.triggerCountStripLabel.Size = new System.Drawing.Size(295, 23);
			this.triggerCountStripLabel.Spring = true;
			this.triggerCountStripLabel.Text = "متراژ برداشت: 000 + 000 کیلومتراژ";
			this.triggerCountStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// projectStatusStripLabel
			// 
			this.projectStatusStripLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.projectStatusStripLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
			this.projectStatusStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.projectStatusStripLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectStatusStripLabel.Name = "projectStatusStripLabel";
			this.projectStatusStripLabel.Size = new System.Drawing.Size(295, 23);
			this.projectStatusStripLabel.Spring = true;
			this.projectStatusStripLabel.Text = "وضعیت پروژه: شروع نشده";
			this.projectStatusStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpsStatusLabel
			// 
			this.gpsStatusLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gpsStatusLabel.Name = "gpsStatusLabel";
			this.gpsStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.gpsStatusLabel.Size = new System.Drawing.Size(295, 23);
			this.gpsStatusLabel.Spring = true;
			this.gpsStatusLabel.Text = "Lon:  , Lat: ";
			this.gpsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayout
			// 
			this.tableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
			this.tableLayout.ColumnCount = 2;
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
			this.tableLayout.Controls.Add(this.cameraBox6, 1, 2);
			this.tableLayout.Controls.Add(this.cameraBox5, 0, 2);
			this.tableLayout.Controls.Add(this.cameraBox4, 1, 1);
			this.tableLayout.Controls.Add(this.cameraBox3, 0, 1);
			this.tableLayout.Controls.Add(this.cameraBox1, 0, 0);
			this.tableLayout.Controls.Add(this.cameraBox2, 1, 0);
			this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayout.Location = new System.Drawing.Point(0, 24);
			this.tableLayout.Name = "tableLayout";
			this.tableLayout.RowCount = 3;
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
			this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
			this.tableLayout.Size = new System.Drawing.Size(1195, 667);
			this.tableLayout.TabIndex = 3;
			// 
			// cameraBox6
			// 
			this.cameraBox6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox6.Location = new System.Drawing.Point(5, 668);
			this.cameraBox6.Name = "cameraBox6";
			this.cameraBox6.Size = new System.Drawing.Size(1, 1);
			this.cameraBox6.TabIndex = 11;
			this.cameraBox6.TabStop = false;
			// 
			// cameraBox5
			// 
			this.cameraBox5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox5.Location = new System.Drawing.Point(7, 668);
			this.cameraBox5.Name = "cameraBox5";
			this.cameraBox5.Size = new System.Drawing.Size(1183, 1);
			this.cameraBox5.TabIndex = 10;
			this.cameraBox5.TabStop = false;
			// 
			// cameraBox4
			// 
			this.cameraBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox4.Location = new System.Drawing.Point(5, 666);
			this.cameraBox4.Name = "cameraBox4";
			this.cameraBox4.Size = new System.Drawing.Size(1, 1);
			this.cameraBox4.TabIndex = 9;
			this.cameraBox4.TabStop = false;
			// 
			// cameraBox3
			// 
			this.cameraBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox3.Location = new System.Drawing.Point(7, 666);
			this.cameraBox3.Name = "cameraBox3";
			this.cameraBox3.Size = new System.Drawing.Size(1183, 1);
			this.cameraBox3.TabIndex = 8;
			this.cameraBox3.TabStop = false;
			// 
			// cameraBox1
			// 
			this.cameraBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox1.Location = new System.Drawing.Point(7, 5);
			this.cameraBox1.Name = "cameraBox1";
			this.cameraBox1.Size = new System.Drawing.Size(1183, 653);
			this.cameraBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.cameraBox1.TabIndex = 6;
			this.cameraBox1.TabStop = false;
			// 
			// cameraBox2
			// 
			this.cameraBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cameraBox2.Location = new System.Drawing.Point(5, 5);
			this.cameraBox2.Name = "cameraBox2";
			this.cameraBox2.Size = new System.Drawing.Size(1, 653);
			this.cameraBox2.TabIndex = 7;
			this.cameraBox2.TabStop = false;
			// 
			// projectTimerUpdater
			// 
			this.projectTimerUpdater.Enabled = true;
			this.projectTimerUpdater.Interval = 1000;
			this.projectTimerUpdater.Tick += new System.EventHandler(this.ProjectTimerTick);
			// 
			// statusStrip2
			// 
			this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.camera1Label,
            this.camera2Label,
            this.camera3Label,
            this.camera4Label,
            this.camera5Label,
            this.camera6Label});
			this.statusStrip2.Location = new System.Drawing.Point(0, 719);
			this.statusStrip2.Name = "statusStrip2";
			this.statusStrip2.Size = new System.Drawing.Size(1195, 24);
			this.statusStrip2.TabIndex = 4;
			this.statusStrip2.Text = "statusStrip2";
			// 
			// camera1Label
			// 
			this.camera1Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera1Label.Name = "camera1Label";
			this.camera1Label.Size = new System.Drawing.Size(196, 19);
			this.camera1Label.Spring = true;
			this.camera1Label.Text = "تصویر 1:";
			this.camera1Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// camera2Label
			// 
			this.camera2Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera2Label.Name = "camera2Label";
			this.camera2Label.Size = new System.Drawing.Size(196, 19);
			this.camera2Label.Spring = true;
			this.camera2Label.Text = "تصویر 2:";
			this.camera2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// camera3Label
			// 
			this.camera3Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera3Label.Name = "camera3Label";
			this.camera3Label.Size = new System.Drawing.Size(196, 19);
			this.camera3Label.Spring = true;
			this.camera3Label.Text = "تصویر 3:";
			this.camera3Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// camera4Label
			// 
			this.camera4Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera4Label.Name = "camera4Label";
			this.camera4Label.Size = new System.Drawing.Size(196, 19);
			this.camera4Label.Spring = true;
			this.camera4Label.Text = "تصویر 4:";
			this.camera4Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// camera5Label
			// 
			this.camera5Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera5Label.Name = "camera5Label";
			this.camera5Label.Size = new System.Drawing.Size(196, 19);
			this.camera5Label.Spring = true;
			this.camera5Label.Text = "تصویر 5:";
			this.camera5Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// camera6Label
			// 
			this.camera6Label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.camera6Label.Name = "camera6Label";
			this.camera6Label.Size = new System.Drawing.Size(196, 19);
			this.camera6Label.Spring = true;
			this.camera6Label.Text = "تصویر 6:";
			this.camera6Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1195, 743);
			this.Controls.Add(this.tableLayout);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainWindow";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Bifler";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			this.ResizeBegin += new System.EventHandler(this.OnResizeStart);
			this.ResizeEnd += new System.EventHandler(this.OnResizeEnd);
			this.Resize += new System.EventHandler(this.OnResizing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tableLayout.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cameraBox6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cameraBox2)).EndInit();
			this.statusStrip2.ResumeLayout(false);
			this.statusStrip2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem projectToolstrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem endProjectMenu;
		private System.Windows.Forms.ToolStripMenuItem startProjectMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayout;
		public libBifler.UI.CameraBox cameraBox1;
		public libBifler.UI.CameraBox cameraBox6;
		public libBifler.UI.CameraBox cameraBox5;
		public libBifler.UI.CameraBox cameraBox4;
		public libBifler.UI.CameraBox cameraBox3;
		public libBifler.UI.CameraBox cameraBox2;
		private System.Windows.Forms.ToolStripStatusLabel timeStripLabel;
		private System.Windows.Forms.ToolStripStatusLabel triggerCountStripLabel;
		private System.Windows.Forms.ToolStripStatusLabel projectStatusStripLabel;
		private System.Windows.Forms.Timer projectTimerUpdater;
		private System.Windows.Forms.ToolStripStatusLabel gpsStatusLabel;
		private System.Windows.Forms.StatusStrip statusStrip2;
		private System.Windows.Forms.ToolStripStatusLabel camera1Label;
		private System.Windows.Forms.ToolStripStatusLabel camera2Label;
		private System.Windows.Forms.ToolStripStatusLabel camera3Label;
		private System.Windows.Forms.ToolStripStatusLabel camera4Label;
		private System.Windows.Forms.ToolStripStatusLabel camera5Label;
		private System.Windows.Forms.ToolStripStatusLabel camera6Label;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem cameraSettingsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem portsSettingsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createProjectMenu;
		private System.Windows.Forms.ToolStripMenuItem mapMenuItem;
	}
}