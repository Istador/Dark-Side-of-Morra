using UnityEngine;
using System.Collections;

public class SSpiderAusschluepfen : State<Enemy<Spider>> {
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			case "kokon_offen":
				//TODO: zu State stehen
				owner.MoveFSM.ChangeState(null);
				return true;
			default:
				return false;
		}
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderAusschluepfen instance;
	private SSpiderAusschluepfen(){}
	public static SSpiderAusschluepfen Instance{get{
			if(instance==null) instance = new SSpiderAusschluepfen();
			return instance;
		}}
	
	
	
}
