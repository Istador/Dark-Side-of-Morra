using UnityEngine;
using System.Collections;

/// <summary>
/// Verlassen der Leiter, Auswahl des nächsten Zustandes
/// </summary>
public class SLLeave : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		
		//Jagd auf den Gegner
		if( owner.MoveFSM.GlobalState == SSoldierEngaged.Instance ){
			//Spieler auf neuer Platform sichtbar?
			if( ((Soldier)owner).IsPlayerVisible() ){
				owner.MoveFSM.ChangeState(SSoldierStay.Instance);
				return;
			}
			//nicht sichtbar
			else {
				//Letzt Bekannte Position sichtbar -> Patroullieren
				if(owner.LineOfSight( ((Soldier)owner).LastKnownPosition() )){
					owner.MoveFSM.ChangeGlobalState(SSoldierPatrol.Instance);
				}
				//nicht sichtbar, position hat sich auf der Leiter verändert
				else {
					//Leiter erneut betreten
					owner.MoveFSM.ChangeState(SLEnter.Instance);
					return;
				}
				
			}
		}
		
		//Patroulieren Links
		if(owner.MoveFSM.PreviousState == SLLeaveL.Instance){
			owner.MoveFSM.ChangeState(SPatrolLeft<Soldier>.Instance);
		}
		//Patroulieren Rechts
		else {
			owner.MoveFSM.ChangeState(SPatrolRight<Soldier>.Instance);
		}
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		((Soldier)owner).IsOnLadder = false;
		owner.rigidbody.useGravity = true;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeave instance;
	private SLLeave(){}
	public static SLLeave Instance{get{
			if(instance==null) instance = new SLLeave();
			return instance;
		}}
	
	
	
}
