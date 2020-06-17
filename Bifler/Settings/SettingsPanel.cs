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
using System.IO.Ports;
using System.Windows.Forms;

using libBifler;

namespace Bifler
{
	/// <summary>
	/// Application settings panel.
	/// </summary>
	public partial class SettingsPanel : Form
	{
		private Core appCore = null;


		private string previewPort = "COM3"; // Defeault port when the settings loaded, used to revert GPS back to it's state before settings.
		private uint refreshPorts = 0; // Maximum 5 Intervals till we refresh the ports, every 5 seconds.
		private uint waitTicks = 3; // GPS has a delay on giving coordinations so we wait 3 intervals.


		private string triggerPortPreview = "3";


		/// <summary>
		/// Initializes the settings panel.
		/// </summary>
		/// <param name="ApplicationCore">Application's core.</param>
		public SettingsPanel (Core ApplicationCore)
		{
			InitializeComponent ();

			appCore = ApplicationCore;
		}

		private void Initialization (object sender, EventArgs e)
		{
			if (appCore.Projects.Started) {
				caliberButton.Enabled = false;
			}

			var portNames = SerialPort.GetPortNames ();

			foreach (var port in portNames) {
				comBox.Items.Add (port);
			}



			var configPort = appCore.MainConfigManager.GetConfig ("GPSPort");

			var triggerPort = appCore.MainConfigManager.GetConfig ("TriggerPort");

			var triggerCaliber = appCore.MainConfigManager.GetConfig ("TriggerCount");

			var triggerMeter = appCore.MainConfigManager.GetConfig ("TriggerMeter");


			previewPort = configPort;
			triggerPortPreview = triggerPort;


			if (!String.IsNullOrWhiteSpace (configPort)) {
				if (Array.IndexOf (portNames, configPort) > -1) {

					comBox.SelectedIndex = comBox.Items.IndexOf (configPort);

				}
			}

			//if (Array.IndexOf(portNames, triggerPort) > -1) {

			if (!String.IsNullOrWhiteSpace (triggerPort)) {
				if (int.TryParse (triggerPort, out int result))
					trigBox.Value = result;
			}

			//}

			if (!String.IsNullOrWhiteSpace (triggerCaliber)) {

				if (int.TryParse (triggerCaliber, out int result))

					triggerCaliberCount.Value = result;

			}

			if (!String.IsNullOrWhiteSpace (triggerMeter)) {

				if (int.TryParse (triggerMeter, out int result))

					meterCount.Value = result;

			}


			appCore.Trigger.OnCaliberStatus += CaliberStatusReceived;
			appCore.Trigger.OnHeartbeatReceived += OnHeartbeatReceived;
			//appCore.Trigger.OnPendingProjectReceived += PendingProjectReceived;

			DeviceRefresher.Enabled = true; // Enable refresher timer.
		}

        private void OnHeartbeatReceived () { caliberButton.Enabled = true; HeartbeatStatus = true; }

		/// <summary>
		/// Used for hearbeat status changing.
		/// </summary>
		private bool HeartbeatStatus = false;


		private int CaliberValue = 0;

		private void CaliberStatusReceived (int value)
		{
			/*if (finished) {
				MessageBox.Show (Language.CaliberFinish, Language.Caliber);

				caliberButton.Text = Language.CaliberStart;

				CaliberStatus = false;
			}
			else {

				CaliberStatus = true;

				MessageBox.Show (Language.CaliberStart, Language.Caliber);

				caliberButton.Text = Language.CaliberFinish;

			}*/

			if (value < 0) {

				InvokeControl (caliberButton, "Text", Language.CaliberStart);
				//caliberButton.Text = Language.CaliberStart;

				CaliberValue = 0;

			} else {

				InvokeControl (caliberButton, "Text", Language.CaliberFinish);

				//caliberButton.Text = Language.CaliberFinish;

				//Action<int> act = InvokeSet;

				//triggerCaliberCount.Invoke (act, value);

				InvokeControl (triggerCaliberCount, "Value", (decimal)value);

				CaliberValue = value;
			}

		}

		/* Invoke set field on control. */
		private void InvokeControl (Control control, string key, object value)
		{
			Action<Control, string, object> act = InvokeSetField;

			control.Invoke (act, control, key, value);
		}

		private void InvokeSetField (Control control, string key, object value)
		{
			var field = control.GetType ().GetProperty (key);

			field.SetValue (control, value);
		}

		private void InvokeSet (int value)
		{
			triggerCaliberCount.Value = value;
		}

		private void OnCancelClick (object sender, EventArgs e)
		{
			appCore.GPSDevice.SetPort (previewPort);
			appCore.Trigger.SetPort ("192.168.1." + triggerPortPreview);


			Close ();
		}

		private void OnSaveClick (object sender, EventArgs e)
		{
			// Using variables in-case of Whitespaces we can't get the SelectedItem and app errors.
			var GPSPort = "";
			var TriggerPort = "";
			var TriggerCount = "";
			var TriggerMeter = "";


			if (comBox.SelectedItem != null)
				GPSPort = comBox.SelectedItem.ToString ();
			//if (trigBox.Value != null)
			TriggerPort = ((int)trigBox.Value).ToString ();

			if (triggerCaliberCount != null)
				TriggerCount = triggerCaliberCount.Value.ToString ();
			if (meterCount != null)
				TriggerMeter = meterCount.Value.ToString ();

			if (meterCount != null && triggerCaliberCount != null) {
				var trig = (int)triggerCaliberCount.Value;
				var meter = (int)meterCount.Value;

				appCore.Trigger.Caliber = trig / meter;
			}

			triggerPortPreview = TriggerPort;

			appCore.MainConfigManager.SetConfig ("GPSPort", GPSPort);
			appCore.MainConfigManager.SetConfig ("TriggerPort", TriggerPort);
			appCore.MainConfigManager.SetConfig ("TriggerCount", TriggerCount);
			appCore.MainConfigManager.SetConfig ("TriggerMeter", TriggerMeter);


			Close ();
		}

		private void OnInterval (object sender, EventArgs e)
		{
			/* GPS Status */
			if (appCore == null || !appCore.GPSDevice.IsConnected) {

				if (waitTicks >= 3) {
					gpsStatusLabel.Text = Language.GPSConnectionFailure;
					gpsStatusLabel.ForeColor = Color.Red;
				}

			} else {
				gpsStatusLabel.Text = Language.GPSConnectionSuccess;
				gpsStatusLabel.ForeColor = Color.Green;
			}


			/* Trigger Status */
			if (appCore == null || appCore.Trigger == null || appCore.Trigger.TriggerCount <= -1 || !appCore.Trigger.Connected) {

				if (waitTicks >= 3) {
					triggerStatusLabel.Text = Language.TriggerConnectionFailure;
					triggerStatusLabel.ForeColor = Color.Red;
				}

			}
			else {
				triggerStatusLabel.Text = Language.TriggerConnectionSuccess;
				triggerStatusLabel.ForeColor = Color.Green;

                caliberButton.Enabled = true;
			}


			if (waitTicks <= 3)
				waitTicks++;


			refreshPorts++;

			// This block must also send heartbeats.

			if (refreshPorts >= 5) {
				refreshPorts = 0;

				// Don't send heartbet request If we're calibrating.
				if (!HeartbeatStatus) 
					appCore.Trigger.RequestHeartbeat ();

				var needsRefresh = false;

				var ports = SerialPort.GetPortNames ();

				/* List doesn't have a port in it. */
				foreach (var port in ports) {

					if (!comBox.Items.Contains(port)) {
						needsRefresh = true;
					}

				}

				/* List has a port that doesn't exist in Array. */
				foreach (var entry in comBox.Items) {

					if (Array.IndexOf(ports, entry) <= -1) {
						needsRefresh = true;
					}

				}


				if (needsRefresh) {
					comBox.Items.Clear ();
					comBox.Text = "";

					//trigBox.Items.Clear ();
					//trigBox.Text = "3";


					foreach (var port in ports) {
						comBox.Items.Add (port);
						//trigBox.Items.Add (port);
					}
				}

			}
		}

		private void OnClosing (object sender, FormClosingEventArgs e)
		{
			appCore.GPSDevice.SetPort (previewPort);
			appCore.Trigger.SetPort ("192.168.1." + triggerPortPreview);



			// Remove the event on form closing.
			appCore.Trigger.OnCaliberStatus -= CaliberStatusReceived;
			appCore.Trigger.OnHeartbeatReceived -= OnHeartbeatReceived;
			//appCore.Trigger.OnPendingProjectReceived -= PendingProjectReceived;




			DeviceRefresher.Enabled = false;
			DeviceRefresher.Dispose ();
			//OnCancelClick (null, null);
		}

		private void OnValueChanged (object sender, EventArgs e)
		{
			appCore.GPSDevice.SetPort (comBox.Text);

			waitTicks = 0;

			gpsStatusLabel.Text = Language.PleaseWait;
			gpsStatusLabel.ForeColor = Color.Black;
		}

		private void OnTriggerValueChanged (object sender, EventArgs e)
		{
			appCore.Trigger.SetPort ("192.168.1." + trigBox.Text);

			/*caliberButton.Enabled = */ HeartbeatStatus = false;
			

			waitTicks = 0;

			triggerStatusLabel.Text = Language.PleaseWait;
			triggerStatusLabel.ForeColor = Color.Black;
		}

		private void OnCaliberClick (object sender, EventArgs e)
		{
			if (CaliberValue > 0)
				appCore.Trigger.FinishCaliber ();
			else
				appCore.Trigger.StartCaliber ();
		}
	}
}
