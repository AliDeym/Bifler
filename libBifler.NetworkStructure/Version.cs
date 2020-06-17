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


namespace libBifler.NetworkStructure
{
	public struct Version
	{
		/// <summary>
		/// Version X, Overall format: X.Y.Z
		/// </summary>
		public byte X;

		/// <summary>
		/// Version Y, Overall format: X.Y.Z
		/// </summary>
		public byte Y;

		/// <summary>
		/// Version Z, Overall format: X.Y.Z
		/// </summary>
		public byte Z;

		/// <summary>
		/// Creates a version formatted data. Format: X.Y.Z, example: 1.9.1 | 0.5.76
		/// </summary>
		/// <param name="versionX"></param>
		/// <param name="versionY"></param>
		/// <param name="versionZ"></param>
		public Version (byte versionX, byte versionY, byte versionZ)
		{
			X = versionX;
			Y = versionY;
			Z = versionZ;
		}

		public string GetVersion ()
		{
			return X + "." + Y + "." + Z;
		}
	}
}
