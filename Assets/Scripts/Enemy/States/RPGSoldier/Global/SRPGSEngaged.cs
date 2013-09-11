using UnityEngine;

/// 
/// Zustand in dem der RPG-Soldat weiß das der Spieler da ist, und ihn
/// aktiv verfolgt. Vergisst ihn nach einiger Zeit.
/// 
public class SRPGSEngaged : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//Bewegungs Zustandsautomat von Patrolieren zum Verfolgen des Spielers
		owner.MoveFSM.ChangeState(SRPGSStay.I);
		
		//Merke die Position des Spielers
		((RPGSoldier)owner).RememberNow();
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//gemerkte Position und Zeit aktualisieren wenn sichtbar
		((RPGSoldier)owner).DeterminePlayerPosition();
		
		//wenn er den Spieler zu lange nicht mehr gesehen hat
		if( ! ((RPGSoldier)owner).IsRememberingPlayer ){
			//Patrolieren
			owner.MoveFSM.ChangeGlobalState(SRPGSPatrol.I);
		}
	}
	
	
	
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
	public static SRPGSEngaged I{get{return Instance;}}
}
