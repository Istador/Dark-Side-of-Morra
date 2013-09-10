using UnityEngine;
using System.Collections;

/// <summary>
/// Eingangszustand für das Betreten der Leiter.
/// Schwerkraft ausschalten, und herausfinden ob nach links oder 
/// rechts gegangen werden muss zur Leitermitte.
/// </summary>
public class SLEnter : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		
		((Soldier)owner).IsOnLadder = true;
		
		//anhalten
		((Soldier)owner).StopMoving();
		
		//Schwerkraft aus (für einheitliche hoch und runter geschwindigkeit)
		owner.rigidbody.useGravity = false;	
		
		//Herausfinden ob die Leiter links oder rechts ist.
		//Richtung zur Leitermitte
		Vector3 heading = ((Soldier)owner).DirectionToLadder;
		
		//von rechts kommend nach links
		if(heading == Vector3.left){
			owner.MoveFSM.ChangeState(SLEnterL.Instance);
		}
		//von links kommend nach rechts
		else if(heading == Vector3.right){
			owner.MoveFSM.ChangeState(SLEnterR.Instance);
		}
		//ist bereits in Leitermitte
		else if(heading == Vector3.zero){
			owner.MoveFSM.ChangeState(SLClimb.Instance);
		}
		//Fehler- Verlasse die Leiter wieder
		else {
			//Debug.Log("keine Richtung");
			owner.MoveFSM.ChangeState(SLLeaveD.Instance);
		}
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
