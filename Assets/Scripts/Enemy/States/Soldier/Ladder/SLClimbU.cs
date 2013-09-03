using UnityEngine;
using System.Collections;

public class SLClimbU : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nicht weiter nach oben
		if( ! ((Soldier)owner).CanClimbUp()){
			//verlasse die Leiter nach unten
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
			return;
		}
		
		//weiter nach oben
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.up * ((Soldier)owner).maxSpeed);		
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimbU instance;
	private SLClimbU(){}
	public static SLClimbU Instance{get{
			if(instance==null) instance = new SLClimbU();
			return instance;
		}}
	
	
	
}
