using UnityEngine;
using System.Collections;

/// <summary>
/// Verlasse die Leiter nach unten, bei der ersten Plattform verlassen
/// </summary>
public class SLLeaveD : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		// Rechts ist eine Platform sowie der Spieler
		if( owner.IsRight(((Soldier)owner).LastKnownPosition()) && ((Soldier)owner).IsPlatformRight ){
			owner.MoveFSM.ChangeState(SLLeaveR.Instance);
			return;
		}
		
		//Links ist eine Platform sowie der Spieler
		if( !owner.IsRight(((Soldier)owner).LastKnownPosition()) &&  ((Soldier)owner).IsPlatformLeft ){
			owner.MoveFSM.ChangeState(SLLeaveL.Instance);
			return;
		}
		
		// Rechts ist eine Platform
		if( ((Soldier)owner).IsPlatformRight ){
			owner.MoveFSM.ChangeState(SLLeaveR.Instance);
			return;
		}
		
		//Links ist eine Platform sowie der Spieler
		if( ((Soldier)owner).IsPlatformLeft ){
			owner.MoveFSM.ChangeState(SLLeaveL.Instance);
			return;
		}
		
		
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown ){
			//kann auch nicht weiter nach oben
			if( ! ((Soldier)owner).CanClimbUp ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.Instance);
				return;
			}
			
			//gehe wieder nach oben
			owner.MoveFSM.ChangeState(SLLeaveU.Instance);
			return;
		}
		
		//weiter nach unten
		((Soldier)owner).MoveDown();
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
