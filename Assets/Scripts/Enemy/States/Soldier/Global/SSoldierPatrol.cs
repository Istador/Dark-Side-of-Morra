using UnityEngine;
using System.Collections;

/// 
/// Patroliere so lange Links/Rechts, bis der Soldat den Spieler sieht
/// oder angegriffen wird.
/// 
public class SSoldierPatrol : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//wenn der Gegner sich nicht auf der Leiter befindet
		if(! ((Soldier)owner).IsOnLadder){
			//zufällig nach links/rechts patrouillieren
			if(GeneralObject.rnd.Next(0,2) == 0)
				//nach Links
				owner.MoveFSM.ChangeState(SPatrolLeft<Soldier>.I);
			else
				//nach Rechts
				owner.MoveFSM.ChangeState(SPatrolRight<Soldier>.I);
		}
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//wenn der Spieler gesehen wird
		if( ((Soldier)owner).IsPlayerVisible ){
			//angreifen
			owner.MoveFSM.ChangeGlobalState(SSoldierEngaged.I);
		}
	}
	
	
	
	public override bool OnMessage(Enemy<Soldier> owner, Telegram msg){
		switch(msg.message){
			//wenn der Gegner angegriffen wird
			case "damage":
				//Spieler angreifen
				owner.MoveFSM.ChangeGlobalState(SSoldierEngaged.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierPatrol instance;
	private SSoldierPatrol(){}
	public static SSoldierPatrol Instance{get{
			if(instance==null) instance = new SSoldierPatrol();
			return instance;
		}}
	public static SSoldierPatrol I{get{return Instance;}}
}
