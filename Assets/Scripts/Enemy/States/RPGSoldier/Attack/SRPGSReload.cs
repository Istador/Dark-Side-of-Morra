using UnityEngine;

//
// Zustand in dem der RPG-Soldat nachlädt.
//
public class SRPGSReload : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//Nachtricht an sich selbst in x sekunden, das Nachladen vorbei
		MessageDispatcher.I.Dispatch(owner, "reloaded", RPGSoldier.f_reloadTime);
	}
	
	
	
	//die Nachricht verarbeiten
	public override bool OnMessage(Enemy<RPGSoldier> owner, Telegram msg){
		switch(msg.message){
			case "reloaded":
				//zum Warte Zustand wechseln
				owner.AttackFSM.ChangeState(SRPGSHoldFire.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSReload instance;
	private SRPGSReload(){}
	private static SRPGSReload Instance{get{
			if(instance==null) instance = new SRPGSReload();
			return instance;
		}}
	public static SRPGSReload I{get{return Instance;}}
}
