using UnityEngine;

//
// Zustand in dem der RPG-Soldat eine Rakete abfeuert.
//
public class SRPGSFire : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//Rakete vom Prefab erstellen
		GameObject rocket = owner.Instantiate("pRocket", ((RPGSoldier)owner).bulletSpawn);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		owner.IgnoreCollision(rocket);
		
		//Setze den owner der Rakete
		rocket.GetComponent<PRocket>().owner = owner.gameObject;
		
		//zum Nachlade Zustand wechseln
		owner.AttackFSM.ChangeState(SRPGSReload.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSFire instance;
	private SRPGSFire(){}
	private static SRPGSFire Instance{get{
			if(instance==null) instance = new SRPGSFire();
			return instance;
		}}
	public static SRPGSFire I{get{return Instance;}}
}
