using UnityEngine;

/**
 * Zustand in der die Mine rot blinkt, und nach einer Sekunde explodiert.
 * 
*/
public class SMineRed : State<Enemy<Mine>> {
	
	
	
	public override void Enter(Enemy<Mine> owner){
		owner.Sprite = 2; //rot blinken
		
		//sich selbst eine Nachricht in 2 Sekunden schicken zum explodieren
		MessageDispatcher.I.Dispatch(owner, "minetimer", 2.0f);
	}
	
	
	
	public override void Execute(Enemy<Mine> owner){
		//Ticken
		if(owner.SpriteCntrl.index == 1){
			if(! ((Mine)owner).ticked){
				AudioSource.PlayClipAtPoint(Mine.ac_tick, owner.collider.bounds.center);
				((Mine)owner).ticked = true;
			}
		} else
			((Mine)owner).ticked = false;
	}
	
	
	
	public override void Exit(Enemy<Mine> owner){}
	
	
	
	public override bool OnMessage(Enemy<Mine> owner, Telegram msg){
		switch(msg.message){
			case "minetimer":
				owner.AttackFSM.ChangeState(SMineDead.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SMineRed instance;
	private SMineRed(){}
	public static SMineRed Instance{get{
			if(instance==null) instance = new SMineRed();
			return instance;
		}}
}
