using UnityEngine;
using System.Collections;

public class SRPGSHoldFire : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//auf Spieler kann geschossen werden
		if( ((RPGSoldier)owner).IsPlayerInFireRange ){
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SRPGSFire.Instance);
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSHoldFire instance;
	private SRPGSHoldFire(){}
	public static SRPGSHoldFire Instance{get{
			if(instance==null) instance = new SRPGSHoldFire();
			return instance;
		}}
	
	
	
}
