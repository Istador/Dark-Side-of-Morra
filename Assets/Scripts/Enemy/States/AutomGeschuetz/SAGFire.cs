using UnityEngine;
/**
 * Zustand in der das Automatische Geschütz eine Rakete abfeuert.
*/
public class SAGFire : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		//Rakete vom Prefab erstellen
		GameObject rocket = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("pRocket"), owner.transform.position, owner.transform.rotation);
		
		//Kollisionen zwischen diesem Gegner und dieser Rakete ignorieren
		Physics.IgnoreCollision(owner.collider, rocket.collider);
		Physics.IgnoreCollision(rocket.collider, owner.collider);
		
		//zum Nachlade Zustand wechseln
		owner.AttackFSM.ChangeState(SAGReload.Instance);
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
}
