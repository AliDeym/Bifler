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

namespace libBifler.NetworkStructure
{
	public abstract class Packet
	{
		/// <summary>
		/// Packet's OpCode.
		/// </summary>
		public Byte OpCode { get; protected set; }

		/// <summary>
		/// Data length for read or write.
		/// </summary>
		protected int DataLength { get; set; }


		/* Old Version:
		 * public Action<object []> ReadMethod;
		 */
		/// <summary>
		/// Function which is executed after packet provider finishes reading the whole data.
		/// </summary>
		public Action<object> ReadMethod;


		private byte [] _writeData;
		private byte [] _readData;


		private int _writeOffset;
		private int _readOffset;


		/// <summary>
		/// Creates a base read/write class for packets on the Network stream. Note that a packet can be used again after sending data.
		/// </summary>
		/// <param name="opCode">Packet opcode.</param>
		/// <param name="dataLength"></param>
		public Packet (OpCodes opCode, int dataLength)
		{
			OpCode = (byte)opCode;

			DataLength = dataLength;

			_readOffset = _writeOffset = 0;

			_writeData = new byte [dataLength];
			_readData = new byte [dataLength];
		}


		/// <summary>
		/// Handles reading for specified opcode.
		/// </summary>
		public abstract void HandleRead ();

		/// <summary>
		/// Handles no parameter writing of specified packet.
		/// </summary>
		public abstract void HandleWrite ();

		/// <summary>
		/// Handles parameterized write of specified packet. return true to indicate that the packet supports multi parameters.
		/// </summary>
		/// <param name="arguments">arguments as objects.</param>
		/// <returns>true for supporting parameterized writes.</returns>
		public virtual bool HandleParameterizedWrite (object data) {
			return false;
		}


		/// <summary>
		/// Resets the read offset for future packets.
		/// </summary>
		internal void ResetReadOffset ()
		{
			_readOffset = 0;
		}


		#region Write
		/// <summary>
		/// Write byte into buffer.
		/// </summary>
		/// <param name="b">Byte.</param>
		public void WriteByte (byte b)
		{
			if (_writeOffset > DataLength) return;

			_writeData [_writeOffset] = b;
			_writeOffset++;
		}

		/// <summary>
		/// Writes boolean into buffer.
		/// </summary>
		/// <param name="b">Boolean.</param>
		public void WriteBoolean (bool b)
		{
			if (b) {
				WriteByte (0x1);
				return;
			}

			WriteByte (0x0);
		}

		/// <summary>
		/// Writes Int16 into buffer.
		/// </summary>
		/// <param name="num">Int16.</param>
		public void WriteInt16 (Int16 num)
		{
			var data = BitConverter.GetBytes (num);

			data.CopyTo (_writeData, _writeOffset);

			_writeOffset += 2;
		}


		/// <summary>
		/// Writes Int32 into buffer.
		/// </summary>
		/// <param name="num">Int32.</param>
		public void WriteInt32 (Int32 num)
		{
			var data = BitConverter.GetBytes (num);

			data.CopyTo (_writeData, _writeOffset);

			_writeOffset += 4;
		}


		/// <summary>
		/// Writes Int64 into buffer.
		/// </summary>
		/// <param name="num">Int64.</param>
		public void WriteInt64 (Int64 num)
		{
			var data = BitConverter.GetBytes (num);

			data.CopyTo (_writeData, _writeOffset);

			_writeOffset += 8;
		}


		/// <summary>
		/// Writes Float into buffer.
		/// </summary>
		/// <param name="num">Float.</param>
		public void WriteFloat (Single num)
		{
			var data = BitConverter.GetBytes (num);


			data.CopyTo (_writeData, _writeOffset);

			_writeOffset += 4;
		}
		#endregion


		#region Read
		/// <summary>
		/// Reads a single byte from buffer.
		/// </summary>
		/// <returns>Byte.</returns>
		public byte ReadByte ()
		{
			var b = _readData [_readOffset];

			_readOffset++;

			return b;
		}

		/// <summary>
		/// Reads boolean from buffer.
		/// </summary>
		/// <returns>Boolean.</returns>
		public bool ReadBoolean ()
		{
			var b = _readData [_readOffset];

			_readOffset++;

			return (b == 0x1 ? true : false);
		}


		/// <summary>
		/// Reads Int16 from buffer.
		/// </summary>
		/// <returns>Int16.</returns>
		public Int16 ReadInt16 ()
		{
			var num = BitConverter.ToInt16 (_readData, _readOffset);

			_readOffset += 2;

			return num;
		}


		/// <summary>
		/// Reads Int32 from buffer.
		/// </summary>
		/// <returns>Int32.</returns>
		public Int32 ReadInt32 ()
		{
			var num = BitConverter.ToInt32 (_readData, _readOffset);

			_readOffset += 4;

			return num;
		}


		/// <summary>
		/// Reads Int64 from buffer.
		/// </summary>
		/// <returns>Int64.</returns>
		public Int64 ReadInt64 ()
		{
			var num = BitConverter.ToInt64 (_readData, _readOffset);

			_readOffset += 8;

			return num;
		}


		/// <summary>
		/// Reads Float from buffer.
		/// </summary>
		/// <returns>Float.</returns>
		public Single ReadFloat ()
		{
			var num = BitConverter.ToSingle (_readData, _readOffset);

			_readOffset += 4;

			return num;
		}
		#endregion


		/// <summary>
		/// Reads data from network stream and writes it into buffer.
		/// </summary>
		/// <param name="stream">Network Stream.</param>
		internal void ReadData (NetworkStream stream)
		{
			stream.ReadAsync (_readData, 0, DataLength);
		}


		/// <summary>
		/// Writes buffer data into stream, then clears the buffer.
		/// </summary>
		/// <param name="stream">Network Stream.</param>
		internal void SendData (NetworkStream stream)
		{
			if (stream == null) {
				_writeOffset = 0;

				return;
			}


			try {
				stream.WriteByte (OpCode);
				stream.Write (_writeData, 0, DataLength);
			} catch { }

			_writeOffset = 0;
		}
	}
}
