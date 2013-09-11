//
// Zustand in der die Mine sichtbar ist, aber nicht blinkt.
// Der Zustand überprüft ob der Player in Reichweite gerät
// 
public class SMineIdle : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		//Sprite auswählen
		owner.Sprite = 0; //nicht blinken
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer;
		//rote Reichweite
		if(distance <= Mine.f_redRange)
			//zu Roten Zustand
			owner.AttackFSM.ChangeState(SMineRed.I);
		//gelbe Reichweite
		else if(distance <= Mine.f_yellowRange)
			//zu Gelben Zustand
			owner.AttackFSM.ChangeState(SMineYellow.I);
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
	private static SMineIdle instance;
	private SMineIdle(){}
	public static SMineIdle Instance{get{
			if(instance==null) instance = new SMineIdle();
			return instance;
		}}
	public static SMineIdle I{get{return Instance;}}
}
