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


namespace libBifler.NetworkStructure.Packets
{
	public class GPSDataPacket : Packet
	{
		/// <summary>
		/// Creates a heartbeat transmit packet handler. you should Listen(ReadMethod) only on PC/Windows device.
		/// </summary>
		public GPSDataPacket () 
			: base(OpCodes.HeartbeatTransmit, 12)
		{
		}

		/// <summary>
		/// Handles reading heartbeats.
		/// </summary>
		public override void HandleRead ()
		{
			var lon = ReadFloat ();
			var lat = ReadFloat ();
			var alt = ReadFloat ();


		}


		/// <summary>
		/// Handles writing magic number as heartbeat packet.
		/// </summary>
		public override void HandleWrite ()
		{
			throw new System.FormatException ("Invalid Writing packet for GPSDataPacket.");
		}


		public override bool HandleParameterizedWrite (object data)
		{
			var numbers = ((float[]) data);

			WriteFloat (numbers [0]);
			WriteFloat (numbers [1]);
			WriteFloat (numbers [2]);


			return true;
		}
	}
}
