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


using System.Threading;


namespace libBifler.Utils.Structures
{
	/// <summary>
	/// Contains data about thread jobs which are used by image saving scheduler.
	/// </summary>
	class ThreadInformation
	{
		/// <summary>
		/// Thread finished job that was given by scheduler or not.
		/// </summary>
		public bool HasFinished;

		/// <summary>
		/// Thread is set busy by scheduler or not.
		/// </summary>
		public bool IsBusy;

		/// <summary>
		/// Current thread ID.
		/// </summary>
		public readonly int ThreadID;

		/// <summary>
		/// Image index given by scheduler.
		/// </summary>
		public string ImageIndex;

		/// <summary>
		/// Image buffer index (used by JobWorker, not BufferManager).
		/// </summary>
		public int ReadingImageIndex;


		/// <summary>
		/// Save thread.
		/// </summary>
		public Thread MainThread { get; private set; }


		/// <summary>
		/// Creates a thread information class for saving threads.
		/// </summary>
		/// <param name="method">Save thread.</param>
		public ThreadInformation (ThreadStart method)
		{
			MainThread = new Thread (method);

			ThreadID = MainThread.ManagedThreadId;
		}


		// TODO: Write comments.
		public void SetPriorityHigh () { } /*=> MainThread.Priority = ThreadPriority.AboveNormal;*/


		/// <summary>
		/// Starts the save thread.
		/// </summary>
		public void Start () => MainThread.Start ();
	}
}
