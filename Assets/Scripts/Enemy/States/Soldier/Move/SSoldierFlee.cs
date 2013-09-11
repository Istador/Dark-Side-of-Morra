using UnityEngine;

/// 
/// In diesem Zustand ist der Spieler dem Gegner zu nahe, weshalb
/// dieser Rückwärtsgehend vor ihm flieht.
/// 
public class SSoldierFlee : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if( !owner.LineOfSight(owner.Player) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Höhe nicht in Ordnung
		if( ! owner.IsHeightOk(pos) ){
			//Strebe die zuletzt bekannte Position an
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.I);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceToPlayer;
		
		//optimale position erreicht
		if( distance >= (((Soldier)owner).f_optimum_min + ((Soldier)owner).f_optimum_max)/2.0f ){
			//zum Stehen Zustand
			owner.MoveFSM.ChangeState(SSoldierStay.I);
			return;
		}
		
		//Kann sich in gewünschte Richtung bewegen
		if( ((Soldier)owner).CanMoveTo(pos, true)   )
			//Fliehe in die Richtung
			((Soldier)owner).Steering.DoFlee(pos);
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
	private static SSoldierFlee instance;
	private SSoldierFlee(){}
	public static SSoldierFlee Instance{get{
			if(instance==null) instance = new SSoldierFlee();
			return instance;
		}}
	public static SSoldierFlee I{get{return Instance;}}
}
