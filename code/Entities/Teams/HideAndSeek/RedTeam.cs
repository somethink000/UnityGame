using MyGame;
using Sandbox;

// ReSharper disable once CheckNamespace
namespace MyGame;

class RedTeam : BaseTeam
{
	public override string HudClassName => "team_red";
	public override string TeamName => "BadTeam";
	public override float TeamHealth => 200;
}
