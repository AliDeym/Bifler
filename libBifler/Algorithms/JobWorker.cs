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


using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


using PylonC.NETSupportLibrary;

using libBifler.Utils.Structures;


namespace libBifler.Algorithms
{
	class JobWorker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		/* Camera which is the sender. */
		private Camera master;

		/* Contains buffers for images. */
		private Bitmap [] _buffer;

		private DramaticBuffer<string> queue;

		private Dictionary<int, ThreadInformation> threadJobs;
		private Thread schedulerThread;


		private BufferManager bufferManager;


		private ImageProvider.Image image = null;

		private bool onPreviewSaving = false;



		/// <summary>
		/// Number of working threads (saving into buffer manager's RAM).
		/// </summary>
		public int ThreadCount { get; private set; }


		/// <summary>
		/// Creates a new job worker handler.
		/// </summary>
		/// <param name="cam">Camera.NET master which this slave is working for.</param>
		/// <param name="buff">Buffer manager.</param>
		/// <param name="threadsCount">Number of working threads.</param>
		/// <param name="bufferSize">How many buffers can we have in memory for framerate.</param>
		/// <param name="queueSize">Buffer of Queue.</param>
		public JobWorker (Camera cam, BufferManager buff, int threadsCount = 6, int bufferSize = 64, int queueSize = 128)
		{
			master = cam;

			bufferManager = buff;

			ThreadCount = threadsCount;

			threadJobs = new Dictionary<int, ThreadInformation> ();

			queue = new DramaticBuffer<string> (queueSize);

			_buffer = new Bitmap [bufferSize];
		}




		private int _bufferIndex = 0;

		/// <summary>
		/// Handles buffer updating from Managed Camera.
		/// </summary>
		public void HandleBufferUpdate(ImageProvider imageProvider)
		{
			image = imageProvider.GetLatestImage ();

			var ImageObject = _buffer [_bufferIndex];

			if (image != null) {


				if (BitmapFactory.IsCompatible (ImageObject, image.Width, image.Height, image.Color)) {

					BitmapFactory.UpdateBitmap (ImageObject, image.Buffer, image.Width, image.Height, image.Color);


				}
				else {

					BitmapFactory.CreateBitmap (out Bitmap b, image.Width, image.Height, image.Color);
					BitmapFactory.UpdateBitmap (b, image.Buffer, image.Width, image.Height, image.Color);

					_buffer [_bufferIndex] = b;

					/*BitmapFactory.CreateBitmap (out Bitmap bitmap, image.Width, image.Height, image.Color);
					BitmapFactory.UpdateBitmap (bitmap, image.Buffer, image.Width, image.Height, image.Color);
					*/


					//pictureBox.InvokeSet (bitmap);


				}

				_bufferIndex++;



				if (_bufferIndex >= _buffer.Length)
					_bufferIndex = 0;



				imageProvider.ReleaseImage ();

				// First check for preview saving, then call the function because:
				//		Function increases the FPS Counter for next calls.
				if (!onPreviewSaving) {
					if (master.ShouldDrawOnBufferUpdate()) {

						var pictureIndex = _bufferIndex - 1;

						if (pictureIndex < 0)
							pictureIndex = _buffer.Length - 1;

						DrawPreviewImage (_buffer [pictureIndex]);

					}
				}

				/*if (FpsLimit > 1 && !project.Started && !onPreviewSaving) {

					fpsCounter++;

					if (fpsCounter >= FpsLimit) {

						OnPreviewImageReadyHandler ();

						fpsCounter = 0;

					}
				}
				else if (FpsLimit > 0 && !project.Started && !onPreviewSaving) {

					OnPreviewImageReadyHandler ();

				}*/
			}
		}


		/* Draws the image buffer on CameraBox. */
		private void DrawPreviewImage (Bitmap picture)
		{
			if (master.pictureBox != null) {

				//Action<Bitmap> act = master.pictureBox.InvokeRefresh;

				Task.Factory.StartNew(() => { master.pictureBox.InvokeRefresh (picture); });

			}
		}



		/// <summary>
		/// Initializes this handler's instance.
		/// </summary>
		public void Initialize ()
		{
			schedulerThread = new Thread (schedule);

			for (int i = 0; i < ThreadCount; i++) {

				var savingThread = new ThreadInformation (threadedMemoryWriting);

				savingThread.SetPriorityHigh ();

				threadJobs.Add (savingThread.ThreadID, savingThread);

			}

            //schedulerThread.Start ();

            foreach (var thread in threadJobs) {
               // Thread.Sleep (3);

                thread.Value.Start ();
            }
		}

		public void SaveImage (string path)
		{
			if (image == null) return;

			//queue.Enqueue (path);

			var countAvailableThreads = 0;

			// Check if any thread finished it's job.
			foreach (var thread in threadJobs) {

				if (thread.Value.IsBusy && thread.Value.HasFinished) {

					// not found in the list ERROR.
					/*if (!imageList.ContainsKey (thread.Value.ImageIndex)) {
						thread.Value.IsBusy = false;

						continue;
					}*/


					thread.Value.IsBusy = false;

				}

				if (!thread.Value.IsBusy)
					countAvailableThreads++;

			};


			/* We use the thread counts minus 1 for drawing while also saving. */

			if (!onPreviewSaving) {

				// This also updates fpsCounter.
				if (master.ShouldDrawOnBufferSave ()) {

					var drawIndex = _bufferIndex - 1 - 5;

					if (drawIndex < 0) {
						// You might ask why, but the fact is, we have a negative number, we wanna reduce size from it.
						// So, we need size minus one to get the last index, now we positive our number by multiplying to minus one.
						// Then we reduce the last index by our positive number.
						drawIndex = _buffer.Length - 1 - (drawIndex * -1);
					}

					DrawPreviewImage (_buffer [drawIndex]);

				}

			}

			/* Handles giving job and dequeuing the threads works. */

			if (countAvailableThreads > 0) {
				// We only schedule one thread per cycle, so we don't run it in a for-loop.
				//for (int i = 0; i <= queue.Count; i++) {

				/*var imagePath = queue.Dequeue ();

				if (imagePath == null) continue;*/

				var handled = false;

				//if (keyPair == null) continue;

				foreach (var thread in threadJobs) {

					//if (keyPair == null) break;

					if (!thread.Value.IsBusy && !handled) {

						var imageIndex = _bufferIndex - 4;

						if (imageIndex < 0)
                            imageIndex = _buffer.Length - 1 - (imageIndex * -1);

                        foreach (var t in threadJobs) {
							if (t.Value.IsBusy && t.Value.ReadingImageIndex == imageIndex) {
								imageIndex++;

                                if (imageIndex >= _buffer.Length)
                                    imageIndex = 0;

								//Console.WriteLine ("COLLISION: " + path + ", T: " + thread.Key + ", C: " + t.Key);

								/*logger.Trace ("Same image sequential on path: " + path);
								logger.Trace ("Collision: " + thread.Key + "(collider) with " + t.Key + "(solid)");*/

								// Double-check out of array statement.
								/*if (imageIndex < 0)
									imageIndex = _buffer.Length - 1;*/
							}
						}

						handled = true;

						thread.Value.ImageIndex = path;

						thread.Value.ReadingImageIndex = imageIndex;


						thread.Value.HasFinished = false;
						thread.Value.IsBusy = true;

						return;
					}

				}

				//}
			} else {
				// DO SOMETHING LOL :D
				logger.Fatal ("Ran out of threads.");
				//throw new ArgumentOutOfRangeException ("Worker Threads");
			}
		}

		public void SavePreview (string path)
		{
			logger.Trace ("Saving preview on path: " + path);

			onPreviewSaving = true;

			if (image != null) {
				SaveImage (path);

				logger.Trace ("Finished preview save for path: " + path);
				onPreviewSaving = false;
			}
			else {
				new Thread (() => {
					var requested = true;
					var requestedPath = path;

					while (requested) {
						Thread.Sleep (500);

						if (image != null) {
							SaveImage (requestedPath);

							logger.Trace ("Finished preview save for path: " + path + " with delayed sequential.");
							requested = false;
							onPreviewSaving = false;
						}
					}
				}).Start ();
			}
		}
		

		//private int garbageCollection = 0;

		/* Handles scheduling for working threads. */
		private void schedule ()
		{
			for (; ;) {

				// Collect GC.
				/*garbageCollection++;

				// Garbage past the limit.
				if (garbageCollection >= 500) {
					GC.Collect ();

					garbageCollection = 0;
				}*/

				var countAvailableThreads = 0;

				// Check if any thread finished it's job.
				foreach (var thread in threadJobs) {

					if (thread.Value.IsBusy && thread.Value.HasFinished) {

						// not found in the list ERROR.
						/*if (!imageList.ContainsKey (thread.Value.ImageIndex)) {
							thread.Value.IsBusy = false;

							continue;
						}*/


						thread.Value.IsBusy = false;

					}

					if (!thread.Value.IsBusy)
						countAvailableThreads++;

				};

				/* Handles giving job and dequeuing the threads works. */

				if (countAvailableThreads > 0) {
					// We only schedule one thread per cycle, so we don't run it in a for-loop.
					//for (int i = 0; i <= queue.Count; i++) {

						var imagePath = queue.Dequeue ();
						
						if (imagePath == null) continue;

						var handled = false;

						//if (keyPair == null) continue;

						foreach (var thread in threadJobs) {

							//if (keyPair == null) break;

							if (!thread.Value.IsBusy && !handled) {

								var imageIndex = _bufferIndex - 1;

								if (imageIndex < 0)
									imageIndex = _buffer.Length - 1;

								handled = true;

								thread.Value.ImageIndex = imagePath;
							
								thread.Value.ReadingImageIndex = imageIndex;

								thread.Value.HasFinished = false;
								thread.Value.IsBusy = true;

								break;
							}

						}

					//}
				}

				Thread.Sleep (2);
			}
		}


		/* Handles threaded writing. */
		private void threadedMemoryWriting ()
		{
			for (; ;) {

				var ID = Thread.CurrentThread.ManagedThreadId;

				var information = threadJobs [ID];



				if (information.IsBusy && !information.HasFinished) {

					var imageBuffer = _buffer [information.ReadingImageIndex];

					if (imageBuffer != null) {

						bufferManager.ReadBuffer (information.ImageIndex, imageBuffer);

						information.HasFinished = true;

					}

				}


				Thread.Sleep (2);//2);
			}
		}



	}
}
