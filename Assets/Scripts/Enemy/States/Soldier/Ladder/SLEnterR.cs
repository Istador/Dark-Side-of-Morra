using UnityEngine;
using System.Collections;

/// <summary>
/// Betrete die Leiter von Links nach Rechts
/// </summary>
public class SLEnterR : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> mitte erreicht
		if(   ((Soldier)owner).CanClimbUp() || ((Soldier)owner).CanClimbDown()   ){
			owner.MoveFSM.ChangeState(SLClimb.Instance);
			return;
		}
		
		//kann nach rechts gehen oder klettern
		if(   ((Soldier)owner).CanClimbRight()   ){
			//Bewegung nach rechts
			((Soldier)owner).MoveRight();
		}
		//klettern nicht möglich - Hindernis? wieder zurück
		else {
			Debug.Log("Can'tClimb");
			owner.MoveFSM.ChangeState(SLEnter.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnterR instance;
	private SLEnterR(){}
	public static SLEnterR Instance{get{
			if(instance==null) instance = new SLEnterR();
			return instance;
		}}
	
	
	
}
