using UnityEngine;
using System.Collections;

public class SAGHoldFire : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		//auf Spieler kann geschossen werden
		if( ((AutomGeschuetz)owner).IsPlayerInFireRange ){
			//zum Feuern Zustand wechseln
			owner.AttackFSM.ChangeState(SAGFire.Instance);
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SAGHoldFire instance;
	private SAGHoldFire(){}
	public static SAGHoldFire Instance{get{
			if(instance==null) instance = new SAGHoldFire();
			return instance;
		}}
	
	
	
}
