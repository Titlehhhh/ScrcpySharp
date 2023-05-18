using Android.Views;

namespace ControllingAndroid.Core
{
	public class PointersState
	{
		public Pointer this[int index] => pointers[index];
		private List<Pointer> pointers = new();

		private int indexOf(long id)
		{
			for (int i = 0; i < pointers.Count; ++i)
			{
				Pointer pointer = pointers[i];
				if (pointer.LocalId == id)
				{
					return i;
				}
			}
			return -1;
		}

		private bool IsLocalIdAvailable(int localId)
		{
			return !pointers.Any(x => x.LocalId == localId);
		}

		private int nextUnusedLocalId()
		{
			for (int localId = 0; localId < 10; ++localId)
			{
				if (IsLocalIdAvailable(localId))
				{
					return localId;
				}
			}


			return -1;
		}


		public int getPointerIndex(long id)
		{
			int index = indexOf(id);
			if (index != -1)
			{
				// already exists, return it
				return index;
			}
			if (pointers.Count >= 10)
			{
				// it's full
				return -1;
			}
			// id 0 is reserved for mouse events
			int localId = nextUnusedLocalId();
			if (localId == -1)
			{
				throw new Exception("pointers.size() < maxFingers implies that a local id is available");
			}
			Pointer pointer = new Pointer(id, localId);
			pointers.Add(pointer);
			// return the index of the pointer
			return pointers.Count - 1;
		}

		/**
		 * Initialize the motion event parameters.
		 *
		 * @param props  the pointer properties
		 * @param coords the pointer coordinates
		 * @return The number of items initialized (the number of pointers).
		 */
		public int update(MotionEvent.PointerProperties[] props, MotionEvent.PointerCoords[] coords)
		{
			int count = pointers.Count;
			for (int i = 0; i < count; ++i)
			{
				Pointer pointer = pointers[i];

				// id 0 is reserved for mouse events
				props[i].Id = pointer.LocalId;

				Point point = pointer.Point;
				coords[i].X = (float)point.X;
				coords[i].Y = (float)point.Y;
				coords[i].Pressure = 0.01F;
			}
			cleanUp();
			return count;
		}

		private void cleanUp()
		{
			for (int i = pointers.Count - 1; i >= 0; --i)
			{
				Pointer pointer = pointers[i];
				if (pointer.Up)
				{
					pointers.RemoveAt(i);
				}
			}
		}
	}
}

