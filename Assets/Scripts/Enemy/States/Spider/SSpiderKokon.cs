using UnityEngine;
using System.Collections;

public class SSpiderKokon : State<Enemy<Spider>> {
	
	
	
	public override void Enter(Enemy<Spider> owner){
		owner.SetInvisible();
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			case "ausschluepfen":
				owner.MoveFSM.ChangeState(SSpiderAusschluepfen.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.player))
			owner.SetSprite(1);
		else owner.SetSprite(0);
		owner.SetVisible();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderKokon instance;
	private SSpiderKokon(){}
	public static SSpiderKokon Instance{get{
			if(instance==null) instance = new SSpiderKokon();
			return instance;
		}}
	
	
	
}
