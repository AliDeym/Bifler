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

using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;


using System.Drawing;
using System.Drawing.Imaging;

using libBifler.Utils.Structures;

namespace libBifler.Algorithms
{
	/// <summary>
	/// Handles image scheduling and displaying at the same time, asynchronously.
	/// </summary>
	class BufferManager
	{
		// Imaging parameters:
		private EncoderParameters encoderParams = null;
		private ImageCodecInfo JPEGCodec = null;
		private Encoder encoder = null;




		private ConcurrentDictionary<string, BufferImage> imageList;

		private Dictionary<int, ThreadInformation> threadJobs;
		private Thread schedulerThread;



		/// <summary>
		/// Number of save threads.
		/// </summary>
		public int ThreadCount { get; private set; }


		/// <summary>
		/// Creates a new buffered image handler.
		/// </summary>
		/// <param name="threadsCount">Number of saving threads.</param>
		public BufferManager (int threadsCount = 4)
		{
			ThreadCount = threadsCount;

			threadJobs = new Dictionary<int, ThreadInformation> ();

			imageList = new ConcurrentDictionary<string, BufferImage> ();
		}


		/* Gets codec from ImageFormat enum. */
		private ImageCodecInfo GetEncoder (ImageFormat format)
		{

			ImageCodecInfo [] codecs = ImageCodecInfo.GetImageDecoders ();

			foreach (ImageCodecInfo codec in codecs) {
				if (codec.FormatID == format.Guid) {
					return codec;
				}
			}
			return null;
		}




		/// <summary>
		/// Initializes this handler's instance.
		/// </summary>
		public void Initialize ()
		{
			JPEGCodec = GetEncoder (ImageFormat.Jpeg);

			encoder = Encoder.Compression;

			encoderParams = new EncoderParameters (1);

			encoderParams.Param [0] = new EncoderParameter (encoder, 40L);



			schedulerThread = new Thread (schedule);

			for (int i = 0; i < ThreadCount; i++) {

				var savingThread = new ThreadInformation (saveHandler);

				threadJobs.Add (savingThread.ThreadID, savingThread);

			}

			schedulerThread.Start ();

            foreach (var thread in threadJobs) {

                //Thread.Sleep(3);

                thread.Value.Start ();
            }
		}


		/* Handles threaded saving. */
		private void saveHandler ()
		{
			for (;;) {

				var ID = Thread.CurrentThread.ManagedThreadId;

				var information = threadJobs [ID];



				if (information.IsBusy && !information.HasFinished) {

					var imageBuffer = imageList [information.ImageIndex];

					if (imageBuffer != null) {


						var bmp = Image.FromStream (imageBuffer.mStream, true, false);

						bmp.Save (information.ImageIndex, ImageFormat.Jpeg);

						bmp.Dispose ();

						information.HasFinished = true;

					}

				}


				Thread.Sleep (5);//5);
			}
		}

		private int garbageCollection = 0;

		/* Handles scheduling for save threads. */
		private void schedule ()
		{
			for (; ; ) {

				// Collect GC.
				garbageCollection++;

				// Garbage past the limit.
				if (garbageCollection >= 500) {
					GC.Collect ();

					garbageCollection = 0;
				}

				// Check if any thread finished it's job.
				foreach (var thread in threadJobs) {

					if (thread.Value.IsBusy && thread.Value.HasFinished) {

						// not found in the list ERROR.
						if (!imageList.ContainsKey (thread.Value.ImageIndex)) {
							thread.Value.IsBusy = false;

							continue;
						}

						var image = imageList [thread.Value.ImageIndex];


						if (imageList.ContainsKey (thread.Value.ImageIndex))
							((IDictionary)imageList).Remove (thread.Value.ImageIndex);

						image.Dispose ();
						image = null;


						thread.Value.IsBusy = false;

					}

				};

				/* Handles giving job and dequeuing the threads works. */
				foreach (var keyVal in imageList) {

					var keyPair = keyVal.Value;

					if (keyPair == null) continue;

					foreach (var thread in threadJobs) {

						if (keyPair == null) break;

						if (!thread.Value.IsBusy && !keyPair.IsHandled) {

							keyPair.IsHandled = true;
							thread.Value.ImageIndex = keyPair.FilePath;
							thread.Value.HasFinished = false;
							thread.Value.IsBusy = true;

							break;
						}

					}

				}

				Thread.Sleep (3);//2);
			}
		}


		/// <summary>
		/// Puts the image into saving schedule list.
		/// </summary>
		/// <param name="filePath">Save path.</param>
		/// <param name="image">Bitmap image.</param>
		public void ReadBuffer (string filePath, Bitmap image)
		{
			if (imageList.ContainsKey (filePath)) return;

			var imageBuff = new BufferImage ();

			imageBuff.FilePath = filePath;

			// Copy the image buffer into memory buffer.
			image.Save (imageBuff.mStream, ImageFormat.Bmp);


			imageList.AddOrUpdate (filePath, imageBuff, (key, val) => { return imageBuff; });
		}
	}
}
