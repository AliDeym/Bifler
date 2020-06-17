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
using System.IO;
using System.Collections.Generic;

using libBifler.Utils.Structures;

namespace libBifler.Project
{
	/// <summary>
	/// GPS pointed data structure for Map project.
	/// </summary>
	public struct MapData
	{
		/// <summary>
		/// Longitude Map information.
		/// </summary>
		public double Longitude;

		/// <summary>
		/// Latitude Map information.
		/// </summary>
		public double Latitude;

		/// <summary>
		/// Creates a new structured Map Data.
		/// </summary>
		/// <param name="lon">Longitude</param>
		/// <param name="lat">Latitude</param>
		public MapData (double lat, double lon)
		{
			Longitude = lon;

			Latitude = lat;
		}
	}

	/* Ali deym's protocol and OpCodes on map data saving:
	 
	 * [First lane, copyright].
	 * On position 48, the binary data begins.
	 * Position 48 has two 8 bytes serialized dates. Here's the complete list of data stored in files at & after position 48:
	 * Project Code (String), Project Start Date (Int64), Project Finish Date (Int64), Number of points stored (Int32).
	 * This was the header packet. after you read the header, you should begin reading content packets, number of content packets are same as
	 * number of stored points in header packet.
	 * Each content packet is created of:
	 * Latitude (Double), Longitude (Double).
	 
	*/

	/// <summary>
	/// Handles GPS Map data.
	/// </summary>
	public class MapDataHandler : IDisposable
	{
		static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger ();

		private int saveInterval;

		/// <summary>
		/// Project unique code.
		/// </summary>
		public string ProjectCode;

		/// <summary>
		/// Project name.
		/// </summary>
		public string ProjectName;

		/// <summary>
		/// Project province.
		/// </summary>
		public string Province;

		/// <summary>
		/// Project city.
		/// </summary>
		public string City;

		/// <summary>
		/// Starting area.
		/// </summary>
		public string StartingArea;

		/// <summary>
		/// Finishing area.
		/// </summary>
		public string FinishingArea;

		/// <summary>
		/// Number of triggers.
		/// </summary>
		public int TriggerCount;



		/// <summary>
		/// Project start time.
		/// </summary>
		public DateTime ProjectStartTime;

		/// <summary>
		/// Project finish time.
		/// </summary>
		public DateTime ProjectFinishTime;


		private readonly string dataFolder = "mapdata";


		/// <summary>
		/// Indicates wether the current map handler is loaded or not.
		/// </summary>
		public bool IsLoaded { get; private set; }

		/// <summary>
		/// Contains map data for specified project.
		/// </summary>
		public List<MapData> Data = null;

		/// <summary>
		/// Creates a new Map Handler instance, for handling map structured data.
		/// </summary>
		/// <param name="projectName">Project name.</param>
		/// <param name="provinceName">Province name.</param>
		/// <param name="cityName">City name.</param>
		/// <param name="interval">Saving interval, how many points should be skipped to save the next point.</param>
		/// <param name="startArea">Starting area.</param>
		/// <param name="finishArea">Finishing area.</param>
		public MapDataHandler (string projectName, string provinceName, string cityName, string startArea, string finishArea, int interval)
		{
			IsLoaded = false;

			ProjectName = projectName;
			Province = provinceName;
			City = cityName;

			StartingArea = startArea;
			FinishingArea = finishArea;

			saveInterval = interval;

			Data = new List<MapData> ();
		}

		/// <summary>
		/// Cleans memory on dispose of current handler.
		/// </summary>
		public void Dispose ()
		{
			Data.Clear ();

			GC.Collect ();
		}

		/// <summary>
		/// Gets a developer friendly name for storing key on collections.
		/// </summary>
		/// <returns>Dev-friendly handler name.</returns>
		public string GetFriendlyName () => ProjectName + "-" + Province + "-" + City + "-" + StartingArea + "-" + FinishingArea;


		/// <summary>
		/// Generates MapData from Project data.
		/// </summary>
		/// <param name="projectData">List of project data.</param>
		public void GenerateData (List<ProjectData> projectData)
		{
			var offset = -1;
			var globalOffset = 0;

			foreach (var db in projectData) {
				globalOffset++;


				if (offset >= 0 && offset < saveInterval && globalOffset < projectData.Count) {
					offset++;

					continue;
				}

				offset = 0;

				if (double.TryParse (db.Latitude, out double lat)) {

					if (double.TryParse (db.Longitude, out double lon)) {

						Data.Add (new MapData (lat, lon));

					}

				}

			}

			IsLoaded = true;
		}


		/// <summary>
		/// Saves the generated data into it's own categorized folder.
		/// </summary>
		public void HandleSave ()
		{
			if (saveInterval <= 0) return;

			var checkDirs = new [] {
				dataFolder + "\\",
				dataFolder + "\\" + ProjectName + "\\",
				dataFolder + "\\" + ProjectName + "\\" + Province + "\\" + City + "\\",
				dataFolder + "\\" + ProjectName + "\\" + Province + "\\" + City + "\\" + StartingArea + "\\"
			};

			log.Trace ("Checking directories for MapDataSave: " + ProjectName);

			for (var i = 0; i < checkDirs.Length; i++) {

				if (!Directory.Exists (checkDirs [i]))
					Directory.CreateDirectory (checkDirs [i]);

			}

			log.Trace ("Created unexisting directories.");


			var saveDirectory = checkDirs [checkDirs.Length - 1];

			var fileName = FinishingArea + ".libdata";

			log.Trace ("Writing map data into " + fileName + "...");
			log.Trace ("With directory exactly being: " + saveDirectory + " concated with filename.");

			/*using (*/
			var sw = new StreamWriter (saveDirectory + fileName);//) {


			sw.WriteLine ("[Map Data by Ali Deym Copyright (c) 2017-2018]");

			sw.Flush ();

			using (var bw = new BinaryWriter (sw.BaseStream)) {

				bw.Write (ProjectName);

				bw.Write (ProjectStartTime.ToBinary ());
				bw.Write (ProjectFinishTime.ToBinary ());

				bw.Write (TriggerCount);

				bw.Write (Data.Count);

				foreach (var generatedData in Data) {

					bw.Write (generatedData.Longitude);
					bw.Write (generatedData.Latitude);

				}

			}

			log.Trace ("Finished writing map data into " + fileName + ".");

			//}
		}


		/// <summary>
		/// Returns file name for 
		/// </summary>
		/// <returns></returns>
		public string GetFileName () => dataFolder + "\\" + ProjectName + "\\" + Province + "\\" + City + "\\" + StartingArea + "\\" + FinishingArea + ".libdata";


		/// <summary>
		/// Generates data from file.
		/// </summary>
		public void HandleLoad ()
		{
			var loadDirectory = dataFolder + "\\" + ProjectName + "\\" + Province + "\\" + City + "\\" + StartingArea + "\\";

			var fileName = FinishingArea + ".libdata";

			if (!File.Exists (loadDirectory + fileName)) return;


			try {
				log.Trace ("Loading " + fileName + "...");

				var sr = new StreamReader (loadDirectory + fileName);

				var copyLane = sr.ReadLine ();

				if (copyLane != "[Map Data by Ali Deym Copyright (c) 2017-2018]") {
					log.Fatal ("Invalid Copyright for " + fileName + ".");

					sr.Close ();

					return;
				}


				using (var br = new BinaryReader (sr.BaseStream)) {

					sr.BaseStream.Seek (48, SeekOrigin.Begin);

					/* Read project code from header. */
					var projectCode = br.ReadString ();
					ProjectCode = projectCode;


					/* Read dates from header. Read file protocol from top comment. */
					var startDateSerialized = br.ReadInt64 ();
					var finishDateSerialized = br.ReadInt64 ();

					ProjectStartTime = DateTime.FromBinary (startDateSerialized);
					ProjectFinishTime = DateTime.FromBinary (finishDateSerialized);


					TriggerCount = br.ReadInt32 ();


					/* Read data count. */
					var dataCount = br.ReadInt32 ();


					for (int i = 0; i < dataCount; i++) {
						var lon = br.ReadDouble ();
						var lat = br.ReadDouble ();



						Data.Add (new MapData (lat, lon));
					}
				}



				IsLoaded = true;

				sr.Close ();

				log.Trace ("Finished loading " + fileName + ".");
			}
			catch (Exception ex) {
				// Failure of map data load. (Maybe broken data file?.)
				log.Fatal ("Failed to load " + fileName + ".");
				log.Trace ("Detailed exception: " + ex.ToString ());
			}
		}
	}
}
