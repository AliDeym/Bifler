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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Radio
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		/* Radio's core. */
		private RadioCore appCore;

		public MainPage ()
		{
			this.InitializeComponent ();
		}


		/// <summary>
		/// Retreives files from Storage.
		/// </summary>
		/// <param name="dir">directory</param>
		/// <returns></returns>
		public async Task<IReadOnlyList<StorageFile>> RetrieveFilesFromStorage (StorageFolder dir)
		{
			return await dir.GetFilesAsync ();
		}

		/// <summary>
		/// Retrieves folders from storage.
		/// </summary>
		/// <param name="dir">directory.</param>
		/// <returns></returns>
		public async Task<IReadOnlyList<StorageFolder>> RetrieveFoldersFromStorage (StorageFolder dir)
		{
			return await dir.GetFoldersAsync ();
		}

		/// <summary>
		/// Retrieves all folders in the directory.
		/// </summary>
		/// <param name="letter">Letter</param>
		/// <returns></returns>
		public async Task<IReadOnlyList<StorageFolder>> RetrieveFolderFromUSB ()
		{
			return await KnownFolders.RemovableDevices.GetFoldersAsync ();
		}

		/// <summary>
		/// Finds control by Type and Name.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="parent">Mother</param>
		/// <param name="targetType">Type</param>
		/// <param name="ControlName">Name</param>
		/// <returns></returns>
		public static T FindControl<T> (UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
		{

			if (parent == null) return null;

			if (parent.GetType () == targetType && ((T)parent).Name == ControlName) {
				return (T)parent;
			}
			T result = null;
			int count = VisualTreeHelper.GetChildrenCount (parent);
			for (int i = 0; i < count; i++) {
				UIElement child = (UIElement)VisualTreeHelper.GetChild (parent, i);

				if (FindControl<T> (child, targetType, ControlName) != null) {
					result = FindControl<T> (child, targetType, ControlName);
					break;
				}
			}
			return result;
		}

		protected override void OnNavigatedTo (NavigationEventArgs e) => OnInitialize (null, null);

		private Dictionary<int, string> playlistNames;
		private MediaPlaybackList playList;

		/// <summary>
		/// Form load.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void OnInitialize (object sender, RoutedEventArgs e)
		{

			// Create the core for radio.
			if (appCore == null) {
				appCore = new RadioCore (this);

				appCore.Init ();


				mediaPlayer.TransportControls.IsNextTrackButtonVisible = true;
				//mediaPlayer.TransportControls.IsFastRewindButtonVisible = true;
				//mediaPlayer.TransportControls.IsFastForwardButtonVisible = true;
				//mediaPlayer.TransportControls.IsSkipForwardButtonVisible = true;
				//mediaPlayer.TransportControls.IsSkipBackwardButtonVisible = true;
				mediaPlayer.TransportControls.IsPreviousTrackButtonVisible = true;

				//mediaPlayer.TransportControls.IsFastRewindEnabled = true;
				//mediaPlayer.TransportControls.IsSkipForwardEnabled = true;
				//mediaPlayer.TransportControls.IsFastForwardEnabled = true;
				//mediaPlayer.TransportControls.IsSkipBackwardEnabled = true;

				//mediaPlayer.TransportControls.IsHitTestVisible = false;
				mediaPlayer.TransportControls.IsZoomButtonVisible = false;
				mediaPlayer.TransportControls.IsFullWindowButtonVisible = false;
				mediaPlayer.TransportControls.IsPlaybackRateButtonVisible = false;

				/*mediaPlayer.MediaPlayer.CommandManager.NextReceived += MediaPlayer_Next;
				mediaPlayer.MediaPlayer.CommandManager.PreviousReceived += MediaPlayer_Previous;*/
				/*var b = GetTemplateChild ("NextTrackButton") as AppBarButton;

				b.Opacity = 0;*/
			}




			/*var picker = new FolderPicker ();

			picker.FileTypeFilter.Add (".mp3");

			var folder = await picker.PickSingleFolderAsync ();*/

			var folders = await RetrieveFolderFromUSB ();


			/* Create playlist. */
			var currentIndex = 0;

			playlistNames = new Dictionary<int, string> ();

			playList = new MediaPlaybackList ();
			playList.CurrentItemChanged += PlaylistItemChanged;



			if (folders == null) {

				OnInitialize (sender, e);

				return;
			}



			/* Create playlist. */

			foreach (var dir in folders) {

				currentIndex = await ReadDirectory (dir, currentIndex);
				var insiderFolders = await dir.GetFoldersAsync ();




				foreach (var insider in insiderFolders) {
					currentIndex = await ReadDirectory (insider, currentIndex);
				}
			}



			mediaPlayer.Source = playList;


			/*mediaPlayer.MediaFailed += Mp3Failure;
			mediaPlayer.MediaOpened += Mp3Opened;
			mediaPlayer.MediaEnded += Mp3Finished;*/


			startedText.Opacity = calibratingText.Opacity = 0;

			//appCore.HandlePlay ();

			mediaPlayer.AutoPlay = true;
		}

		/// <summary>
		/// Plays file on MP3 Player.
		/// </summary>
		/// <param name="file"></param>
		public async void PlayFile (StorageFile file)
		{
			var stream = await file.OpenReadAsync ();



			mediaPlayer.Source = MediaSource.CreateFromStream (stream, file.ContentType);

			mediaPlayer.MediaPlayer.Play ();
		}

		/// <summary>
		/// Called whenever project status gets changed.
		/// </summary>
		/// <param name="status">Wether project started or stopped.</param>
		public async void ProjectStatusChanged (bool status)
		{
			await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
				if (status) {
					startedText.Opacity = 100;

					startProjectButton.Opacity = startProjectButton.Opacity = calibratingText.Opacity = stoppedText.Opacity = 0;

					return;
				}

				stoppedText.Text = (String)Application.Current.Resources ["Stopped"];
				stoppedText.Opacity = 100;

				startProjectButton.Opacity = startProjectButton.Opacity = startedText.Opacity = calibratingText.Opacity = 0;

				
			});
		}

		/// <summary>
		/// Called whenever caliber status gets changed.
		/// </summary>
		/// <param name="status">Wether calibrating or not.</param>
		public async void CaliberStatusChanged (bool status)
		{
			await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
				if (status) {
					calibratingText.Text = (String)Application.Current.Resources ["Calibrating"];

					calibratingText.Opacity = 100;

					startProjectButton.Opacity = stoppedText.Opacity = startedText.Opacity = 0;

					return;
				}

				stoppedText.Text = (String)Application.Current.Resources ["Stopped"];
				stoppedText.Opacity = 100;

				startProjectButton.Opacity = startedText.Opacity = calibratingText.Opacity = 0;
			});
		}
		
		/// <summary>
		/// Text received for trigger, from Network manager.
		/// </summary>
		/// <param name="text">Text to input.</param>
		public async void ChangeTriggerCounter (string text)
		{
			await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
				triggerCounter.Text = text;
			});
		}

		public async void PrepareProject ()
		{
			await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
				
				calibratingText.Text = (String)Application.Current.Resources ["PendingProject"];
				calibratingText.Opacity = startProjectButton.Opacity = 100;

				

				startedText.Opacity = stoppedText.Opacity = 0;
			});
		}

		public 

		private async void OnPlaylistChange (object sender, RoutedEventArgs e)
		{
			if (mediaPlayer != null && mediaPlayer.MediaPlayer != null)
				mediaPlayer.MediaPlayer.Pause ();

			var folders = await RetrieveFolderFromUSB ();


			if (folders == null) {
				return;
			}

			/* Create playlist. */
			var currentIndex = 0;

			playlistNames = new Dictionary<int, string> ();

			playList = new MediaPlaybackList ();
			playList.CurrentItemChanged += PlaylistItemChanged;

			foreach (var dir in folders) {

				currentIndex = await ReadDirectory (dir, currentIndex);
				var insiderFolders = await dir.GetFoldersAsync ();




				foreach (var insider in insiderFolders) {
					currentIndex = await ReadDirectory (insider, currentIndex);
				}
			}


			mediaPlayer.Source = playList;

			mediaPlayer.AutoPlay = true;
		}

		private async Task<int> ReadDirectory (StorageFolder dir, int currentIndex)
		{
			var files = await dir.GetFilesAsync ();






			foreach (var file in files) {
				if (file.Name.EndsWith (".mp3")) {
					var mediaPlybackItem = new MediaPlaybackItem (MediaSource.CreateFromStorageFile (file));

					playList.Items.Add (mediaPlybackItem);

					playlistNames [currentIndex] = file.DisplayName;

					currentIndex++;
				}

				//appCore.AddFile (file);
			}

			return currentIndex;
		}




		private async void PlaylistItemChanged (MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
		{
			await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync (Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
				//var track = playList.CurrentItem.AudioTracks [0];

				if (playlistNames.Count <= 0) return;
				if (!playlistNames.ContainsKey ((int)playList.CurrentItemIndex)) return;

				musicName.Text = playlistNames [(int)playList.CurrentItemIndex];
			});
		}

		private void OnStartProjectButton (object sender, RoutedEventArgs e)
		{
			
		}
	}
}
