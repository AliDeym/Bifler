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

namespace libBifler.Utils.Structures
{
	/// <summary>
	/// Contains information about each point of trigger.
	/// </summary>
	public class ProjectData
	{
		/// <summary>
		/// Camera pictures index (Trigger count.)
		/// </summary>
		public int Index;

		/// <summary>
		/// Latitude.
		/// </summary>
		public string Latitude;

		/// <summary>
		/// Longitude.
		/// </summary>
		public string Longitude;

		/*/// <summary>
		/// GeoHeight above WGS84 ellipsoid.
		/// </summary>
		public string GeoHeight;*/

		/// <summary>
		/// Meters above sea level.
		/// </summary>
		public string Altitude;



		/// <summary>
		/// Date and Time since creation of this data instance.
		/// </summary>
		public DateTime DataTime;

		/// <summary>
		/// Image index for each camera device.
		/// </summary>
		public Tuple<int, uint> [] Images;



		/// <summary>
		/// Creates a Project Data instance.
		/// </summary>
		/// <param name="index">Unique point index Identifier.</param>
		/// <param name="lat">Latitude</param>
		/// <param name="lng">Longitude</param>
		/// <param name="altitude">Altitude, distance from sea levels.</param>
		/// <param name="images">Contains picture index for each camera device.</param>
		public ProjectData (int index, string lat, string lng, string altitude, Tuple<int, uint>[] images)
		{
			Index = index;

			Latitude = lat;

			Longitude = lng;

			//GeoHeight = geoHeight;

			Altitude = altitude;

			Images = images;

			DataTime = DateTime.Now;
		}
	}
}
