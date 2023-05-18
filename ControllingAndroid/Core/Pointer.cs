namespace ControllingAndroid.Core
{
	public class Pointer
	{
		public long Id { get; private set; }
		public int LocalId { get; private set; }

		public Point Point { get; set; }
		public float Pressure { get; set; }

		public bool Up { get; set; }

		public Pointer(long id, int localId)
		{
			Id = id;
			LocalId = localId;
		}
	}
}

