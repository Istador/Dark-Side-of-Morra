using UnityEngine;

// 
// Zustand um nach oben zu klettern
// 
public class SLClimbU : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//auf Höhe des Spielers && Spieler ist unter einem
		if( owner.IsHeightOk( ((Soldier)owner).LastPos )  
			&& owner.IsUnder( ((Soldier)owner).LastPos )
		){
			owner.MoveFSM.ChangeState(SLLeaveU.I);
			return;	
		}
		
		//weiter nach oben sofern es geht
		SLClimb.ClimbUpCheck(owner);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimbU instance;
	private SLClimbU(){}
	public static SLClimbU Instance{get{
			if(instance==null) instance = new SLClimbU();
			return instance;
		}}
	public static SLClimbU I{get{return Instance;}}
}
