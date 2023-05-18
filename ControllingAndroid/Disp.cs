

namespace ControllingAndroid
{
	internal static class Disp
	{
		public static IDisposable Create(Action action)
		{
			return new AnonymusDisposable(action);
		}
	}
}