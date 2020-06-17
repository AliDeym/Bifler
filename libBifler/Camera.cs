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

using libBifler.UI;
using libBifler.Project;
using libBifler.Algorithms;


using PylonC.NET;
using PylonC.NETSupportLibrary;

namespace libBifler
{
	/// <summary>
	/// Managed .NET Pylon (basler) camera objects.
	/// </summary>
	public class Camera : IDisposable
	{
		/// <summary>
		/// Config manager for this instance of camera.
		/// </summary>
		public ConfigManager Config { get; private set; }

		/* Logger for Camera. */
		private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger ();

		/// <summary>
		/// Determines wether the camera is currently disabled for this project or not.
		/// </summary>
		public bool Disabled = false;


		/// <summary>
		/// Determines wether the camera is started or not yet.
		/// </summary>
		public bool HasStarted = false;


		/// <summary>
		/// Lock object. used for multi-threading.
		/// </summary>
		public readonly object _lock = new object ();


		/// <summary>
		/// How many triggers needed to Save a new Image. (Default: 1).
		/// </summary>
		public int SaveRange = 1;


		/// <summary>
		/// How many triggers should we skip in the beginning. (Default: 0).
		/// </summary>
		public int StartRange {
			get {
				return _startRange;
			}
			set {
				_startRange = value;
				_startCounter = (uint)value;
			}
		}


		private int _startRange = 0;


		/// <summary>
		/// PictureBox Framerate limiter. Every x Frames received from Basler, refresh picturebox. (Default: 3).
		/// </summary>
		public int FpsLimit = 3;


		/// <summary>
		/// DOTNet Image put into Camera object.
		/// </summary>
		public Bitmap Image = null;


		/// <summary>
		/// Form pictureBox.
		/// </summary>
		public CameraBox pictureBox;


		/// <summary>
		/// Pylon's Image Provider.
		/// </summary>
		public ImageProvider ImageProvider { get; private set; }


		/// <summary>
		/// Handles device removing per Managed camera instance.
		/// </summary>
		/// <param name="sender">Sender camera.</param>
		public delegate void DeviceRemovedHandler (Camera sender);


		/// <summary>
		/// Event raised whenever this instance of managed camera gets removed by Basler's Pylon library.
		/// </summary>
		public event DeviceRemovedHandler OnDeviceRemoved;


		/// <summary>
		/// ErrorReceived Event Handler.
		/// </summary>
		/// <param name="sender">Instance of Camera object.</param>
		/// <param name="e">Error exception.</param>
		public delegate void ErrorReceivedHandler (Camera sender, Exception e);


		/// <summary>
		/// Event for Receiving error exceptions from the Pylon library.
		/// </summary>
		public event ErrorReceivedHandler OnErrorReceived;


		/// <summary>
		/// Returns the saved image index.
		/// </summary>
		public uint SavedIndex {
			get {
				return _savedIndex;
			}
		}


		/// <summary>
		/// Counts saved/triggered index of file.
		/// </summary>
		internal uint _savedIndex;


		/// <summary>
		/// Save image camera index. (NOTE: This is not the current value count of saved image, this is App managed camera index changing).
		/// </summary>
		public int SaveIndex { get; set; }


		/// <summary>
		/// Device Index using Pylon structure.
		/// </summary>
		public uint Index { get; private set; }


		/// <summary>
		/// Device Fullname grabbed using Pylon.
		/// </summary>
		public string FullName { get; private set; }


		/// <summary>
		/// Device Name grabbed using Pylon.
		/// </summary>
		public string Name { get; private set; }


		/// <summary>
		/// Device Tooltip grabbed using Pylon.
		/// </summary>
		public string Tooltip { get; private set; }

		/// <summary>
		/// Device serial number grabbed from Basler directly.
		/// </summary>
		public string SerialNumber { get; private set; }


		/// <summary>
		/// Handles image buffer saving (threaded saving.)
		/// </summary>
		private BufferManager ImageBuffers = null;


		/* Contains buffers for images. */
		//private Bitmap [] _buffer;


		private ImageProvider.Image image = null;
		internal uint _triggeredCounter;
		internal uint _startCounter;

		private ProjectManager project = null;

		private JobWorker worker = null;

		/// <summary>
		/// Called when disposing.
		/// </summary>
		~Camera ()
		{
			Dispose ();
		}


		/// <summary>
		/// Instantiates a Bifler Camera.
		/// </summary>
		/// <param name="deviceInfo">Basler Camera device information.</param>
		/// <param name="projectMngr">Project manager which has information about projects.</param>
		public Camera (DeviceEnumerator.Device deviceInfo, ProjectManager projectMngr)
		{
			Index = deviceInfo.Index;

			SaveIndex = (int)Index + 1;

			FullName = deviceInfo.FullName;

			Name = deviceInfo.Name;

			Tooltip = deviceInfo.Tooltip;

			project = projectMngr;

			_triggeredCounter = _savedIndex = 0;

			/*_buffer = new Bitmap [bufferSize];
			_bufferIndex = 0;*/
		}


		/// <summary>
		/// Releases the Pylon.C camera devices from .CLR and .NET.
		/// </summary>
		public void Dispose ()
		{
			Stop ();
			Close ();
		}


		/// <summary>
		/// Initializes and Opens the Pylon Image Provider to the Camera.
		/// </summary>
		/// <param name="maxExposure">Maximum Exposure.</param>
		public void Initialize (int maxExposure)
		{
			log.Info ("Initializing camera on Index: " + Index + "...");


			log.Info ("Creating Buffermanager(" + Index + ")...");


			ImageBuffers = new BufferManager ();

			ImageBuffers.Initialize ();


			log.Info ("Buffermanager (" + Index + ") Created with " + ImageBuffers.ThreadCount + " threads.");



			log.Info ("Initializing Worker threads on Cam(" + Index + ")...");

			worker = new JobWorker (this, ImageBuffers);

			worker.Initialize ();

			log.Info ("Worker Threads(" + Index + ") Created with " + worker.ThreadCount + " threads.");


			log.Info ("Creating Image Providers(" + Index + ")...");

			ImageProvider = new ImageProvider ();

			ImageProvider.ImageReadyAction = OnImageReadyHandler;
			//ImageProvider.ImageReadyEvent += OnImageReadyHandler;
			ImageProvider.DeviceRemovedEvent += OnDeviceRemovedHandler;



			log.Info ("Image Provider(" + Index + ") created successfully.");
			log.Info ("Opening the Basler(" + Index + ") device...");

			Open ();

			log.Info ("Basler(" + Index + ") is now open!");


			/* Config part. */
			log.Info ("Loading config for '" + Name + "'...");

			Config = new ConfigManager (Name);

			LoadConfigNodes (
				"Width", "Height",
				"OffsetX", "OffsetY"
			);

			LoadFloatNodes (
				"Gain", "ExposureTime",
				"AutoTargetBrightness"
			);

			LoadStringNode ("ExposureAuto");

			// Setting max Exposure.
			var node = ImageProvider.GetNodeFromDevice ("AutoExposureTimeUpperLimit");

			if (node.IsValid) {
				if (GenApi.NodeIsWritable (node))
					Pylon.DeviceSetFloatFeature (ImageProvider.m_hDevice, "AutoExposureTimeUpperLimit", maxExposure);
			}


			log.Info ("Loaded configurations for the camera '" + Name + "'.");

			/* Done config part. */


			
		}

		/* Loads config nodes which have float type. */
		private void LoadFloatNodes (params string[] nodeName)
		{
			foreach (var name in nodeName) {
				var nodeConf = Config.GetConfig (name);


				if (nodeConf == null || string.IsNullOrWhiteSpace (nodeConf)) continue;
				if (!float.TryParse (nodeConf, out float value)) continue;



				var node = ImageProvider.GetNodeFromDevice (name);

				if (node.IsValid) {

					/* Is Writtable. */
					if (GenApi.NodeIsWritable (node))
						GenApi.FloatSetValue (node, value);
				}
			}
		}


		/* Loads config nodes and configures the basler camera. */
		private void LoadConfigNodes (params string[] nodeNames)
		{
			foreach (var name in nodeNames) {
				var nodeConf = Config.GetConfig (name);


				if (nodeConf == null || string.IsNullOrWhiteSpace (nodeConf)) continue;
				if (!long.TryParse (nodeConf, out long value)) continue;



				var node = ImageProvider.GetNodeFromDevice (name);

				if (node.IsValid) {

					/* Is Writtable. */
					if (GenApi.NodeIsWritable (node))
						GenApi.IntegerSetValue (node, value);
				}
			}
		}

		/* Load a string node. */
		private void LoadStringNode (string nodeName)
		{
			var nodeConf = Config.GetConfig (nodeName);

			if (nodeConf == null || string.IsNullOrWhiteSpace (nodeConf)) return;



			var node = ImageProvider.GetNodeFromDevice (nodeName);



			if (node.IsValid) {
				if (GenApi.NodeIsWritable (node))
					GenApi.NodeFromString (node, nodeConf);
			}
		}


		/* Load a boolean node. */
		private void LoadBooleanNode (string nodeName)
		{
			var nodeConf = Config.GetConfig (nodeName);

			if (nodeConf == null || string.IsNullOrWhiteSpace (nodeConf)) return;
			if (!bool.TryParse (nodeConf, out bool value)) return;



			var node = ImageProvider.GetNodeFromDevice (nodeName);



			if (node.IsValid) {
				if (GenApi.NodeIsWritable (node))
					GenApi.BooleanSetValue (node, value);
			}
		}



		/// <summary>
		/// Indicates wether the worker should draw while updating buffer or not.
		/// </summary>
		/// <returns>Boolean | Update or not the camerabox.</returns>
		internal bool ShouldDrawOnBufferUpdate ()
		{
			if (FpsLimit > 1 && !project.Started) {
				fpsCounter++;

				if (fpsCounter >= FpsLimit) {
					fpsCounter = 0;

					return true;
				}
			}
			else if (FpsLimit > 0 && !project.Started) {
				return true;
			}

			return false;
		}


		/// <summary>
		/// Indicates wether the worker should draw while saving images or not.
		/// </summary>
		/// <returns>Boolean | Update or not the camerabox.</returns>
		internal bool ShouldDrawOnBufferSave ()
		{
            if (FpsLimit <= 0) return false;

			var updateFPS = FpsLimit;

			// We need it to be at least: update per 20 triggs.
			if (FpsLimit < 20)
				updateFPS = 20;

			//updateFPS = updateFPS * 10;


			fpsCounter++;

			if (fpsCounter >= updateFPS) {
				fpsCounter = 0;

				return true;
			}
			
			return false;
		}


		/* Handles device removing errors. */
		private void OnDeviceRemovedHandler ()
		{
			log.Fatal ("Device(" + Index + ") unexpectedly removed!");

			OnDeviceRemoved?.Invoke (this);
		}

		private void OnPreviewImageReadyHandler ()
		{

			/*if (pictureBox != null) {

				var displayIndex = _bufferIndex;


				// Decrease buffer index in case of SavePreview.
				if (project.Started)
					displayIndex = readIndex;
				else
					displayIndex--;

				displayIndex--;

				if (displayIndex == _bufferIndex)
					displayIndex --;

				if (displayIndex < 0)
					displayIndex = _buffer.Length - 1;
				

				pictureBox.InvokeRefresh (_buffer [displayIndex]);

			}*/
		}



		private int fpsCounter = 0;

		//private int _bufferIndex;

		/* Handles image receiving from Pylon library. */
		private void OnImageReadyHandler ()
		{


			worker.HandleBufferUpdate (ImageProvider);

			/*image = ImageProvider.GetLatestImage ();

			var ImageObject = _buffer [_bufferIndex];

			if (image != null) {


				if (BitmapFactory.IsCompatible (ImageObject, image.Width, image.Height, image.Color)) {


					BitmapFactory.UpdateBitmap (ImageObject, image.Buffer, image.Width, image.Height, image.Color);


				}
				else {

					BitmapFactory.CreateBitmap (out Bitmap b, image.Width, image.Height, image.Color);
					BitmapFactory.UpdateBitmap (b, image.Buffer, image.Width, image.Height, image.Color);

					_buffer [_bufferIndex] = b;

					BitmapFactory.CreateBitmap (out Bitmap bitmap, image.Width, image.Height, image.Color);
					BitmapFactory.UpdateBitmap (bitmap, image.Buffer, image.Width, image.Height, image.Color);



					pictureBox.InvokeSet (bitmap);


				}

				_bufferIndex++;



				if (_bufferIndex >= _buffer.Length) 
					_bufferIndex = 0;



				ImageProvider.ReleaseImage ();

				if (FpsLimit > 1 && !project.Started && !onPreviewSaving) {

					fpsCounter++;

					if (fpsCounter >= FpsLimit) {

						OnPreviewImageReadyHandler ();

						fpsCounter = 0;

					}
				}
				else if (FpsLimit > 0 && !project.Started && !onPreviewSaving) {

					OnPreviewImageReadyHandler ();

				}
			}*/

		}


		/* Index for reading in SaveImage method. this is used to correct the timing between save buffer and display buffer. */
		private int readIndex = -1;

		private DateTime _timechange = DateTime.Now;

		/// <summary>
		/// Safely saves last grabbed Image In a file.
		/// </summary>
		/// <param name="path">File path, with formats.</param>
		public void SaveImage (string path)
		{
			if (Disabled || SaveRange <= 0) return;

			//OneShot ();
			/*var totalT = (int)(DateTime.Now - _timechange).TotalMilliseconds;

			_timechange = DateTime.Now;

			if (totalT < 20)
			Console.WriteLine (Name + " - SS: " + totalT + "ms");*/

			worker.SaveImage (path);

			//if (image == null) return;

			/*readIndex = _bufferIndex - 1;

			if (readIndex < 0)
				readIndex = _buffer.Length - 1;





			ImageBuffers.ReadBuffer (path, _buffer[readIndex]);
			*/





			/*if (FpsLimit > 1) {

				fpsCounter++;

				if (fpsCounter >= FpsLimit) {

					//OnPreviewImageReadyHandler ();

					fpsCounter = 0;

				}
			}*/
		}

		/// <summary>
		/// Saves any image that camera has stored, if there's no image, this task awaits for image to be created then saves it.
		/// </summary>
		/// <param name="path">Preview path.</param>
		public void SavePreview (string path)
		{
			if (Disabled || SaveRange <= 0) return;


			log.Info ("Saving preview with device(" + Index + ") on path: " + path);

			worker.SavePreview (path);

		}


		/// <summary>
		/// Opens the Pylon library to Camera Index.
		/// </summary>
		public void Open ()
		{
			try {

				ImageProvider.Open (Index);

			} catch (Exception ex) {

				log.Fatal ("ERROR while opening device with Index " + Index);
				log.Fatal ("Device Info: " + FullName);

				log.Fatal ("Exception: " + ex.ToString ());
				OnErrorReceived?.Invoke (this, ex);

			}
		}


		/// <summary>
		/// Stops the image provider safely.
		/// </summary>
		public void Stop ()
		{
			try {

				ImageProvider.Stop ();

				HasStarted = false;

			} catch (Exception ex) {

				log.Fatal ("ERROR while stopping image provider with Index " + Index);
				log.Fatal ("Device Info: " + FullName);

				log.Fatal ("Exception: " + ex.ToString ());

				OnErrorReceived?.Invoke (this, ex);

			}
		}


		/// <summary>
		/// Closes the image provider safely.
		/// </summary>
		public void Close ()
		{
			try {

				ImageProvider.Close ();

				HasStarted = false;

			} catch (Exception ex) {

				log.Fatal ("ERROR while closing device with Index " + Index);
				log.Fatal ("Device Info: " + FullName);

				log.Fatal ("Exception: " + ex.ToString ());

				OnErrorReceived?.Invoke (this, ex);

			}
		}


		/// <summary>
		/// Starts continous grabbing of ImageProvider.
		/// </summary>
		public void Start ()
		{
			try {

				// Save range set to 0, but the imageProvider is still working. We Stop it.
				if (HasStarted && SaveRange <= 0) {
					Stop ();

					return;
				}

				// Disable the camera on Save range zero.
				if (HasStarted || SaveRange <= 0) return;

				ImageProvider.ContinuousShot ();

				/*using (var p = Process.GetCurrentProcess ()) {

					Console.Clear ();

					int pS = 1;
					

					foreach (ProcessThread t in p.Threads) {
						if (t.PriorityLevel == ThreadPriorityLevel.Highest) {
							//t.PriorityLevel = ThreadPriorityLevel.TimeCritical;

							t.IdealProcessor = pS;

							pS++;

							if (pS >= 8)
								pS = 1;
						}
						//Console.WriteLine (t.CurrentPriority + " - " + t.Id + " - " + t.PriorityBoostEnabled + " - " + t.PriorityLevel);
					}

				}*/

				HasStarted = true;

			} catch (Exception ex) {

				log.Fatal ("ERROR while starting continuous sht on device with Index " + Index);
				log.Fatal ("Device Info: " + FullName);

				log.Fatal ("Exception: " + ex.ToString ());

				OnErrorReceived?.Invoke (this, ex);

			}
		}


		/// <summary>
		/// Grabs a single image from ImageProvider.
		/// </summary>
		public void OneShot ()
		{
			try {

				ImageProvider.OneShot ();

			}
			catch (Exception ex) {

				log.Fatal ("ERROR while single shotting on device with Index " + Index);
				log.Fatal ("Device Info: " + FullName);

				log.Fatal ("Exception: " + ex.ToString ());

				OnErrorReceived?.Invoke (this, ex);

			}
		}
	}
}
