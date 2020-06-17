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




using TriggerServer;

using System.Threading.Tasks;

using Windows.Storage;
using System.Collections.Generic;
using System.IO;
using Windows.Storage.Pickers;

namespace Radio
{
	public partial class RadioCore
	{
		private readonly Version currentVersion = new Version (0, 1, 16);

		private NetworkManager radioManager = null;

		private MainPage mainPage = null;

		/// <summary>
		/// Holds the current trigger counter.
		/// </summary>
		public int Triggers { get; private set; }


		public int MusicIndex { get; private set; }


		/* Holds all files. */
		private List<StorageFile> filesList;


		/// <summary>
		/// Creates a radio core handler.
		/// </summary>
		/// <param name="sender">Creater of this instance.</param>
		public RadioCore (MainPage sender)
		{
			mainPage = sender;

			filesList = new List<StorageFile> ();

			MusicIndex = 0;
		}

		/// <summary>
		/// Initialize the radio core.
		/// </summary>
		public void Init ()
		{
			radioManager = new NetworkManager ("192.168.1.10", false, true);

			radioManager.Initialize (currentVersion);

			// Instantiate the methods.
			ReadMethods ();

			//InitUSB ();
		}

		/// <summary>
		/// Next music index.
		/// </summary>
		public void NextMusic ()
		{
			MusicIndex++;

			if (MusicIndex >= filesList.Count)
				MusicIndex = 0;

		}

		/// <summary>
		/// Next music index.
		/// </summary>
		public void PreviousMusic ()
		{
			MusicIndex--;

			if (MusicIndex < 0)
				MusicIndex = filesList.Count - 1;

			if (MusicIndex >= filesList.Count)
				MusicIndex = 0;
		}

		/// <summary>
		/// Handles playing indexed music.
		/// </summary>
		public void HandlePlay ()
		{
			if (MusicIndex >= filesList.Count)
				MusicIndex = 0;


			mainPage.PlayFile (filesList [MusicIndex]);
		}

		public void AddFile (StorageFile file)
		{
			filesList.Add (file);
		}

		/*
		/// <summary>
		/// Initializes the files on USB disk.
		/// </summary>
		public async void InitUSB (StorageFolder folder)
		{
			var files = await folder.GetFilesAsync ();

			foreach (var file in folder
				)

			var mainDir = await mainPage.RetrieveFolderFromUSB ("I");

			var files = await mainPage.RetrieveFilesFromStorage (mainDir);

			foreach (var file in files) {
				if (file != null) {
					if (file.Name.EndsWith (".mp3")) {
						filesList.Add (file);
					}
				}
			}

			var dirs = await mainPage.RetrieveFoldersFromStorage (mainDir);

			foreach (var dir in dirs) {
				if (dir != null) {

					var dirFiles = await mainPage.RetrieveFilesFromStorage (dir);

					foreach (var dirFile in dirFiles) {

						if (dirFile != null) {

							if (dirFile.Name.EndsWith (".mp3")) {
								filesList.Add (dirFile);
							}

						}
					}

				}*/

	}

}
