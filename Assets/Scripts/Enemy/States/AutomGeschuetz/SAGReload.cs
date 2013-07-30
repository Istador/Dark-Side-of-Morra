using UnityEngine;
/**
 * Zustand in dem das Geschütz nachlädt.
 * Der Zustand überprüft ob der Player außer Reichweite gerät.
 * 
*/
public class SAGReload : State<Enemy<AutomGeschuetz>> {
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		((AutomGeschuetz)owner).BeginReload();
	}
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//zu dicht
		if(distance <= AutomGeschuetz.f_closeRange)
			owner.AttackFSM.ChangeState(SAGTooClose.Instance);
		//zu weit
		else if(distance > AutomGeschuetz.f_outOfRange)
			owner.AttackFSM.ChangeState(SAGIdle.Instance);
		//nachladen vorbei
		else if(((AutomGeschuetz)owner).reloadStart + AutomGeschuetz.reloadTime <= Time.time )
			owner.AttackFSM.ChangeState(SAGFire.Instance);
	}
	
	
	public override void Exit(Enemy<AutomGeschuetz> owner){}
		
	/**
	 * Singleton
	*/
	private static SAGReload instance;
	private SAGReload(){}
	public static SAGReload Instance{get{
			if(instance==null) instance = new SAGReload();
			return instance;
		}}
}
