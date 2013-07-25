/**
 * Zustand in der die Mine nichts tut, als zu überprüfen ob
 * der Player in Range ist
 * 
*/
public class SMineYellow : State<Enemy<Mine>> {
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.SetSprite(1);
	}
	
	
	public override void Execute(Enemy<Mine> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//rote Reichweite
		if(distance <= ((Mine)owner).f_redRange)
			owner.AttackFSM.ChangeState(SMineRed.Instance);
		//außerhalb gelber Reichweite
		else if(distance > ((Mine)owner).f_yellowRange)
			owner.AttackFSM.ChangeState(SMineVisible.Instance);
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
	private static SMineYellow instance;
	private SMineYellow(){}
	public static SMineYellow Instance{get{
			if(instance==null) instance = new SMineYellow();
			return instance;
		}}
}
