

using Android.Views;
using ControllingAndroid.Core;
using System.Collections.Concurrent;

namespace ControllingAndroid
{
	public class Screen : IScreen
	{
		public List<IDisposable> Fingers { get; } = new();
		private Controller controller;
		private BlockingCollection<Action> actions = new();
		public Screen()
		{
			controller = new Controller();
			Task.Run(() =>
			{
				foreach (Action action in actions.GetConsumingEnumerable())
				{
					action.Invoke();
				}
			});

		}
		public IDisposable Move(Point vector, TimeSpan timeout)
		{
			return Disp.Create(() => { });
		}
		private HashSet<long> ids = new();
		public IDisposable Press(Point position)
		{
			long id = -1;
			lock (ids)
			{

				for (long i = 0; i < 10; i++)
				{
					if (!ids.Contains(i))
					{
						id = i;
						break;
					}
				}


				if (id == -1)
					throw new Exception("Все пальцы заняты");
				ids.Add(id);
			}

			actions.TryAdd(() =>
			{
				controller.InjectTouch(MotionEventActions.Down, id, position);
			});
			IDisposable disp = new FingerUp(controller, id, position, actions, Fingers);
			Fingers.Add(disp);
			return disp;

		}

		public void Dispose()
		{
			actions?.Dispose();
			actions = null;
		}
	}
}