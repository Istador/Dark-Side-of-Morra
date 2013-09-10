using UnityEngine;
using System.Collections;

public class SSpiderAusschluepfen : State<Enemy<Spider>> {
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.Player))
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		//Health Bar einblenden
		((Spider)owner).healthbar.Show();
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			case "kokon_offen":
				owner.MoveFSM.ChangeState(SSpiderStehen.Instance);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		((Spider)owner).invincible = false;
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
