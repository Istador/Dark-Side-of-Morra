using UnityEngine;

// 
// Verlasse die Leiter nach oben, bei der ersten Plattform verlassen
// 
public class SLLeaveU : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		
		//Links oder Rechts ist eine Plattform mit der die Leiter verlassen wird
		if( SLLeave.CanLeave(owner) ) return;
		
		//weiter nach oben sofern es geht
		SLClimb.ClimbUpCheck(owner);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeaveU instance;
	private SLLeaveU(){}
	public static SLLeaveU Instance{get{
			if(instance==null) instance = new SLLeaveU();
			return instance;
		}}
	public static SLLeaveU I{get{return Instance;}}
}
