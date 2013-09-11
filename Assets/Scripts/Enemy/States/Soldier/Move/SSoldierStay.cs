using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler in optimaler Distanz zum Gegner, weshalb
/// dieser stehen bleibt.
/// 
public class SSoldierStay : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Höhe nicht in Ordnung
		if( !owner.IsHeightOk(pos) ){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		
		//zu nah
		if( distance < ((Soldier)owner).f_optimum_min && ((Soldier)owner).CanMoveTo(pos, true) )
			//zurückgehen
			owner.MoveFSM.ChangeState(SSoldierFlee.I);
		//zu weit weg
		else if( distance > ((Soldier)owner).f_optimum_max  && ((Soldier)owner).CanMoveTo(pos) )
			//annähern
			owner.MoveFSM.ChangeState(SSoldierSeek.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierStay instance;
	private SSoldierStay(){}
	public static SSoldierStay Instance{get{
			if(instance==null) instance = new SSoldierStay();
			return instance;
		}}
	public static SSoldierStay I{get{return Instance;}}
}
