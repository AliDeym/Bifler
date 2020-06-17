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

using System.Net;
using System.IO.Ports;
using System.Threading;

using TriggerServer;

namespace libBifler
{
	/// <summary>
	/// Trigger device, Raspberry PI. Electrical Pin Inputs by Biftor (Parham Abedi).
	/// </summary>
	public partial class TriggerDevice
	{
		/// <summary>
		/// Serial Port for GPS device. (Default COM1).
		/// </summary>
		public IPAddress IPAddress { get; private set; }


		/// <summary>
		/// Trigger counted from device. (ERROR: -1).
		/// </summary>
		public int TriggerCount { get; set; }


		/// <summary>
		/// Number of total software-Triggers. (Used for checksum).
		/// </summary>
		public int Triggered { get; set; }


		/// <summary>
		///  Caliber received from the trigger device. NOTE: This is no longer received from the device, instead it's a: local app to device variable.
		/// </summary>
		public int Caliber { get; set; }


		/// <summary>
		/// Determines wether we're connected or not. (This might have no-realtime speed, so avoid using it in infinite high-speed loops.).
		/// </summary>
		public bool Connected {
			get {
				if (networkManager == null) return false;

				return networkManager.IsConnected;
			}
		}



		//private CancellationTokenSource keepAliveToken;
		/*private SerialPort serialConnection = null;
		private Thread dataReceiveThread = null;
		private const int baudRate = 460800;*/

		private NetworkManager networkManager = null;
		private static readonly TriggerServer.Version biflerVersion = new TriggerServer.Version (4, 74, 23);
		private static NLog.Logger log = NLog.LogManager.GetLogger ("TriggerClass");



		/// <summary>
		/// Trigger event Handler.
		/// </summary>
		/// <param name="deviceTrig">Number of device Triggers.</param>
		/// <param name="softwareTrig">Number of software Triggers.</param>
		public delegate void TriggerHandler (int deviceTrig, int softwareTrig);


		/// <summary>
		/// Handles calibers and live-preview of it.
		/// </summary>
		public delegate void CaliberHandler (int value);


		/// <summary>
		/// Handles heartbeat receives.
		/// </summary>
		public delegate void HeartbeatHandler ();


		/// <summary>
		/// Called whenever caliber received from the board. The number indicates live-preview of the caliber.
		/// </summary>
		public event CaliberHandler OnCaliberStatus;


		/// <summary>
		/// Called whenever the heartbeat request is sent back.
		/// </summary>
		public event HeartbeatHandler OnHeartbeatReceived;


		/// <summary>
		/// Called whenever there's a pending project packet received from radio, meaning we should start trigging.
		/// </summary>
		public event EventHandler OnPendingProjectReceived;


		/// <summary>
		/// Called whenever the device triggers.
		/// </summary>
		public event TriggerHandler OnDeviceTrigger;


		/// <summary>
		/// Creates a Trigger device connection.
		/// </summary>
		/// <param name="IP">IP Address of the Server. (Default 192.168.1.3).</param>
		public TriggerDevice (string IP = "192.168.1.3")
		{
			if (IP == null)
				IP = "192.168.1.3";

			IPAddress = IPAddress.Parse (IP);

			TriggerCount = -1;
			Triggered = -1;


			//log.Automate (30000);
			log.Info ("Instantiating on IP: " + IP);



			log.Info ("Instantiating manager...");



			networkManager = new NetworkManager (IP);

			//networkManager.Initialize ();



			log.Info ("Instantiated the network manager.");

			/*dataReceiveThread = new Thread (handleReceiving);

			dataReceiveThread.Priority = ThreadPriority.Highest;



			dataReceiveThread.Start ();*/
		}


		/* Logs and writes packet into networkManager. */
		private void WriteLoggedPacket (OpCodes packet)
		{
			log.Info ("Writing packet: " + packet + ".");

			networkManager.WritePacket (packet);
		}


		/* Logs and writes packet (with data) into networkManager. */
		private void WriteLoggedPacket (OpCodes packet, object data)
		{
			log.Info ("Writing packet: " + packet + ", data: " + data + ".");

			networkManager.WritePacket (packet, data);
		}


		/// <summary>
		/// Re-Sets the IP Address for the Trigger Device. Please NOTE that this only works if connection fails.
		/// </summary>
		/// <param name="IP">Network host IP address. (Default 192.168.1.3).</param>
		public void SetPort(string IP = "192.168.1.3")
		{
			if (IP == null)
				IP = "192.168.1.3";



			TriggerCount = -1;
			Triggered = -1;

			


			log.Info ("Re-setting the Trigger IP to: " + IP + ".");

			networkManager.Reconnect ();

			IPAddress = IPAddress.Parse (IP);
			networkManager.IPAddress = IPAddress;


			//return TryConnect ();
		}


		/* Simply, just Deprecated, since we have our NetworkManager pretty much safely done.
		/// <summary>
		/// Safely connects to the Trigger device.
		/// </summary>
		/// <returns>ErrorType or ErrorType.None.</returns>
		public ErrorType TryConnect ()
		{
			try {
				Connect ();
			}
			catch (Exception ex) {
				log.WriteLog ("Trigger Connection Error: " + ex.ToString ());

				log.Save ();
				log.Flush ();

				return ErrorType.SerialConnection;
			}

			return ErrorType.None;
		}*/

		/// <summary>
		/// Initializes this instance for future connections.
		/// </summary>
		public void Initialize ()
		{
			networkManager.Initialize (biflerVersion);

			HookMethods ();
		}

		/*
		/// <summary>
		/// Unsafe connection to the Trigger device.
		/// </summary>
		public void Connect ()
		{

			//NetworkClient.Initialize (this, "192.168.1.10");

			networkManager.Initialize (biflerVersion);

			var t = new Thread (NetworkClient.Connect);

			t.Priority = ThreadPriority.Highest;

			t.Start ();

			serialConnection = new SerialPort (IPAddress, baudRate, Parity.None, 8, StopBits.One);

			//serialConnection.DataReceived += OnConnectionDataReceived;

			serialConnection.RtsEnable = true;
			serialConnection.DtrEnable = true;

			serialConnection.Open ();
			

			
		}*/

		// TODO: Clean this method.
		/*public void ExternalTrig (int trig)
		{
			TriggerCount = trig;
			Triggered++;

			OnDeviceTrigger.Invoke (TriggerCount, Triggered);
		}*/

		/*private void handleReceiving ()
		{*/
			/*for (; ; ) {

				if (serialConnection != null && serialConnection.IsOpen) {


					if (serialConnection.BytesToRead > 0) {

						OnConnectionDataReceived ();

					}

				}
			}*/
		//}

		
		/// <summary>
		/// Updates the calibrate number to TriggerDevice.
		/// </summary>
		public void UpdateCalibration ()
		{
			try {

				networkManager.WritePacket (OpCodes.Trigger, Caliber);

			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}
		
		/// <summary>
		/// Starts the trigger-device process.
		/// </summary>
		public void StartTrigger ()
		{
			try {

				networkManager.WritePacket (OpCodes.Trigger, Caliber);
				networkManager.WritePacket (OpCodes.ProjectRequest, true);

			} catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}


		/// <summary>
		/// Initializes the project, puts trigger device into ready state so it be ready for starting.
		/// </summary>
		public void StartProject ()
		{
			try {

				networkManager.WritePacket (OpCodes.PendingRequest);

			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}

		
		/// <summary>
		/// Completes the trigger process.
		/// </summary>
		public void FinishTrigger ()
		{
			try {
				networkManager.WritePacket (OpCodes.ProjectRequest, false);
			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}


		/// <summary>
		/// Starts Caliber process.
		/// </summary>
		public void StartCaliber ()
		{
			try {
				networkManager.WritePacket (OpCodes.CaliberRequest, true);
			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}


		/// <summary>
		/// Starts Caliber process.
		/// </summary>
		public void FinishCaliber ()
		{
			try {
				networkManager.WritePacket (OpCodes.CaliberRequest, false);
			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}

		
		/// <summary>
		/// Requests a heartbeat event from the trigger device.
		/// </summary>
		public void RequestHeartbeat ()
		{
			try {
				networkManager.WritePacket (OpCodes.HeartbeatRequest);
			}
			catch (Exception ex) {
				log.Fatal ("ERROR: " + ex.ToString ());
			}
		}


		/* Writes log, and then data into TX COM Port. */
		/*private void writeData (string data)
		{
			log.WriteLog ("TX >> " + data);

			serialConnection.Write (data);
		}*/


		/*private void OnConnectionDataReceived () //object sender, SerialDataReceivedEventArgs e)
		{
			var sp = serialConnection;

			if (!sp.IsOpen) {

				TryConnect ();
				return;

			}


			//try {

				var data = sp.ReadLine ();

				data = data.Replace ("\r", "");

				//log.WriteLog ("RX << " + data);



				var rawData = data.Split (',');

				// Handle OPCodes with switch.
				switch (rawData [0]) {
					case "t": {

						if (int.TryParse (rawData [1], out int trigg)) {

							TriggerCount = trigg;
							Triggered++;

							OnDeviceTrigger.Invoke (TriggerCount, Triggered);

						}
						else {
							TriggerCount = -1;
							Triggered = -1;
						}

					}
					break;
					case "s": {

						if (rawData [1] == "1") {

							TriggerCount = 0;
							Triggered = 0;

						}
					}
					break;
					case "c": {

						if (int.TryParse (rawData [1], out int result))
							OnCaliberStatus?.Invoke (result);

					}
					break;
					case "h": {

						if (TriggerCount <= 0 && Triggered <= 0) {

							TriggerCount = 0;
							Triggered = 0;

						}

						OnHeartbeatReceived?.Invoke ();

					}
					break;
				}

			//} catch (Exception ex) {
			//	log.WriteLog ("ERROR: " + ex.ToString ());
			//}
		}*/
	}
}
