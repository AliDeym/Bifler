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

namespace TriggerServer
{
	partial class Program
	{
		/// <summary>
		/// Registers receive methods for each packet manager.
		/// </summary>
		static void RegisterMethods ()
		{
			/* Calibration read from PC. */
			netManager.GetPacket (OpCodes.Trigger).ReadMethod = ReadCaliber;

			/* Project start or stop. */
			netManager.GetPacket (OpCodes.ProjectRequest).ReadMethod = ReadProjectRequest;

			/* Device handshake read. */
			netManager.GetPacket (OpCodes.Handshake).ReadMethod = ReadHandshake;

			/* Request caliber start/stop. */
			netManager.GetPacket (OpCodes.CaliberRequest).ReadMethod = ReadCaliberRequest;


			/* Request project start/pending. */
			netManager.GetPacket (OpCodes.PendingRequest).ReadMethod = ReadPendingRequest;


			/* Error received packet. */
			netManager.GetPacket (OpCodes.ErrorReceived).ReadMethod = ReadErrorReceived;
		}


		/// <summary>
		/// Handles redirecting libBifler's errors into radio.
		/// </summary>
		/// <param name="errorCodeBoxed">Boxed Byte.</param>
		static void ReadErrorReceived (object errorCodeBoxed)
		{
			radioManager.WritePacket (OpCodes.ErrorReceived, errorCodeBoxed);
		}


		/// <summary>
		/// Handles reading pending project request packets.
		/// </summary>
		/// <param name="magicNumberBoxed">Boxed magic byte.</param>
		static void ReadPendingRequest (object magicNumberBoxed)
		{
			// We cannot use the Magic Number for sending, but we can just only and only receive it.
			// So we can do checks for versioning later here, but currently, they're all using the same standards.
			// This packet is kinda special, It's only here so TriggerDevice redirects the packet to the RadioDevice.
			// Technically, (currently) the only packet that is just there for redirecting.
			radioManager.WritePacket (OpCodes.PendingRequest);
		}

		/// <summary>
		/// Handles reading caliber request packets.
		/// </summary>
		/// <param name="requestBoxed">Boxed boolean</param>
		static void ReadCaliberRequest (object requestBoxed)
		{
			var shouldStartCalibrate = (bool)requestBoxed;


			WriteMultidimensionalPacket (OpCodes.CaliberRequest, shouldStartCalibrate);


			WriteLine ("Calibrate request: " + shouldStartCalibrate + ".");


			if (shouldStartCalibrate == false) {
				caliberNumber = triggerCount;

				if (caliberNumber <= 0)
					caliberNumber = 1;

				WriteMultidimensionalPacket (OpCodes.CaliberTransmit, -1);
			}

			triggerCount = 0;
			pulses = 0;

			calibrating = shouldStartCalibrate;
		}

		/// <summary>
		/// Handles Handshake packet read.
		/// </summary>
		/// <param name="versionBoxed">Boxed version(struct).</param>
		static void ReadHandshake (object versionBoxed)
		{
			var version = (Version)versionBoxed;

			WriteLine ("Handshake with partner successful, partner version: " + version.X + "." + version.Y + "." + version.Z);

			// Send back the hand shakes.
			netManager.WritePacket (OpCodes.Handshake);
		}

		/// <summary>
		/// Reads caliber number from Master.
		/// </summary>
		/// <param name="num">Boxed integer(32).</param>
		static void ReadCaliber (object num)
		{
			caliberNumber = (int)num;

			if (caliberNumber <= 0) {
				WriteLine ("Caliber auto-reset to 97 because it was set to 0.");

				caliberNumber = 97;
			}

			WriteLine ("Calibration number received: " + caliberNumber + ".");
			// Suprisingly, the only method which doesn't ping anything back.
		}

		/// <summary>
		/// Reads project status, and determines wether to start project or stop it.
		/// </summary>
		/// <param name="status">Boxed boolean.</param>
		static void ReadProjectRequest (object status)
		{
			var statusBool = (bool)status;

			projectStarted = statusBool;


			if (!statusBool) {
				WriteLine ("Finalizating the trigger counting with: " + triggerCount + ".");
			}

			triggerCount = 0;
			pulses = 0;


			WriteLine ("Project status received: " + statusBool + ".");

			WriteMultidimensionalPacket (OpCodes.ProjectRequest, status);

			
		}


	}
}
