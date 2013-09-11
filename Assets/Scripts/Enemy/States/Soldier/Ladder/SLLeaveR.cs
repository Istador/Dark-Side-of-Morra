using UnityEngine;

// 
// Verlasse die Leiter nach Rechts
// 
public class SLLeaveR : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann gehen statt klettern
		if( ((Soldier)owner).CanMoveRight )
			//Verlasse die Leiter
			owner.MoveFSM.ChangeState(SLLeave.I);
		
		//kann klettern
		else if( ((Soldier)owner).CanClimbRight )
			//Bewegung nach Rechts
			((Soldier)owner).MoveRight();
		//kein klettern oder gehen möglich - Hindernis?
		else
			//wieder auf die Leiter zurück
			owner.MoveFSM.ChangeState(SLEnter.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveR instance;
	private SLLeaveR(){}
	public static SLLeaveR Instance{get{
			if(instance==null) instance = new SLLeaveR();
			return instance;
		}}
	public static SLLeaveR I{get{return Instance;}}
}
