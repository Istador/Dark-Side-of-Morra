using UnityEngine;

/// 
/// Patroliere so lange Links/Rechts, bis der RPG-Soldat den Spieler sieht
/// oder angegriffen wird.
/// 
public class SRPGSPatrol : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//zufällig nach links/rechts patrollieren
		if(GeneralObject.rnd.Next(0,2) == 0)
			//nach Links
			owner.MoveFSM.ChangeState(SPatrolLeft<RPGSoldier>.I);
		else
			//nach Rechts
			owner.MoveFSM.ChangeState(SPatrolRight<RPGSoldier>.I);
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//wenn der Spieler gesehen wird
		if( ((RPGSoldier)owner).IsPlayerVisible ){
			//angreifen
			owner.MoveFSM.ChangeGlobalState(SRPGSEngaged.I);
		}
	}
	
	
	
	public override bool OnMessage(Enemy<RPGSoldier> owner, Telegram msg){
		switch(msg.message){
			//wenn der Gegner angegriffen wird
			case "damage":
				//Spieler angreifen
				owner.MoveFSM.ChangeGlobalState(SRPGSEngaged.I);
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
	public static SRPGSPatrol I{get{return Instance;}}
}
