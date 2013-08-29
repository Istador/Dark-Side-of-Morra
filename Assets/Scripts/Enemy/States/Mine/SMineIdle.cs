/**
 * Zustand in der die Mine sichtbar ist, aber nicht blinkt.
 * Der Zustand überprüft ob der Player in Reichweite gerät
 * 
*/
public class SMineIdle : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.SetSprite(0); //nicht blinken
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//rote Reichweite
		if(distance <= Mine.f_redRange)
			owner.AttackFSM.ChangeState(SMineRed.Instance);
		//gelbe Reichweite
		else if(distance <= Mine.f_yellowRange)
			owner.AttackFSM.ChangeState(SMineYellow.Instance);
	}
	
	
	
	public override void Exit(Enemy<Mine> owner){}
	
	
	
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
	private static SMineIdle instance;
	private SMineIdle(){}
	public static SMineIdle Instance{get{
			if(instance==null) instance = new SMineIdle();
			return instance;
		}}
}