namespace ControllingAndroid
{
	public interface IScreen : IDisposable
	{
		List<IDisposable> Fingers { get; }

		IDisposable Press(Point position);


		IDisposable Move(Point vector, TimeSpan timeout);
	}
}