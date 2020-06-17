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
	public class HandshakePacket : Packet
	{
		/// <summary>
		/// Other device's version.
		/// </summary>
		public Version OtherVersion;

		private Version handshakeVersion;

		/// <summary>
		/// Creates a handshake packet handler.
		/// </summary>
		/// <param name="currentVersion">Current device version.</param>
		public HandshakePacket (Version currentVersion) 
			: base(OpCodes.Handshake, 3)
		{
			OtherVersion = new Version (0, 0, 0);

			handshakeVersion = currentVersion;
		}

		/// <summary>
		/// Handles reading version over network.
		/// </summary>
		public override void HandleRead ()
		{
			var x = ReadByte ();
			var y = ReadByte ();
			var z = ReadByte ();

			OtherVersion = new Version (x, y, z);

			ReadMethod?.Invoke (OtherVersion);
		}


		/// <summary>
		/// Handles writing our version into the network.
		/// </summary>
		public override void HandleWrite ()
		{
			WriteByte (handshakeVersion.X);
			WriteByte (handshakeVersion.Y);
			WriteByte (handshakeVersion.Z);
		}
	}
}
