using UnityEngine;

//
// In diesem Zustand wartet die Spinne darauf, dass das Platten-Event 
// beendet wird
//
public class SSpiderVerschwunden : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//Unsichtbar werden
		owner.Visible = false;
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			//Nachricht, dass das Plattenevent beendet wurde.
			case "auftauchen":
				owner.MoveFSM.ChangeState(SSpiderAuftauchen.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Sichtbar werden
		owner.Visible = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderVerschwunden instance;
	private SSpiderVerschwunden(){}
	public static SSpiderVerschwunden Instance{get{
			if(instance==null) instance = new SSpiderVerschwunden();
			return instance;
		}}
	public static SSpiderVerschwunden I{get{return Instance;}}
}
