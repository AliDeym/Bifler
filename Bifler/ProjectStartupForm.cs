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
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Threading.Tasks;

using libBifler;

namespace Bifler
{
	/// <summary>
	/// Creates a project start form.
	/// </summary>
	public partial class ProjectStartupForm : Form
	{
		private string _projectPath;

		/* Application core. */
		Core appCore = null;

		ConfigManager projectStartupConfig = null;

		/// <summary>
		/// Initializes the form.
		/// </summary>
		/// <param name="sender">Application's core.</param>
		public ProjectStartupForm (Core sender)
		{
			_projectPath = "projects/";

			InitializeComponent ();

			appCore = sender;
		}

		private void OnCancellation (object sender, EventArgs e)
		{
			Close ();
		}

		[DllImport ("kernel32.dll", SetLastError = true)]
		[return: MarshalAs (UnmanagedType.Bool)]
		static extern bool AllocConsole ();


		[DllImport ("kernel32", SetLastError = true)]
		static extern bool AttachConsole (int dwProcessId);

		[DllImport ("user32.dll")]
		static extern IntPtr GetForegroundWindow ();

		[DllImport ("user32.dll", SetLastError = true)]
		static extern uint GetWindowThreadProcessId (IntPtr hWnd, out int lpdwProcessId);



		private void SaveButtonOnConfigTextBox (Control control)
		{
			projectStartupConfig.SetConfig (control.Name, control.Text);
		}

		private void SaveButtonOnConfigNumeric (Control control)
		{
			projectStartupConfig.SetConfig (control.Name, ((NumericUpDown)control).Value.ToString ());
		}


		/* Start project button. */
		private void OnStartReaction (object sender, EventArgs e)
		{
			SaveButtonOnConfigTextBox (nameBox);
			SaveButtonOnConfigTextBox (provinceCodeBox);
			SaveButtonOnConfigTextBox (codeBox);
			SaveButtonOnConfigTextBox (startBox);
			SaveButtonOnConfigTextBox (finishBox);
			SaveButtonOnConfigTextBox (provinceBox);
			SaveButtonOnConfigTextBox (cityBox);
			

			SaveButtonOnConfigTextBox (directionBox);


			SaveButtonOnConfigNumeric (startKilometerNumber);
			SaveButtonOnConfigNumeric (laneBox);
			SaveButtonOnConfigNumeric (projectLane);


			/* Managed folders (categorized) for project. */
			var checkDirs = new [] {
				_projectPath + "\\",
				_projectPath + "\\" + nameBox.Text + "\\",
				_projectPath + "\\" + nameBox.Text + "\\" + provinceBox.Text + " - " + cityBox.Text + "\\",
				_projectPath + "\\" + nameBox.Text + "\\" + provinceBox.Text + " - " + cityBox.Text + "\\" + startBox.Text + " - " + finishBox.Text + "\\",
				_projectPath + "\\" + nameBox.Text + "\\" + provinceBox.Text + " - " + cityBox.Text + "\\" + startBox.Text + " - " + finishBox.Text + "\\" + Language.Lane + " " + projectLane.Value.ToString() + "\\"
			};

			try {
				for (var i = 0; i < checkDirs.Length; i++) {

					if (!Directory.Exists (checkDirs [i]))
						Directory.CreateDirectory (checkDirs [i]);

				}
			} catch {
				MessageBox.Show (Language.FolderError, Language.Error);
				return;
			}

			var projectFolder = checkDirs [checkDirs.Length - 1];


			appCore.Projects.InitializeProject (codeBox.Text,
				nameBox.Text, startBox.Text, finishBox.Text,
				cityBox.Text, provinceCodeBox.Text, provinceBox.Text,
				directionBox.Text,
				(int)laneBox.Value, appCore.GetGPSData (), (int)startKilometerNumber.Value, appCore.Trigger.Caliber,
				(int)projectLane.Value,
				projectFolder);



			appCore.Projects.StartProject ();
			appCore.Trigger.StartProject ();


			//appCore.Trigger.StartTrigger ();



			Close ();
		}

		private void SetPathInvoke (string path)
		{
			if (path == null || String.IsNullOrWhiteSpace (path)) return;

			if (InvokeRequired) {
				Action<string> act = SetPathInvoke;

				Invoke (act, path);

				return;
			}

			_projectPath = path;
			pathBox.Text = path;

			// Set path on config for next uses.
			appCore.MainConfigManager.SetConfig ("ProjectPath", path);
		}

		private void ChooseButtonClick (object sender, EventArgs e)
		{
			/* Create STA Thread, because we're calling OLE objects from COM library which require STA, not MTA. */

			var staThread = new System.Threading.Thread (() => {

				var fd = new FolderBrowserDialog () {
					Description = "Project Folder",
					ShowNewFolderButton = true
				};



				fd.ShowDialog ();


				SetPathInvoke (fd.SelectedPath);
			});

			staThread.SetApartmentState (System.Threading.ApartmentState.STA);
			staThread.Start ();


			

			
		}

		private void LoadConfigForTextBox (Control control)
		{
			if (!String.IsNullOrWhiteSpace (projectStartupConfig.GetConfig (control.Name)))
				control.Text = projectStartupConfig.GetConfig (control.Name);
		}

		private void LoadConfigForNumeric (Control control)
		{
			if (!String.IsNullOrWhiteSpace (projectStartupConfig.GetConfig (control.Name))) {
				control.Text = projectStartupConfig.GetConfig (control.Name);

				if (int.TryParse (control.Text, out int res))
					((NumericUpDown)control).Value = res;
			}
		}


		/* Load project path from config. */
		private void FormLoaded (object sender, EventArgs e)
		{
			projectStartupConfig = new ConfigManager ("projectinfo");

			LoadConfigForTextBox (nameBox);
			LoadConfigForTextBox (provinceCodeBox);
			LoadConfigForTextBox (codeBox);
			LoadConfigForTextBox (startBox);
			LoadConfigForTextBox (finishBox);
			LoadConfigForTextBox (provinceBox);
			LoadConfigForTextBox (cityBox);

			LoadConfigForTextBox (directionBox);

			LoadConfigForNumeric (startKilometerNumber);
			LoadConfigForNumeric (laneBox);
			LoadConfigForNumeric (projectLane);



			var configPath = appCore.MainConfigManager.GetConfig ("ProjectPath");

			if (configPath != null && configPath != "") {

				_projectPath = configPath;
				pathBox.Text = configPath;

			}
		}
	}
}
