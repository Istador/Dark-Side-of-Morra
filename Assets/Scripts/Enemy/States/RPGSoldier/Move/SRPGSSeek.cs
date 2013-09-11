using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler zu weit entfernt für den Gegner, weshalb
/// dieser Vorwärtsgehend zu ihm geht.
/// 
public class SRPGSSeek : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if( distance <= (RPGSoldier.f_optimum_min + RPGSoldier.f_optimum_max)/2.0f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SRPGSStay.I);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((RPGSoldier)owner).CanMoveTo(pos) )
			//Strebe die Richtung an
			((RPGSoldier)owner).Steering.DoSeek(pos);
		//Kann sich nicht in die gewünschte Richtung bewegen
		else
			//stehen bleiben
			((RPGSoldier)owner).StopMoving();
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){
		//anhalten
		((RPGSoldier)owner).StopMoving();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSSeek instance;
	private SRPGSSeek(){}
	public static SRPGSSeek Instance{get{
			if(instance==null) instance = new SRPGSSeek();
			return instance;
		}}
	public static SRPGSSeek I{get{return Instance;}}
}
