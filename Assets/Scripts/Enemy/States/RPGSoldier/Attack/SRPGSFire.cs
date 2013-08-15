using UnityEngine;
using System.Collections;

public class SRPGSFire : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//Rakete vom Prefab erstellen
		GameObject rocket = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("pRocket"), owner.transform.position, owner.transform.rotation);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		Physics.IgnoreCollision(owner.collider, rocket.collider);
		Physics.IgnoreCollision(rocket.collider, owner.collider);
		
		//zum Nachlade Zustand wechseln
		owner.AttackFSM.ChangeState(SRPGSReload.Instance);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSFire instance;
	private SRPGSFire(){}
	public static SRPGSFire Instance{get{
			if(instance==null) instance = new SRPGSFire();
			return instance;
		}}
	
	
	
}
