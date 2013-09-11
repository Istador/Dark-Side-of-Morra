using UnityEngine;

// 
// Verlasse die Leiter nach unten, bei der ersten Plattform verlassen
// 
public class SLLeaveD : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		
		//Links oder Rechts ist eine Plattform mit der die Leiter verlassen wird
		if( SLLeave.CanLeave(owner) ) return;
		
		//weiter nach unten sofern es geht
		SLClimb.ClimbDownCheck(owner);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveD instance;
	private SLLeaveD(){}
	public static SLLeaveD Instance{get{
			if(instance==null) instance = new SLLeaveD();
			return instance;
		}}
	public static SLLeaveD I{get{return Instance;}}
}
