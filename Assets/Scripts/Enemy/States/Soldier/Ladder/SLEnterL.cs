using UnityEngine;

// 
// Betrete die Leiter von Rechts nach Links
// 
public class SLEnterL : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> beginne klettern
		if(SLEnter.ClimbCheck(owner)) return;
		
		//kann nach links gehen oder klettern
		if( ((Soldier)owner).CanClimbLeft )
			//Bewegung nach links
			((Soldier)owner).MoveLeft();
		//klettern nicht möglich - Hindernis?
		else
			//verlasse die Leiter wieder nach Rechts
			owner.MoveFSM.ChangeState(SLLeaveR.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnterL instance;
	private SLEnterL(){}
	public static SLEnterL Instance{get{
			if(instance==null) instance = new SLEnterL();
			return instance;
		}}
	public static SLEnterL I{get{return Instance;}}
}
