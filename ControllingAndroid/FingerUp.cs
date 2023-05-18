

using Android.Views;
using ControllingAndroid.Core;
using System.Collections.Concurrent;

namespace ControllingAndroid
{
	internal sealed class FingerUp : IDisposable
	{
		private Controller controller;
		private long id;
		private Point position;
		BlockingCollection<Action> actions;
		List<IDisposable> disposables;
		public FingerUp(Controller controller, long id, Point position, BlockingCollection<Action> actions, List<IDisposable> disposables)
		{
			this.controller = controller;
			this.id = id;
			this.position = position;
			this.actions = actions;
			this.disposables = disposables;
		}
		private volatile bool disposed = false;
		private object _lock = new();
		public void Dispose()
		{
			lock (_lock)
			{
				if (Volatile.Read(ref disposed))
				{
					return;
				}
				Volatile.Write(ref disposed, true);
				disposables?.Remove(this);
				disposables = null;
				actions.TryAdd(() =>
				{
					controller.InjectTouch(MotionEventActions.Up, id, position);
				});
			}
		}
	}
}