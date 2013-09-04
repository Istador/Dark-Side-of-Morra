using UnityEngine;
using System.Collections;

/// <summary>
/// Eingangszustand für das Betreten der Leiter.
/// Schwerkraft ausschalten, und herausfinden ob nach links oder 
/// rechts gegangen werden muss zur Leitermitte.
/// </summary>
public class SLEnter : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		Debug.Log("SLEnter");
		
		((Soldier)owner).IsOnLadder = true;
		
		//anhalten
		owner.rigidbody.velocity = Vector3.zero;
		owner.rigidbody.angularVelocity = Vector3.zero;
		((Soldier)owner).steering.Seek(false);
		((Soldier)owner).steering.Evade(false);
		((Soldier)owner).steering.Flee(false);
		((Soldier)owner).steering.Pursuit(false);
		
		//Schwerkraft aus (für einheitliche hoch und runter geschwindigkeit)
		owner.rigidbody.useGravity = false;	
		
		//Herausfinden ob die Leiter links oder rechts ist.
		Vector3 pos = owner.collider.bounds.center;
		
		
		//Richtung zur Leitermitte
		Vector3 heading = ((Soldier)owner).DirectionToLadder();
		
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
			Debug.Log("keine Richtung");
			owner.MoveFSM.ChangeState(SLLeave.Instance);
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
