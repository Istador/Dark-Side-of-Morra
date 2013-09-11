using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler dem Gegner zu nahe, weshalb
/// dieser Rückwärtsgehend vor ihm flieht.
/// 
public class SRPGSFlee : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer;
		
		//optimale position erreicht
		if( distance >= (RPGSoldier.f_optimum_min + RPGSoldier.f_optimum_max)/2.0f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SRPGSStay.I);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((RPGSoldier)owner).CanMoveTo(pos, true) )
			//Fliehe in die Richtung
			((RPGSoldier)owner).Steering.DoFlee(pos);
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
	private static SRPGSFlee instance;
	private SRPGSFlee(){}
	public static SRPGSFlee Instance{get{
			if(instance==null) instance = new SRPGSFlee();
			return instance;
		}}
	public static SRPGSFlee I{get{return Instance;}}
}
