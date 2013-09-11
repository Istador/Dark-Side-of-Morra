using UnityEngine;

// 
// Zustand um nach unten zu klettern
// 
public class SLClimbD : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Höhe des Spielers && Spieler ist unter einem
		if( owner.IsHeightOk( ((Soldier)owner).LastPos )  
			&& owner.IsUnder( ((Soldier)owner).LastPos )
		){
			owner.MoveFSM.ChangeState(SLLeaveD.I); // evtl. U?
			return;
		}
		
		//weiter nach unten sofern es geht
		SLClimb.ClimbDownCheck(owner);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimbD instance;
	private SLClimbD(){}
	public static SLClimbD Instance{get{
			if(instance==null) instance = new SLClimbD();
			return instance;
		}}
	public static SLClimbD I{get{return Instance;}}
}
