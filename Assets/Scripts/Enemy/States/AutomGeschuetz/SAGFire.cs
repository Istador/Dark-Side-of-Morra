using UnityEngine;
/**
 * Zustand in der das Automatische Geschütz eine Rakete abfeuert.
 * 
*/
public class SAGFire : State<Enemy<AutomGeschuetz>> {
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		GameObject rocket = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("pRocket"), owner.transform.position, owner.transform.rotation);
		
		//Kollisionen zwischen Geschütz und Rakete ignorieren
		Physics.IgnoreCollision(owner.collider, rocket.collider);
	}
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		owner.AttackFSM.ChangeState(SAGReload.Instance);
	}
	
	
	public override void Exit(Enemy<AutomGeschuetz> owner){}
	
	
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
