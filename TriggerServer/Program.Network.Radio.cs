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
		/// Registers receive methods for radio.
		/// </summary>
		static void RegisterRadioMethods ()
		{
			/* Device handshake read. */
			radioManager.GetPacket (OpCodes.Handshake).ReadMethod = ReadRadioHandshake;

			radioManager.GetPacket (OpCodes.PendingRequest).ReadMethod = ReadRadioPendingAnswer;
		}


		/// <summary>
		/// Reads pending project from radio and redirects to libBifler
		/// </summary>
		/// <param name="magicBoxed">Boxed Byte.</param>
		static void ReadRadioPendingAnswer (object magicBoxed)
		{
			netManager.WritePacket (OpCodes.PendingRequest);
		}


		/// <summary>
		/// Handles Radio Handshake packet read.
		/// </summary>
		/// <param name="versionBoxed">Boxed version(struct).</param>
		static void ReadRadioHandshake (object versionBoxed)
		{
			var version = (Version)versionBoxed;

			WriteLine ("Handshake with radio successful, radio version: " + version.X + "." + version.Y + "." + version.Z);

			// Send back the hand shakes.
			radioManager.WritePacket (OpCodes.Handshake);
		}

	}
}
