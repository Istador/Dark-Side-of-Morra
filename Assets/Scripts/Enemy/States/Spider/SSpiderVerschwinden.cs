using UnityEngine;

/// 
/// In diesem Zustand verschwindet die Spinne nach oben um das Plattenevent zu starten
/// 
public class SSpiderVerschwinden : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//Gravitation ausschalten
		owner.constantForce.enabled = false;
		
		//Unbesiegbar werden
		((Spider)owner).Invincible = true;
		
		//Platten-Event starten
		MessageDispatcher.I.Dispatch(owner, ((Spider)owner).platten, "begin", 0.5f);
		
		//selbst nach zwei sekunden unsichtbar werden
		MessageDispatcher.I.Dispatch(owner, "hide", 2.0f);
	}
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if( owner.IsRight(owner.Player) )
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		//nach oben bewegen
		owner.rigidbody.AddRelativeForce(Vector3.up * Spider.f_seilSpeed);
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			//unsichtbar werden
			case "hide":
				owner.MoveFSM.ChangeState(SSpiderVerschwunden.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderVerschwinden instance;
	private SSpiderVerschwinden(){}
	public static SSpiderVerschwinden Instance{get{
			if(instance==null) instance = new SSpiderVerschwinden();
			return instance;
		}}
	public static SSpiderVerschwinden I{get{return Instance;}}
}
