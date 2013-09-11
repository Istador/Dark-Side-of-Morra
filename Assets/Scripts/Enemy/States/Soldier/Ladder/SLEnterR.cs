using UnityEngine;

// 
// Betrete die Leiter von Links nach Rechts
// 
public class SLEnterR : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//kann nach oben oder unten klettern -> beginne klettern
		if(SLEnter.ClimbCheck(owner)) return;
		
		//kann nach rechts gehen oder klettern
		if( ((Soldier)owner).CanClimbRight )
			//Bewegung nach rechts
			((Soldier)owner).MoveRight();
		//klettern nicht möglich - Hindernis? wieder zurück
		else
			//verlasse die Leiter wieder nach Links
			owner.MoveFSM.ChangeState(SLLeaveL.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLEnterR instance;
	private SLEnterR(){}
	public static SLEnterR Instance{get{
			if(instance==null) instance = new SLEnterR();
			return instance;
		}}
	public static SLEnterR I{get{return Instance;}}
}
