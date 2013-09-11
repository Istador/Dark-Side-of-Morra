using UnityEngine;

// 
// Verlasse die Leiter nach Links
// 
public class SLLeaveL : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann gehen statt klettern
		if( ((Soldier)owner).CanMoveLeft )
			//Verlasse die Leiter
			owner.MoveFSM.ChangeState(SLLeave.I);
		
		//kann klettern
		else if( ((Soldier)owner).CanClimbLeft )
			//Bewegung nach Links
			((Soldier)owner).MoveLeft();
		//kein klettern oder gehen möglich - Hindernis?
		else
			//wieder auf die Leiter zurück
			owner.MoveFSM.ChangeState(SLEnter.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveL instance;
	private SLLeaveL(){}
	public static SLLeaveL Instance{get{
			if(instance==null) instance = new SLLeaveL();
			return instance;
		}}
	public static SLLeaveL I{get{return Instance;}}
}
