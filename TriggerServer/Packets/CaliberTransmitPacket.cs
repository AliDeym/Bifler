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

namespace TriggerServer.Packets
{
	public class CaliberTransmitPacket : Packet
	{
		/// <summary>
		/// Creates a caliber packet handler.
		/// </summary>
		public CaliberTransmitPacket () 
			: base(OpCodes.CaliberTransmit, 4)
		{
		}

		/// <summary>
		/// Handles reading caliber packets.
		/// </summary>
		public override void HandleRead ()
		{
			var caliberCount = ReadInt32 ();


			ReadMethod?.Invoke (caliberCount);
		}


		/// <summary>
		/// Handles writing caliber packets. This should not be used AT ALL.
		/// </summary>
		public override void HandleWrite ()
		{
			throw new NotImplementedException ("Caliber cannot be without parameters.");
		}


		/// <summary>
		/// Sends caliber count over the network. 
		/// </summary>
		/// <param name="data">Only has one parameter which is an Int32, the caliber count.</param>
		/// <returns>Turn function on.</returns>
		public override bool HandleParameterizedWrite (object data)
		{
			WriteInt32 ((int)data);

			return true;
		}
	}
}
