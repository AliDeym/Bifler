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


namespace TriggerServer.Packets
{
	public class HeartbeatRequestPacket : Packet
	{
		private NetworkManager manager;

		public const byte MAGIC_NUMBER = 0x86;

		/// <summary>
		/// Creates a heartbeat request packet handler. you should Listen(ReadMethod) only on TriggerDevice.
		/// </summary>
		public HeartbeatRequestPacket (NetworkManager sender) 
			: base(OpCodes.HeartbeatRequest, 1)
		{
			manager = sender;
		}

		/// <summary>
		/// Handles reading heartbeats.
		/// </summary>
		public override void HandleRead ()
		{
			var heartBeat = ReadByte ();

			ReadMethod?.Invoke (heartBeat);

			manager.WritePacket (OpCodes.HeartbeatTransmit);
		}


		/// <summary>
		/// Handles writing our version into the network. use this only as Master (in this case, PC).
		/// </summary>
		public override void HandleWrite ()
		{
			WriteByte (MAGIC_NUMBER);
		}
	}
}
