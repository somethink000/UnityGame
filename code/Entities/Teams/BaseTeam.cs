using Sandbox;


namespace MyGame;

public abstract class BaseTeam : Entity
{
	
	public virtual string HudClassName => "null";
	public virtual string TeamName => "null";
	public virtual float TeamHealth => 100;



}
