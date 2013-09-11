using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler in optimaler Distanz zum Gegner, weshalb
/// dieser stehen bleibt.
/// 
public class SRPGSStay : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		//anhalten
		((RPGSoldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		
		//zu nah
		if( distance < RPGSoldier.f_optimum_min )
			//zurückgehen
			owner.MoveFSM.ChangeState(SRPGSFlee.I);
		//zu weit weg
		else if( distance > RPGSoldier.f_optimum_max )
			//annähern
			owner.MoveFSM.ChangeState(SRPGSSeek.I);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSStay instance;
	private SRPGSStay(){}
	public static SRPGSStay Instance{get{
			if(instance==null) instance = new SRPGSStay();
			return instance;
		}}
	public static SRPGSStay I{get{return Instance;}}
}
