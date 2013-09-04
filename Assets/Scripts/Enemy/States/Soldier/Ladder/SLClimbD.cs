using UnityEngine;
using System.Collections;

/// <summary>
/// Zustand um nach unten zu klettern
/// </summary>
public class SLClimbD : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		Debug.Log("SLClimbD");
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Soldier)owner).steering.Seek(false);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//TODO: feststellen ob auf Höhe des Spielers
		
		//Player bereits über einen
		if(  ((Soldier)owner).IsOver( ((Soldier)owner).LastKnownPosition() )  ){
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
			return;	
		}
		
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown()){
			//verlasse die Leiter nach oben
			owner.MoveFSM.ChangeState(SLLeaveU.Instance);
			return;
		}
		
		//weiter nach unten
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.down * ((Soldier)owner).maxSpeed);
		((Soldier)owner).steering.Seek(true);
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
