using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach oben, bei der ersten Plattform verlassen
/// </summary>
public class SLLeaveU : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		// Rechts ist eine Platform
		if( ((Soldier)owner).IsPlatformRight() ){
			owner.MoveFSM.ChangeState(SLLeaveR.Instance);
			return;
		}
		
		//Links ist eine Platform
		if( ((Soldier)owner).IsPlatformLeft() ){
			owner.MoveFSM.ChangeState(SLLeaveL.Instance);
			return;
		}
		
		//kann nicht weiter nach oben
		if( ! ((Soldier)owner).CanClimbUp()){
			//gehe wieder nach unten
			owner.MoveFSM.ChangeState(SLClimbD.Instance);
			return;
		}
		
		//weiter nach oben
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.up * ((Soldier)owner).maxSpeed);
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveU instance;
	private SLLeaveU(){}
	public static SLLeaveU Instance{get{
			if(instance==null) instance = new SLLeaveU();
			return instance;
		}}
	
	
	
}
