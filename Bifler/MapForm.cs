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
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;


using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using libBifler;
using libBifler.Project;



namespace Bifler
{
	/// <summary>
	/// Creates a map display form.
	/// </summary>
	public partial class MapForm : Form
	{
		/// <summary>
		/// Project data. Contains the overlay and the bool to indicate wether it's being shown or not. Third item being path to project.
		/// </summary>
		private Dictionary<string, Tuple<GMapOverlay, bool, string>> ProjectsData = null;

		private Dictionary<string, MapDataHandler> NodeHandler = null;




		private Random colorRandomizer = null;

		private ConfigManager excludeList = null;
		private ConfigManager colorList = null;

		private PersianCalendar persianCal = null;



		/* Core received from senders (usually the map menu button.) */
		private Core appCore = null;

		/// <summary>
		/// Initializes the map form.
		/// </summary>
		/// <param name="senderCore">Application's core.</param>
		public MapForm (Core senderCore)
		{
			InitializeComponent ();

			appCore = senderCore;

			
		}

		/* Handles loading the map form. */
		private void OnInitialization (object sender, EventArgs e)
		{
			/* Initialize private objects. */
			ProjectsData = new Dictionary<string, Tuple<GMapOverlay, bool, string>> ();
			NodeHandler = new Dictionary<string, MapDataHandler> ();

			colorRandomizer = new Random ();


			excludeList = new ConfigManager ("mapExclude");
			colorList = new ConfigManager ("mapColor");


			persianCal = new PersianCalendar ();
			

			if (!Directory.Exists ("mapdata"))
				Directory.CreateDirectory ("mapdata");

			if (!Directory.Exists ("mapimage"))
				Directory.CreateDirectory ("mapimage");



			gmap.MapProvider = GoogleMapProvider.Instance;

			GMaps.Instance.Mode = AccessMode.ServerAndCache;

			gmap.CacheLocation = "mapcache";

			gmap.Position = new PointLatLng (35.8, 50.9);

			/* Load marker size from config. if it doesn't exist, then create it using defaults. */
			var markerSize = 10;

			var confMarkerSize = appCore.MainConfigManager.GetConfig ("MapMarkerSize");

			if (confMarkerSize != null) 
				int.TryParse (confMarkerSize, out markerSize);
			else
				appCore.MainConfigManager.SetConfig ("MapMarkerSize", markerSize.ToString ());


			/* Load all existing projects data. */

			var dirs = new DirectoryInfo ("mapdata").GetDirectories ();

			foreach (var dir in dirs) {

				var headDir = dir.Name;

				/* HEADER (project name) Node. */
				TreeNode headNode = null; //projectsBox.Nodes.Add (headDir);
				//headNode.Checked = true;

				var provinceDirectories = dir.GetDirectories ();

				foreach (var provinceDir in provinceDirectories) {

					/* PROVINCE Node. */
					TreeNode provinceNode = null; //headNode.Nodes.Add (provinceDir.Name);
					//provinceNode.Checked = true;

					var cityDirectories = provinceDir.GetDirectories ();

					foreach (var cityDir in cityDirectories) {

						var startAreaDirectories = cityDir.GetDirectories ();

						foreach (var startArea in startAreaDirectories) {

							var files = startArea.GetFiles ();

							foreach (var finishArea in files) {

								var fileName = finishArea.Name.Substring (0, finishArea.Name.Length - 8);

								var mapHandler = new MapDataHandler (headDir, provinceDir.Name, cityDir.Name, startArea.Name, fileName, 0);


								NodeHandler [mapHandler.GetFriendlyName ()] = mapHandler;

								var excludedNode = excludeList.GetConfig (mapHandler.GetFriendlyName ());

								bool isExcluded = false;

								if (excludedNode != null && bool.TryParse (excludedNode, out isExcluded)) {
									// This line of code is set, because we set IsVisible on config, we want IsExcluded, so the project is Excluded only
									// when it's not visible. For doing this, we need !IsVisible.
									isExcluded = !isExcluded;

									

								}

								// Node is not disabled, so lets load it.
								if (!isExcluded)
									mapHandler.HandleLoad ();


								/* Create the un-existing nodes. */
								if (headNode == null) {
									headNode = projectsBox.Nodes.Add (headDir);

									headNode.ContextMenuStrip = rightClickMenu;

									headNode.Checked = true;
								}
								if (provinceNode == null) {
									provinceNode = headNode.Nodes.Add (provinceDir.Name + " - " + cityDir.Name);

									provinceNode.ContextMenuStrip = rightClickMenu;

									provinceNode.Checked = true;
								}

								/* CITY Node. */
								var handlerBox = provinceNode.Nodes.Add (mapHandler.GetFriendlyName (), startArea.Name + " - " + fileName);

								handlerBox.ContextMenuStrip = rightClickMenu;

								handlerBox.Checked = !isExcluded;


								// Create the specific overlay on map.

								var overlay = new GMapOverlay (mapHandler.GetFriendlyName ());

								// Create the route only in-case of project being enabled.
								if (mapHandler.IsLoaded) {
									var col = colorList.GetConfig (mapHandler.GetFriendlyName ());

									var selectedCol = default (Color);

									/* Load saved color configs. */
									if (col != null && int.TryParse(col, out int value)) {
										selectedCol = Color.FromArgb (value);
									}


									handleDataCreation (mapHandler, overlay, selectedCol);
								}

								/* Set the node text label color same as Pen color received from first available route. */
								if (overlay.Routes.Count > 0) {
									var strokeColor = overlay.Routes.First ().Stroke.Color;

									handlerBox.ForeColor = strokeColor;

									/* Handle single child nodes and their parents */

									if (files.Length <= 1) {

										/* Parent */
										if (handlerBox.Parent != null) {

											handlerBox.Parent.ForeColor = strokeColor;

											/* Parent of parent. */
											if (handlerBox.Parent.Parent != null) {

												handlerBox.Parent.Parent.ForeColor = strokeColor;

											}

										}

									}
								}


								/* Create the project data tuple for display on-off functionality. */
								ProjectsData [mapHandler.GetFriendlyName ()] = new Tuple<GMapOverlay, bool, string> (overlay, true, mapHandler.GetFileName ());

								gmap.Overlays.Add (overlay);
							}
						}
					}

					/* Handle check of Province nodes. */

					/*if (provinceNode != null && provinceNode.Nodes != null && provinceNode.Nodes.Count >= 1) {

						var anyEnabled = false;

						foreach (var node in provinceNode.Nodes) {

							if (((TreeNode)node).Checked)
								anyEnabled = true;

						}

						provinceNode.Checked = anyEnabled;

					}*/

				}

				/* Check for Head nodes. */
				/*if (headNode != null && headNode.Nodes != null && headNode.Nodes.Count >= 1) {

					var anyEnabled = false;

					foreach (var node in headNode.Nodes) {

						if (((TreeNode)node).Checked)
							anyEnabled = true;

					}

					headNode.Checked = anyEnabled;

				}*/


			}
		}

		/* Displays a persian formatted date & time. */
		private string displayPersianTime (DateTime time) =>
			persianCal.GetYear (time) + "/" + persianCal.GetMonth (time) +
					"/" + persianCal.GetDayOfMonth (time) + " " +
					persianCal.GetHour (time) + ":" + persianCal.GetMinute (time) + ":" + persianCal.GetSecond (time) + "." + persianCal.GetMilliseconds (time);

		private string filePersianTime (DateTime time) =>
			persianCal.GetYear (time) + "-" + persianCal.GetMonth (time) +
					"-" + persianCal.GetDayOfMonth (time) + " " +
					persianCal.GetHour (time) + "-" + persianCal.GetMinute (time) + "-" + persianCal.GetSecond (time) + "-" + persianCal.GetMilliseconds (time);


		/* Handles data creation on map for the Data Handler. this function creates routes and points. also tooltips. */
		private void handleDataCreation (MapDataHandler nodeHandler, GMapOverlay overlay, Color selectionColor = default(Color))
		{
			if (selectionColor == default (Color))
				selectionColor = Color.DarkGreen;

			var points = new List<PointLatLng> ();

			/* Get all the points from data. */
			foreach (var data in nodeHandler.Data)
				points.Add (new PointLatLng (data.Latitude, data.Longitude));


			/* We need two points for tooltips. */
			if (points.Count > 1) {

				var startPoint = new GMarkerGoogle (points.First (), GMarkerGoogleType.white_small);
				var finishPoint = new GMarkerGoogle (points.Last (), GMarkerGoogleType.black_small);



                finishPoint.ToolTipText = startPoint.ToolTipText = Language.ProjectName + ": " + nodeHandler.ProjectName + "\n" +
                    Language.ProjectCode + ": " + nodeHandler.ProjectCode + "\n" +
					Language.GrabCount + ": " + nodeHandler.TriggerCount + " " + Language.Meter + "\n\n" + 
                    Language.ProjectStartTime + ": " + displayPersianTime(nodeHandler.ProjectStartTime) + "\n" +
                    Language.ProjectFinishTime + ": " + displayPersianTime(nodeHandler.ProjectFinishTime) + "";



				startPoint.ToolTip.Font = finishPoint.ToolTip.Font = new Font ("B Nazanin", 14f);

				startPoint.ToolTipText = Language.StartArea + "\n\n" + startPoint.ToolTipText;
				finishPoint.ToolTipText = Language.FinishArea + "\n\n" + finishPoint.ToolTipText;



                /* White pin's tooltip (Start point). */
                startPoint.ToolTip.Fill = Brushes.Black;
				startPoint.ToolTip.Foreground = Brushes.White;
				startPoint.ToolTip.Stroke = Pens.Black;
				startPoint.ToolTip.TextPadding = new Size (20, 20);

				

				/* Black pin's tooltip (Finish point). */

				finishPoint.ToolTip.Fill = Brushes.White;
				finishPoint.ToolTip.Foreground = Brushes.Black;
				finishPoint.ToolTip.Stroke = Pens.White;
				finishPoint.ToolTip.TextPadding = new Size (20, 20);


				overlay.Markers.Add (startPoint);
				overlay.Markers.Add (finishPoint);

			}

			/* Create the pen and route. */
			var route = new GMapRoute (points, nodeHandler.GetFriendlyName ());


			/* Load marker size from config. if it doesn't exist, then create it using defaults. */
			var markerSize = 10;

			var confMarkerSize = appCore.MainConfigManager.GetConfig ("MapMarkerSize");

			if (confMarkerSize != null)
				int.TryParse (confMarkerSize, out markerSize);
			else
				appCore.MainConfigManager.SetConfig ("MapMarkerSize", markerSize.ToString ());



			var penColor = selectionColor;

			// Set random pen color.
			if (selectionColor == default(Color)) {
				penColor = Color.FromArgb (255,
				colorRandomizer.Next (256),
				colorRandomizer.Next (256),
				colorRandomizer.Next (256));
			}

			colorList.SetConfig (nodeHandler.GetFriendlyName (), penColor.ToArgb().ToString ());


			route.Stroke = new Pen (penColor, markerSize);



			overlay.Routes.Add (route);


		}

		/// <summary>
		/// Handles data draw on map, wether to display or no-longer display the points.
		/// </summary>
		/// <param name="sender">The node that draw change is occuring on it.</param>
		/// <param name="friendlyName">Handler name.</param>
		/// <param name="drawn">Wether specified data should be drawn or not.</param>
		private void HandleDrawChange (TreeNode sender, string friendlyName, bool drawn)
		{
			if (!ProjectsData.ContainsKey (friendlyName)) return;

			// Overlay is not present at all.
			if (!NodeHandler.ContainsKey (friendlyName)) return;


			if (NodeHandler.ContainsKey (friendlyName) && drawn && ProjectsData.ContainsKey(friendlyName)) {

				var nodeHandler = NodeHandler [friendlyName];

				// Specified node is not loaded into the map.
				if (!nodeHandler.IsLoaded) {
					
					// Load data points.
					nodeHandler.HandleLoad ();

					/* Load color config for the node. */
					var colConf = colorList.GetConfig (friendlyName);

					var selectedCol = default (Color);

					/* Load saved color configs. */
					if (colConf != null && int.TryParse (colConf, out int value)) {
						selectedCol = Color.FromArgb (value);
					}

					// Create data points on map overlay.
					handleDataCreation (nodeHandler, ProjectsData [friendlyName].Item1, selectedCol);

					/* Set the node text label color same as Pen color received from first available route. */
					if (ProjectsData [friendlyName].Item1.Routes.Count > 0) {
						var col = ProjectsData [friendlyName].Item1.Routes.First ().Stroke.Color;

						sender.ForeColor = col;

						if (sender.Parent != null && sender.Parent.Nodes.Count <= 1) {
							sender.Parent.ForeColor = col;

							if (sender.Parent.Parent != null && sender.Parent.Parent.Nodes.Count <= 1) {
								sender.Parent.Parent.ForeColor = col;
							}
						}

					}

				}

			}

			ProjectsData [friendlyName].Item1.IsVisibile = drawn;

			// Set in config for future uses.
			excludeList.SetConfig (friendlyName, drawn.ToString ());
		}

		/* GMap position changed using right-mouse button. */
		private void OnPositionChanged (PointLatLng point)
		{
			lonBox.Text = point.Lng.ToString ();
			latBox.Text = point.Lat.ToString ();
		}

		/* GMap zoom change using scrollbar. */
		private void OnZoomChanged ()
		{
			zoomBar.Value = (int)gmap.Zoom;
		}

		/* Manual zoom bar value change. */
		private void OnZoomBarMove (object sender, EventArgs e)
		{
			gmap.Zoom = zoomBar.Value;
		}


		/* Automated update of map from Core's GPS Device. defaults every 5 seconds. */
		private void OnUpdateInterval (object sender, EventArgs e)
		{
			if (appCore == null) return;

			if (!automateMoveCheckbox.Checked) return;

			/* Get GPS Data from Core. */
			var gpsData = appCore.GetGPSData ();



			/* Check if it's empty/signal failure. */
			if (string.IsNullOrWhiteSpace (gpsData.Latitude)) return;
			if (string.IsNullOrWhiteSpace (gpsData.Longitude)) return;


			/* Cast it to double and in-case of failure, stop. */
			if (!double.TryParse (gpsData.Longitude, out double lon)) return;
			if (!double.TryParse (gpsData.Latitude, out double lat)) return;



			lonBox.Text = lon.ToString ();
			latBox.Text = lat.ToString ();

			
			gmap.Position = new PointLatLng (lat, lon);


		}

		private void OnPositionManualChange (object sender, EventArgs e)
		{
			/* Cast our text boxes into double. */
			if (!double.TryParse (lonBox.Text, out double lon)) return;
			if (!double.TryParse (latBox.Text, out double lat)) return;


			gmap.Position = new PointLatLng (lat, lon);

		}

		/* Called on checkbox(projects) status change. */
		private void OnVisibilityChange (object sender, TreeViewEventArgs e)
		{
			HandleDrawChange (e.Node, e.Node.Name, e.Node.Checked);

			foreach (var node in e.Node.Nodes) {
				((TreeNode)node).Checked = e.Node.Checked;
			}

			// Check the boxes if they have only one child.
			if (e.Node.Parent != null && e.Node.Parent.Nodes.Count <= 1) {

				if (e.Node.Parent.Checked == e.Node.Checked) return;

				e.Node.Parent.Checked = e.Node.Checked;

			}


			
			/*if (e.Node.Parent != null && e.Node.Parent.Nodes != null && e.Node.Parent.Nodes.Count <= 1) {

				e.Node.Parent.Checked = e.Node.Checked;

				if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Nodes.Count <= 1) {
					e.Node.Parent.Parent.Checked = e.Node.Checked;
				}

			}*/
		}
		
		/* Handles position changing to node's start pos. */
		private void OnNodeDoubleClick (object sender, TreeNodeMouseClickEventArgs e)
		{
			if (ProjectsData.ContainsKey (e.Node.Name)) {

				var overlay = ProjectsData [e.Node.Name].Item1;

				if (overlay.Routes.Count > 0) {

					var pointer = overlay.Routes.First ().From;

					if (pointer != null) {
						gmap.Position = (PointLatLng)pointer;
					}

				}
			}
			else {
				if (e.Node.Nodes != null && e.Node.Nodes.Count > 0) {

					OnNodeDoubleClick (sender,
						new TreeNodeMouseClickEventArgs (e.Node.Nodes [0],
						e.Button, e.Clicks,
						e.X, e.Y));

				}
			}
		}

		/* Handles node deleting sequential. */
		private void HandleDeleteNode (TreeNode node)
		{
			if (node == null) return;


			if (ProjectsData.ContainsKey (node.Name)) {

				var data = ProjectsData [node.Name];

				var overlay = data.Item1;

				if (overlay != null)
					overlay.Clear ();

				if (File.Exists (data.Item3))
					File.Delete (data.Item3);

				node.Remove ();


				ProjectsData.Remove (node.Name);

			}
			else {

				if (node.Nodes != null && node.Nodes.Count > 0) {

					foreach (var child in node.Nodes) {
						if (child != null && child is TreeNode)
							HandleDeleteNode ((TreeNode)child);
					}
				}

				node.Remove ();

			}
		}

		/* Handles deleting nodes. */
		private void OnDeletePress (object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Delete) return;

			HandleDeleteNode (projectsBox.SelectedNode);
		}

		/* Handles color changing. */
		private void HandleColorChange (TreeNode node, Color col)
		{
			if (node != null) {
				if (ProjectsData.ContainsKey (node.Name) && NodeHandler.ContainsKey (node.Name)) {

					var overlay = ProjectsData [node.Name].Item1;

					if (overlay != null) {
						overlay.Clear ();
					}



					handleDataCreation (NodeHandler[node.Name], ProjectsData [node.Name].Item1, col);

					node.ForeColor = col;

					// Set parents colors.
					if (node.Parent != null && node.Parent.Nodes.Count <= 1) {
						node.Parent.ForeColor = col;

						if (node.Parent.Parent != null && node.Parent.Parent.Nodes.Count <= 1) {
							node.Parent.Parent.ForeColor = col;
						}
					}
				}
				else {
					if (node.Nodes != null && node.Nodes.Count > 0) {

						HandleColorChange (node.Nodes [0], col);

					}
				}
			}
		}


		/* Context menu click for color changing. */
		private void changeColorContextClick (object sender, EventArgs e)
		{
			colorPicker.ShowDialog ();

			var col = colorPicker.Color;

			if (projectsBox.SelectedNode != null) {

				HandleColorChange (projectsBox.SelectedNode, col);

			}
		}

		/* Handle map image saving. */
		private void OnSaveImageButtonClicked (object sender, EventArgs e)
		{
			var image = gmap.ToImage ();


			image.Save ("mapimage\\" + filePersianTime(DateTime.Now) + ".png", System.Drawing.Imaging.ImageFormat.Png);


			var previewInfo = new ProcessStartInfo () {
				FileName = Application.StartupPath + "\\mapimage\\",
				Verb = "open",
				CreateNoWindow = false
			};

			Process.Start (previewInfo);
		}

		private void OnPinpointClick (GMapMarker item, MouseEventArgs e)
		{
			// Toggles always on of pinpoints if you click them.
			item.ToolTipMode = item.ToolTipMode == MarkerTooltipMode.OnMouseOver ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
		}
	}
}
