using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler zu weit entfernt für den Gegner, weshalb
/// dieser Vorwärtsgehend zu ihm geht.
/// 
public class SSoldierSeek : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Höhe nicht in Ordnung
		if( !owner.IsHeightOk(pos) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if( distance <= (((Soldier)owner).f_optimum_min + ((Soldier)owner).f_optimum_max)/2.0f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SSoldierStay.I);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((Soldier)owner).CanMoveTo(pos) )
			//Strebe die Richtung an
			((Soldier)owner).Steering.DoSeek(pos);
		//kann nicht gehen, aber klettern
		else if( ((Soldier)owner).CanClimbTo(pos) )
			owner.MoveFSM.ChangeState(SLEnter.I);
		//Kann sich nicht in die gewünschte Richtung bewegen
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
	private static SSoldierSeek instance;
	private SSoldierSeek(){}
	public static SSoldierSeek Instance{get{
			if(instance==null) instance = new SSoldierSeek();
			return instance;
		}}
	public static SSoldierSeek I{get{return Instance;}}
}
