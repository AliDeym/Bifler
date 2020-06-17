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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace libBifler.UI
{
	/// <summary>
	/// Displays pictures from Managed Cameras directly into WinForm UI.
	/// </summary>
	public class CameraBox : PictureBox
	{
		/// <summary>
		/// Camera box uses Bitmap instead of Image.
		/// </summary>
		public Bitmap BitmapImage = null;


		private Camera _cam = null;

		/// <summary>
		/// Wether this Camerabox has a managed camera.
		/// </summary>
		/// <returns></returns>
		public bool HasInformation () => _cam != null;


		/// <summary>
		/// Sets managed camera on the picturebox.
		/// </summary>
		/// <param name="cam">Managed camera.</param>
		public void SetCamera (Camera cam) => _cam = cam;


		/// <summary>
		/// Gets camera save index.
		/// </summary>
		/// <returns>Save index.</returns>
		public int GetSaveIndex () => _cam.SaveIndex;


		/// <summary>
		/// Gets displaying camera device name.
		/// </summary>
		/// <returns>Device name.</returns>
		public string GetDeviceName () => _cam.Name;


		/// <summary>
		/// Invoke sets the Bitmap Image.
		/// </summary>
		/// <param name="map">Bitmap file.</param>
		public void InvokeSet (Bitmap map)
		{
			if (InvokeRequired) {

				Action<Bitmap> voke = InvokeSet;
				BeginInvoke (voke, map);
				return;
			}

			BitmapImage = map;
		}


		/// <summary>
		/// Win32 Handled resuming of Control.
		/// </summary>
		protected void ResumePainting ()
		{
			Win32UI.ResumePainting (Handle);
			ResumeLayout ();
		}

		
		/*
		/// <summary>
		/// Win32 handled suspend of Control.
		/// </summary>
		protected void SuspendPainting ()
		{
			Win32UI.SuspendPainting (Handle);

		}*/

		/// <summary>
		/// Invoke refreshes the picturebox.
		/// </summary>
		public void InvokeRefresh (Bitmap img) //ImageProvider.Image image)
		{
			BitmapImage = new Bitmap (img);


			if (InvokeRequired) {

				//Invoke (new MethodInvoker (ResumePainting));
				Invoke (new MethodInvoker (Refresh));
				//Invoke (new MethodInvoker (SuspendPainting));
			}
		}

		/*
		/// <summary>
		/// Handles disable of re-painting on resize or stuff like that.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoadCompleted (AsyncCompletedEventArgs e)
		{
			base.OnLoadCompleted (e);

			SuspendLayout ();
			Win32UI.SuspendPainting (Handle);
		}*/

		int fps = 0;

		/// <summary>
		/// Handles display of Camera Boxes.
		/// </summary>
		/// <param name="e">Paint event arguments.</param>
		protected override void OnPaint (PaintEventArgs e)
		{
			if (BitmapImage == null)
				e.Graphics.Clear (BackColor);
			else {
				Size sourceSize = BitmapImage.Size, targetSize = ClientSize;
				float scale = Math.Max ((float)targetSize.Width / sourceSize.Width, (float)targetSize.Height / sourceSize.Height);
				var rect = new RectangleF ();
				rect.Width = scale * sourceSize.Width;
				rect.Height = scale * sourceSize.Height;
				rect.X = (targetSize.Width - rect.Width) / 2;
				rect.Y = (targetSize.Height - rect.Height) / 2;


				e.Graphics.DrawImage (BitmapImage, rect);
				fps = 0;
			}
		}
	}
}
