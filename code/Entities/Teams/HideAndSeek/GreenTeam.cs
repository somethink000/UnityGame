using MyGame;
using Sandbox;

// ReSharper disable once CheckNamespace
namespace MyGame;

class GreenTeam : BaseTeam
{
	public override string HudClassName => "team_green";
	public override string TeamName => "GreenTeam";
	public override float TeamHealth => 110;

}
