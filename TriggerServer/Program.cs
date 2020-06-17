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
using System.Threading;

using DotLog;


using RaspberryPiDotNet;

namespace TriggerServer
{
	partial class Program
	{
		/* Statics. */
		static readonly Version triggerDeviceVersion = new Version (4, 21, 60);
		static readonly string IPAddress = "0.0.0.0";


		/* LED Timer. */
		static DateTime LEDTimer = DateTime.Now;
		static bool LED_On = false;

		// Default Port.
		static GPIOPins interruptPin = GPIOPins.GPIO_17;
		static GPIOPins flashPin = GPIOPins.GPIO_21;
		static GPIOPins ledPin = GPIOPins.GPIO_23;


		// Logger.
		static FileLogger logger;


		/* Network managers */
		static NetworkManager radioManager = null;
		static NetworkManager netManager = null;


		/* GPIO & Interrupt */
		static PinState lastInterruptState;
		static GPSHandler gpsDevice;
		static GPIOMem flash = null;
		static GPIOMem pin = null;
		static GPIOMem led = null;

		static GPIOMem[] list = null;



		static Thread interruptThread;


		/* Variables: */
		static int triggerCount = 0;
		static int caliberNumber = 1;
		static int pulses = 0;
		

		static bool projectStarted = false;
		static bool calibrating = false;
		

		/// <summary>
		/// Writes into the console in a developer friendly timed format, also logs the content.
		/// </summary>
		/// <param name="str">Message.</param>
		static void WriteLine (string str)
		{
			Console.WriteLine ("[" + DateTime.Now.ToString ("hh:mm:ss.ffff") + "] " + str);

			if (String.IsNullOrWhiteSpace (str))
				return;


			//logger.Log (str);
		}


		/// <summary>
		/// Writes into console but doesn't log it.
		/// </summary>
		/// <param name="str">Message.</param>
		static void WriteLineNoLog (string str)
		{
			Console.WriteLine ("[" + DateTime.Now.ToString ("hh:mm:ss.ffff") + "] " + str);
		}


		static void Main (string [] args)
		{
			/*string assemblyFolder = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
			NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration (assemblyFolder + "/NLog/NLog.config", true);*/




			Console.Title = "Trigger Device By Ali Deym and Biftor (C) 2017-2018";


			WriteLineNoLog ("Creating logger instance...");

			logger = new FileLogger () {
				FileNameTemplate = "main-########.log"
			};


			WriteLine ("Logger instance created.");


			WriteLine ("Trigger device (C) 2017 - 2018 by:");
			WriteLine ("Ali Deym, Biftor (Parham Abedi).");
			WriteLine ("");
			WriteLine ("");
			WriteLine ("Initializing device...");

			WriteLine ("");
			WriteLine ("");





			WriteLine ("Trigger Program v" + triggerDeviceVersion.X + "." + triggerDeviceVersion.Y + "." + triggerDeviceVersion.Z);

			WriteLine ("");
			WriteLine ("Device info: ");


			var OS = Environment.OSVersion;

			WriteLine ("Device: " + Environment.MachineName);
			WriteLine ("Username: " + Environment.UserName);
			WriteLine ("OS: " + OS.VersionString + " (" + Enum.GetName (typeof (PlatformID), OS.Platform) + ")");
			WriteLine ("CPU Cores: " + Environment.ProcessorCount + " Cores.");


			WriteLine ("");
			WriteLine ("");

			WriteLine ("Checking for GPIO config...");


			try {
				if (File.Exists (".gpio")) {
					var content = File.ReadAllText (".gpio").Trim ();

					//if (int.TryParse (content, out int result)) {

						if (Enum.TryParse (content, out GPIOPins configuredPin)) {

							interruptPin = configuredPin;

							WriteLine ("Changing GPIO pin to: " + content + ".");

						}

					//}
				} else {
					WriteLine ("Creating default GPIO configuration...");

					File.WriteAllText (".gpio", "GPIO_17");


				}

				if (File.Exists (".ledgpio")) {
					var content = File.ReadAllText (".ledgpio").Trim ();


					if (Enum.TryParse (content, out GPIOPins configuredPin)) {

						ledPin = configuredPin;

						WriteLine ("Changing LED GPIO pin to: " + content + ".");

					}

				} else {
					WriteLine ("Creating default LED GPIO configuration...");

					File.WriteAllText (".ledgpio", "GPIO_23");
				}


				if (File.Exists (".flashgpio")) {
					var content = File.ReadAllText (".flashgpio").Trim ();


					if (Enum.TryParse (content, out GPIOPins configuredPin)) {

						flashPin = configuredPin;

						WriteLine ("Changing Flash GPIO pin to: " + content + ".");

					}

				}
				else {
					WriteLine ("Creating default Flash GPIO configuration...");

					File.WriteAllText (".flashgpio", "GPIO_21");
				}
			} catch (Exception ex) {
				WriteLine ("GPIO config ERROR: " + ex.ToString ());
			}

			WriteLine ("Initializing GPIO Pin: " + Enum.GetName (typeof(GPIOPins), interruptPin) + "...");


			LEDTimer = DateTime.Now;

			/* Initialize GPIO Pin: */
			pin = new GPIOMem (interruptPin, GPIODirection.In);

			flash = new GPIOMem (flashPin, GPIODirection.Out);
			led = new GPIOMem (ledPin, GPIODirection.Out);

			lastInterruptState = PinState.High;


			interruptThread = new Thread (interruptCheck);

			interruptThread.Priority = ThreadPriority.AboveNormal;




			/* Network manager */
			WriteLine ("Listening network on IP: (" + IPAddress + ")");



			/* Bifler listener */
			WriteLine ("Starting libBifler listener...");

			netManager = new NetworkManager (IPAddress, true);

			netManager.Initialize (triggerDeviceVersion);

			WriteLine ("Successfully listening libBifler.");



			/* Radio listener */
			WriteLine ("Starting radio listener...");

			radioManager = new NetworkManager (IPAddress, true, true);

			radioManager.Initialize (triggerDeviceVersion);

			WriteLine ("Successfully listening radio.");


			WriteLine ("Registering methods...");

			/* Register netManager packet's read methods into their origins. */
			RegisterMethods ();
			RegisterRadioMethods ();


			WriteLine ("Started listening network successfully.");



			WriteLine ("Initializing GPS Device...");


			/*var ports = SerialPort.GetPortNames ();
			WriteLine ("COM Ports (" + ports.Length + "): ");

			foreach (var port in ports) {
				WriteLine ("\t" + port);
			}


			if (ports.Length == 1) {
				WriteLine ("Creating GPS device on Port: " + ports [0] + "...");

				gpsDevice = new GPSHandler (ports [0], sendGPSData);

				WriteLine ("SerialPort running for GPS.");
			}
            */

			/* Run GPIO Scheduler. */

			WriteLine ("Starting GPIO Interrupt.");
			interruptThread.Start ();


		}


		/* Handles GPS data sending over ether-Net. */
		static void sendGPSData (float latitude, float longitude, float altitude)
		{
			if (latitude == 0f && latitude == 0f && altitude == 0f) return;

			WriteLine ("GPS: " + latitude + longitude + altitude);
		}



		/// <summary>
		/// Called for checking interrupts on specified pin.
		/// </summary>
		static void interruptCheck ()
		{
			while (true) {

				if ((DateTime.Now - LEDTimer).TotalSeconds > 3) {
					LED_On = !LED_On;

					led.Write (LED_On);

					LEDTimer = DateTime.Now;
				}

				var pinState = pin.Read ();

				if (lastInterruptState != pinState) {
					lastInterruptState = pinState;

					Interrupt ();
				}

				Thread.Sleep (1);

				/*Interrupt ();
				Thread.Sleep (20);*/
			}
		}


		/// <summary>
		/// Writes a packet into both radio and library streams.
		/// </summary>
		/// <param name="opCode">OpCode</param>
		/// <param name="data">Data</param>
		static void WriteMultidimensionalPacket (OpCodes opCode, object data)
		{
			netManager.WritePacket (opCode, data);
			radioManager.WritePacket (opCode, data);
		}


		/// <summary>
		/// Called whenever pin state goes from High to Low or vice-versa.
		/// </summary>
		static void Interrupt ()
		{
			/* Prevent from maximum size collision. */
			if (triggerCount >= Int32.MaxValue - 1024) {
				triggerCount = 0;
			}

			if (pulses >= Int32.MaxValue - 1024) {
				pulses = 0;
			}


			/* Calibration. */
			if (calibrating) {

				triggerCount++;

				WriteMultidimensionalPacket (OpCodes.CaliberTransmit, triggerCount);

			} else if (projectStarted) {

				/* Preform no caliber (single caliber) triggers and pulses. */
				if (caliberNumber <= 1) {
					triggerCount++;

					
					new Thread (() => {

						flash.Write (true);

						Thread.Sleep (1);

						flash.Write (false);

					}).Start ();

					WriteMultidimensionalPacket (OpCodes.Trigger, triggerCount);


					/* Display console information every 500 trigs. */
					if (triggerCount % 500 == 0)
						WriteLineNoLog ("Performing trigger: " + triggerCount.ToString () + ".");
				}
				else {
					/* Calculate pulses and trigger, then decide to send it or not. */
					pulses++;

					if (pulses >= caliberNumber) {

						triggerCount++;
						pulses = 0;


						new Thread (() => {

							flash.Write (true);

							Thread.Sleep (1);

							flash.Write (false);

						}).Start ();

						WriteMultidimensionalPacket (OpCodes.Trigger, triggerCount);


						/* Display console information every 500 trigs. */
						if (triggerCount % 500 == 0)
							WriteLineNoLog ("Performing trigger: " + triggerCount.ToString () + ".");
					}
				}
			}
		}
	}
}
