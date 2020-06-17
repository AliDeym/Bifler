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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;



namespace libBifler.Utils
{
	/// <summary>
	/// Unsafe bitmap (using CLR).
	/// </summary>
	public unsafe class FastBitmap : IDisposable
	{
		private Bitmap subject;
		private int subject_width;
		private BitmapData bitmap_data = null;
		private Byte* p_base = null;

		/// <summary>
		/// Bitmap data.
		/// </summary>
		public byte [] pixelData = null;

		/// <summary>
		/// Creates a CLR Bitmap data.
		/// </summary>
		/// <param name="subject_bitmap">.NET Bitmap.</param>
		public FastBitmap (Bitmap subject_bitmap)
		{
			this.subject = subject_bitmap;
			try {
				LockBitmap ();
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		/// <summary>
		/// Calls the release resource function on current instance.
		/// </summary>
		public void Dispose ()
		{
			Dispose (true);

			GC.SuppressFinalize (this);
		}

		private bool disposed = false;

		/// <summary>
		/// Releases all resources held by CLR.
		/// </summary>
		/// <param name="disposing">Are we disposing?</param>
		protected virtual void Dispose (bool disposing)
		{
			if (!this.disposed) {
				if (disposing) {
					UnlockBitmap ();
					Bitmap.Dispose ();
				}

				subject = null;
				bitmap_data = null;
				p_base = null;

				disposed = true;
			}
		}

		/// <summary>
		/// Called when instance gets destroyed.
		/// </summary>
		~FastBitmap ()
		{
			Dispose (false);
		}

		/// <summary>
		/// Returns the .NET bitmap.
		/// </summary>
		public Bitmap Bitmap {
			get { return subject; }
		}

		/// <summary>
		/// Locks down and reads Bitmap data into Memory.
		/// </summary>
		public void LockBitmap ()
		{
			GraphicsUnit unit = GraphicsUnit.Pixel;
			RectangleF boundsF = subject.GetBounds (ref unit);
			Rectangle bounds = new Rectangle ((int)boundsF.X, (int)boundsF.Y, (int)boundsF.Width, (int)boundsF.Height);
			subject_width = (int)boundsF.Width * sizeof (int);

			if (subject_width % 4 != 0) {
				subject_width = 4 * (subject_width / 4 + 1);
			}

			bitmap_data = subject.LockBits (bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			p_base = (Byte*)bitmap_data.Scan0.ToPointer ();

			IntPtr pointer = bitmap_data.Scan0;



			var Depth = Bitmap.GetPixelFormatSize (subject.PixelFormat);
			int step = Depth / 8;

			var pixelCount = subject.Width * subject.Height;
			pixelData = new byte [pixelCount * step];

			Marshal.Copy (pointer, pixelData, 0, pixelData.Length);
		}

		/// <summary>
		/// Unlocks bitmap pixels.
		/// </summary>
		public void UnlockBitmap ()
		{
			if (bitmap_data == null) return;
			subject.UnlockBits (bitmap_data); bitmap_data = null; p_base = null;
		}
	}
}
