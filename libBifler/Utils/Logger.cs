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
using System.Timers;
using System.Collections.Generic;

namespace libBifler.Utils
{
	/// <summary>
	/// Logging handler object.
	/// </summary>
	[Obsolete]
	public class Logger
	{
		private List<string> logContent { get; set; }
		private Timer automationTimer = null;
		private readonly object _lock;


		private string instanceSeed = "0";
		private uint saveIndex = 0;
		
		

		/// <summary>
		/// Returns the last saved file path.
		/// </summary>
		public string LastSavedLogFile { get; private set; }


		/// <summary>
		/// Folder to create logs on.
		/// </summary>
		public string LogFolder { get; set; }


		/// <summary>
		/// Creates a logger instance.
		/// </summary>
		public Logger (string logFolder = "logs/")
		{
			_lock = new object ();


			if (!Directory.Exists (logFolder))
				Directory.CreateDirectory (logFolder);


			LogFolder = logFolder;

			logContent = new List<string> ();


			var rand = new Random ();

			rand.Next (50, 5000);


			// Generate a logger seed.
			instanceSeed = String.Format("{0}-{1}", 
				DateTime.Now.Ticks, rand.Next(50, 500) * rand.Next(10, 50));
		}


		/// <summary>
		/// Automates the logger saving process.
		/// </summary>
		/// <param name="time">Timer to save every x milliseconds.</param>
		public void Automate (int time)
		{
			automationTimer = new Timer (time);


			automationTimer.Elapsed += saveFlushFunction;
			automationTimer.Enabled = true;
		}


		private void saveFlushFunction (object sender, ElapsedEventArgs e)
		{
			Save ();
			Flush ();
		}


		/// <summary>
		/// Disables the automation proccess.
		/// </summary>
		public void Humanize ()
		{
			automationTimer.Enabled = false;
			automationTimer.Stop ();


			automationTimer.Dispose ();
			automationTimer = null;


			GC.Collect ();
		}


		/// <summary>
		/// Cleans up the stored logs.
		/// </summary>
		public void Flush () => logContent.Clear ();


		/// <summary>
		/// Log a content, adds timespan into the string.
		/// </summary>
		/// <param name="log">Log string.</param>
		public void WriteLog (string log)
		{
			/*lock (_lock) {
				logContent.Add (String.Format ("[{0}]: {1}", DateTime.Now.ToString ("HH:mm:ss.fff"), log));
			}*/
		}
		


		
		private string getFileID () => instanceSeed + "-" + saveIndex + ".log";




		/// <summary>
		/// Saves logs into specified folder.
		/// </summary>
		public void Save ()
		{
			// Stop saving process if the content is none and empty.
			if (logContent.Count <= 0) return;


			var workingFolder = LogFolder + DateTime.Now.ToString ("yyyy-MM-dd") + "/";

			if (!Directory.Exists (workingFolder))
				Directory.CreateDirectory (workingFolder);

			saveIndex++;


			lock (_lock) {
				File.WriteAllLines (workingFolder + getFileID (), logContent);
			}

			// Set the last saved file path variable.
			LastSavedLogFile = workingFolder + getFileID ();
		}
	}
}
