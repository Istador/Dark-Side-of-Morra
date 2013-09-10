using UnityEngine;
using System.Collections;

public class SSoldierSeek : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if(!owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Höhe nicht in Ordnung
		if(!((Soldier)owner).IsHeightOk(pos)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if(distance <= (((Soldier)owner).f_optimum_min + ((Soldier)owner).f_optimum_max)/2.0f ){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if(   ((Soldier)owner).CanMoveTo(pos)   ){
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
	private static SSoldierSeek instance;
	private SSoldierSeek(){}
	public static SSoldierSeek Instance{get{
			if(instance==null) instance = new SSoldierSeek();
			return instance;
		}}
	
	
	
}
