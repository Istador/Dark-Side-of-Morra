using UnityEngine;
using System.Collections;

public class SRPGSEngaged : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		owner.MoveFSM.ChangeState(SRPGSStay.Instance);
		((RPGSoldier)owner).RememberNow();
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//gemerkte Position und Zeit aktualisieren wenn sichtbar
		((RPGSoldier)owner).DeterminePlayerPosition();
		
		//wenn er den Spieler zu lange nicht mehr gesehen hat
		if( ! ((RPGSoldier)owner).IsRememberingPlayer ){
			//Patrolieren
			owner.MoveFSM.ChangeGlobalState(SRPGSPatrol.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){}
	
	
	
	public override bool OnMessage(Enemy<RPGSoldier> owner, Telegram msg){
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
	private static SRPGSEngaged instance;
	private SRPGSEngaged(){}
	public static SRPGSEngaged Instance{get{
			if(instance==null) instance = new SRPGSEngaged();
			return instance;
		}}
	
	
	
}
