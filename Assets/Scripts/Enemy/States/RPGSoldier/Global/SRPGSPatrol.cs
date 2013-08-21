using UnityEngine;
using System.Collections;

public class SRPGSPatrol : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//zufällig nach links/rechts patrouillieren
		if(Enemy<RPGSoldier>.rnd.Next(0,2) == 0)
			owner.MoveFSM.ChangeState(SPatrolLeft<RPGSoldier>.Instance);
		else
			owner.MoveFSM.ChangeState(SPatrolRight<RPGSoldier>.Instance);
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//wenn der Spieler gesehen wird
		if( ((RPGSoldier)owner).IsPlayerVisible() ){
			//angreifen
			owner.MoveFSM.ChangeGlobalState(SRPGSEngaged.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){}
	
	
	
	public override bool OnMessage(Enemy<RPGSoldier> owner, Telegram msg){
		switch(msg.message){
			//wenn der Gegner angegriffen wird
			case "damage":
				//angreifen
				owner.MoveFSM.ChangeGlobalState(SRPGSEngaged.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSPatrol instance;
	private SRPGSPatrol(){}
	public static SRPGSPatrol Instance{get{
			if(instance==null) instance = new SRPGSPatrol();
			return instance;
		}}
	
	
	
}
