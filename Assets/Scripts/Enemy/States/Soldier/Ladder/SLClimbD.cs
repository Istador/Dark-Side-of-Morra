using UnityEngine;
using System.Collections;

public class SLClimbD : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		((Soldier)owner).IsOnLadder = true;
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		owner.rigidbody.useGravity = false;
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		if(((Soldier)owner).CanClimbDown()){
			((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.down * ((Soldier)owner).maxSpeed);
		} else {
			owner.MoveFSM.ChangeState(SLClimbU.Instance);
		}
		
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		//((Soldier)owner).IsOnLadder = false;
		owner.rigidbody.useGravity = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimbD instance;
	private SLClimbD(){}
	public static SLClimbD Instance{get{
			if(instance==null) instance = new SLClimbD();
			return instance;
		}}
	
	
	
}
