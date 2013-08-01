/**
 * Zustand in der das Automatische Geschütz zu weit vom Spieler entfernt ist.
 * Der Zustand überprüft ob der Player in Reichweite kommt.
*/
public class SAGIdle : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){}
	
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//zu dicht
		if(distance <= AutomGeschuetz.f_closeRange)
			owner.AttackFSM.ChangeState(SAGTooClose.Instance);
		//in reichweite && LOS
		else if(distance <= AutomGeschuetz.f_outOfRange && owner.LineOfSight(owner.player))
			owner.AttackFSM.ChangeState(SAGReload.Instance);
	}
	
	
	
	public override void Exit(Enemy<AutomGeschuetz> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SAGIdle instance;
	private SAGIdle(){}
	public static SAGIdle Instance{get{
			if(instance==null) instance = new SAGIdle();
			return instance;
		}}
}
