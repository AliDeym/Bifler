﻿/*
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

namespace libBifler.Algorithms
{
	class DramaticBuffer<T>
	{
		private T [] nodes;
		private bool [] dequeued;

		private int writeBuffer = 0;
		private int readBuffer = 0;

		private int size;

		public DramaticBuffer (int bufferSize)
		{
			nodes = new T [bufferSize];
			dequeued = new bool [bufferSize];

			size = bufferSize;
		}

		public void Enqueue (T item)
		{
			nodes [writeBuffer] = item;
			dequeued [writeBuffer] = false;

			writeBuffer++;

			if (writeBuffer >= size)
				writeBuffer = 0;
		}


		public T Dequeue ()
		{
			T returnedObject = default(T);

			if (nodes [readBuffer] != null && !dequeued [readBuffer]) {

				dequeued [readBuffer] = true;

				returnedObject = nodes [readBuffer];

				readBuffer++;

				if (readBuffer >= size)
					readBuffer = 0;
					
			}

			return returnedObject;
		}
	}
}
