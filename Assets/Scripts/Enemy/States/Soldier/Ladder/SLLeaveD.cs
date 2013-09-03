using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach unten, bei der ersten Plattform verlassen
/// </summary>
public class SLLeaveD : State<Enemy<Soldier>> {
	
	
	
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
		
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown()){
			//gehe wieder nach oben
			owner.MoveFSM.ChangeState(SLClimbU.Instance);
			return;
		}
		
		//weiter nach unten
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.down * ((Soldier)owner).maxSpeed);
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveD instance;
	private SLLeaveD(){}
	public static SLLeaveD Instance{get{
			if(instance==null) instance = new SLLeaveD();
			return instance;
		}}
	
	
	
}
