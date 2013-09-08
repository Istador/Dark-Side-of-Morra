using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach oben, bei der ersten Plattform verlassen
/// </summary>
public class SLLeaveU : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Soldier)owner).steering.Seek(false);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		// Rechts ist eine Platform sowie der Spieler
		if( owner.IsRight(((Soldier)owner).LastKnownPosition()) && ((Soldier)owner).IsPlatformRight() ){
			owner.MoveFSM.ChangeState(SLLeaveR.Instance);
			return;
		}
		
		//Links ist eine Platform sowie der Spieler
		if( !owner.IsRight(((Soldier)owner).LastKnownPosition()) &&  ((Soldier)owner).IsPlatformLeft() ){
			owner.MoveFSM.ChangeState(SLLeaveL.Instance);
			return;
		}
		
		// Rechts ist eine Platform
		if( ((Soldier)owner).IsPlatformRight() ){
			owner.MoveFSM.ChangeState(SLLeaveR.Instance);
			return;
		}
		
		//Links ist eine Platform sowie der Spieler
		if( ((Soldier)owner).IsPlatformLeft() ){
			owner.MoveFSM.ChangeState(SLLeaveL.Instance);
			return;
		}
		
		//kann nicht weiter nach oben
		if( ! ((Soldier)owner).CanClimbUp()){
			//kann auch nicht weiter nach unten
			if( ! ((Soldier)owner).CanClimbDown() ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.Instance);
				return;
			}
			
			//gehe wieder nach unten
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
			return;
		}
		
		//weiter nach oben
		((Soldier)owner).steering.SetTarget(owner.collider.bounds.center + Vector3.up * ((Soldier)owner).maxSpeed);
		((Soldier)owner).steering.Seek(true);
		
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
