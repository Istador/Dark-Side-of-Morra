using UnityEngine;
using System.Collections;

public class SSoldierPatrol : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//wenn der Gegner sich nicht auf der Leiter befindet
		if(! ((Soldier)owner).IsOnLadder){
			//zufällig nach links/rechts patrouillieren
			if(Enemy<Soldier>.rnd.Next(0,2) == 0)
				owner.MoveFSM.ChangeState(SPatrolLeft<Soldier>.Instance);
			else
				owner.MoveFSM.ChangeState(SPatrolRight<Soldier>.Instance);
		}
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//wenn der Spieler gesehen wird
		if( ((Soldier)owner).IsPlayerVisible() ){
			//angreifen
			owner.MoveFSM.ChangeGlobalState(SSoldierEngaged.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	public override bool OnMessage(Enemy<Soldier> owner, Telegram msg){
		switch(msg.message){
			//wenn der Gegner angegriffen wird
			case "damage":
				//angreifen
				owner.MoveFSM.ChangeGlobalState(SSoldierEngaged.Instance);
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
	
	
	
}
