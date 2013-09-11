using UnityEngine;

// 
// In diesem Zustand fällt die Spinne wieder auf den Boden, um danach
// mit dem Kampf fortzufahren
// 
public class SSpiderAuftauchen : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//in zwei Sekunde wieder normales Verhalten
		MessageDispatcher.I.Dispatch(owner, "show", 2.0f);
	}
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if( owner.IsRight(owner.Player) )
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		//nach unten bewegen
		owner.rigidbody.AddRelativeForce(Vector3.down * Spider.f_seilSpeed);
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			//Wieder mit dem Kampf fortsetzen
			case "show":
				owner.MoveFSM.ChangeState(SSpiderStehen.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//wieder angreifbar werden
		((Spider)owner).Invincible = false;
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
	public static SSpiderAuftauchen I{get{return Instance;}}
}
