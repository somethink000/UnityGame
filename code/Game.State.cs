using Sandbox;
using System;
using System.Collections.Generic;

namespace MyGame
{
	public partial class MyGame
	{
		[Net]public MiniGame CurrentGame { get; set; }


		public void SetGame( MiniGame game )
		{
			if ( CurrentGame != game )
			{
				CurrentGame = game;
			}
		}


	}
}
