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
using System.Threading;
using System.IO.Ports;


using libBifler.Utils.Structures;


namespace libBifler
{
	/// <summary>
	/// GPS device with NMEA Protocols.
	/// </summary>
	public class GPS
	{
		/// <summary>
		/// Serial Port for GPS device. (Default COM3).
		/// </summary>
		public string Port { get; private set; }

		/// <summary>
		/// Indicates wether our Serial port is connected and is also a NMEA protocol device.
		/// </summary>
		public bool IsConnected { get; private set; }


		private SerialPort serialConnection = null;
		private const int baudRate = 4800;
		private static NLog.Logger log = null;
		private GPSData gpsData;


		private Thread workerThread = null;



		/// <summary>
		/// Creates a GPS device connection.
		/// </summary>
		/// <param name="COMPort">Serial connection port. (Default COM3).</param>
		public GPS (string COMPort = "COM3")
		{
			IsConnected = false;

			Port = COMPort;

			log = NLog.LogManager.GetLogger ("GPS");

			//log.Automate (30000);
			log.Info ("GPS Instantiate on Port: " + COMPort + ".");

		}


		/// <summary>
		/// Re-Sets the COMPort for the GPS.
		/// </summary>
		/// <param name="COMPort">Serial connection port. (DEfault COM3).</param>
		public ErrorType SetPort(string COMPort = "COM3")
		{
			if (COMPort == null)
				COMPort = "COM3";

			IsConnected = false;

			if (serialConnection != null && serialConnection.IsOpen) {

				serialConnection.Close ();
				serialConnection.Dispose ();

				serialConnection = null;

				GC.Collect ();
			}

			gpsData = new GPSData ();

			log.Info ("Re-setting the GPS Port on: " + COMPort + ".");

			Port = COMPort;

			return TryConnect ();
		}


		/// <summary>
		/// Returns the last updated GPS Data.
		/// </summary>
		/// <returns>GPS Information Data.</returns>
		public GPSData GetData () => gpsData;


		/// <summary>
		/// Safely connects to the GPS device.
		/// </summary>
		/// <returns>ErrorType or ErrorType.None.</returns>
		public ErrorType TryConnect ()
		{
			try {
				Connect ();
			}
			catch (Exception ex) {
				log.Error ("GPS Connection Error: " + ex.ToString ());

				//log.Save ();
				//log.Flush ();

				return ErrorType.SerialConnection;
			}

			return ErrorType.None;
		}


		/// <summary>
		/// Unsafe connection to the GPS device.
		/// </summary>
		public void Connect ()
		{
			IsConnected = false;


			if (workerThread != null) {
				workerThread.Abort ();

				workerThread = null;
			}


			serialConnection = new SerialPort (Port, baudRate, Parity.None, 8, StopBits.One);

			serialConnection.Handshake = Handshake.None;

			//serialConnection.DataReceived += OnConnectionDataReceived;

			serialConnection.Open ();


			/* Handle reading GPS in such a low-cpu load way. */
			workerThread = new Thread (receiveBackgroundThread);

			workerThread.Priority = ThreadPriority.Lowest;

			workerThread.IsBackground = true;


			workerThread.Start ();
		}


		/* Handles the NMEA protocol messages. */
		private void receiveBackgroundThread ()
		{

			while (true) {
				var sp = serialConnection;

				if (sp == null) continue;


				try {

					if (!sp.IsOpen) {

						//TryConnect ();
						continue;

					}

					

					var data = sp.ReadLine ();

					//Console.WriteLine (data);
					//log.Info (data);

					if (!IsConnected)
						IsConnected = true;

					var rawData = data.Split (',');

					// Check for invalid packets.
					if (rawData.Length <= 5) continue;

					// Ignore no signal data.
					if (String.IsNullOrWhiteSpace (rawData [2]) || String.IsNullOrWhiteSpace (rawData [4])) continue;

					if (rawData [0] == "$GPGGA") {

						gpsData = new GPSData (rawData);


						var rawLat = gpsData.Latitude;
						var rawLon = gpsData.Longitude;


						var lon = "";
						var lat = "";

						if (rawLat != null) {

							if (rawLat.Length > 5) {
								var degree = rawLat.Substring (0, 2);

								if (degree.StartsWith ("0")) {
									degree = degree.Substring (1);
								}


								var minutes = rawLat.Substring (2);


								double fMinutes = double.Parse (minutes);


								fMinutes = fMinutes / 60;

								fMinutes = Math.Round (fMinutes, 6);

								var deciDegree = double.Parse (degree);



								lat = (deciDegree + fMinutes).ToString ();
							}
						}

						if (rawLon != null) {

							if (rawLon.Length > 6) {
								var degree = rawLon.Substring (0, 3);

								if (degree.StartsWith ("00")) {
									degree = degree.Substring (2);
								}
								else if (degree.StartsWith ("0")) {
									degree = degree.Substring (1);
								}


								var minutes = rawLon.Substring (3);


								double fMinutes = double.Parse (minutes);


								fMinutes = fMinutes / 60;

								fMinutes = Math.Round (fMinutes, 6);

								var deciDegree = double.Parse (degree);

								lon = (deciDegree + fMinutes).ToString ();
							}
						}


						gpsData = new GPSData (lon, lat, gpsData.Altitude);
					}

				}
				catch (Exception ex) {

					if (ex is ThreadAbortException)
						break;
					else if (ex is System.IO.IOException)
						break;
					else
						log.Fatal ("RECEIVE ERROR: " + ex.ToString ());


				}
			}
		}
	}
}
