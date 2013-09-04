using UnityEngine;
using System.Collections;

/// <summary>
/// Verlassen der Leiter, Auswahl des nächsten Zustandes
/// </summary>
public class SLLeave : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		Debug.Log("SLLeave");
		
		//Jagd auf den Gegner
		if(owner.MoveFSM.GetGlobalState() == SSoldierEngaged.Instance){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
		}
		//Patroulieren Links
		else if(owner.MoveFSM.GetPreviousState() == SLLeaveL.Instance){
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
