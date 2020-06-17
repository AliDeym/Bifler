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
using System.IO;

namespace libBifler.Utils.Structures
{
	/// <summary>
	/// Buffered Image Stream object.
	/// </summary>
	internal class BufferImage : IDisposable
	{
		/// <summary>
		/// Memory stream that contains the image.
		/// </summary>
		internal MemoryStream mStream;


		/// <summary>
		/// Is the current buffer handled by a worker thread or not yet?
		/// </summary>
		internal bool IsHandled = false;


		/// <summary>
		/// Buffered image stream file path to be worked on.
		/// </summary>
		internal string FilePath = "";


		/// <summary>
		/// Creates a buffered image instance.
		/// </summary>
		public BufferImage ()
		{
			mStream = new MemoryStream ();
		}


		/// <summary>
		/// Disposes the mainStream.
		/// </summary>
		public void Dispose()
		{
			mStream.Dispose ();
			mStream = null;
		}
	}
}
