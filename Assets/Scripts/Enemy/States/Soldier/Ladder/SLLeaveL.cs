using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach Links
/// </summary>
public class SLLeaveL : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Soldier)owner).steering.Seek(false);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann gehen statt klettern
		if(((Soldier)owner).CanMoveLeft()){
			owner.MoveFSM.ChangeState(SLLeave.Instance);
		}
		
		//kann nur klettern
		else if(
			((Soldier)owner).CanClimbLeft()
		){
			Vector3 direction = owner.collider.bounds.center + Vector3.left * ((Soldier)owner).maxSpeed;
			((Soldier)owner).steering.SetTarget(direction);
			((Soldier)owner).steering.Seek(true);
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
	private static SLLeaveL instance;
	private SLLeaveL(){}
	public static SLLeaveL Instance{get{
			if(instance==null) instance = new SLLeaveL();
			return instance;
		}}
	
	
	
}
