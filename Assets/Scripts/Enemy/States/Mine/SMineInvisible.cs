//
// Globaler Zustand in der die Mine noch nicht sichtbar ist.
// Der Zustand überprüft ob der Player in Reichweite gerät.
//
public class SMineInvisible : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		//unsichtbar werden
		owner.Visible = false;
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//wenn der Spieler die Mine sehen kann
		if(owner.LineOfSight(owner.Player))
			//wenn der Spieler in Reichweite ist
			if(owner.DistanceToPlayer <= Mine.f_yellowRange)
				//Globalen Zustand verlassen
				owner.AttackFSM.ChangeGlobalState(null);
	}
	
	
	
	public override void Exit(Enemy<Mine> owner){
		//sichtbar werden
		owner.Visible = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SMineInvisible instance;
	private SMineInvisible(){}
	public static SMineInvisible Instance{get{
			if(instance==null) instance = new SMineInvisible();
			return instance;
		}}
	public static SMineInvisible I{get{return Instance;}}
}
