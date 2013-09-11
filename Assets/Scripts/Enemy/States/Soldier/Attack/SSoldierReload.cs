using UnityEngine;

//
// Zustand in dem der Soldat nachlädt.
//
public class SSoldierReload : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//Nachtricht an sich selbst in x sekunden, das Nachladen vorbei
		MessageDispatcher.I.Dispatch(owner, "reloaded", ((Soldier)owner).f_reloadTime);
	}
	
	
	
	//die Nachricht verarbeiten
	public override bool OnMessage(Enemy<Soldier> owner, Telegram msg){
		switch(msg.message){
			case "reloaded":
				//zum Warte Zustand wechseln
				owner.AttackFSM.ChangeState(SSoldierHoldFire.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierReload instance;
	private SSoldierReload(){}
	public static SSoldierReload Instance{get{
			if(instance==null) instance = new SSoldierReload();
			return instance;
		}}
	public static SSoldierReload I{get{return Instance;}}
}
