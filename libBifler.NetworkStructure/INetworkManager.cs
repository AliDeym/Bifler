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
using System.Collections.Generic;
using System.Text;

namespace libBifler.NetworkStructure
{
    public interface INetworkManager
    {
		/// <summary>
		/// Writes a packet into the network manager using default arguments.
		/// </summary>
		/// <param name="opcode">OpCode.</param>
		void WritePacket (OpCodes opcode);

		/// <summary>
		/// Writes a parameterized data into network manager.
		/// </summary>
		/// <param name="opcode">Packet opcode.</param>
		/// <param name="data">Data.</param>
		void WritePacket (OpCodes opcode, object data);
    }
}
