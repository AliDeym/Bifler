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
using System.Windows.Forms;
using System.Diagnostics;


using libBifler;
using libBifler.Utils.Structures;


namespace Bifler
{
	/// <summary>
	/// Creates a loading form which has loading bar and loads the core.
	/// </summary>
	public partial class LoadingForm : Form
	{
		Core loadingCore = null;

		/// <summary>
		/// Initializes the form.
		/// </summary>
		public LoadingForm()
		{
			InitializeComponent ();
		}

		private void MakeProgress (string text, int percentage)
		{
			progressBar.Value = percentage;
			statusText.Text = text;
		}

		private void OnInitialization (object sender, EventArgs e)
		{
			loadingCore = new Core ("Bifler", "4.65.10");
			MakeProgress (Language.InitializingCore, 20);


			var processList = Process.GetProcessesByName ("bifler");

			if (processList.Length > 1) {
				MessageBox.Show (Language.AppIsAlreadyOpen, Language.Error);
				Environment.Exit (0);

				return;
			}

			loadingCore.Initialize ();


			MakeProgress (Language.PylonLibraryLoading, 40);


			if (loadingCore.BaslerManager == null || !loadingCore.BaslerManager.Loaded) {

				MessageBox.Show (Language.PylonLibraryError, Language.Error);

				Application.Exit ();

			}


			MakeProgress (Language.TriggerConnecting, 60);

			var triggerPort = loadingCore.MainConfigManager.GetConfig ("TriggerPort");

			if (triggerPort == null)
				triggerPort = "3";



			loadingCore.InstantiateTriggerDevice (triggerPort);

			/*if (triggerStatus == ErrorType.SerialConnection) {

				MessageBox.Show (Language.SerialConnectionError, Language.Error);

				var logger = loadingCore.MainLogger;

				logger.WriteLog ("ERROR with serial connection for Trigger.");

				logger.Save ();
				logger.Flush ();

			}*/




			var gpsPort = loadingCore.MainConfigManager.GetConfig ("GPSPort");

			if (gpsPort == null)
				gpsPort = "COM3";



			MakeProgress (Language.InitializingGPS, 90);
			var gpsStatus = loadingCore.InstantiateGPS (gpsPort);


			

			if (gpsStatus == ErrorType.SerialConnection) {
				MessageBox.Show (Language.SerialConnectionError, Language.Error);

				loadingCore.ExternalLog ("ERROR with serial connection for GPS.");
			}


			MakeProgress (Language.LoadingFinished, 100);


			//new Thread (ShowMainWindow).Start ();


			//Close ();
			Hide ();

			ShowMainWindow ();
		}


		private void ShowMainWindow ()
		{
			var settingsPanel = new MainWindow (loadingCore);

			settingsPanel.ShowDialog ();
		}


		private void OnLoading (object sender, EventArgs e)
		{
			var screenBounds = Screen.PrimaryScreen.Bounds;

			Location = new System.Drawing.Point (screenBounds.Width / 2 - Size.Width / 2, 50);
		}
	}
}
