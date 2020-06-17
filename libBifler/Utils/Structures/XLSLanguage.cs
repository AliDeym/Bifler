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

namespace libBifler.Utils.Structures
{
	/// <summary>
	/// XLS Bifler Language file protocol.
	/// </summary>
	public class XLSLanguage
	{
		/// <summary>
		/// Project name language.
		/// </summary>
		public string ProjectName;


		/// <summary>
		/// Project code language.
		/// </summary>
		public string ProjectCode;


		/// <summary>
		/// City name language.
		/// </summary>
		public string CityName;


		/// <summary>
		/// Provine name language.
		/// </summary>
		public string ProvinceName;


		/// <summary>
		/// Province code language.
		/// </summary>
		public string ProvinceCode;


		/// <summary>
		/// Start area language.
		/// </summary>
		public string StartArea;


		/// <summary>
		/// Finish area language.
		/// </summary>
		public string FinishArea;


		/// <summary>
		/// Lane count language.
		/// </summary>
		public string LaneCount;


		/// <summary>
		/// Caliber text.
		/// </summary>
		public string Caliber;


		/// <summary>
		/// Range for trigger text.
		/// </summary>
		public string Range;


		/// <summary>
		/// Trigger count text.
		/// </summary>
		public string TriggerCount;


		/// <summary>
		/// Camera language keyword.
		/// </summary>
		public string Camera;


		/// <summary>
		/// Plural form keyword.
		/// </summary>
		public string PluralS;


		/// <summary>
		/// Name keyword.
		/// </summary>
		public string Name;


		/// <summary>
		/// Full-name keyword.
		/// </summary>
		public string FullName;


		/// <summary>
		/// Index keyword.
		/// </summary>
		public string Index;


		/// <summary>
		/// Save index keyword.
		/// </summary>
		public string SaveIndex;


		/// <summary>
		/// Camera informations keyword.
		/// </summary>
		public string CameraInformations;


		/// <summary>
		/// Camera photo index keyword.
		/// </summary>
		public string CameraPhotoIndex;


		/// <summary>
		/// Project information keyword.
		/// </summary>
		public string ProjectInformation;


		/// <summary>
		/// Starting kilometer keyword.
		/// </summary>
		public string StartKilometer;


		/// <summary>
		/// Start time keyword.
		/// </summary>
		public string StartTime;


		/// <summary>
		/// Finish time keyword.
		/// </summary>
		public string FinishTime;


		/// <summary>
		/// GPS Index keyword.
		/// </summary>
		public string GPSIndex;


		/// <summary>
		/// GPS Latitude keyword.
		/// </summary>
		public string Latitude;


		/// <summary>
		/// GPS Longitude keyword.
		/// </summary>
		public string Longitude;


		/// <summary>
		/// GPS Geoheight keyword.
		/// </summary>
		public string GeoHeight;


		/// <summary>
		/// GPS Altitude keyword.
		/// </summary>
		public string Altitude;


		/// <summary>
		/// GPS Data time keyword.
		/// </summary>
		public string DataTime;


		/// <summary>
		/// Geographical direction.
		/// </summary>
		public string GeoDirection;
	}
}
