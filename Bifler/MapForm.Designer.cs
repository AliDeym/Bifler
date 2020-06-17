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
	partial class MapForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
			this.gmapPanel = new System.Windows.Forms.TableLayoutPanel();
			this.rightPanel = new System.Windows.Forms.TableLayoutPanel();
			this.topRightPanel = new System.Windows.Forms.Panel();
			this.savePictureButton = new System.Windows.Forms.Button();
			this.automateMoveCheckbox = new System.Windows.Forms.CheckBox();
			this.zoomLabel = new System.Windows.Forms.Label();
			this.zoomBar = new System.Windows.Forms.TrackBar();
			this.lonBox = new System.Windows.Forms.TextBox();
			this.lonLabel = new System.Windows.Forms.Label();
			this.latBox = new System.Windows.Forms.TextBox();
			this.latLabel = new System.Windows.Forms.Label();
			this.bottomRightPanel = new System.Windows.Forms.Panel();
			this.projectsBox = new System.Windows.Forms.TreeView();
			this.gmap = new GMap.NET.WindowsForms.GMapControl();
			this.updatePositionScheduler = new System.Windows.Forms.Timer(this.components);
			this.colorPicker = new System.Windows.Forms.ColorDialog();
			this.rightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeColorContextButton = new System.Windows.Forms.ToolStripMenuItem();
			this.gmapPanel.SuspendLayout();
			this.rightPanel.SuspendLayout();
			this.topRightPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
			this.bottomRightPanel.SuspendLayout();
			this.rightClickMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// gmapPanel
			// 
			this.gmapPanel.ColumnCount = 2;
			this.gmapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.gmapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.gmapPanel.Controls.Add(this.rightPanel, 0, 0);
			this.gmapPanel.Controls.Add(this.gmap, 1, 0);
			this.gmapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gmapPanel.Location = new System.Drawing.Point(0, 0);
			this.gmapPanel.Name = "gmapPanel";
			this.gmapPanel.RowCount = 1;
			this.gmapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.gmapPanel.Size = new System.Drawing.Size(910, 622);
			this.gmapPanel.TabIndex = 1;
			// 
			// rightPanel
			// 
			this.rightPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.rightPanel.ColumnCount = 1;
			this.rightPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.rightPanel.Controls.Add(this.topRightPanel, 0, 0);
			this.rightPanel.Controls.Add(this.bottomRightPanel, 1, 1);
			this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rightPanel.Location = new System.Drawing.Point(686, 3);
			this.rightPanel.Name = "rightPanel";
			this.rightPanel.RowCount = 1;
			this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.rightPanel.Size = new System.Drawing.Size(221, 616);
			this.rightPanel.TabIndex = 0;
			// 
			// topRightPanel
			// 
			this.topRightPanel.BackColor = System.Drawing.Color.LightGray;
			this.topRightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.topRightPanel.Controls.Add(this.savePictureButton);
			this.topRightPanel.Controls.Add(this.automateMoveCheckbox);
			this.topRightPanel.Controls.Add(this.zoomLabel);
			this.topRightPanel.Controls.Add(this.zoomBar);
			this.topRightPanel.Controls.Add(this.lonBox);
			this.topRightPanel.Controls.Add(this.lonLabel);
			this.topRightPanel.Controls.Add(this.latBox);
			this.topRightPanel.Controls.Add(this.latLabel);
			this.topRightPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topRightPanel.Location = new System.Drawing.Point(3, 3);
			this.topRightPanel.Name = "topRightPanel";
			this.topRightPanel.Size = new System.Drawing.Size(215, 179);
			this.topRightPanel.TabIndex = 0;
			// 
			// savePictureButton
			// 
			this.savePictureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.savePictureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(255)))), ((int)(((byte)(64)))));
			this.savePictureButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.savePictureButton.Location = new System.Drawing.Point(7, 144);
			this.savePictureButton.Name = "savePictureButton";
			this.savePictureButton.Size = new System.Drawing.Size(200, 29);
			this.savePictureButton.TabIndex = 8;
			this.savePictureButton.Text = " ذخیره تصویر";
			this.savePictureButton.UseVisualStyleBackColor = false;
			this.savePictureButton.Click += new System.EventHandler(this.OnSaveImageButtonClicked);
			// 
			// automateMoveCheckbox
			// 
			this.automateMoveCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.automateMoveCheckbox.AutoSize = true;
			this.automateMoveCheckbox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.automateMoveCheckbox.Location = new System.Drawing.Point(3, 114);
			this.automateMoveCheckbox.Name = "automateMoveCheckbox";
			this.automateMoveCheckbox.Size = new System.Drawing.Size(204, 23);
			this.automateMoveCheckbox.TabIndex = 6;
			this.automateMoveCheckbox.Text = "بروزرسانی خودکار موقعیت";
			this.automateMoveCheckbox.UseVisualStyleBackColor = true;
			// 
			// zoomLabel
			// 
			this.zoomLabel.AutoSize = true;
			this.zoomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.zoomLabel.Location = new System.Drawing.Point(3, 83);
			this.zoomLabel.Name = "zoomLabel";
			this.zoomLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.zoomLabel.Size = new System.Drawing.Size(56, 19);
			this.zoomLabel.TabIndex = 5;
			this.zoomLabel.Text = "Zoom:";
			// 
			// zoomBar
			// 
			this.zoomBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.zoomBar.LargeChange = 4;
			this.zoomBar.Location = new System.Drawing.Point(65, 74);
			this.zoomBar.Maximum = 19;
			this.zoomBar.Minimum = 2;
			this.zoomBar.Name = "zoomBar";
			this.zoomBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.zoomBar.Size = new System.Drawing.Size(142, 45);
			this.zoomBar.TabIndex = 4;
			this.zoomBar.Value = 7;
			this.zoomBar.ValueChanged += new System.EventHandler(this.OnZoomBarMove);
			// 
			// lonBox
			// 
			this.lonBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lonBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lonBox.Location = new System.Drawing.Point(44, 41);
			this.lonBox.Name = "lonBox";
			this.lonBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lonBox.Size = new System.Drawing.Size(163, 27);
			this.lonBox.TabIndex = 3;
			this.lonBox.Text = "50.9493085741997";
			this.lonBox.Enter += new System.EventHandler(this.OnPositionManualChange);
			// 
			// lonLabel
			// 
			this.lonLabel.AutoSize = true;
			this.lonLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lonLabel.Location = new System.Drawing.Point(2, 44);
			this.lonLabel.Name = "lonLabel";
			this.lonLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lonLabel.Size = new System.Drawing.Size(41, 19);
			this.lonLabel.TabIndex = 2;
			this.lonLabel.Text = "Lon:";
			// 
			// latBox
			// 
			this.latBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.latBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.latBox.Location = new System.Drawing.Point(44, 4);
			this.latBox.Name = "latBox";
			this.latBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.latBox.Size = new System.Drawing.Size(163, 27);
			this.latBox.TabIndex = 1;
			this.latBox.Text = "35.8101895761745";
			this.latBox.Enter += new System.EventHandler(this.OnPositionManualChange);
			// 
			// latLabel
			// 
			this.latLabel.AutoSize = true;
			this.latLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.latLabel.Location = new System.Drawing.Point(3, 7);
			this.latLabel.Name = "latLabel";
			this.latLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.latLabel.Size = new System.Drawing.Size(36, 19);
			this.latLabel.TabIndex = 0;
			this.latLabel.Text = "Lat:";
			// 
			// bottomRightPanel
			// 
			this.bottomRightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.bottomRightPanel.Controls.Add(this.projectsBox);
			this.bottomRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomRightPanel.Location = new System.Drawing.Point(3, 188);
			this.bottomRightPanel.Name = "bottomRightPanel";
			this.bottomRightPanel.Size = new System.Drawing.Size(215, 425);
			this.bottomRightPanel.TabIndex = 1;
			// 
			// projectsBox
			// 
			this.projectsBox.CheckBoxes = true;
			this.projectsBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectsBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectsBox.Location = new System.Drawing.Point(0, 0);
			this.projectsBox.Name = "projectsBox";
			this.projectsBox.RightToLeftLayout = true;
			this.projectsBox.Size = new System.Drawing.Size(211, 421);
			this.projectsBox.TabIndex = 1;
			this.projectsBox.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.OnVisibilityChange);
			this.projectsBox.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnNodeDoubleClick);
			this.projectsBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnDeletePress);
			// 
			// gmap
			// 
			this.gmap.Bearing = 0F;
			this.gmap.CanDragMap = true;
			this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
			this.gmap.GrayScaleMode = false;
			this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gmap.LevelsKeepInMemmory = 5;
			this.gmap.Location = new System.Drawing.Point(3, 3);
			this.gmap.MarkersEnabled = true;
			this.gmap.MaxZoom = 19;
			this.gmap.MinZoom = 2;
			this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.gmap.Name = "gmap";
			this.gmap.NegativeMode = false;
			this.gmap.PolygonsEnabled = true;
			this.gmap.RetryLoadTile = 0;
			this.gmap.RoutesEnabled = true;
			this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.gmap.ShowTileGridLines = false;
			this.gmap.Size = new System.Drawing.Size(677, 616);
			this.gmap.TabIndex = 1;
			this.gmap.Zoom = 16D;
			this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.OnPinpointClick);
			this.gmap.OnPositionChanged += new GMap.NET.PositionChanged(this.OnPositionChanged);
			this.gmap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.OnZoomChanged);
			// 
			// updatePositionScheduler
			// 
			this.updatePositionScheduler.Enabled = true;
			this.updatePositionScheduler.Interval = 5000;
			this.updatePositionScheduler.Tick += new System.EventHandler(this.OnUpdateInterval);
			// 
			// colorPicker
			// 
			this.colorPicker.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			// 
			// rightClickMenu
			// 
			this.rightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorContextButton});
			this.rightClickMenu.Name = "rightClickMenu";
			this.rightClickMenu.Size = new System.Drawing.Size(138, 28);
			// 
			// changeColorContextButton
			// 
			this.changeColorContextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.changeColorContextButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.changeColorContextButton.Name = "changeColorContextButton";
			this.changeColorContextButton.Size = new System.Drawing.Size(137, 24);
			this.changeColorContextButton.Text = "تغییر رنگ";
			this.changeColorContextButton.Click += new System.EventHandler(this.changeColorContextClick);
			// 
			// MapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(910, 622);
			this.Controls.Add(this.gmapPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MapForm";
			this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.RightToLeftLayout = true;
			this.Text = "نقشه";
			this.Load += new System.EventHandler(this.OnInitialization);
			this.gmapPanel.ResumeLayout(false);
			this.rightPanel.ResumeLayout(false);
			this.topRightPanel.ResumeLayout(false);
			this.topRightPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
			this.bottomRightPanel.ResumeLayout(false);
			this.rightClickMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel gmapPanel;
		private GMap.NET.WindowsForms.GMapControl gmap;
		private System.Windows.Forms.Timer updatePositionScheduler;
		private System.Windows.Forms.ColorDialog colorPicker;
		private System.Windows.Forms.ContextMenuStrip rightClickMenu;
		private System.Windows.Forms.ToolStripMenuItem changeColorContextButton;
		private System.Windows.Forms.TableLayoutPanel rightPanel;
		private System.Windows.Forms.Panel topRightPanel;
		private System.Windows.Forms.Button savePictureButton;
		private System.Windows.Forms.CheckBox automateMoveCheckbox;
		private System.Windows.Forms.Label zoomLabel;
		private System.Windows.Forms.TrackBar zoomBar;
		private System.Windows.Forms.TextBox lonBox;
		private System.Windows.Forms.Label lonLabel;
		private System.Windows.Forms.TextBox latBox;
		private System.Windows.Forms.Label latLabel;
		private System.Windows.Forms.Panel bottomRightPanel;
		private System.Windows.Forms.TreeView projectsBox;
	}
}