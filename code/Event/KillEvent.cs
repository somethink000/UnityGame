using Sandbox;

namespace MyGame;

public static class KillEvent
{
	public const string Kill = "kill";

	public class KillAttribute : EventAttribute
	{
		public KillAttribute() : base( Kill ) { }
	}
}
