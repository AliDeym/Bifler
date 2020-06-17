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

using libBifler.Project;
using libBifler.Utils.Structures;


using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace libBifler
{
	/// <summary>
	/// Main entry point and base of libBifler, by Ali Deym.
	/// </summary>
    public class Core : IDisposable
    {
		/// <summary>
		/// GPS Device.
		/// </summary>
		public GPS GPSDevice { get; private set; }


		/// <summary>
		/// Trigger Device.
		/// </summary>
		public TriggerDevice Trigger { get; private set; }


		/// <summary>
		/// Application name to be used on Core.
		/// </summary>
		public string AppName { get; private set; }

		
		/// <summary>
		/// Application version.
		/// </summary>
		public string AppVersion { get; private set; }


		/// <summary>
		/// Main logging object for the core.
		/// </summary>
		public static NLog.Logger MainLogger { get; private set; }


		/// <summary>
		/// Basler Camera manager.
		/// </summary>
		public Basler BaslerManager { get; private set; }


		/// <summary>
		/// Application's settings.
		/// </summary>
		public ConfigManager MainConfigManager { get; private set; }


		/// <summary>
		/// Application's main project handler.
		/// </summary>
		public ProjectManager Projects { get; private set; }



		/// <summary>
		/// Disposes the core on garbage collection.
		/// </summary>
		~Core ()
		{
			Dispose ();
		}


		/// <summary>
		/// Instantiates a Core object using the required parameters.
		/// </summary>
		/// <param name="name">Requester's Name.</param>
		/// <param name="version">Requester's Version.</param>
		public Core (string name, string version = "1.0.0b")
		{
			AppName = name;

			AppVersion = version;
		}


		/// <summary>
		/// Disposes the objects held by the Core.
		/// </summary>
		public void Dispose ()
		{
			BaslerManager = null;
		}


		/// <summary>
		/// Initializes the instance for this Core.
		/// </summary>
		public void Initialize()
		{
			string assemblyFolder = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
			NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration (assemblyFolder + "\\Utils\\NLog\\NLog.config", false);

			MainLogger = NLog.LogManager.GetCurrentClassLogger ();

			MainLogger.Info ("Initializing a new Core object for " + AppName + " (" + AppVersion + ")...");

			MainLogger.Info ("Automating the logger save proccess every 30s(30000ms).");
			//MainLogger.Automate (30000);


			MainLogger.Info ("Loading Application Configs...");

			MainConfigManager = new ConfigManager ("application");

			MainLogger.Info ("Configs successfully loaded.");


			MainLogger.Info ("Creating a Project manager...");

			Projects = new ProjectManager (this);


			MainLogger.Info ("Project manager successfully created.");


			MainLogger.Info ("Creating a Basler Manager...");

			BaslerManager = new Basler (Projects);

			MainLogger.Info ("Basler manager created successfully.");

		}


		/// <summary>
		/// External log from Application's UI.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="type">Log types</param>
		public void ExternalLog (string message, LogType type = LogType.Info)
		{
			if (type == LogType.Warn)
				MainLogger.Warn (message);
			else if (type == LogType.Error)
				MainLogger.Fatal (message);
			else
				MainLogger.Info (message);
		}


		/// <summary>
		/// Instantiates the GPS on this core.
		/// </summary>
		/// <param name="COMPort">GPS Device port. (default COM3).</param>
		public ErrorType InstantiateGPS (string COMPort = "COM3")
		{
			MainLogger.Info ("Creating a GPS object on Port " + COMPort + "...");
			GPSDevice = new GPS (COMPort);


			var gpsConnection = GPSDevice.TryConnect ();

			if (gpsConnection != ErrorType.None)
				MainLogger.Error ("Failed to connect GPS device. ERROR Type: " + Enum.GetName (typeof (ErrorType), gpsConnection));
			else
				MainLogger.Info ("GPS Successfully paired on Port " + COMPort + ".");


			return gpsConnection;
		}


		/// <summary>
		/// Instantiates the Trigger Device object.
		/// </summary>
		/// <param name="IPNumeric">Host IP's Network number (last number).</param>
		public void InstantiateTriggerDevice (string IPNumeric = "3")
		{
			MainLogger.Info ("Instantiating TriggerDevice object...");

			Trigger = new TriggerDevice ();

			Trigger.Initialize ();

			Trigger.SetPort ("192.168.1." + IPNumeric);

			Projects.InitializeManager ();
		}

		// TODO: Trigger network connected.
		/* This part of code is for Serial connected trigger device (Arduino) which is now deprecated.
		 * but in case if you want to re-try itjust uncomment the code below.
		/// <summary>
		/// Instantiates the Trigger on this core.
		/// </summary>
		/// <param name="COMPort">Trigger Device port. (default COM1).</param>
		public ErrorType InstantiateTriggerDevice (string COMPort = "COM1")
		{
			/*MainLogger.WriteLog ("Creating a TriggerDevice object on Port " + COMPort + "...");
			Trigger = new TriggerDevice (COMPort);


			var triggerConnection = Trigger.TryConnect ();

			if (triggerConnection != ErrorType.None)
				MainLogger.WriteLog ("Failed to connect Trigger device. ERROR Type: " + Enum.GetName (typeof (ErrorType), triggerConnection));
			else
				MainLogger.WriteLog ("Trigger Successfully paired on Port " + COMPort + ".");


			// Init projects manager after trigger device instantiates.
			Projects.InitializeManager ();
			
			// TODO: Write Networked Trigger device.
			return triggerConnection;
		}
		*/


		/// <summary>
		/// Gets latest GPS Data.
		/// </summary>
		/// <returns>NMEA GPS Structure.</returns>
		public GPSData GetGPSData () => GPSDevice.GetData ();

    }
}
