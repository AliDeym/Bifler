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
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;


using TriggerServer.Packets;


namespace TriggerServer
{
	public class NetworkManager
	{
		/// <summary>
		/// Server or Client(listener) IP Address.
		/// </summary>
		public System.Net.IPAddress IPAddress { get; set; }


		/// <summary>
		/// Wether current manager is host (listening) or not.
		/// </summary>
		public bool IsHost { get; private set; }


		/// <summary>
		/// Wether current manager is for radio purposes or not.
		/// </summary>
		public bool IsRadio { get; private set; }


		/// <summary>
		/// Clients are connected or not.
		/// </summary>
		public bool IsConnected {
			get {
				if (client == null) return false;
				if (stream == null) return false;

				return client.Connected;
			}
		}


		/* Packets size. */
		private const int PACKET_SIZE = 64;
		/* Const port. */
		private const int PORT = 1999;
		private const int PORT_RADIO = 1998;
		/* Timeout reconnect delay in MS. */
		private const int TIMEOUT = 3000;



		private TcpClient	client;
		private TcpListener server;

		private NetworkStream stream;
		private Dictionary<byte, Packet> packets;


		private Task mainThread;

		


		/// <summary>
		/// Creates a network manager class, which manages listens or data receives and or writes.
		/// </summary>
		/// <param name="IP">Server or listener's IP Address.</param>
		/// <param name="listen">Wether we are listening or not.</param>
		/// <param name="isRadio">Wether we are using radio port or not. </param>
		public NetworkManager (string IP, bool listen = false, bool isRadio = false)
		{
			IPAddress = System.Net.IPAddress.Parse(IP);

			IsHost = listen;

			IsRadio = isRadio;
		}


		/// <summary>
		/// Retrieves a packet from stored handlers (using their OpCode).
		/// </summary>
		/// <param name="code">OpCode | Byte</param>
		/// <returns></returns>
		public Packet GetPacket (OpCodes code)
		{
			if (packets.ContainsKey ((byte)code))
				return packets [(byte)code];

			return null;
		}


		/// <summary>
		/// Initializes the manager.
		/// </summary>
		/// <param name="deviceVersion">Current device version (software/firmware).</param>
		/// <param name="isRadio">Determines wether the current manager is for Radio or not.</param>
		public async void Initialize (Version deviceVersion)
		{
			packets = new Dictionary<byte, Packet> () {
				/* Handshake */
				{ (byte)OpCodes.Handshake, new HandshakePacket (deviceVersion) },

				/* Heartbeat */
				{ (byte)OpCodes.HeartbeatRequest, new HeartbeatRequestPacket (this) },
				{ (byte)OpCodes.HeartbeatTransmit, new HeartbeatTransmitPacket () },

				/* Caliber */
				{ (byte)OpCodes.CaliberRequest, new CaliberRequestPacket () },
				{ (byte)OpCodes.CaliberTransmit, new CaliberTransmitPacket () },

				/* Project start & trigger */
				{ (byte)OpCodes.ProjectRequest, new ProjectRequestPacket () },
				{ (byte)OpCodes.Trigger, new TriggerPacket () },


				/* V3 Packet. */
				{ (byte)OpCodes.PendingRequest, new PendingRequestPacket () },
				{ (byte)OpCodes.ErrorReceived, new ErrorReceivedPacket () }
			};

			/*if (IsHost) {
				server = new TcpListener (IPAddress, IsRadio ? PORT_RADIO : PORT);


				//try {
					server.Start ();

					server.Server.NoDelay = true;
					
					server.Server.ReceiveBufferSize = server.Server.SendBufferSize = PACKET_SIZE;

					

					mainThread = new Task (ServerReceiveLoop);


				//} catch {

					//Environment.Exit (71);
				//}
			} else {*/
				client = new TcpClient ();

				client.NoDelay = true;

				client.SendBufferSize = PACKET_SIZE;

			//mainThread = new Task (ClientReceiveLoop);

			Task.Factory.StartNew (ClientReceiveLoop);
			//}



			//mainThread.Priority = ThreadPriority.AboveNormal;

			//mainThread.Start ();

		}


		/// <summary>
		/// Reconnects the client in-case it is already connected, does nothing if not connected already.
		/// </summary>
		public void Reconnect ()
		{
			if (client == null) return;

			//if (asyncClientConnection != null)
			//client.EndConnect (asyncClientConnection);

			//stream = null;

			//client.Close ();

			client = new TcpClient ();

			client.NoDelay = true;

			client.SendBufferSize = PACKET_SIZE;

			if (client.Client != null)
				client.ReceiveBufferSize = client.SendBufferSize;
			//client.Close ();
		}

		/// <summary>
		/// Writes the specified packet into the network.
		/// </summary>
		/// <param name="opCode">OpCode of the specified Packet.</param>
		public void WritePacket (OpCodes opCode)
		{
			var packet = packets [(byte)opCode];

			packet.HandleWrite ();

			packet.SendData (stream);
		}

		/// <summary>
		/// Writes a parameterized packet into the network.
		/// </summary>
		/// <param name="opCode">OpCode of the specified Packet.</param>
		/// <param name="data">Data to write on stream.</param>
		public void WritePacket (OpCodes opCode, object data)
		{
			var packet = packets [(byte)opCode];

			if (!packet.HandleParameterizedWrite (data)) return;

			packet.SendData (stream);
		}


		/* Awaits for stream to receive a packet, then handles that specified packet. */
		private void AwaitStream ()
		{
			if (stream == null) {
				//Thread.Sleep (TIMEOUT);

				return;
			}


			byte opCode;

			try {
				opCode = (byte)stream.ReadByte ();
			} catch {
				return;
			}

			// Close response sent from .NET Framework or Mono Framework.
			if (opCode == 255) {

				//client.Close ();
				client = null;

				return;
			}

			if (!packets.ContainsKey (opCode)) {
				// Unsupported extension, disconnect immidiately.
				//client.Close ();
				return;
			}

			var packet = packets [opCode];

			packet.ReadData (stream);
			packet.HandleRead ();

			packet.ResetReadOffset ();
		}


		/* Loop for the listener, awaiting client connects or attempt to connects. */
		/*private void ServerReceiveLoop ()
		{
			while (true) {
				if (client == null || !client.Connected) {

					try {
						client = server.AcceptTcpClient ();

						client.NoDelay = true;

						client.ReceiveBufferSize = client.SendBufferSize = 5;

						stream = client.GetStream ();

						if (stream == null) {
							client = null;

							Thread.Sleep (TIMEOUT);

							continue;
						}

					} catch {

					//	Thread.Sleep (TIMEOUT);
					}

				}

				if (client != null && client.Connected)
					AwaitStream ();
			}
		}*/


		/* Loop for receiving data and/or connecting to the server for client-side. */
		private async void ClientReceiveLoop ()
		{
			while (true) {

				if (client == null || client.Client == null || !client.Connected || stream == null) {
					try {
						if (client == null)
							client = new TcpClient ();

						await client.ConnectAsync (IPAddress, IsRadio ? PORT_RADIO : PORT);

						client.NoDelay = true;

						client.SendBufferSize = PACKET_SIZE;

						stream = client.GetStream ();

						



						if (stream != null) {

							client.ReceiveBufferSize = client.SendBufferSize;

							WritePacket (OpCodes.Handshake);

						} else {
							//client.Close ();

							continue;
						}
					} catch (InvalidOperationException) {
						//client.Close ();

						client = new TcpClient ();

						client.NoDelay = true;

						client.SendBufferSize = PACKET_SIZE;
					}
					catch {
						//	Thread.Sleep (TIMEOUT);
					}

					continue;
				}

				AwaitStream ();

			}
		}
	}
}
