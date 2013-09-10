using UnityEngine;
using System.Collections;

public class SSpiderVerschwunden : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//Unsichtbar werden
		owner.Visible = false;
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			case "auftauchen":
				owner.MoveFSM.ChangeState(SSpiderAuftauchen.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Sichtbar werden
		owner.Visible = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderVerschwunden instance;
	private SSpiderVerschwunden(){}
	public static SSpiderVerschwunden Instance{get{
			if(instance==null) instance = new SSpiderVerschwunden();
			return instance;
		}}
	
	
	
}
