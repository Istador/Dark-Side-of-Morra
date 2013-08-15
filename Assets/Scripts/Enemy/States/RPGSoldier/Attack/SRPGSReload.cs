using UnityEngine;
using System.Collections;

public class SRPGSReload : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//Nachtricht an sich selbst in x sekunden, das Nachladen vorbei
		MessageDispatcher.Instance.Dispatch(owner, owner, "reloaded", (float)RPGSoldier.d_reloadTime, null);
	}
	
	
	
	public override bool OnMessage(Enemy<RPGSoldier> owner, Telegram msg){
		switch(msg.message){
			case "reloaded":
				//zum Warte Zustand wechseln
				owner.AttackFSM.ChangeState(SRPGSHoldFire.Instance);
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
	public static SRPGSReload Instance{get{
			if(instance==null) instance = new SRPGSReload();
			return instance;
		}}
	
	
	
}
