using UnityEngine;
using System.Collections;

public class SSpiderVerschwinden : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//Gravitation ausschalten
		owner.constantForce.enabled = false;
		
		//Unbesiegbar werden
		((Spider)owner).Invincible = true;
		
		//Platten-Event starten
		MessageDispatcher.I.Dispatch(owner, ((Spider)owner).platten, "begin", 0.5f);
		
		//selbst nach zwei sekunde verschwinden
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
			case "hide":
				owner.MoveFSM.ChangeState(SSpiderVerschwunden.Instance);
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
	
	
	
}
