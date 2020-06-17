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
using System.Runtime.InteropServices;

namespace libBifler.UI
{
	static class Win32UI
	{
		public const int WM_SETREDRAW = 0x0b;

		[DllImport ("user32.dll")]
		public static extern IntPtr SendMessage (IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		public static void SuspendPainting (IntPtr hWnd)
		{
			SendMessage (hWnd, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
		}

		public static void ResumePainting (IntPtr hWnd)
		{
			SendMessage (hWnd, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
		}
	}
}
