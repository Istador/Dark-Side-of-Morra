using UnityEngine;
using System.Collections;

public class SLClimbU : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		((Soldier)owner).IsOnLadder = true;
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		owner.rigidbody.useGravity = false;
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		if(((Soldier)owner).CanClimbUp()){
			((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.up * ((Soldier)owner).maxSpeed);
		} else {
			owner.MoveFSM.ChangeState(SLClimbD.Instance);
		}
		
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		//((Soldier)owner).IsOnLadder = false;
		owner.rigidbody.useGravity = true;
	}
	
	
	
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
