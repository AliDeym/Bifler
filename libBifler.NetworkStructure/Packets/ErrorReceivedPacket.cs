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
	public class ErrorReceivedPacket : Packet
	{
		/// <summary>
		/// Creates a project pending packet handler.
		/// </summary>
		public ErrorReceivedPacket () 
			: base(OpCodes.ErrorReceived, 1)
		{
		}

		/// <summary>
		/// Handles an error message packets.
		/// </summary>
		public override void HandleRead ()
		{
			var errorCode = ReadByte ();


			ReadMethod?.Invoke (errorCode);
		}


		/// <summary>
		/// DO NOT Use, We cannot have unparameterized error packets.
		/// </summary>
		public override void HandleWrite ()
		{
			throw new NotImplementedException ("ErrorReceived must have error code parameter.");
		}


		/// <summary>
		/// Writes error code into the network.
		/// </summary>
		/// <param name="data">Error Code, Boxed Byte.</param>
		/// <returns></returns>
		public override bool HandleParameterizedWrite (object data)
		{
			WriteByte ((byte)data);

			return true;
		}
	}
}
