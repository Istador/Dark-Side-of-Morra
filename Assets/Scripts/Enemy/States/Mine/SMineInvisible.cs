/**
 * Zustand in der die Mine noch nicht sichtbar ist.
 * Der Zustand überprüft ob der Player in Reichweite gerät.
 * 
*/
public class SMineInvisible : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.Visible = false; //unsichtbar
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//nur wenn der Spieler die Mine auch sehen kann
		if(owner.LineOfSight(owner.Player))
		{
			//Distanz zum Spieler ermitteln
			float distance = owner.DistanceToPlayer;
			//Spieler in Reichweite
			if(distance <= Mine.f_yellowRange)
				owner.AttackFSM.ChangeGlobalState(null);
		}
	}
	
	
	
	public override void Exit(Enemy<Mine> owner){
		owner.Visible = true; //sichtbar
	}
	
	
	
	public override bool OnMessage(Enemy<Mine> owner, Telegram msg){
		switch(msg.message){
			case "explode":
				owner.AttackFSM.ChangeState(SMineRed.Instance);
				return true;
			default:
				return false;
		}
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
}
