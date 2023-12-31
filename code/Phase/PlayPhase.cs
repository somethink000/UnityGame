﻿using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;

namespace MyGame;

public class PlayPhase : BasePhase
{
	public IDictionary<Entity, int> Blinded = new Dictionary<Entity, int>();
	public int TicksElapsed;
	public override string Title => "Play";
	private string MurdererNames { get; set; }

	public override void Activate()
	{
		TimeLeft = MyGame.RoundTime;
		Event.Register( this );
		foreach ( var client in Game.Clients )
		{
			if ( client.Pawn is not Player pawn || pawn.Team == Team.Spectator )
			{
				continue;
			}

			pawn.Respawn();
			TeamCapabilities.GiveLoadouts( pawn );
		}

		MurdererNames = string.Join( ',',
			Game.Clients.Where( c => ((Player)c.Pawn).Team == Team.Murderer ).Select( c => c.Name ) );
	}

	public override void Deactivate()
	{
		TimeLeft = MyGame.RoundTime;
		Event.Unregister( this );
		foreach ( var item in Blinded )
		{
			ClearDebuffs( item.Key );
		}

		Blinded.Clear();
	}

	public void ClearDebuffs( Entity entity )
	{
		Log.Info( "Removing blind from " + entity.Name );
		//BlindedOverlay.Hide( To.Single( entity ) );
		//DeathOverlay.Hide( To.Single( entity ) );
		if ( entity is not Player pawn || !pawn.IsValid() )
		{
			return;
		}

		//if ( pawn.Controller is WalkControllerComponent controller )
		//{
		//	controller.SpeedMultiplier = 1;
		//}

		//if ( pawn.Inventory != null )
		//{
		//	pawn.Inventory.AllowPickup = true;
		//}
	}

	public override void Tick()
	{
		++TicksElapsed;
		if ( TimeLeft != -1 && TicksElapsed % Game.TickRate == 0 && --TimeLeft == 0 )
		{
			TriggerEndOfGame();
			return;
		}

		var bystandersAlive = Game.Clients.Any( c =>
			((Player)c.Pawn).Team == Team.Bystander || ((Player)c.Pawn).Team == Team.Detective );
		var murderersAlive = Game.Clients.Any( c => ((Player)c.Pawn).Team == Team.Murderer );
		if ( !bystandersAlive || !murderersAlive )
		{
			TriggerEndOfGame();
		}

		foreach ( var item in Blinded )
		{
			var blindLeft = item.Value - 1;
			if ( blindLeft < 0 )
			{
				Blinded.Remove( item.Key );
				ClearDebuffs( item.Key );
				Log.Info( "Removing blind from " + item.Key.Name );
			}
			else
			{
				Blinded[item.Key] = blindLeft;
			}
		}
	}

	public void TriggerEndOfGame()
	{
		var bystandersWin = Game.Clients.Any( c => ((Player)c.Pawn).Team is Team.Bystander or Team.Detective );
		ChatBox.Say( (bystandersWin ? "Bystanders" : "Murderers") + " win! The murderers were: " + MurdererNames );
		NextPhase = new EndPhase();
		IsFinished = true;
	}

	[KillEvent.Kill]
	public void OnKill( Entity killer, Entity victim )
	{
		if ( killer is not Player && victim is not Player )
		{
			return;
		}

		var victimPlayer = (Player)victim;
		var victimTeam = victimPlayer.Team;
		victimPlayer.Team = Team.Spectator;

		if ( killer == null )
		{
			Log.Info( victimPlayer + " died mysteriously" );
			return;
		}

		var killerPlayer = (Player)killer;
		var killerTeam = killerPlayer.Team;

		Log.Info( victimPlayer + " died to " + killerPlayer );

		if ( victimTeam != Team.Murderer && killerTeam != Team.Murderer )
		{
			Log.Info( killerPlayer + " shot a bystander" );

			ChatBox.Say( killerPlayer.Client.Name + " killed an innocent bystander" );

			//BlindedOverlay.Show( To.Single( killer ) );

			//if ( killerPlayer.Controller is WalkControllerComponent controller )
			//{
			//	controller.SpeedMultiplier = 0.3f;
			//}

			//if ( killerPlayer.Inventory != null )
			//{
			//	killerPlayer.Inventory.AllowPickup = false;
			//	killerPlayer.Inventory.SpillContents( killerPlayer.EyePosition, killerPlayer.AimRay.Forward );
			//}

			Blinded[killer] = 30 * Game.TickRate;
		}
		else if ( victimTeam == Team.Murderer )
		{
			Log.Info( killerPlayer + " killed a murderer" );
			ChatBox.Say( killerPlayer.Client.Name + " killed a murderer" );
		}
	}
}
