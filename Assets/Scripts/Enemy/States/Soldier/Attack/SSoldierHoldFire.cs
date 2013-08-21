using UnityEngine;
using System.Collections;

public class SSoldierHoldFire : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Spieler kann geschossen werden
		if(((Soldier)owner).IsPlayerInFireRange()){
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SSoldierFire.Instance);
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierHoldFire instance;
	private SSoldierHoldFire(){}
	public static SSoldierHoldFire Instance{get{
			if(instance==null) instance = new SSoldierHoldFire();
			return instance;
		}}
	
	
	
}
