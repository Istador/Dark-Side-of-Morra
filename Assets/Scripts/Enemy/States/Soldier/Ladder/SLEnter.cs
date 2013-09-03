using UnityEngine;
using System.Collections;

public class SLEnter : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		((Soldier)owner).IsOnLadder = true;
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		owner.rigidbody.useGravity = false;
		
		((Soldier)owner).steering.Seek(true);
		((Soldier)owner).steering.Evade(false);
		((Soldier)owner).steering.Flee(false);
		((Soldier)owner).steering.Pursuit(false);
		
		owner.MoveFSM.ChangeState(SLClimbU.Instance);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnter instance;
	private SLEnter(){}
	public static SLEnter Instance{get{
			if(instance==null) instance = new SLEnter();
			return instance;
		}}
	
	
	
}
