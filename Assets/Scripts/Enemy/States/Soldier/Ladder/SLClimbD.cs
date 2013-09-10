using UnityEngine;
using System.Collections;

/// <summary>
/// Zustand um nach unten zu klettern
/// </summary>
public class SLClimbD : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Höhe des Spielers && Spieler ist unter einem
		if( ((Soldier)owner).IsHeightOk( ((Soldier)owner).LastKnownPosition() )  
			&& ! ((Soldier)owner).IsOver( ((Soldier)owner).LastKnownPosition() )
		){
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
			return;	
		}
		
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown()){
			//kann auch nicht weiter nach oben
			if( ! ((Soldier)owner).CanClimbUp() ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.Instance);
				return;
			}
			
			//verlasse die Leiter nach oben
			owner.MoveFSM.ChangeState(SLLeaveU.Instance);
			return;
		}
		
		//weiter nach unten
		((Soldier)owner).Steering.DoSeek(owner.Pos + Vector3.down * ((Soldier)owner).MaxSpeed);
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
