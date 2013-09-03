using UnityEngine;
using System.Collections;

public class SLClimbD : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown()){
			//verlasse die Leiter nach oben
			owner.MoveFSM.ChangeState(SLLeaveU.Instance);
			return;
		}
		
		//weiter nach unten
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.down * ((Soldier)owner).maxSpeed);
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
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
