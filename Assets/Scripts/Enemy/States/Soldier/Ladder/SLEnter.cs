using UnityEngine;

// 
// Eingangszustand für das Betreten der Leiter.
// Schwerkraft ausschalten, und herausfinden ob nach links oder 
// rechts gegangen werden muss zur Leitermitte.
// 
public class SLEnter : SLState {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		base.Enter(owner);
		
		//Schalter umlegen, dass sich nun auf der Leiter befunden wird
		((Soldier)owner).IsOnLadder = true;
		
		//Schwerkraft aus (für eine einheitliche hoch/runter Geschwindigkeit)
		owner.rigidbody.useGravity = false;	
		
		//Herausfinden ob die Leiter links oder rechts ist.
		//Richtung zur Leitermitte
		Vector3 heading = ((Soldier)owner).DirectionToLadder;
		
		//von rechts kommend nach links
		if(heading == Vector3.left)
			//nach Links
			owner.MoveFSM.ChangeState(SLEnterL.I);
		//von links kommend nach rechts
		else if(heading == Vector3.right)
			//nach Rechts
			owner.MoveFSM.ChangeState(SLEnterR.I);
		//ist bereits in Leitermitte
		else if(heading == Vector3.zero)
			//starte das Klettern
			owner.MoveFSM.ChangeState(SLClimb.I);
		//Fehler- Verlasse die Leiter wieder
		else {
			Debug.LogError("SLEnter : Ungekannte Richtung zur Leitermitte");
			owner.MoveFSM.ChangeState(SLLeave.I);
		}
	}
	
	
	
	/// <summary>
	/// Wenn die Mitte erreicht wurde, beginne zu Klettern
	/// </summary>
	public static bool ClimbCheck(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> mitte erreicht
		if( ((Soldier)owner).CanClimbUp || ((Soldier)owner).CanClimbDown ){
			//Beginne das Klettern
			owner.MoveFSM.ChangeState(SLClimb.I);
			return true;
		}
		return false;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnter instance;
	private SLEnter(){}
	public static SLEnter Instance{get{
			if(instance==null) instance = new SLEnter();
			return instance;
		}}
	public static SLEnter I{get{return Instance;}}
}
