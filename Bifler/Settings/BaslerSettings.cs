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
using System.Windows.Forms;
using PylonC.NET;

using libBifler;

namespace Bifler
{
	/// <summary>
	/// Managed camera settings.
	/// </summary>
	public partial class BaslerSettings : Form
	{
		/* Config manager for this instance of managed camera device. */
		private ConfigManager config;

		private Camera managedCamera;

		/* Maximum exposure time. */
		private int maxExposure;

		/// <summary>
		/// Loads a camera settings form.
		/// </summary>
		/// <param name="cam">Managed .NET Camera.</param>
		/// <param name="maximumExposure">Maximum exposure.</param>
		public BaslerSettings (Camera cam, int maximumExposure = 5000)
		{
			managedCamera = cam;

			config = cam.Config;


			maxExposure = maximumExposure;


			cam.Stop ();


			InitializeComponent ();
			
		}

		private void OnInit (object sender, EventArgs e)
		{

			var imageProvider = managedCamera.ImageProvider;

			var autoNode = imageProvider.GetNodeFromDevice ("ExposureAuto");

			if (autoNode.IsValid) {
				string selected = GenApi.NodeToString (autoNode);


				if (selected == "Off")
					autoBrightnessCheckbox.Checked = false;
				else
					autoBrightnessCheckbox.Checked = true;
			}


			ExposureSlider.MyImageProvider = imageProvider;
			GainSlider.MyImageProvider = imageProvider;
			WidthSlider.MyImageProvider = imageProvider;
			HeightSlider.MyImageProvider = imageProvider;
			xPosBar.MyImageProvider = imageProvider;
			yPosBar.MyImageProvider = imageProvider;
			BrightnessBar.MyImageProvider = imageProvider;


			ExposureSlider.MaximumExposure = maxExposure;



			ExposureSlider.DeviceOpenedEventHandler ();
			GainSlider.DeviceOpenedEventHandler ();
			WidthSlider.DeviceOpenedEventHandler ();
			HeightSlider.DeviceOpenedEventHandler ();
			xPosBar.DeviceOpenedEventHandler ();
			yPosBar.DeviceOpenedEventHandler ();
			BrightnessBar.DeviceOpenedEventHandler ();


			

			cameraLabel.Text = managedCamera.Name;
		}

		/* Saves the nodes into config for future uses. */
		private void SaveIntegerNode (params string[] nodeNames)
		{
			foreach (var name in nodeNames) {
				var node = managedCamera.ImageProvider.GetNodeFromDevice (name);



				if (node.IsValid) {
					var value = GenApi.IntegerGetValue (node);
					config.SetConfig (name, value.ToString ());
				}
			}
		}

		/* Saves float nodes. */
		private void SaveFloatNode (params string [] nodeNames)
		{
			foreach (var name in nodeNames) {
				var node = managedCamera.ImageProvider.GetNodeFromDevice (name);



				if (node.IsValid) {
					var value = GenApi.FloatGetValue (node);
					config.SetConfig (name, value.ToString ());
				}
			}
		}

		/* Saves a boolean node. */
		private void SaveBooleanNode (string nodeName)
		{
			var node = managedCamera.ImageProvider.GetNodeFromDevice (nodeName);



			if (node.IsValid) {
				var value = GenApi.BooleanGetValue (node);
				config.SetConfig (nodeName, value.ToString ());
			}
		}

		/* Saves enumeration node. */
		private void SaveStringNode (string nodeName)
		{
			var node = managedCamera.ImageProvider.GetNodeFromDevice (nodeName);



			if (node.IsValid) {
				var value = GenApi.NodeToString (node);
				config.SetConfig (nodeName, value.ToString ());
			}
		}


		private void OnClosingForm (object sender, FormClosingEventArgs e)
		{
			/*var nodeX = managedCamera.ImageProvider.GetNodeFromDevice ("CenterX");
			var nodeY = managedCamera.ImageProvider.GetNodeFromDevice ("CenterY");




			if (nodeX.IsValid) {
				if (GenApi.NodeGetType (nodeX) == EGenApiNodeType.BooleanNode) {
					bool writable = GenApi.NodeIsWritable (nodeX);

					if (writable)
						GenApi.BooleanSetValue (nodeX, true);
				}
			}

			if (nodeY.IsValid) {
				if (GenApi.NodeGetType (nodeY) == EGenApiNodeType.BooleanNode) {
					bool writable = GenApi.NodeIsWritable (nodeY);

					if (writable)
						GenApi.BooleanSetValue (nodeY, true);
				}
			}*/

			if (config != null) {
				SaveIntegerNode (
					"Width", "Height",
					"OffsetX", "OffsetY"
				);

				SaveFloatNode (
					"Gain", "ExposureTime",
					"AutoTargetBrightness"
				);

				SaveStringNode ("ExposureAuto");
			}

			ExposureSlider.MyImageProvider = null;
			GainSlider.MyImageProvider = null;

			managedCamera.Start ();
		}

		private void OnCloseClick (object sender, EventArgs e)
		{
			Close ();
		}

		/* Checkbox changed for auto brightness. */
		private void AutoBrightnessChanged (object sender, EventArgs e)
		{
			var nodeAutoExposure = managedCamera.ImageProvider.GetNodeFromDevice ("ExposureAuto");




			if (nodeAutoExposure.IsValid) {
				bool writable = GenApi.NodeIsWritable (nodeAutoExposure);

				if (writable)
					GenApi.NodeFromString (nodeAutoExposure, autoBrightnessCheckbox.Checked ? "Continuous" : "Off");
			}
		}
	}
}
