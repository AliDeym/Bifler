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
using System.Media;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;


using libBifler;
using libBifler.UI;
using libBifler.Utils.Structures;


namespace Bifler
{
	/// <summary>
	/// Main window.
	/// </summary>
	public partial class MainWindow : Form
	{
		internal int CameraCount;

		Core appCore = null;

		internal List<Camera> deviceList;


		// Constants for Window state.
		private const int WM_SYSCOMMAND = 0x0112;
		private const int SC_MAXIMIZE = 0xf030;
		private const int SC_RESTORE = 0xf120;


		private ToolStripStatusLabel [] cameraStatusBars;


		bool hasCheckedCameras, hasCheckedPorts, hasPendingProject;


		/// <summary>
		/// Initializes a main window handler.
		/// </summary>
		/// <param name="mainCore">Application's main core.</param>
		public MainWindow (Core mainCore)
		{
			InitializeComponent ();

			// Copyright Ali Deym (c) 2017-2018
			Console.WriteLine ("COPYRIGH Ali Deym (c) 2017-2018");
			Text = "تدبیرگر راه - مهندسین مشاور تدبیر فرودراه";

			hasPendingProject = hasCheckedCameras = hasCheckedPorts = false;

			cameraStatusBars = new [] {
				camera1Label,
				camera2Label,
				camera3Label,
				camera4Label,
				camera5Label,
				camera6Label
			};


			appCore = mainCore;


			appCore.Trigger.OnPendingProjectReceived += ReceivedPendingProjectPacket;


			deviceList = new List<Camera> ();


			appCore.BaslerManager.RefreshDevices ();

			var devices = appCore.BaslerManager.GetDeviceList ();


			/* Set Camera count and change main window alignment for that. */
			CameraCount = devices.Length;

			var config = appCore.MainConfigManager.GetConfig ("CameraCount");

			if (config != null) {
				if (int.TryParse(config, out int count)) {

					CameraCount = count;

				}
			}

			if (CameraCount <= 1) {
				SetColumnSize (1);
				SetRowSize (1);
			}
			else
				SetColumnSize (2);


			// Get correct row for the main panel.
			float f = CameraCount;

			SetRowSize ((int)Math.Ceiling (f / 2));





			/* Detect creation of Cameras. */
			bool cam1Made, cam2Made, cam3Made, cam4Made, cam5Made, cam6Made;
			cam1Made = cam2Made = cam3Made = cam4Made = cam5Made = cam6Made = false;




			if (devices.Length > 0) {


				foreach (var pylonCam in devices) {

					/* If there's no more slot left in main window, do not create more camera devices. */
					if (true.AllEqual (cam1Made, cam2Made, cam3Made, cam4Made, cam5Made, cam6Made))
						break;


					
					var cam = appCore.BaslerManager.AddCamera ((int)pylonCam.Index);

					var conf = appCore.MainConfigManager.GetConfig (cam.Name);


					if (conf == null) {

						MessageBox.Show (Language.ErrorExposure, Language.Error);
						Environment.Exit (0);


						return;

					}

					if (!int.TryParse (conf, out int exposureNumber)) {

						MessageBox.Show (Language.ErrorExposure, Language.Error);
						Environment.Exit (0);

						return;
					}


					deviceList.Add (cam);

					CameraBox cameraBox = null;

					/* Get an available slot in camera boxes. */
					if (!cam1Made) {
						cameraBox = cameraBox1;
						cam1Made = true;
					}
					else if (!cam2Made) {
						cameraBox = cameraBox2;
						cam2Made = true;
					}
					else if (!cam3Made) {
						cameraBox = cameraBox3;
						cam3Made = true;
					}
					else if (!cam4Made) {
						cameraBox = cameraBox4;
						cam4Made = true;
					}
					else if (!cam5Made) {
						cameraBox = cameraBox5;
						cam5Made = true;
					}
					else if (!cam6Made) {
						cameraBox = cameraBox6;
						cam6Made = true;
					}


					// In case of failure.
					if (cameraBox == null) continue;


					// Set the picturebox's managed camera to this new created instance.
					cameraBox.SetCamera (cam);


					/* Handle new camera box slot and prepare it for the managed camera. */
					cam.OnErrorReceived += OnErrorReceived;

					
					cam.OnDeviceRemoved += OnDeviceRemoved;


					cam.pictureBox = cameraBox;


					cam.Initialize (exposureNumber);


					// Because we moved picture box into JobWorkers, we need to initialize the camera first, and then change it's camerabox object.
					// NO Nevermind, used master / slave format.
					// Nevermind this.
					// It's back top of initialize.


					cam.ImageProvider.GrabErrorEvent += ImageProviderError;
				}
			}
		}

		/* Handles radio button's start project click function. */
		private void ReceivedPendingProjectPacket (object sender, EventArgs e)
		{
			if (InvokeRequired) {

				Action<object, EventArgs> action = ReceivedPendingProjectPacket;

				Invoke (action, sender, e);


				return;
			}

			startProjectMenuItem.Enabled = hasPendingProject = false;


			projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Progressing;
			projectStatusStripLabel.ForeColor = Color.Black;


			appCore.Projects.RenewProjectTime ();
			appCore.Trigger.StartTrigger ();
		}


		/* Handles the warning beep sound play. */
		private void PlayWarningSound ()
		{
			// TEMPORARLY DISABLED.
			//if (true == true) return;
			new Thread (() => {
				SystemSounds.Beep.Play ();
				Console.Beep (2000, 1500);
			}).Start ();
		}

		private void PlayCriticalErrorSound ()
		{
			new Thread (() => {
				SystemSounds.Beep.Play ();
				Console.Beep (3000, 4000);
			}).Start ();
		}


		/* Handles notifications for device removing. */
		private void OnDeviceRemoved (Camera sender)
		{
			/* Our device might be disabled, so It's pretty much safe to remove it. */
			if (sender.Disabled || sender.SaveRange <= 0) {

				appCore.ExternalLog ("Device " + sender.FullName + " (Name: " + sender.Name + ", Index: " + sender.Index + ", Save Index: " + sender.SavedIndex + ") removed safely.", LogType.Warn);

				PlayWarningSound ();

				return;
			}

			if (InvokeRequired) {
				Action<Camera> action = OnDeviceRemoved;

				Invoke (action, sender);

				return;
			}

			//appCore.MainLogger.WriteLog ("WARNING: Device " + sender.FullName + " (" + sender.Name + ", " + sender.Index + ", " + sender.SaveIndex + ") removed unsafely.");

			appCore.ExternalLog ("Device " + sender.FullName + " (Name: " + sender.Name + ", Index: " + sender.Index + ", Save Index: " + sender.SavedIndex + ") removed unsafely.", LogType.Error);

			projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Error;
			projectStatusStripLabel.ForeColor = Color.Red;


			//PlayWarningSound ();
			PlayCriticalErrorSound ();


			MessageBox.Show (Language.DeviceDisconnected + "\n" + sender.Name + " (" + sender.SaveIndex + ")", Language.Error);
		}


		/* Handles Basler Pylon errors (Mainly Grab errors). */
		private void ImageProviderError (Exception grabException, string additionalErrorMessage)
		{
			if (InvokeRequired) {
				Action<Exception, string> action = ImageProviderError;

				Invoke (action, grabException, additionalErrorMessage);

				return;
			}

			string error = grabException + "\n" + additionalErrorMessage;

			appCore.ExternalLog ("GRAB ERROR: " + error, LogType.Error);

			/*appCore.MainLogger.Save ();
			appCore.MainLogger.Flush ();*/

			projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Error;
			projectStatusStripLabel.ForeColor = Color.Red;


			PlayCriticalErrorSound ();


			MessageBox.Show (Language.ErrorLogFile + "\n" + "application.log", Language.Error);
		}

		/// <summary>
		/// Sets table layout columns.
		/// </summary>
		/// <param name="columns">Number of columns.</param>
		public void SetColumnSize (int columns)
		{
			if (InvokeRequired) {
				Action<int> invoker = SetColumnSize;

				BeginInvoke (invoker, columns);
				return;
			}

			/* Is the column single? or duos? Set it with both states. */
			switch (columns) {
				case 1: {

					tableLayout.ColumnStyles [1].Width = 0;
					tableLayout.ColumnStyles [0].Width = 100;

				}
				break;

				case 2: {

					tableLayout.ColumnStyles [0].Width = 50;
					tableLayout.ColumnStyles [1].Width = 50;

				}
				break;

				default:
				break;
			}
		}

		/// <summary>
		/// Sets table layout rows.
		/// </summary>
		/// <param name="rows">Number of rows.</param>
		public void SetRowSize (int rows)
		{
			if (InvokeRequired) {
				Action<int> invoker = SetRowSize;

				BeginInvoke (invoker, rows);
				return;
			}

			/* There's only 3 states for rows, so set others sizes on each state. */
			switch (rows) {
				case 1: {
					tableLayout.RowStyles [1].Height = 0;
					tableLayout.RowStyles [2].Height = 0;

					tableLayout.RowStyles [0].Height = 100;
					
				}
				break;


				case 2: {
					tableLayout.RowStyles [2].Height = 0;

					tableLayout.RowStyles [0].Height = 50;
					tableLayout.RowStyles [1].Height = 50;
				}
				break;

				case 3: {
					
					tableLayout.RowStyles [0].Height = 33.33f;
					tableLayout.RowStyles [1].Height = 33.33f;
					tableLayout.RowStyles [2].Height = 33.33f;

				}
				break;

				default:
				break;
			}
		}


		/* Handles normal camera errors (Managed camera errors). */
		private void OnErrorReceived (Camera sender, Exception e)
		{
			if (InvokeRequired) {
				Action<Camera, Exception> action = OnErrorReceived;

				Invoke (action, sender, e);

				return;
			}


			string error = e.ToString () + "\n" + sender.ImageProvider.GetLastErrorMessage ();

			appCore.ExternalLog ("CAMERA ERROR: " + error, LogType.Error);

			//appCore.MainLogger.WriteLog ("NORMAL CAMERA ERROR: " + error);

			//appCore.MainLogger.Save ();
			//appCore.MainLogger.Flush ();

			projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Error;
			projectStatusStripLabel.ForeColor = Color.Red;


			PlayCriticalErrorSound ();

			MessageBox.Show (Language.CameraAlreadyOpen, Language.Error);
			//MessageBox.Show (Language.ErrorLogFile + "\napplication.log", Language.Error);
		}

		private void SettingsPanelClick (object sender, EventArgs e)
		{
			hasCheckedPorts = true;

			if (hasCheckedCameras && hasCheckedPorts)
				createProjectMenu.Enabled = true;

			var settingsForm = new SettingsPanel (appCore);

			settingsForm.ShowDialog ();
		}

		private void OnResizing (object sender, EventArgs e)
		{
		}

		private void OnClosing (object sender, FormClosingEventArgs e)
		{
			appCore.BaslerManager.Dispose ();


			// Using Environment exit since we have some threads and schedulers that has high priority and won't allow application exit to happen completely.
			Environment.Exit (0);
		}


		private void OnResizeStart (object sender, EventArgs e)
		{
			foreach (var cams in deviceList) {
				cams.HasStarted = true;
			}

		}

		private void OnResizeEnd (object sender, EventArgs e)
		{
			foreach (var cams in deviceList) {
				cams.HasStarted = false;
			}
		}

		private void OnCreateProjectMenuItemClick (object sender, EventArgs e)
		{
			var projectStartForm = new ProjectStartupForm (appCore);


			projectStartForm.ShowDialog ();


			startProjectMenuItem.Enabled = endProjectMenu.Enabled = appCore.Projects.Started;

			// Disable menu items and settings on startup.
			createProjectMenu.Enabled = portsSettingsMenuItem.Enabled = cameraSettingsMenuItem.Enabled = !appCore.Projects.Started;

			if (appCore.Projects.Started) {
				projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.ProjectPending;
				projectStatusStripLabel.ForeColor = Color.Black;
			}
		}

		private void OnEndProjectMenuItemClick (object sender, EventArgs e)
		{
			var result = MessageBox.Show (Language.EndProjectRequest, Language.EndProjectText, MessageBoxButtons.YesNo);

			if (result == DialogResult.Yes) {

				appCore.Trigger.FinishTrigger ();

				var form = new SavingMenu ();


				/* Load form in a new thread so save can get procceed. */
				new Thread (() => { form.ShowDialog ();  }).Start ();

				appCore.Projects.FinishProject (new XLSLanguage () {
					ProjectInformation = Language.ProjectInformation,
					CameraInformations = Language.CameraInformation,
					CameraPhotoIndex = Language.CameraPhotoIndex,
					StartKilometer = Language.StartKilometer,
					GeoDirection = Language.GeoDirection,
					TriggerCount = Language.TriggerCount,
					ProvinceCode = Language.ProvinceCode,
					ProvinceName = Language.ProvinceName,
					ProjectName = Language.ProjectName,
					ProjectCode = Language.ProjectCode,
					FinishTime = Language.FinishTime,
					FinishArea = Language.FinishArea,
					LaneCount = Language.LaneCount,
					StartArea = Language.StartArea,
					SaveIndex = Language.SaveIndex,
					StartTime = Language.StartTime,
					GeoHeight = Language.GeoHeight,
					Longitude = Language.Longitude,
					Latitude = Language.Latitude,
					Altitude = Language.Altitude,
					FullName = Language.FullName,
					GPSIndex = Language.GPSIndex,
					CityName = Language.CityName,
					PluralS = Language.PluralS,
					Caliber = Language.Caliber,
					DataTime = Language.Time,
					Camera = Language.Camera,
					Index = Language.Index,
					Range = Language.Range,
					Name = Language.Name
				});

				/* Finished saving, so unload(close) the form. */
				form.Invoke (new MethodInvoker (form.Close));

				createProjectMenu.Enabled = true;
				portsSettingsMenuItem.Enabled = true;
				cameraSettingsMenuItem.Enabled = true;
				endProjectMenu.Enabled = false;

				projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Finished;
			}
		}

		
		private void OnCameraSettingsMenuClick (object sender, EventArgs e)
		{
			hasCheckedCameras = true;

			if (hasCheckedCameras && hasCheckedPorts)
				createProjectMenu.Enabled = true;


			new Thread (() => {
				var cameraSettingsForm = new CameraSettingsPanel (this, appCore);


				cameraSettingsForm.ShowDialog ();
			}).Start ();

		}


		//private void OnMainFormInit (object sender, EventArgs e) => appCore.Trigger.OnDeviceTrigger += OnTriggerReceived;

		// Description below on UpdateTriggerCounter method.
		private DateTime lastReceivedTrigger;


		private void UpdateTriggerCounter () //int deviceTrig, int softwareTrig)
		{
			var softwareTrig = appCore.Trigger.Triggered;

			// Project not started or trigger device not initialized yet.
			if (softwareTrig < 0)
				softwareTrig = 0;



			//if (true == true) return;
			var unit = Language.TraveledUnit;
			var numberStr = "";

			var kmCounter = "000";
			var mmCounter = "000";

			float meters = softwareTrig;
			float kiloMeters = 0;


			if (meters >= 1000) {

				kiloMeters = meters / 1000;
				meters = meters % 1000;

			}

			
			 // Is-1 Digit:
			if (meters < 10) {
				mmCounter = "00" + ((int)meters);
			}
			// Is-2 digits:
			else if (meters < 100) {
				mmCounter = "0" + ((int)meters);
			}
			// Is-3 digits and not zero.
			else if (meters > 0) {
				mmCounter = ((int)meters).ToString ();
			}


			if (kiloMeters < 10) {
				kmCounter = "00" + ((int)kiloMeters);
			} else if (kiloMeters < 100) {
				kmCounter = "0" + ((int)kiloMeters);
			} else if (kiloMeters > 0) {
				kmCounter = ((int)kiloMeters).ToString ();
			}

			numberStr = mmCounter + " + " + kmCounter;

			triggerCountStripLabel.Text = Language.GrabCount + ": " + numberStr + " " + unit;

			// This is for checking if we received any trigger since last 30 seconds or not. failure can be caused by router which signals pulses to trigger device.
			lastReceivedTrigger = DateTime.Now;
		}

		/* Handles project start click. */
		private void OnStartProjectClick (object sender, EventArgs e)
		{
			if (InvokeRequired) {

				Action<object, EventArgs> action = OnStartProjectClick;

				Invoke (action, sender, e);


				return;
			}


			startProjectMenuItem.Enabled = hasPendingProject = false;


			projectStatusStripLabel.Text = Language.ProjectStatus + ": " + Language.Progressing;
			projectStatusStripLabel.ForeColor = Color.Black;


			appCore.Projects.RenewProjectTime ();
			appCore.Trigger.StartTrigger ();
		}


		/* Project timer tick for updating status bar. */
		private void ProjectTimerTick (object sender, EventArgs e)
		{
			if (appCore.Projects == null) return;


			/* Update GPS, no matter if we've started the project yet or not. we need to see If It's already working or not. */
			var gpsData = appCore.GetGPSData ();


			gpsStatusLabel.Text = "Lon: " + gpsData.Longitude + ", Lat: " + gpsData.Latitude;





			if (!appCore.Projects.Started) return;

			UpdateTriggerCounter ();


			if (appCore.Projects.Started && !hasPendingProject)
				timeStripLabel.Text = Language.ProjectTime + ": " + (DateTime.Now - appCore.Projects.StartTime).ToString (@"hh\:mm\:ss") + "s";
			else
				timeStripLabel.Text = Language.ProjectTime + ": 00:00:00s";



			/* Update camera image count every tick. */

			foreach (var cam in deviceList) {

				var saveIndex = cam.SaveIndex - 1;

				if (saveIndex >= 0 && saveIndex < cameraStatusBars.Length) {

					cameraStatusBars [saveIndex].Text = Language.CameraPicture + " " + cam.SaveIndex + ": " + cam.SavedIndex;

				}

			}

			if (appCore.Projects.Started) {

				if (lastReceivedTrigger != null) {

					var timeSpan = (DateTime.Now - lastReceivedTrigger);


					/* Get maximum time of no receive from config. */
					var configTimer = appCore.MainConfigManager.GetConfig ("NoReceiveTimer");


					var totalSeconds = 30;

					if (configTimer != null && !String.IsNullOrWhiteSpace(configTimer)) {

						if (int.TryParse(configTimer, out int result)) {

							totalSeconds = result;

						}

					} else {
						appCore.MainConfigManager.SetConfig ("NoReceiveTimer", totalSeconds.ToString ());
					}



					// Time past the configured maximum time?.
					if (timeSpan.TotalSeconds > totalSeconds) {

						// Re-set the timer so the beep won't be in an overflow.
						lastReceivedTrigger = DateTime.Now;


						PlayWarningSound ();

					}

				}

			}
		}

		/* Create the Map window form in a new Thread. */
		private void onMapButtonClick (object sender, EventArgs e)
		{
			new Thread (() => {
				if (MessageBox.Show (Language.MapWarning, Language.Warning, MessageBoxButtons.OKCancel) == DialogResult.OK) 
					new MapForm (appCore).ShowDialog ();
			}).Start ();
		}
		
	}
}
