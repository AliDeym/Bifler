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


namespace libBifler.Utils.Structures
{
	/// <summary>
	/// NMEA Structured GPS Data.
	/// </summary>
	public struct GPSData
	{
		/// <summary>
		/// UTC Timezone.
		/// </summary>
		public string UTCTime { get; private set; }

		/// <summary>
		/// Latitude position.
		/// </summary>
		public string Latitude { get; private set; }

		/// <summary>
		/// Longitude position.
		/// </summary>
		public string Longitude { get; private set; }

		/// <summary>
		/// Signal Strength.
		/// </summary>
		public string Quality { get; private set; }

		/// <summary>
		/// Number of Satellite connections. (Format: xx, e.g: 05).
		/// </summary>
		public string NumberSatellites { get; private set; }

		/// <summary>
		/// Relative accuracy of horizontal position (Horizontal Dilution of Precision).
		/// </summary>
		public string HDOP { get; private set; }

		/// <summary>
		/// Meters above sea level.
		/// </summary>
		public string Altitude { get; private set; }

		/// <summary>
		/// Height of geoid above WGS84 ellipsoid
		/// </summary>
		public string GeoHeight { get; private set; }

		/// <summary>
		/// Re-structured GPS data.
		/// </summary>
		/// <param name="lon">Longitude</param>
		/// <param name="lat">Latitude</param>
		/// <param name="alt">Altitude</param>
        public GPSData (string lon, string lat, string alt)
        {
            Latitude = lat;

            Longitude = lon;

			Altitude = alt;

            UTCTime = Quality = NumberSatellites = HDOP = GeoHeight = "";
        }

		/// <summary>
		/// NMEA GPS Data Structure.
		/// </summary>
		/// <param name="data">Parameterized Data.</param>
		public GPSData(string[] data)
		{
			UTCTime = data [1];

			// Disabled N and E signs from Latitude and Longitude.
			Latitude = data [2]; // + data [3];
			Longitude = data [4]; // + data [5];

			Quality = data [6];

			NumberSatellites = data [7];

			HDOP = data [8];

			Altitude = data [9] + data [10];

			GeoHeight = data [11] + data [12];
		}

		/// <summary>
		/// Indicates wether the two of same structures are same or not using their UTCTime received from the sattelites.
		/// </summary>
		/// <param name="obj">Checking object.</param>
		/// <returns>Wether the objects are exactly the same or not.</returns>
		public override bool Equals (object obj) => UTCTime == ((GPSData)obj).UTCTime;

		/// <summary>
		/// Gets .NET Hash code of GPS struct data.
		/// </summary>
		/// <returns>Hash code.</returns>
		public override int GetHashCode () => UTCTime.GetHashCode ();
	}
}
