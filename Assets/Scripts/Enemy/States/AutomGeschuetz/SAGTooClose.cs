/**
 * Zustand in dem der Spieler zu nah am Geschütz ist.
 * Der Zustand überprüft ob der Player in Reichweite kommt.
 * 
*/
public class SAGTooClose : State<Enemy<AutomGeschuetz>> {
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){}
	
	
	public override void Execute(Enemy<AutomGeschuetz> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//zu weit
		if(distance > AutomGeschuetz.f_outOfRange)
			owner.AttackFSM.ChangeState(SAGIdle.Instance);
		//in reichweite
		else if(distance > AutomGeschuetz.f_closeRange)
			owner.AttackFSM.ChangeState(SAGReload.Instance);
	}
	
	
	public override void Exit(Enemy<AutomGeschuetz> owner){}
	
	
	/**
	 * Singleton
	*/
	private static SAGTooClose instance;
	private SAGTooClose(){}
	public static SAGTooClose Instance{get{
			if(instance==null) instance = new SAGTooClose();
			return instance;
		}}
}
