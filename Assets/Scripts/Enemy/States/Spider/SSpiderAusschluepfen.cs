using UnityEngine;

// 
// Zustand in dem die Spinne darauf wartet das die Animation des Kokons
// vorbei ist.
// 
public class SSpiderAusschluepfen : State<Enemy<Spider>> {
	
	
	
	public override void Enter(Enemy<Spider> owner){
		//Health Bar einblenden
		((Spider)owner).healthbar.Show();
	}
	
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.Player))
			owner.Sprite = 1;
		else owner.Sprite = 0;
	}
	
	
	
	public override bool OnMessage(Enemy<Spider> owner, Telegram msg){
		switch(msg.message){
			//Nachricht dass die Kokon-Animation vorbei ist
			case "kokon_offen":
				//in den Kampfmodus gehen
				owner.MoveFSM.ChangeState(SSpiderStehen.I);
				return true;
			default:
				return false;
		}
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Spinne jetzt nicht mehr unbesiegbar.
		((Spider)owner).Invincible = false;
		
		//playerCollider verursacht nun Schaden bei Berührung
		((Spider)owner).playerCollider.enabled = true;
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
	public static SSpiderAusschluepfen I{get{return Instance;}}
}
