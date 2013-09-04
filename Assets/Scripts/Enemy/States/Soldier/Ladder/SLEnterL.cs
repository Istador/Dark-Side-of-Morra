using UnityEngine;
using System.Collections;

/// <summary>
/// Betrete die Leiter von Rechts nach Links
/// </summary>
public class SLEnterL : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		Debug.Log("SLEnterL");
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Soldier)owner).steering.Seek(false);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> mitte erreicht
		if(   ((Soldier)owner).CanClimbUp() || ((Soldier)owner).CanClimbDown()   ){
			owner.MoveFSM.ChangeState(SLClimb.Instance);
			return;
		}
		
		//kann nach links gehen oder klettern
		if(   ((Soldier)owner).CanClimbLeft()   ){
			//Bewegung nach links
			Vector3 direction = owner.collider.bounds.center + Vector3.left * ((Soldier)owner).maxSpeed;
			((Soldier)owner).steering.SetTarget(direction);
			((Soldier)owner).steering.Seek(true);
		}
		//klettern nicht möglich - Hindernis? wieder zurück
		else {
			owner.MoveFSM.ChangeState(SLEnter.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnterL instance;
	private SLEnterL(){}
	public static SLEnterL Instance{get{
			if(instance==null) instance = new SLEnterL();
			return instance;
		}}
	
	
	
}
