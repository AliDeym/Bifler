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
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using libBifler;
using libBifler.UI;

using System.Diagnostics;
using System.IO;

namespace Bifler
{
	/// <summary>
	/// Project camera settings.
	/// </summary>
	public partial class CameraSettingsPanel : Form
	{
		private MainWindow mainPanel = null;
		private Core appCore = null;

		private int _cameraCountBeforeChange;

		/// <summary>
		/// Loads the form.
		/// </summary>
		/// <param name="mainWindow">Main Window.</param>
		/// <param name="mainCore">Application's core.</param>
		public CameraSettingsPanel (MainWindow mainWindow, Core mainCore)
		{
			InitializeComponent ();

			mainPanel = mainWindow;
			appCore = mainCore;

			_cameraCountBeforeChange = mainPanel.CameraCount;
		}

		private void OnValueChanged (object sender, EventArgs e)
		{
			var cameraCount = valueBox.SelectedIndex + 1;

			if (cameraCount <= 1) {
				mainPanel.SetColumnSize (1);
				mainPanel.SetRowSize (1);
			}
			else
				mainPanel.SetColumnSize (2);


			// Get correct row for the main panel.
			float f = cameraCount;

			mainPanel.SetRowSize ((int)Math.Ceiling (f / 2));

			// 55px Every row count.
			// 85px From beginning to first row.
			// 83px From last row to the end.
			Height = cameraCount * 55 + 85 + 83;


			var realDeviceCount = appCore.BaslerManager.GetDeviceList ().Length;



			foreach (var control in Controls) {
				var ctrl = (Control)control;

				var getNumber = String.Join ("", Regex.Split (ctrl.Name, @"[^\d]"));

				if (int.TryParse(getNumber, out int num)) {
					if (num <= cameraCount)
						ctrl.Visible = true;
					else
						ctrl.Visible = false;
				}

				
			}

			// Set button positions.
			cancelButton.Location = new Point (cancelButton.Location.X, Height - 78);
			saveButton.Location = new Point (saveButton.Location.X, Height - 78);

			mainPanel.CameraCount = cameraCount;

			// Save config for camera count.
			appCore.MainConfigManager.SetConfig ("CameraCount", cameraCount.ToString ());
		}

		private void setCameraCount (int num)
		{
			if (num <= 1) {
				mainPanel.SetColumnSize (1);
				mainPanel.SetRowSize (1);
			}
			else
				mainPanel.SetColumnSize (2);


			// Get correct row for the main panel.
			float f = (float)num;

			mainPanel.SetRowSize ((int)Math.Ceiling (f / 2));
		}

		private void OnCancelClick (object sender, EventArgs e)
		{
			setCameraCount (_cameraCountBeforeChange);


			// Revert config back to its normal.
			appCore.MainConfigManager.SetConfig ("CameraCount", _cameraCountBeforeChange.ToString ());

			Close ();
		}

		private void OnInitialized (object sender, EventArgs e)
		{

			var comboBoxes = new ComboBox [6] {
				cameraSelected1,
				cameraSelected2,
				cameraSelected3,
				cameraSelected4,
				cameraSelected5,
				cameraSelected6
			};


			var settingsButtons = new [] {
				settingsButton1,
				settingsButton2,
				settingsButton3,
				settingsButton4,
				settingsButton5,
				settingsButton6
			};

			var ranges = new [] {
				camera1Range,
				camera2Range,
				camera3Range,
				camera4Range,
				camera5Range,
				camera6Range
			};

			var frames = new [] {
				camera1Frame,
				camera2Frame,
				camera3Frame,
				camera4Frame,
				camera5Frame,
				camera6Frame
			};

			var starts = new [] {

				start1Range,
				start2Range,
				start3Range,
				start4Range,
				start5Range,
				start6Range

			};


			// Disable camera changing position & Range changing during runtime(project start time).
			if (appCore.Projects.Started) {

				foreach (var range in ranges)
					range.Enabled = false;

				foreach (var comboBox in comboBoxes)
					comboBox.Enabled = false;

				foreach (var range in ranges)
					range.Enabled = false;

				foreach (var frame in frames)
					frame.Enabled = false;

				foreach (var start in starts)
					start.Enabled = false;

			}


			var managedCameras = appCore.BaslerManager.GetManagedCameras ();

			// Add cameras into combo boxes.
			foreach (var cam in managedCameras) {
				
				foreach (var box in comboBoxes) {

					box.Items.Add (cam.Name);
				}

			}


			// Set box values for each camera device.
			foreach (var camera in managedCameras) {

				if (camera.pictureBox != null) {

					var cameraIndex = int.Parse (camera.pictureBox.Name [camera.pictureBox.Name.Length - 1].ToString ());

					if (comboBoxes.Length >= cameraIndex && cameraIndex > 0) {

						var comboBox = comboBoxes [cameraIndex - 1];

						comboBox.SelectedText = camera.Name;
						comboBox.Text = camera.Name;
						comboBox.SelectedItem = camera.Name;
						comboBox.SelectedValue = camera.Name;

						// Enable specified setting button for that camera device.
						settingsButtons [cameraIndex - 1].Enabled = true;


						// Load FPS and Range from config (If it exists).
						var strRange = appCore.MainConfigManager.GetConfig (camera.FullName + "-RANGE");
						var strFPS = appCore.MainConfigManager.GetConfig (camera.FullName + "-FPS");
						var strStart = appCore.MainConfigManager.GetConfig (camera.FullName + "-START");


						if (strRange == null)
							strRange = "";
						if (strFPS == null)
							strFPS = "";
						if (strStart == null)
							strStart = "";



						if (int.TryParse (strRange, out int camRange)) {
							ranges [cameraIndex - 1].Value = camRange;

							camera.SaveRange = camRange;
						}

						if (int.TryParse (strFPS, out int camFPS)) {
							frames [cameraIndex - 1].Value = camFPS;

							camera.FpsLimit = camFPS;
						}

						if (int.TryParse(strStart, out int camStart)) {
							starts [cameraIndex - 1].Value = camStart;

							camera.StartRange = camStart;
						}
						

					}

				}

			}


			valueBox.SelectedIndex = mainPanel.CameraCount - 1;

			// BUG FIX: Value change not calling on index 0.
			if (mainPanel.CameraCount == 1) {
				OnValueChanged (null, null);
			}



			var cameraRanges = new [] {
				camera1Range,
				camera2Range,
				camera3Range,
				camera4Range,
				camera5Range,
				camera6Range
			};

			var cameraFrames = new [] {

				camera1Frame,
				camera2Frame,
				camera3Frame,
				camera4Frame,
				camera5Frame,
				camera6Frame

			};

			foreach (var cam in mainPanel.deviceList) {

				if (cam.pictureBox == null) continue;
				
				var cameraSaveIndex = int.Parse (cam.pictureBox.Name [cam.pictureBox.Name.Length - 1].ToString ());

				if (cameraRanges.Length >= cameraSaveIndex && cameraSaveIndex > 0) {

					cameraRanges [cameraSaveIndex - 1].Value = cam.SaveRange;
					cameraFrames [cameraSaveIndex - 1].Value = cam.FpsLimit;
					starts [cameraSaveIndex - 1].Value = cam.StartRange;
					cameraRanges [cameraSaveIndex - 1].Enabled = true;
					cameraFrames [cameraSaveIndex - 1].Enabled = true;
					starts [cameraSaveIndex - 1].Enabled = true;

				}
			}
		}

		private void OnPanelSave (object sender, EventArgs e)
		{

			var cameraRanges = new [] {
				camera1Range,
				camera2Range,
				camera3Range,
				camera4Range,
				camera5Range,
				camera6Range
			};

			var cameraFrames = new [] {

				camera1Frame,
				camera2Frame,
				camera3Frame,
				camera4Frame,
				camera5Frame,
				camera6Frame

			};

			var cameraStarts = new [] {

				start1Range,
				start2Range,
				start3Range,
				start4Range,
				start5Range,
				start6Range

			};


			/* Set frame & range for each camera. */
			foreach (var cam in mainPanel.deviceList) {

				if (cam.pictureBox != null) {

					var cameraSaveIndex = int.Parse (cam.pictureBox.Name [cam.pictureBox.Name.Length - 1].ToString ());

					if (cameraRanges.Length >= cameraSaveIndex && cameraSaveIndex > 0) {

						cam.SaveRange = (int)cameraRanges [cameraSaveIndex - 1].Value;
						cam.FpsLimit = (int)cameraFrames [cameraSaveIndex - 1].Value;
						cam.StartRange = (int)cameraStarts [cameraSaveIndex - 1].Value;

						appCore.MainConfigManager.SetConfig (cam.FullName + "-RANGE", cam.SaveRange.ToString ());
						appCore.MainConfigManager.SetConfig (cam.FullName + "-FPS", cam.FpsLimit.ToString ());
						appCore.MainConfigManager.SetConfig (cam.FullName + "-START", cam.StartRange.ToString ());

					}

				}



				// Start code is made to check HasStarted by it-self, we call this function once again so if the range is zero, disable the grabbing events.
				cam.Start ();

			}

			if (!Directory.Exists ("preview")) {
				Directory.CreateDirectory ("preview");
			}


			/* Delete old preview files so we can see the new ones. */
			var dirInfo = new DirectoryInfo ("preview");
			var dirFiles = dirInfo.GetFiles ();

			foreach (var file in dirFiles)
				file.Delete ();


			/* Request for a new preview from each device. */
			foreach (var cam in mainPanel.deviceList) {

				cam.SavePreview ("preview/" + DateTime.Now.Ticks.ToString () + "-" + cam.Name + ".jpg");

			}



			var previewInfo = new ProcessStartInfo () {
				FileName = Application.StartupPath + "\\preview\\",
				Verb = "open",
				CreateNoWindow = false
			};

			Process.Start (previewInfo);

			// Close the form after saving.
			Close ();

		}

		private void OnCameraValueChanged (object sender, EventArgs e)
		{
			var selectedControl = (ComboBox)sender;

			if (selectedControl.SelectedItem == null) return;

			var selectedText = selectedControl.SelectedItem.ToString ().ToLower ();



			var comboBoxes = new [] {

				cameraSelected1,
				cameraSelected2,
				cameraSelected3,
				cameraSelected4,
				cameraSelected5,
				cameraSelected6

			};

			var settingsButtons = new [] {
				settingsButton1,
				settingsButton2,
				settingsButton3,
				settingsButton4,
				settingsButton5,
				settingsButton6
			};

			var cameraRanges = new [] {

				camera1Range,
				camera2Range,
				camera3Range,
				camera4Range,
				camera5Range,
				camera6Range
			};

			var cameraFrames = new [] {

				camera1Frame,
				camera2Frame,
				camera3Frame,
				camera4Frame,
				camera5Frame,
				camera6Frame

			};


			var cams = appCore.BaslerManager.GetManagedCameras ();

			Camera cam = null;

			// Search in managed cameras for camera with the same name.
			foreach (var obj in cams) {

				// Selected text is already lowered.
				if (obj.Name.ToLower() == selectedText) {
					cam = obj;
				}

			}

			if (cam == null) return;

			


			// Check wether any other box uses the same camera.
			foreach (var box in comboBoxes) {

				if (box.SelectedItem != null && box.SelectedItem.ToString().ToLower() == selectedText) {

					// Not the same box but has same value?
					if (box.Name != selectedControl.Name) {

						box.SelectedText = "";
						box.SelectedValue = "";
						box.Text = "";
						box.SelectedItem = "";


						// Add cameras, clear the old value.
						box.Items.Clear ();

						
						foreach (var camera in cams)
							box.Items.Add (camera.Name);



						var boxIndex = int.Parse (box.Name [box.Name.Length - 1].ToString ());

						settingsButtons [boxIndex - 1].Enabled = false;
						cameraRanges [boxIndex - 1].Enabled = false;
						cameraFrames [boxIndex - 1].Enabled = false;

					}

				}

			}

			var cameraTranslator = new Dictionary<int, CameraBox> () {
				{ 1, mainPanel.cameraBox1 },
				{ 2, mainPanel.cameraBox2 },
				{ 3, mainPanel.cameraBox3 },
				{ 4, mainPanel.cameraBox4 },
				{ 5, mainPanel.cameraBox5 },
				{ 6, mainPanel.cameraBox6 },
			};

			var selectedIndex = int.Parse (selectedControl.Name [selectedControl.Name.Length - 1].ToString ());

			var cameraBox = cameraTranslator [selectedIndex];

			// Check for existing CameraBox, set them to null.
			foreach (var camDevice in cams) {

				if (camDevice.pictureBox != null) {

					if (camDevice.pictureBox.Name == cameraBox.Name) {

						camDevice.pictureBox = null;

					}

				}

			}


			cam.pictureBox = cameraTranslator [selectedIndex];

			settingsButtons [selectedIndex - 1].Enabled = true;

		}

		private void SettingsButtonClick (object sender, EventArgs e)
		{
			var senderButton = (Button)sender;

			var buttonIndex = int.Parse (senderButton.Name [senderButton.Name.Length - 1].ToString ());


			var comboBoxes = new[] {
				cameraSelected1,
				cameraSelected2,
				cameraSelected3,
				cameraSelected4,
				cameraSelected5,
				cameraSelected6
			};


			var box = comboBoxes [buttonIndex - 1];

			Camera managedCamera = null;

			foreach (var cam in appCore.BaslerManager.GetManagedCameras ()) {

				if (cam.Name.ToLower() == box.SelectedItem.ToString().ToLower()) {

					managedCamera = cam;

				}

			}

			if (managedCamera != null) {

				var config = appCore.MainConfigManager.GetConfig (managedCamera.Name);


				if (config == null) {

					MessageBox.Show (Language.ErrorExposure, Language.Error);
					return;

				}

				if (!int.TryParse (config, out int exposureNumber)) {
					MessageBox.Show (Language.ErrorExposure, Language.Error);
					return;
				}

				var settingPanel = new BaslerSettings (managedCamera, exposureNumber);

				settingPanel.ShowDialog ();

			}

		}

		private void LoadFinished (object sender, EventArgs e)
		{

		}
	}
}
