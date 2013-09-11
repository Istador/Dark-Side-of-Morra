using UnityEngine;

// 
// Zustand um nach dem betreten der Leiter festzustellen ob nach oben 
// oder unten geklettert werden soll.
// 
public class SLClimb : SLState {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//Höhe ist wieder in Ordnung
		if( owner.IsHeightOk( ((Soldier)owner).LastPos ) )
			//Verlasse die Leiter wieder
			if(SLLeave.CanLeave(owner)) return;
		
		//Spieler ist über dem Gegner
		if( owner.IsOver( ((Soldier)owner).LastPos ) )
			//Klettere die Leiter nach Oben
			owner.MoveFSM.ChangeState(SLClimbU.I);
		//Spieler ist unter dem Gegner
		else
			//Klettere die Leiter nach Unten
			owner.MoveFSM.ChangeState(SLClimbD.I);
	}
	
	
	
	/// <summary>
	/// Wenn nicht nach oben gegangen werden kann verlasse die Leiter nach unten
	/// </summary>
	public static void ClimbUpCheck(Enemy<Soldier> owner){
		//kann nicht weiter nach oben
		if( ! ((Soldier)owner).CanClimbUp ){
			//kann auch nicht weiter nach unten
			if( ! ((Soldier)owner).CanClimbDown ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.I);
				return;
			}
			
			//verlasse die Leiter nach unten
			owner.MoveFSM.ChangeState(SLLeaveD.I);
			return;
		}
		
		//weiter nach oben gehen
		((Soldier)owner).MoveUp();
	}
	
	
	
	/// <summary>
	/// Wenn nicht nach unten gegangen werden kann verlasse die Leiter nach oben
	/// </summary>
	public static void ClimbDownCheck(Enemy<Soldier> owner){
		//kann nicht weiter nach unten
		if( ! ((Soldier)owner).CanClimbDown ){
			//kann auch nicht weiter nach oben
			if( ! ((Soldier)owner).CanClimbUp ){
				//Betrete die Leiter neu
				owner.MoveFSM.ChangeState(SLEnter.I);
				return;
			}
			
			//verlasse die Leiter nach oben
			owner.MoveFSM.ChangeState(SLLeaveU.I);
			return;
		}
		
		//weiter nach unten gehen
		((Soldier)owner).MoveDown();
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SLClimb instance;
	private SLClimb(){}
	public static SLClimb Instance{get{
			if(instance==null) instance = new SLClimb();
			return instance;
		}}
	public static SLClimb I{get{return Instance;}}
}
