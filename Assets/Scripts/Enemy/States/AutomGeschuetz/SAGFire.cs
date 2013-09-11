using UnityEngine;

//
// Zustand in dem das Automatische Geschütz eine Rakete abfeuert.
//
public class SAGFire : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		//Rakete vom Prefab erstellen
		GameObject rocket = owner.Instantiate("pRocket", ((AutomGeschuetz)owner).bulletSpawn);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		owner.IgnoreCollision(rocket);
		
		//Setze den owner der Rakete
		rocket.GetComponent<PRocket>().owner = owner.gameObject;
		
		//zum Nachlade-Zustand wechseln
		owner.AttackFSM.ChangeState(SAGReload.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SAGFire instance;
	private SAGFire(){}
	public static SAGFire Instance{get{
			if(instance==null) instance = new SAGFire();
			return instance;
		}}
	public static SAGFire I{get{return Instance;}}
}
