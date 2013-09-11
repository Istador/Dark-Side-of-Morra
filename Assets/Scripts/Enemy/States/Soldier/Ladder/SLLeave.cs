using UnityEngine;

// 
// Verlassen der Leiter, Auswahl des nächsten Zustandes
// 
public class SLLeave : SLState {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		base.Enter(owner);
		
		//Jagd auf den Gegner?
		if( owner.MoveFSM.GlobalState == SSoldierEngaged.I ){
			//Spieler auf neuer Platform sichtbar?
			if( ((Soldier)owner).IsPlayerVisible ){
				//zum Stehen Zustand (Angreifen)
				owner.MoveFSM.ChangeState(SSoldierStay.I);
				return;
			}
			//Spieler nicht sichtbar
			else {
				//Ist die Letzt Bekannte Position sichtbar? 
				if(owner.LineOfSight( ((Soldier)owner).LastPos )){
					//Patrolliere, weil k.A. wohin der Spieler ist
					owner.MoveFSM.ChangeGlobalState(SSoldierPatrol.I);
				}
				//nicht sichtbar, position hat sich auf der Leiter verändert
				else {
					//Leiter erneut betreten
					owner.MoveFSM.ChangeState(SLEnter.I);
					return;
				}
				
			}
		}
		
		//Patroulieren Links
		if(owner.MoveFSM.PreviousState == SLLeaveL.I){
			owner.MoveFSM.ChangeState(SPatrolLeft<Soldier>.I);
		}
		//Patroulieren Rechts
		else {
			owner.MoveFSM.ChangeState(SPatrolRight<Soldier>.I);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		//nicht mehr auf der Leiter
		((Soldier)owner).IsOnLadder = false;
		//Schwerkraft wieder benutzen
		owner.rigidbody.useGravity = true;
	}
	
	
	
	/// <summary>
	/// Methode um zu überprüfen ob hier die Leiter verlassen werden kann.
	/// Verlässt die Leiter dann auch!
	/// </summary>
	public static bool CanLeave(Enemy<Soldier> owner){
		
		//Ist die Postition rechts?
		bool pos_right = owner.IsRight( ((Soldier)owner).LastPos );
		
		//Ist Rechts eine Platform
		bool right = ((Soldier)owner).IsPlatformRight;
		
		// Rechts ist eine Platform sowie der Spieler
		if( pos_right && right ){
			//Verlasse die Leiter nach Rechts
			owner.MoveFSM.ChangeState(SLLeaveR.I);
			return true;
		}
		
		//Ist Links eine Platform?
		bool left = ((Soldier)owner).IsPlatformLeft;
		
		//Links ist eine Platform sowie der Spieler
		if( ! pos_right &&  left ){
			//Verlasse die Leiter nach Links
			owner.MoveFSM.ChangeState(SLLeaveL.I);
			return true;
		}
		
		// Rechts ist eine Platform
		if( right ){
			//Verlasse die Leiter nach Rechts
			owner.MoveFSM.ChangeState(SLLeaveR.I);
			return true;
		}
		
		//Links ist eine Platform sowie der Spieler
		if( left ){
			//Verlasse die Leiter nach Links
			owner.MoveFSM.ChangeState(SLLeaveL.I);
			return true;
		}
		
		//Verlasse die Leiter nicht
		return false;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLLeave instance;
	private SLLeave(){}
	public static SLLeave Instance{get{
			if(instance==null) instance = new SLLeave();
			return instance;
		}}
	public static SLLeave I{get{return Instance;}}
}
