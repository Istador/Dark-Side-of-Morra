/**
 * Zustand in der die Mine gelb blinkt.
 * 
*/
public class SMineYellow : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		//Sprite auswählen
		owner.Sprite = 1; //gelb blinken
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer;
		//rote Reichweite
		if(distance <= Mine.f_redRange)
			//zu Roten Zustand
			owner.AttackFSM.ChangeState(SMineRed.I);
		//außerhalb gelber Reichweite
		else if(distance > Mine.f_yellowRange)
			//zu Idle Zustand
			owner.AttackFSM.ChangeState(SMineIdle.I);
	}
	
	
	
	public override bool OnMessage(Enemy<Mine> owner, Telegram msg){
		switch(msg.message){
			//Kollision mit Spieler, oder Schaden erlitten	
			case "explode":
				//zu Roten Zustand
				owner.AttackFSM.ChangeState(SMineRed.I);
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
	public static SMineYellow I{get{return Instance;}}
}
