using UnityEngine;

//
// Zustand in dem das Geschütz nachlädt.
// Der Zustand überprüft ob der Player außer Reichweite gerät.
//
public class SAGReload : State<Enemy<AutomGeschuetz>> {
	
	
	
	public override void Enter(Enemy<AutomGeschuetz> owner){
		//Nachtricht an sich selbst in x sekunden, das Nachladen vorbei
		MessageDispatcher.I.Dispatch(owner, "reloaded", AutomGeschuetz.f_reloadTime);
	}
	
	
	
	//die Nachricht verarbeiten
	public override bool OnMessage(Enemy<AutomGeschuetz> owner, Telegram msg){
		switch(msg.message){
			case "reloaded":
				//zum Warte Zustand wechseln
				owner.AttackFSM.ChangeState(SAGHoldFire.I);
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
	public static SAGReload I{get{return Instance;}}
}
