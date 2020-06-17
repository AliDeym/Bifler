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
	public class TriggerPacket : Packet
	{
		/// <summary>
		/// Creates a trigger packet handler.
		/// </summary>
		public TriggerPacket () 
			: base(OpCodes.Trigger, 4)
		{
		}

		/// <summary>
		/// Handles reading version over network.
		/// </summary>
		public override void HandleRead ()
		{
			var trig = ReadInt32 ();

			ReadMethod?.Invoke (trig);
		}


		/// <summary>
		/// Do NOT use this function.
		/// </summary>
		public override void HandleWrite ()
		{
			throw new NotImplementedException ("Trigger cannot be without arguments.");
		}

		/// <summary>
		/// Writes current trigger counter into the network. NOTE: Send trigger packets if you want to set calibration on TriggerDevice.
		/// </summary>
		/// <param name="data">Int32 | Caliber in PC and TrigCount in TriggerDevice.</param>
		/// <returns></returns>
		public override bool HandleParameterizedWrite (object data)
		{
			WriteInt32 ((int)data);

			return true;
		}
	}
}
