using UnityEngine;
/**
 * Zustand in dem das Geschütz nachlädt.
 * Der Zustand überprüft ob der Player außer Reichweite gerät.
*/
public class SAGReload : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		//Nachtricht an sich selbst in x sekunden, das Nachladen vorbei
		MessageDispatcher.Instance.Dispatch(owner, owner, "reloaded", (float)AutomGeschuetz.d_reloadTime, null);
	}
	
	
	
	public override bool OnMessage(Enemy<AutomGeschuetz> owner, Telegram msg){
		switch(msg.message){
			case "reloaded":
				//zum Warte Zustand wechseln
				owner.AttackFSM.ChangeState(SAGHoldFire.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	
	
	/**
	 * Singleton
	*/
	private static SAGReload instance;
	private SAGReload(){}
	public static SAGReload Instance{get{
			if(instance==null) instance = new SAGReload();
			return instance;
		}}
}
