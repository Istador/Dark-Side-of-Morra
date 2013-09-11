using UnityEngine;

/// 
/// Der Spieler ist für den Gegner nicht sichtbar, weshalb dieser dessen letzte
/// bekannte Position anstrebt
/// 
public class SRPGSSeekPosition : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		Vector3 pos = ((RPGSoldier)owner).LastPos;
		
		//Spieler sichtbar
		if( owner.LineOfSight(owner.Player) ){
			owner.MoveFSM.ChangeState(SRPGSStay.I);
			return;
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if( distance <= 0.05f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SRPGSStay.I);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((RPGSoldier)owner).CanMoveTo(pos) )
			//Strebe die Richtung an
			((RPGSoldier)owner).Steering.DoSeek(pos);
		//keine Bewegung möglich
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
	private static SRPGSSeekPosition instance;
	private SRPGSSeekPosition(){}
	public static SRPGSSeekPosition Instance{get{
			if(instance==null) instance = new SRPGSSeekPosition();
			return instance;
		}}
	public static SRPGSSeekPosition I{get{return Instance;}}
}
