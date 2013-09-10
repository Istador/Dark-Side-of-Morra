using UnityEngine;
using System.Collections;

/// <summary>
/// Zustand um nach oben zu klettern
/// </summary>
public class SLClimbU : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Höhe des Spielers && Spieler ist unter einem
		if( ((Soldier)owner).IsHeightOk( ((Soldier)owner).LastKnownPosition() )  
			&& ! ((Soldier)owner).IsOver( ((Soldier)owner).LastKnownPosition() )
		){
			owner.MoveFSM.ChangeState(SLLeaveU.Instance);
			return;	
		}
		
		//kann nicht weiter nach oben
		if( ! ((Soldier)owner).CanClimbUp ){
			//kann auch nicht weiter nach unten
			if( ! ((Soldier)owner).CanClimbDown ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.Instance);
				return;
			}
			
			//verlasse die Leiter nach unten
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
			return;
		}
		
		//weiter nach oben
		((Soldier)owner).Steering.DoSeek(owner.Pos + Vector3.up * ((Soldier)owner).MaxSpeed);		
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
