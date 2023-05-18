using Android.Content;
using Android.Hardware.Input;
using Android.Views;
using Java.Lang.Reflect;

namespace ControllingAndroid.Core
{
	public class InputManagerWrapper
	{

		public static int INJECT_INPUT_EVENT_MODE_ASYNC = 0;
		public static int INJECT_INPUT_EVENT_MODE_WAIT_FOR_RESULT = 1;
		public static int INJECT_INPUT_EVENT_MODE_WAIT_FOR_FINISH = 2;

		private InputManager inputManager;
		private Method method;
		public InputManagerWrapper()
		{
			inputManager = (InputManager)Android.App.Application.Context.GetSystemService(Context.InputService);
			method = inputManager.Class.GetMethods().First(x => x.Name == "injectInputEvent");

		}

		public bool injectInputEvent(InputEvent inputEvent, int mode)
		{
			return (bool)method.Invoke(inputManager, inputEvent, 2);
		}
	}
}

