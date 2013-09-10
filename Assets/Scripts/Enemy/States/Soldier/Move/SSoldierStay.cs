using UnityEngine;
using System.Collections;

public class SSoldierStay : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		//anhalten
		((Soldier)owner).StopMoving();
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		Vector3 pos = owner.PlayerPos;
		
		//Spieler nicht sichtbar
		if(!owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Höhe nicht in Ordnung
		if(!((Soldier)owner).IsHeightOk(pos)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		float distance = owner.DistanceTo(pos);
		//zu nah
		if(distance < ((Soldier)owner).f_optimum_min)
			owner.MoveFSM.ChangeState(SSoldierFlee.Instance); //zurückgehen
		//zu weit weg
		else if(distance > ((Soldier)owner).f_optimum_max)
			owner.MoveFSM.ChangeState(SSoldierSeek.Instance); //annähern
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
	
	
	
}
