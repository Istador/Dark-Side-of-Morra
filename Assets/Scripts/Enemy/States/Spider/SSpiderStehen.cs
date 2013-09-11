using UnityEngine;

// 
// In diesem Zustand steht die Spinne (blöd) rum.
// Er dient vorwiegend dazu im Kampf zu entscheiden in welchen Zustand
// die Spinne als nächstes geht
// 
public class SSpiderStehen : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//anhalten
		((Spider)owner).StopMoving();
		//Gravitation ausschalten (falls sie auf der Rampe steht)
		owner.constantForce.enabled = false;
	}
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.Player))
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		
		//HP in Prozent
		float hp = owner.HealthFactor;
		
		//Ist jetzt Zeit zum verschwinden und das Plattenevent zu starten?
		if(
			((Spider)owner).stage == 0 && hp <= 0.75f		//fällt unter 75% HP
			|| ((Spider)owner).stage == 1 && hp <= 0.50f	//fällt unter 50% HP
			|| ((Spider)owner).stage == 2 && hp <= 0.25f	//fällt unter 25% HP
		){
			//increment Zähler
			((Spider)owner).stage++;
			
			//Verschwinden
			owner.MoveFSM.ChangeState(SSpiderVerschwinden.I);
			
			return;
		}
		
		//Spieler ist in Nahkampfreichweite
		if(owner.DistanceToPlayer <= Spider.f_outOfRange)
			//Angreifen
			owner.MoveFSM.ChangeState(SSpiderAngreifen.I);
		//zu weit weg
		else
			//Zum Spieler bewegen
			owner.MoveFSM.ChangeState(SSpiderSeek.I);
	}
	
	
	
	public override void Exit(Enemy<Spider> owner){
		//Gravitation einschalten
		owner.constantForce.enabled = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSpiderStehen instance;
	private SSpiderStehen(){}
	public static SSpiderStehen Instance{get{
			if(instance==null) instance = new SSpiderStehen();
			return instance;
		}}
	public static SSpiderStehen I{get{return Instance;}}
}
