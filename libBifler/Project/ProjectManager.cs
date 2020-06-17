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


using NLog;

using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

using libBifler.Utils.Structures;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;



namespace libBifler.Project
{
	/// <summary>
	/// Handles project functions, methods and informations (properties).
	/// </summary>
	public class ProjectManager
	{
#if DEBUG
		private int sameTimeCounter = 0;
		private DateTime sameTime = DateTime.Now;
#endif

		private static NLog.Logger log = LogManager.GetCurrentClassLogger ();

		/* Application main Core that's using this instance. */
		private Core appCore = null;

		/* Stored data for project. */
		private List<ProjectData> projectData = null;


		/// <summary>
		/// Project code.
		/// </summary>
		public string Code { get; set; }


		/// <summary>
		/// Project Name.
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Start point Name.
		/// </summary>
		public string StartName { get; set; }


		/// <summary>
		/// End point Name.
		/// </summary>
		public string FinishName { get; set; }


		/// <summary>
		/// City Name.
		/// </summary>
		public string City { get; set; }


		/// <summary>
		/// Province Code.
		/// </summary>
		public string ProvinceCode { get; set; }


		/// <summary>
		/// Province Name.
		/// </summary>
		public string ProvinceName { get; set; }


		/// <summary>
		/// Geo Direction.
		/// </summary>
		public string Direction { get; set; }


		/// <summary>
		/// Lane count.
		/// </summary>
		public int LaneCount { get; set; }


		/// <summary>
		/// Caliber received from Trigger device.
		/// </summary>
		public int Caliber { get; set; }


		/// <summary>
		/// Start point GPS data.
		/// </summary>
		public GPSData StartLocation { get; set; }


		/// <summary>
		/// Finish point GPS data.
		/// </summary>
		public GPSData FinishLocation { get; set; }


		/// <summary>
		/// Number of software triggers.
		/// </summary>
		public int SoftwareTriggers { get; internal set; }


		/// <summary>
		/// Number of hardware triggers.
		/// </summary>
		public int HardwareTriggers { get; internal set; }


		/// <summary>
		/// Projects directory.
		/// </summary>
		public string ProjectsFolder { get; private set; }


		/// <summary>
		/// Pictures folder name.
		/// </summary>
		public string PicturesFolder { get; private set; }


		/// <summary>
		/// Kilometer of project start point.
		/// </summary>
		public int StartKilometer { get; private set; }


		/// <summary>
		/// Boolean that indicates wether our project is started or not.
		/// </summary>
		public bool Started { get; private set; }


		/// <summary>
		/// Start date and time of project.
		/// </summary>
		public DateTime StartTime { get; private set; }


		/// <summary>
		/// Finish date and time of project.
		/// </summary>
		public DateTime FinishTime { get; private set; }


		/// <summary>
		/// Starting lane of project.
		/// </summary>
		public int ProjectLane { get; private set; }


		private int availableCameraCount;


		/// <summary>
		/// Creates a project manager instance.
		/// </summary>
		/// <param name="applicationCore">Application's sender core.</param>
		public ProjectManager (Core applicationCore) => appCore = applicationCore;


		/// <summary>
		/// Initializes the Project Manager.
		/// </summary>
		public void InitializeManager () => appCore.Trigger.OnDeviceTrigger += OnTrigger;


		/// <summary>
		/// Renews the project timer, this is an extension method for TriggerDevice Protocols V3.
		/// </summary>
		public void RenewProjectTime () => StartTime = DateTime.Now;


		/// <summary>
		/// Starts a new Project.
		/// </summary>
		public void InitializeProject (string projectCode, string projectName, string startLocationName, 
			string finishLocationName, string cityName,
			string provinceCode, string provinceName,
			string direction,
			int laneCount, GPSData startPosition,
			int startKilometer, int caliber, int projectlane,
			string projectsFolder = "projects\\", string picturesFolder = "pictures\\")
		{

			/* Project: */
			Code = projectCode;
			Name = projectName;


			/* City & Province: */
			City = cityName;

			ProvinceCode = provinceCode;
			ProvinceName = provinceName;



			/* Locations: */
			StartName = startLocationName;
			FinishName = finishLocationName;

			StartLocation = startPosition;

			StartKilometer = startKilometer;

			Direction = direction;


			/* Lane & Caliber: */
			LaneCount = laneCount;

			ProjectLane = projectlane;

			Caliber = caliber;



			/* Folders: */
			ProjectsFolder = projectsFolder;

			PicturesFolder = picturesFolder;


			/* Codes. */
			Started = false;


			log.Info ("Initializing a new Project: " + projectName);


			try {
				if (!Directory.Exists (getPicturesFolder ()))
					Directory.CreateDirectory (getPicturesFolder ());
			} catch { }




			projectData = new List<ProjectData> ();
		}


		private string getPicturesFolder () => ProjectsFolder + PicturesFolder;


		/// <summary>
		/// Returns the camera folder depending on project.
		/// </summary>
		/// <param name="cam">Camera device.</param>
		/// <returns>Folder path.</returns>
		public string GetCameraFolder (Camera cam) => getPicturesFolder () + cam.SaveIndex + "\\";


		/// <summary>
		/// Starts the project.
		/// </summary>
		public void StartProject ()
		{
#if DEBUG
			sameTime = DateTime.Now;
			sameTimeCounter = 0;
#endif

			// Set available cameras to 0.
			availableCameraCount = 0;


			var devices = appCore.BaslerManager.GetManagedCameras ();

			// Create camera folders with camera informations.
			foreach (var device in devices) {

				// Skip unavailable camera devices.
				if (device.SaveRange <= 0)
					continue;

				// Re-set the camera variables.
				device._triggeredCounter = device._savedIndex = 0;


				availableCameraCount++;


				// Check if camera folder exists.
				if (!Directory.Exists (GetCameraFolder (device)))
					Directory.CreateDirectory (GetCameraFolder (device));



				using (var sw = new StreamWriter (getPicturesFolder () + device.SaveIndex + ".info")) {

					// Header.
					sw.WriteLine ("[Bifler Camera Device] © Ali Deym 2017");

					sw.WriteLine ();

					// Device Info.
					sw.WriteLine ("Index = " + device.Index);
					sw.WriteLine ("Name = " + device.Name);
					sw.WriteLine ("FullName = " + device.FullName);
					sw.WriteLine ("Tooltip = " + device.Tooltip);

				}

			}

			log.Info ("Started new project: " + Name);

			StartTime = DateTime.Now;

			Started = true;
		}

		/// <summary>
		/// Finishes the Project.
		/// </summary>
		public void FinishProject (XLSLanguage language)
		{
			log.Info ("Finished project: " + Name + ".");


			FinishTime = DateTime.Now;

			var cameras = appCore.BaslerManager.GetManagedCameras ();

			try {

				/* Get save interval from config, in-case of failure, use the defaults. */
				var saveInterval = 10;

				var confSaveInterval = appCore.MainConfigManager.GetConfig ("MapSaveInterval");

				if (confSaveInterval != null) {

					if (int.TryParse (confSaveInterval, out int interval)) {

						saveInterval = interval;

					}

				}
				else {
					// Save in config for future uses or configuration stuff.
					appCore.MainConfigManager.SetConfig ("MapSaveInterval", saveInterval.ToString ());
				}


				/* Save Map data. */
				var dataHandler = new MapDataHandler (Name, ProvinceName, City, StartName, FinishName, saveInterval);

				dataHandler.GenerateData (projectData);

				dataHandler.ProjectCode = Code;

				dataHandler.TriggerCount = SoftwareTriggers;

				dataHandler.ProjectStartTime = StartTime;
				dataHandler.ProjectFinishTime = FinishTime;

				dataHandler.HandleSave ();



				/* Excel application Interface. */
				var workBook = new XSSFWorkbook ();
				var sheet = workBook.CreateSheet ("MainSheet");


				sheet.IsRightToLeft = true;

				/* NOTE: This is indepentant of Excel table, this is just to create a double-D array. X is Row, Y is Cell here. */
				var CELL_X = 14;
				var CELL_Y = 2;


				var cellsData = new string [CELL_X, CELL_Y];

				var persianCal = new PersianCalendar ();

				var startTimeStr = persianCal.GetYear (StartTime) + "/" + persianCal.GetMonth (StartTime) +
					"/" + persianCal.GetDayOfMonth (StartTime) + " " +
					persianCal.GetHour (StartTime) + ":" + persianCal.GetMinute (StartTime) + ":" + persianCal.GetSecond (StartTime) + "." + persianCal.GetMilliseconds (StartTime);

				var finishTimeStr = persianCal.GetYear (FinishTime) + "/" + persianCal.GetMonth (FinishTime) +
					"/" + persianCal.GetDayOfMonth (FinishTime) + " " +
					persianCal.GetHour (FinishTime) + ":" + persianCal.GetMinute (FinishTime) + ":" + persianCal.GetSecond (FinishTime) + "." + persianCal.GetMilliseconds (FinishTime);



				/*****************/
				/* PART: Header. */
				/*****************/
				cellsData [0, 0] = language.ProjectCode;		cellsData [0, 1] = Code;
				cellsData [1, 0] = language.ProjectName;		cellsData [1, 1] = Name;
				cellsData [2, 0] = language.ProvinceCode;		cellsData [2, 1] = ProvinceCode;
				cellsData [3, 0] = language.ProvinceName;		cellsData [3, 1] = ProvinceName;
				cellsData [4, 0] = language.CityName;			cellsData [4, 1] = City;
				cellsData [5, 0] = language.StartArea;			cellsData [5, 1] = StartName;
				cellsData [6, 0] = language.FinishArea;			cellsData [6, 1] = FinishName;
				cellsData [7, 0] = language.GeoDirection;		cellsData [7, 1] = Direction;
				cellsData [8, 0] = language.LaneCount;			cellsData [8, 1] = LaneCount.ToString();
				cellsData [9, 0] = language.StartTime;			cellsData [9, 1] = startTimeStr;
				cellsData [10, 0] = language.FinishTime;		cellsData [10, 1] = finishTimeStr;
				cellsData [11, 0] = language.StartKilometer;	cellsData [11, 1] = StartKilometer.ToString ();
				cellsData [12, 0] = language.Caliber;			cellsData [12, 1] = Caliber.ToString ();
				cellsData [13, 0] = language.TriggerCount;		cellsData [13, 1] = SoftwareTriggers.ToString ();



				/************************/
				/* PART: FONT CREATION. */
				/************************/

				var fontName = appCore.MainConfigManager.GetConfig ("ExcelFont");
				var boldSizeStr = appCore.MainConfigManager.GetConfig ("ExcelBoldSize");
				var fontSizeStr = appCore.MainConfigManager.GetConfig ("ExcelFontSize");


				var fontSize = (short)12;
				var boldSize = (short)13;

				if (fontName == null || fontName == "")
					fontName = "B Nazanin";


				if (boldSizeStr != null && boldSizeStr != "") {

					if (int.TryParse(boldSizeStr, out int result)) {

						boldSize = (short)result;

					}

				}

				if (fontSizeStr != null && fontSizeStr != "") {

					if (int.TryParse (fontSizeStr, out int result)) {

						fontSize = (short)result;

					}

				}




				// Bold font.
				var boldFont = workBook.CreateFont ();

				boldFont.FontHeightInPoints = boldSize;
				boldFont.FontName = fontName;
				boldFont.Boldweight = (short)FontBoldWeight.Normal;
				((XSSFFont)boldFont).SetCharSet (FontCharset.ARABIC.Value);


				var boldStyle = workBook.CreateCellStyle ();

				boldStyle.SetFont (boldFont);

				// Bold with black background and white text:
				var headerFont = workBook.CreateFont ();


				headerFont.FontHeightInPoints = boldSize;
				headerFont.FontName = fontName;
				headerFont.Boldweight = (short)FontBoldWeight.Normal;
				headerFont.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
				((XSSFFont)headerFont).SetCharSet (FontCharset.ARABIC.Value);


				var headerStyle = workBook.CreateCellStyle ();

				headerStyle.SetFont (headerFont);
				headerStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
				headerStyle.FillPattern = FillPattern.SolidForeground;


				// Default font.
				var defFont = workBook.CreateFont ();

				defFont.FontHeightInPoints = fontSize;
				defFont.FontName = fontName;
				defFont.Boldweight = (short)FontBoldWeight.Normal;
				((XSSFFont)defFont).SetCharSet (FontCharset.ARABIC.Value);


				var defStyle = workBook.CreateCellStyle ();

				defStyle.SetFont (defFont);


				var defCenterStyle = workBook.CreateCellStyle ();

				defCenterStyle.SetFont (defFont);
				defCenterStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

				// Index, lon, lat, geoheight, altitude, datatime
				var GPSHeadCount = 5;

				// Get maximum cells length dependant on camera count.
				// 4 is for Range, Index, Lon, Lat. We add camera count into that.
				var maxWidth = GPSHeadCount + (appCore.BaslerManager.GetManagedCameras ().Length) - 1;

				// Header part for project information:
				var projInfoCell = sheet.CreateRow (0).CreateCell (0);

				projInfoCell.CellStyle = headerStyle;
				projInfoCell.SetCellValue (language.ProjectInformation + ":");

				sheet.AddMergedRegion (new NPOI.SS.Util.CellRangeAddress (0, 0, 0, maxWidth));



				for (var x = 1; x <= CELL_X; x++) {

					var row = sheet.CreateRow (x);


					for (var y = 0; y < CELL_Y; y++) {

						var cell = row.CreateCell (y);

						if (y == 0)
							cell.CellStyle = boldStyle;
						else {
							cell.CellStyle = defCenterStyle;

							// Centerize and merge the values so they would fit the header style.
							sheet.AddMergedRegion (new NPOI.SS.Util.CellRangeAddress (x, x, 1, maxWidth));
						}

						
						var text = cellsData [x - 1, y];

						if (text != null) {

							// Adds a ':' sign next to rows.
							cell.SetCellValue (text + (y == 0 ? (text == "" ? "" : ":") : ""));
							
						}

					}
				}


				sheet.AutoSizeColumn (0);
				sheet.AutoSizeColumn (1);


				// Offset after writing header.
				// CELL_X is not the index, it's the length. So If we add a number to it, It'll be n + 1. so CELL_X + 1 Is actually lastOffset + 2;
				var offset = CELL_X + 1;
				

				/*****************************/
				/* PART: CAMERA INFORMATION. */
				/*****************************/
				

				/* Get camera informations each into the table. */
				for (int i = 0; i < cameras.Length; i++) {

					var cam = cameras [i];

					// If the save range is zero, then skip this camera.
					if (cam.SaveRange <= 0) continue;


					// Header:
					var infoCell = sheet.CreateRow(offset).CreateCell (0);

					infoCell.CellStyle = headerStyle;
					infoCell.SetCellValue (language.CameraInformations + " " + cam.SaveIndex + ":");

					// Merge header for camera info.
					sheet.AddMergedRegion (new NPOI.SS.Util.CellRangeAddress (offset, offset, 0, maxWidth));

					offset++;

					var camData = new Tuple<string, string> [] {
						new Tuple<string, string> (language.Name, cam.Name),
						new Tuple<string, string> (language.FullName, cam.FullName),
						new Tuple<string, string> (language.Range, cam.SaveRange.ToString()),
						new Tuple<string, string> (language.Index, cam.Index.ToString())
					};

					for (int cx = 0; cx < camData.Length; cx++) {

						var cellRow = sheet.CreateRow (offset);

						// Key creation:
						var cellKey = cellRow.CreateCell (0);

						cellKey.CellStyle = boldStyle;
						cellKey.SetCellValue (camData [cx].Item1);


						var cellValue = cellRow.CreateCell (1);

						cellValue.CellStyle = defCenterStyle;
						cellValue.SetCellValue (camData [cx].Item2);

						sheet.AddMergedRegion (new NPOI.SS.Util.CellRangeAddress (offset, offset, 1, maxWidth));

						offset++;
					}
				}



				/*********************/
				/* PART: GPS Header. */
				/*********************/


				var gpsHeaderRow = sheet.CreateRow (offset);


				/* Get total available cameras which were on during the project. (uncount the disabled ones.) */
				var countAvailableCameras = 0;

				foreach (var cam in cameras) {

					if (cam.SaveRange > 0)
						countAvailableCameras++;

				}

				// Header data.
				

				var projHeaderData = new string [GPSHeadCount + countAvailableCameras];

				projHeaderData [0] = language.GPSIndex;
				projHeaderData [1] = language.Latitude;
				projHeaderData [2] = language.Longitude;
				projHeaderData [3] = language.Altitude;
				//projHeaderData [4] = language.GeoHeight;
				projHeaderData [4] = language.DataTime;

				int increment = 0;

				// Get a translator table.
				Dictionary<int, int> CellForCameraIndex = new Dictionary<int, int> ();


				/* Create header value in array for each camera. */
				foreach (var cam in cameras) {

					if (cam.SaveRange <= 0) continue;

					// Increase array offset.
					increment++;


					CellForCameraIndex [cam.SaveIndex] = increment + GPSHeadCount - 1;
					
					projHeaderData [GPSHeadCount - 1 + increment] = language.CameraPhotoIndex + " " + cam.SaveIndex;

				}


				/* Create GPS Headers. */
				for (var x = 0; x < projHeaderData.Length; x++) {
					var cell = gpsHeaderRow.CreateCell (x);

					cell.CellStyle = headerStyle;

					cell.SetCellValue (projHeaderData [x]);

				}



				// Increase offset after creating header.
				offset ++;


				foreach (var data in projectData) {
					var row = sheet.CreateRow (offset);


					for (int x = 0; x < GPSHeadCount; x++) {
						var cell = row.CreateCell (x);

						cell.CellStyle = defStyle;

						string strData = "";
						
						// Each number is a cell.
						switch (x + 1) {
							
							case 1: {
								strData = data.Index.ToString();

								cell.SetCellType (CellType.Numeric);
							}
							break;


							case 2: {
								strData = data.Latitude;
							}
							break;


							case 3: {
								strData = data.Longitude;
							}
							break;

							case 4: {
								strData = data.Altitude;
							}
							break;

							/*case 5: {
								strData = data.GeoHeight;
							}
							break;*/

							case 5: {
								strData = persianCal.GetMonth (data.DataTime) + "/" + persianCal.GetDayOfMonth (data.DataTime) + " " +
									persianCal.GetHour (data.DataTime) + ":" + persianCal.GetMinute (data.DataTime) + ":" + 
									persianCal.GetSecond (data.DataTime) + "." + persianCal.GetMilliseconds (data.DataTime);
							}
							break;
						}


						cell.SetCellValue (strData);
					}

					/* Insert each camera index into excel file. */
					foreach (var tuple in data.Images) {

						var cell = row.CreateCell (CellForCameraIndex [tuple.Item1]);

						
						cell.CellStyle = defStyle;

						cell.SetCellType (CellType.Numeric);
						cell.SetCellValue (tuple.Item2);

					}

					offset++;
				}

				// Autofit the camera headers.
				if (countAvailableCameras > 0) {
					
					// Camera headers start from GPS header.
					for (int i = GPSHeadCount; i <= GPSHeadCount + countAvailableCameras - 1; i++) {

						sheet.AutoSizeColumn (i);

					}

					for (int i = 0; i < GPSHeadCount; i++) {
						sheet.AutoSizeColumn (i);
					}

				}



				using (var file = File.Create (ProjectsFolder + "content.xlsx")) {

					workBook.Write (file);

				}


			} catch (Exception ex) {
				log.Fatal ("Error saving Excel file: " + ProjectsFolder + "project.xls");
				log.Fatal ("Exception: " + ex.ToString ());
			}

			Process.Start ("explorer.exe", ProjectsFolder);



			/* Sets project state into OFF/Finished. */
			/* This will also prepare the ProjectManager to be able to start a new Project. */
			Started = false;
		}

#if DEBUG
		private DateTime timeSinceLastBufferFull = DateTime.Now;
		private int failureCount = 0;
#endif

		/// <summary>
		/// Called whenever the device requests a trigger method.
		/// </summary>
		public void OnTrigger (int deviceTrig, int softwareTrig)
		{
			// Sometimes the trigger device is still counting up for some reason, but we need to stop it.
			if (!Started) {
				//appCore.Trigger.FinishTrigger ();

				return;
			}

#if DEBUG
			var oldTime = sameTime;
			sameTime = DateTime.Now;

			if (sameTime == oldTime)
				sameTimeCounter++;
#endif

			HardwareTriggers = deviceTrig;

			SoftwareTriggers = softwareTrig;


			

			var gpsInfo = appCore.GetGPSData ();

			//var availableCameraCount = 0;

			
			var tuple = new Tuple<int, uint> [availableCameraCount];

			var tupleIndex = 0;

			foreach (var cam in appCore.BaslerManager.GetManagedCameras ()) {

				if (cam.Disabled || cam.SaveRange <= 0) continue;

				tuple [tupleIndex] = new Tuple<int, uint> (cam.SaveIndex, cam._savedIndex);

				tupleIndex++;

			}


			projectData.Add (new ProjectData (softwareTrig, gpsInfo.Latitude, gpsInfo.Longitude, gpsInfo.Altitude, tuple));

/*#if DEBUG
			int totalTime = (int)((DateTime.Now - timeSinceLastBufferFull).TotalMilliseconds);

			if (totalTime >= 5000)
				Console.Clear ();

			if (totalTime <= 15// || totalTime >= 45) {
				//new System.Threading.Thread (() => { Console.Beep (500, 100); }).Start ();
				failureCount++;

				Console.WriteLine ("TIMED REQUEST: " + totalTime + "ms, " + SoftwareTriggers + " x" + failureCount + " FAILS.");
			}


			timeSinceLastBufferFull = DateTime.Now;
#endif
*/
			foreach (var cam in appCore.BaslerManager.GetManagedCameras ()) {

				if (cam.Disabled || cam.SaveRange <= 0) continue;

				// Skip the frames that we set the camera to skip.
				if (cam._startCounter > 0) {
					cam._startCounter--;

					continue;
				}


				cam._triggeredCounter++;

				if (cam._triggeredCounter >= cam.SaveRange) {
					cam._triggeredCounter = 0;
					cam._savedIndex++;

					cam.SaveImage (GetCameraFolder (cam) + cam._savedIndex + ".jpg");

				}

			}
		}

#if DEBUG
		public void TestTrig()
		{

			foreach (var cam in appCore.BaslerManager.GetManagedCameras ()) {

				if (cam.Disabled || cam.SaveRange <= 0) continue;

				// Skip the frames that we set the camera to skip.
				if (cam._startCounter > 0) {
					cam._startCounter--;

					continue;
				}


				cam._triggeredCounter++;

				if (cam._triggeredCounter >= cam.SaveRange) {
					cam._triggeredCounter = 0;
					cam._savedIndex++;

					cam.SaveImage (GetCameraFolder (cam) + cam._savedIndex + ".jpg");

					//System.Threading.Thread.Sleep (2);

				}

			}
		}
#endif
	}
}
