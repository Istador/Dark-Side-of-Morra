using UnityEngine;
using System.Collections;

/// <summary>
/// Betrete die Leiter von Rechts nach Links
/// </summary>
public class SLEnterL : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> mitte erreicht
		if(   ((Soldier)owner).CanClimbUp || ((Soldier)owner).CanClimbDown   ){
			owner.MoveFSM.ChangeState(SLClimb.Instance);
			return;
		}
		
		//kann nach links gehen oder klettern
		if(   ((Soldier)owner).CanClimbLeft   ){
			//Bewegung nach links
			((Soldier)owner).MoveLeft();
		}
		//klettern nicht möglich - Hindernis? wieder zurück
		else {
			owner.MoveFSM.ChangeState(SLEnter.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnterL instance;
	private SLEnterL(){}
	public static SLEnterL Instance{get{
			if(instance==null) instance = new SLEnterL();
			return instance;
		}}
	
	
	
}
