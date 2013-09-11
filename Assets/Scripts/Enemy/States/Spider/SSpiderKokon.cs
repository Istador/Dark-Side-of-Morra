using UnityEngine;

/// 
/// Zustand in dem die Spinne darauf wartet das der Spieler mit der Story
/// soweit ist mit dem Kampf zu beginnen
/// 
public class SSpiderKokon : State<Enemy<Spider>> {
	
	
	
	public override void Enter(Enemy<Spider> owner){
		//Unsichtbar sein
		owner.Visible = false;
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			//Nachricht das die Spinne ausschlüpfen soll
			case "ausschluepfen":
				//in den ausschlüpfen Zustand gehen
				owner.MoveFSM.ChangeState(SSpiderAusschluepfen.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.Player))
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		//Sichtbar werden
		owner.Visible = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderKokon instance;
	private SSpiderKokon(){}
	public static SSpiderKokon Instance{get{
			if(instance==null) instance = new SSpiderKokon();
			return instance;
		}}
	public static SSpiderKokon I{get{return Instance;}}
}
