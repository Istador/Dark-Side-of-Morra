using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach Rechts
/// </summary>
public class SLLeaveR : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann gehen statt klettern
		if(((Soldier)owner).CanMoveRight()){
			owner.MoveFSM.ChangeState(SLLeave.Instance);
		}
		
		//kann nur klettern
		else if(
			((Soldier)owner).CanClimbRight()
		){
			((Soldier)owner).MoveRight();
		}
		
		//nicht klettern - Hindernis? wieder auf die Leiter zurück
		else {
			owner.MoveFSM.ChangeState(SLEnter.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveR instance;
	private SLLeaveR(){}
	public static SLLeaveR Instance{get{
			if(instance==null) instance = new SLLeaveR();
			return instance;
		}}
	
	
	
}
