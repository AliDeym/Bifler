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


using TriggerServer;

namespace Radio
{
	public partial class RadioCore
	{


		/// <summary>
		/// Instantiates the read methods.
		/// </summary>
		private void ReadMethods ()
		{
			radioManager.GetPacket (OpCodes.Handshake).ReadMethod = HandleHandshake;
			radioManager.GetPacket (OpCodes.Trigger).ReadMethod = HandleTriggerPacket;

			radioManager.GetPacket (OpCodes.CaliberRequest).ReadMethod = HandleCaliberRequest;
			radioManager.GetPacket (OpCodes.ProjectRequest).ReadMethod = HandleProjectRequest;

			radioManager.GetPacket (OpCodes.CaliberTransmit).ReadMethod = HandleCaliberTransmit;

			radioManager.GetPacket (OpCodes.PendingRequest).ReadMethod = HandlePendingRequest;

			radioManager.GetPacket (OpCodes.ErrorReceived).ReadMethod = HandleErrorReceived;
		}


		/// <summary>
		/// Handles receiving errors from libBifler.
		/// </summary>
		/// <param name="boxedErrorCode">Boxed Byte.</param>
		static void HandleErrorReceived (object boxedErrorCode)
		{
			// TODO: Handle errors.
		}

		/// <summary>
		/// Handles project pending startup packet receive.
		/// </summary>
		/// <param name="boxedMagic">Boxed Byte.</param>
		private void HandlePendingRequest (object boxedMagic)
		{
			// TODO: Change label's visiblity and also the new button's.
			mainPage.PrepareProject ();
		}

		/// <summary>
		/// Handles caliber transmit packets.
		/// </summary>
		/// <param name="boxedNum">Boxed Int 32.</param>
		private void HandleCaliberTransmit (object boxedNum)
		{
			int num = (int)boxedNum;

			if (num <= 0)
				return;

			HandleTriggerPacket (boxedNum);
		}

		/// <summary>
		/// Handles project request packets.
		/// </summary>
		/// <param name="reqBoxed">Boxed boolean.</param>
		private void HandleProjectRequest (object reqBoxed)
		{
			bool request = (bool)reqBoxed;

			mainPage.ProjectStatusChanged (request);
		}

		/// <summary>
		/// Handles caliber request packets.
		/// </summary>
		/// <param name="reqBoxed">Boxed boolean.</param>
		private void HandleCaliberRequest (object reqBoxed)
		{
			bool request = (bool)reqBoxed;

			mainPage.CaliberStatusChanged (request);
		}

		/// <summary>
		/// Handles handshake packets.
		/// </summary>
		/// <param name="boxedVersion">Boxed Version(struct).</param>
		private void HandleHandshake (object boxedVersion)
		{
			var version = (TriggerServer.Version)boxedVersion;
		}

		/// <summary>
		/// Reads trigger packets.
		/// </summary>
		/// <param name="num">Trigger counter.</param>
		private void HandleTriggerPacket (object num)
		{
			Triggers = (int)num;

			var numberStr = "";

			var kmCounter = "000";
			var mmCounter = "000";

			float meters = Triggers;
			float kiloMeters = 0;


			if (meters >= 1000) {

				kiloMeters = meters / 1000;
				meters = meters % 1000;

			}


			// Is-1 Digit:
			if (meters < 10) {
				mmCounter = "00" + ((int)meters);
			}
			// Is-2 digits:
			else if (meters < 100) {
				mmCounter = "0" + ((int)meters);
			}
			// Is-3 digits and not zero.
			else if (meters > 0) {
				mmCounter = ((int)meters).ToString ();
			}


			if (kiloMeters < 10) {
				kmCounter = "00" + ((int)kiloMeters);
			}
			else if (kiloMeters < 100) {
				kmCounter = "0" + ((int)kiloMeters);
			}
			else if (kiloMeters > 0) {
				kmCounter = ((int)kiloMeters).ToString ();
			}

			numberStr = kmCounter + "  +  " + mmCounter;

			mainPage.ChangeTriggerCounter (numberStr);
		}
	}
}
