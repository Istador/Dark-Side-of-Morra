using UnityEngine;
using System.Collections;

public class SSpiderStehen : State<Enemy<Spider>> {
	
	
		
	public override void Enter(Enemy<Spider> owner){
		//anhalten
		((Spider)owner).StopMoving();
		owner.constantForce.enabled = false; //Gravitation ausschalten
	}
	
	
	public override void Execute(Enemy<Spider> owner){
		//Texturrichtung
		if(owner.IsRight(owner.Player))
			owner.Sprite = 1;
		else owner.Sprite = 0;
		
		
		//HP in Prozent
		float hp = owner.HealthFactor;
		
		//Zeit zum verschwinden?
		if(
			((Spider)owner).stage == 0 && hp <= 0.75f
			|| ((Spider)owner).stage == 1 && hp <= 0.50f
			|| ((Spider)owner).stage == 2 && hp <= 0.25f
		){
			//increment
			((Spider)owner).stage++;
			
			//Verschwinden
			owner.MoveFSM.ChangeState(SSpiderVerschwinden.Instance);
			
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer();
		//nahkampfreichweite
		
		if(distance <= Spider.f_outOfRange)
			owner.MoveFSM.ChangeState(SSpiderAngreifen.Instance); //angreifen
		//zu weit weg
		else
			owner.MoveFSM.ChangeState(SSpiderSeek.Instance); //annähern
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
	
	
	
}
