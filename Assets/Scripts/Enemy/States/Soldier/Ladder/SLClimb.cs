using UnityEngine;
using System.Collections;

/// <summary>
/// Zustand um nach dem betreten der Leiter festzustellen ob nach oben 
/// oder unten geklettert werden soll.
/// </summary>
public class SLClimb : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//Spieler ist über dem Gegner
		if(   ((Soldier)owner).IsOver( ((Soldier)owner).LastPos )   ){
			owner.MoveFSM.ChangeState(SLClimbU.Instance);
		}
		//Spieler ist unter dem Gegner
		else{
			owner.MoveFSM.ChangeState(SLClimbD.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimb instance;
	private SLClimb(){}
	public static SLClimb Instance{get{
			if(instance==null) instance = new SLClimb();
			return instance;
		}}
	
	
	
}
