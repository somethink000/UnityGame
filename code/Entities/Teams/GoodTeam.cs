﻿using MyGame;
using Sandbox;

// ReSharper disable once CheckNamespace
namespace MyGame;

class GoodTeam : BaseTeam
{

	public override string Name => "GoodTeam";
	public override void SupplyLoadout( Player player )
	{
	//	player.ClearAmmo();
	//	player.Inventory.DeleteContents();

	//	player.Inventory.Add( new Fists(), true );
	}

	public override void OnStart( Player player )
	{
	//	player.ClearAmmo();
	//	player.Inventory.DeleteContents();

		player.SetModel( "models/citizen/citizen.vmdl" );

		player.EnableAllCollisions = true;
		player.EnableDrawing = true;
		player.EnableHideInFirstPerson = true;
		player.EnableShadowInFirstPerson = true;

	//	player.Controller = new BystanderController();
	//	player.Camera = new FirstPersonCamera();
	}

	public override void OnJoin( Player player )
	{
		Log.Info( $"{player.Client.Name} joined the Good team." );

		base.OnJoin( player );
	}

	public override void OnPlayerKilled( Player player )
	{
	//	player.GlowActive = false;
	}

	public override void OnLeave( Player player )
	{
		Log.Info( $"{player.Client.Name} left the Good team." );

		base.OnLeave( player );
	}
}