using MyGame;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sandbox.CitizenAnimationHelper;

namespace MyGame;

public partial class SenseAbility : BaseAbility
{
	public override float Cooldown => 10;
	public override string Name => "Murder";

	public override string GetKeybind()
	{
		return Input.GetKeyWithBinding( "iv_drop" ).ToUpper();
	}

	
}
