

namespace ControllingAndroid
{
	internal sealed class AnonymusDisposable : IDisposable
	{
		private Action action;

		public AnonymusDisposable(Action action)
		{
			this.action = action;
		}

		public void Dispose()
		{
			action?.Invoke();
			action = null;
			GC.SuppressFinalize(this);
		}
	}
}