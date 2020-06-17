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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace TriggerServer
{
	class GPSHandler
	{
		private Action<float, float, float> executeDataFunction;

		/* GPS Serial USB Port. */
		private SerialPort gpsPort = null;

		/// <summary>
		/// Creates a GPS device handler.
		/// </summary>
		/// <param name="portName">Serial port.</param>
		/// <param name="executionMethod">Method (latitude, longitude, altitude);</param>
		public GPSHandler (string portName, Action<float, float, float> executionMethod)
		{
			executeDataFunction = executionMethod;

			gpsPort = new SerialPort (portName, 4800, Parity.None, 8, StopBits.One);

			gpsPort.DataReceived += dataReceived;

			gpsPort.Open ();
		}


		private void dataReceived (object sender, SerialDataReceivedEventArgs e)
		{
			if (!gpsPort.IsOpen) return;

			try {

				var data = gpsPort.ReadLine ();

				Console.WriteLine ("GPS DATA: " + data);
				//log.Info (data);



				var rawData = data.Split (',');

				if (rawData [0] == "$GPGGA") {

					var rawLat = rawData [2];
					var rawLon = rawData [4];
					var rawAlt = rawData [9];


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


					float longitude = 0f;
					float latitude = 0f;

					float altitude = 0f;

					if (!String.IsNullOrWhiteSpace(rawAlt)) {
						altitude = float.Parse (rawAlt);
					}

					longitude = float.Parse (lon);
					latitude = float.Parse (lat);

					executeDataFunction (latitude, longitude, altitude);
				}

			}
			catch (Exception ex) {
				//log.Fatal ("RECEIVE ERROR: " + ex.ToString ());
			}
		}
	}
}
