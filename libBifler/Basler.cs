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
using System.Collections.Generic;

using libBifler.Project;

using PylonC.NET;
using PylonC.NETSupportLibrary;

namespace libBifler
{
	/// <summary>
	/// Managed Pylon handler.
	/// </summary>
	public class Basler : IDisposable
	{
		private List<DeviceEnumerator.Device> rawDevices;
		private List<Camera> cameraDevices;
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		/// <summary>
		/// Indicates wether the Basler Pylon library is loaded successfully or not.
		/// </summary>
		public bool Loaded { get; private set; }


		/// <summary>
		/// Close Pylon library in case of Manual Garbage Collection (GC).
		/// </summary>
		~Basler ()
		{
			Dispose ();
		}


		/// <summary>
		/// Disposes the Basler Manager and closes all connected cameras.
		/// </summary>
		public void Dispose ()
		{
			foreach (var cam in cameraDevices) {

				cam.Close ();

				cam.Dispose ();
			}

			Pylon.Terminate ();
		}


		private ProjectManager project;



		/// <summary>
		/// Initializes a BaslerManager object.
		/// </summary>
		public Basler (ProjectManager projectMgr)
		{
			project = projectMgr;


			logger.Info ("Initializing Pylon library...");


			try {
				Pylon.Initialize ();
			}
			catch (Exception ex) {
				logger.Fatal ("Pylon library load failed, Exception: " + ex.ToString ());

				Loaded = false;
			}
			finally {
				logger.Info ("Pylon library successfully initialized.");

				Loaded = true;

				Initialize ();
			}
		}

		
		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize ()
		{
			rawDevices = new List<DeviceEnumerator.Device> ();
			cameraDevices = new List<Camera> ();

			logger.Info ("Initialized Pylon library successfully.");

			
		}


		/// <summary>
		/// Refreshes the Device-list.
		/// </summary>
		public void RefreshDevices()
		{
			logger.Info ("Refreshing devices list...");


			try {
				rawDevices = DeviceEnumerator.EnumerateDevices ();
			}
			catch (Exception ex) {
				logger.Fatal ("ERROR Refreshing devices list: " + ex.ToString () + ".");
			}
			finally {
				logger.Info ("Successfully refreshed the device list.");
			}


		}


		/// <summary>
		/// Creates a DOTNET Connection to the Camera.
		/// </summary>
		/// <param name="deviceIndex">Basler Camera Index.</param>
		/// <returns>Camera device or NULL.</returns>
		public Camera AddCamera (int deviceIndex)
		{
			if (rawDevices.Count <= 0 || rawDevices [deviceIndex] == null) {
				logger.Error ("ERROR: Camera device not found using index '" + deviceIndex + "'.");

				return null;
			}

			var cam = new Camera (GetDeviceInfo (deviceIndex), project);


			cameraDevices.Add (cam);


			logger.Info ("Camera created on Basler Manager. Device info: \n" +
				"\tIndex: " + cam.Index + "\n" +
				"\tName: " + cam.Name + "\n" +
				"\tFullname: " + cam.FullName + "\n" +
				"\tTooltip: " + cam.Tooltip + ".");



			return cam;
		}


		/// <summary>
		/// Safely removes a Camera object. does nothing if camera doesn't exist.
		/// </summary>
		/// <param name="deviceIndex">Basler Camera Index.</param>
		public void RemoveCamera (int deviceIndex)
		{
			if (rawDevices.Count <= 0 || rawDevices [deviceIndex] == null) return;

			foreach (var cam in cameraDevices) {

				if (cam.Index == deviceIndex) {

					cameraDevices.Remove (cam);

				}

			}

			GC.Collect ();

		}


		/// <summary>
		/// Returns a copy list of Pylon Devices.
		/// </summary>
		/// <returns></returns>
		public DeviceEnumerator.Device [] GetDeviceList () => rawDevices.ToArray ();


		/// <summary>
		/// Returns a copy list of Managed DOTNET Cameras.
		/// </summary>
		/// <returns></returns>
		public Camera [] GetManagedCameras () => cameraDevices.ToArray ();


		private DeviceEnumerator.Device GetDeviceInfo (int deviceIndex) => rawDevices [deviceIndex];
	}
}
