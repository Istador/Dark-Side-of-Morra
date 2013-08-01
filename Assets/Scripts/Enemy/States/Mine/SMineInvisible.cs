/**
 * Zustand in der die Mine noch nicht sichtbar ist.
 * Der Zustand überprüft ob der Player in Reichweite gerät.
 * 
*/
public class SMineInvisible : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.SetInvisible(); //unsichtbar
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
	
	
	
	public override void Exit(Enemy<Mine> owner){
		owner.SetVisible(); //sichtbar
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
