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


namespace TriggerServer
{
	public enum OpCodes : byte
	{
		/* Handshake */
		Handshake				=	1,
		

		/* Shared */
		HeartbeatRequest		=	2,
		HeartbeatTransmit		=	3,
		CaliberRequest			=	4,
		CaliberTransmit			=	5,
		ProjectRequest			=	6,


		/* Master only */
		Trigger					=	7,



		/* Pending packet, Added on V3. */
		PendingRequest			=	8,
		ErrorReceived			=	9
	}
}
