using UnityEngine;

/// 
/// Der Spieler ist für den Gegner nicht sichtbar, weshalb dieser dessen letzte
/// bekannte Position anstrebt
/// 
public class SSoldierSeekPosition : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = ((Soldier)owner).LastPos;
		
		//Spieler sichtbar und Höhe in Ordnung
		if( owner.LineOfSight(owner.Player) && owner.IsHeightOk(pos) ){
			owner.MoveFSM.ChangeState(SSoldierStay.I);
			return;
		}
		
		//höhe nicht in Ordnung
		if( ! owner.IsHeightOk(pos) ){
			//auf Leiter
			if( 
				(
					((Soldier)owner).CanClimbUp && owner.IsOver(pos)  
				)
				||
				(
					((Soldier)owner).CanClimbDown && owner.IsUnder(pos)
				)
			){
				//Betrete die Leiter
				owner.MoveFSM.ChangeState(SLEnter.I);
				return;
			}
			//nicht auf der selben Platform wie der Spieler
			else {
				//Bewege sich weiter in die Richtung weiter, statt in Richtung Ziel
				pos = owner.Pos + ((Soldier)owner).LastHeading * ((Soldier)owner).MaxSpeed;
			}
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if( distance <= 0.05f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SSoldierStay.I);
			return;
		}
		
		
		//Position die Angestrebt wird befindet sich direkt über oder unter
		//dem Gegner
		if( ((Soldier)owner).DirectlyAboveOrUnder(pos) )
			//Bewege sich weiter in die Richtung weiter, statt in Richtung Ziel
			//ansonsten bleibt der Gegner auf dieser Position stehen und dreht sich dauern in die eine und andere Richtung
			pos = owner.Pos + ((Soldier)owner).LastHeading * ((Soldier)owner).MaxSpeed;
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((Soldier)owner).CanMoveTo(pos) )
			//Strebe die Richtung an
			((Soldier)owner).Steering.DoSeek(pos);
		//kann nicht gehen, aber klettern
		else if( ((Soldier)owner).CanClimbTo(pos) )
			//Betrete die Leiter
			owner.MoveFSM.ChangeState(SLEnter.I);
		//keine Bewegung möglich
		else
			//stehen bleiben
			((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierSeekPosition instance;
	private SSoldierSeekPosition(){}
	public static SSoldierSeekPosition Instance{get{
			if(instance==null) instance = new SSoldierSeekPosition();
			return instance;
		}}
	public static SSoldierSeekPosition I{get{return Instance;}}
}
