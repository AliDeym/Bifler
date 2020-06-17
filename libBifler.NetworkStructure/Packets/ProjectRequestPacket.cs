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

namespace libBifler.NetworkStructure.Packets
{
	public class ProjectRequestPacket : Packet
	{
		/// <summary>
		/// Creates a project packet handler.
		/// </summary>
		public ProjectRequestPacket () 
			: base(OpCodes.ProjectRequest, 1)
		{
		}

		/// <summary>
		/// Handles reading project packets.
		/// </summary>
		public override void HandleRead ()
		{
			var shouldStart = ReadBoolean ();


			ReadMethod?.Invoke (shouldStart);
		}


		/// <summary>
		/// Do not use this method at ALL.
		/// </summary>
		public override void HandleWrite ()
		{
			throw new NotImplementedException ("Project status cannot be without parameters.");
		}


		/// <summary>
		/// Sends project request packet, send true for starting project, and false for stopping. this is same on both devices.
		/// </summary>
		/// <param name="data">Boolean | Wether to start or not the project. Send same value on TriggerDevice for pinging.</param>
		/// <returns>Turn function on.</returns>
		public override bool HandleParameterizedWrite (object data)
		{
			WriteBoolean ((bool)data);

			return true;
		}
	}
}
