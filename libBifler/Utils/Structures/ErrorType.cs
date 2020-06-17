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


namespace libBifler.Utils.Structures
{
	/// <summary>
	/// GPS/TriggerDevice connection error types.
	/// </summary>
	public enum ErrorType : int
	{
		/// <summary>
		/// No errors found.
		/// </summary>
		None				=		0,


		/// <summary>
		/// Error connecting on specified port, probably port is already in-use by other applications.
		/// </summary>
		SerialConnection	=		1,


		/// <summary>
		/// The specified network port is already in use.
		/// </summary>
		NetworkPortInUse	=		2,


		/// <summary>
		/// Timeout awaiting for the connection to be made.
		/// </summary>
		NetworkTimeout		=		3,


		/// <summary>
		/// Wrong network protocols (magic number mismatch).
		/// </summary>
		NetworkProtocol		=		4
	}
}
