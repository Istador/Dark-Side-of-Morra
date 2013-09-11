using UnityEngine;
using System.Collections;

/// 
/// Zustand in dem der Soldat weiß das der Spieler da ist, und ihn
/// aktiv verfolgt. Vergisst ihn nach einiger Zeit.
/// 
public class SSoldierEngaged : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//wenn der Gegner sich nicht auf der Leiter befindet
		if(! ((Soldier)owner).IsOnLadder)
			//auf Verfolgungszustände wechseln
			owner.MoveFSM.ChangeState(SSoldierStay.I);
		//merke die Position des Spielers
		((Soldier)owner).RememberNow();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//gemerkte Position und Zeit aktualisieren wenn sichtbar
		((Soldier)owner).DeterminePlayerPosition();
		
		//wenn er den Spieler zu lange nicht mehr gesehen hat
		if( ! ((Soldier)owner).IsRememberingPlayer ){
			//Patrolieren
			owner.MoveFSM.ChangeGlobalState(SSoldierPatrol.I);
		}
	}
	
	
	
	public override bool OnMessage(Enemy<Soldier> owner, Telegram msg){
		switch(msg.message){
			case "damage":
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierEngaged instance;
	private SSoldierEngaged(){}
	public static SSoldierEngaged Instance{get{
			if(instance==null) instance = new SSoldierEngaged();
			return instance;
		}}
	public static SSoldierEngaged I{get{return Instance;}}
}
