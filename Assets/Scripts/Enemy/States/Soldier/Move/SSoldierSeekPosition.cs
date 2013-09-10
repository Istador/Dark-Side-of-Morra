using UnityEngine;
using System.Collections;

public class SSoldierSeekPosition : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = ((Soldier)owner).LastKnownPosition();
		
		//LoS und Höhe in Ordnung
		if(owner.LineOfSight(owner.Player) && ((Soldier)owner).IsHeightOk(pos)){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		//höhe nicht in Ordnung
		if( ! ((Soldier)owner).IsHeightOk(pos) ){
			//auf Leiter
			if( 
				(
					((Soldier)owner).CanClimbUp && ((Soldier)owner).IsOver(pos)  
				)
				||
				(
					((Soldier)owner).CanClimbDown && ! ((Soldier)owner).IsOver(pos)
				)
			){
				owner.MoveFSM.ChangeState(SLEnter.Instance);
				return;
			}
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if(distance <= 0.05f ){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		//Position die Angestrebt wird befindet sich direkt über oder unter
		//dem Gegner
		if( ((Soldier)owner).DirectlyAboveOrUnder(pos) ){
			//Bewege in die Richtung weiter, nicht zum Ziel
			pos = owner.collider.bounds.center + ((Soldier)owner).LastHeading() * ((Soldier)owner).MaxSpeed;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((Soldier)owner).CanMoveTo(pos)   ){
			((Soldier)owner).Steering.DoSeek(pos);
			
		}
		//kann nicht gehen, aber klettern
		else if(   ((Soldier)owner).CanClimbTo(pos)   ){
			owner.MoveFSM.ChangeState(SLEnter.Instance);
		}
		//keine Bewegung möglich
		else {
			((Soldier)owner).StopMoving();
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		((Soldier)owner).StopMoving();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierSeekPosition instance;
	private SSoldierSeekPosition(){}
	public static SSoldierSeekPosition Instance{get{
			if(instance==null) instance = new SSoldierSeekPosition();
			return instance;
		}}
	
	
	
}
