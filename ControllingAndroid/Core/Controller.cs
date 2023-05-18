using Android.OS;
using Android.Views;

namespace ControllingAndroid.Core
{
	class Controller
	{
		private static int POINTER_ID_MOUSE = -1;
		private static int POINTER_ID_VIRTUAL_MOUSE = -3;

		private PointersState pointersState = new PointersState();
		private long lastTouchDown;
		private MotionEvent.PointerProperties[] pointerProperties = new MotionEvent.PointerProperties[10];
		private MotionEvent.PointerCoords[] pointerCoords = new MotionEvent.PointerCoords[10];


		private InputManagerWrapper inputManager = ServiceManager.getInputManager();

		public Controller()
		{
			for (int i = 0; i < 10; i++)
			{
				var p = new MotionEvent.PointerProperties();
				p.ToolType = MotionEventToolType.Finger;
				pointerProperties[i] = p;
				var g = new MotionEvent.PointerCoords();
				g.Orientation = 0;
				g.Size = 0;
				pointerCoords[i] = g;

			}
		}

		public bool InjectTouch(MotionEventActions action, long pointerId, Point point)
		{
			long now = SystemClock.UptimeMillis();


			int pointerIndex = pointersState.getPointerIndex(pointerId);
			if (pointerIndex == -1)
			{
				//Ln.w("Too many pointers for touch event");
				return false;
			}
			Pointer pointer = pointersState[pointerIndex];
			pointer.Point = point;
			pointer.Pressure = 0.01F;



			// POINTER_ID_GENERIC_FINGER, POINTER_ID_VIRTUAL_FINGER or real touch from device
			pointerProperties[pointerIndex].ToolType = MotionEventToolType.Finger;
			InputSourceType source = InputSourceType.Touchscreen;
			// Buttons must not be set for touch events
			
			pointer.Up = action == MotionEventActions.Up;


			int pointerCount = pointersState.update(pointerProperties, pointerCoords);
			if (pointerCount == 1)
			{
				if (action == MotionEventActions.Down)
				{
					lastTouchDown = now;
				}
			}
			else
			{
				// secondary pointers must use ACTION_POINTER_* ORed with the pointerIndex
				if (action == MotionEventActions.Up)
				{
					//MotionEvent.ACTION_POINTER_INDEX_SHIFT
					action = (MotionEventActions)((int)MotionEventActions.PointerUp | (pointerIndex << ((int)MotionEventActions.PointerIndexShift)));
				}
				else if (action == MotionEventActions.Down)
				{

					action = (MotionEventActions)(((int)MotionEventActions.PointerDown) | (pointerIndex << ((int)MotionEventActions.PointerIndexShift)));
				}
			}
			MotionEvent motionEvent = MotionEvent
					.Obtain(lastTouchDown, now, action, pointerCount, pointerProperties, pointerCoords, 0, MotionEventButtonState.Primary, 1f, 1f, 0, 0, source,
							0);
			return inputManager.injectInputEvent(motionEvent, 0);
		}
	}
}

