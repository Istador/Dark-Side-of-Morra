using UnityEngine;
using System.Collections;

public class SSpiderAuftauchen : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//in zwei Sekunde wieder normales Verhalten
		MessageDispatcher.Instance.Dispatch(owner, owner, "show", 2.0f, null);
	}
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		bool right = owner.IsRight(owner.player);
		if(right)
			owner.SetSprite(1);
		else owner.SetSprite(0);
		
		//nach unten bewegen
		owner.rigidbody.AddRelativeForce(Vector3.down * Spider.f_seilSpeed);
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			case "show":
				owner.MoveFSM.ChangeState(SSpiderStehen.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//wieder angreifbar werden
		((Spider)owner).invincible = false;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderAuftauchen instance;
	private SSpiderAuftauchen(){}
	public static SSpiderAuftauchen Instance{get{
			if(instance==null) instance = new SSpiderAuftauchen();
			return instance;
		}}
	
	
	
}
