using UnityEngine;
using System.Collections;

public class SSoldierStay : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		((Soldier)owner).steering.Seek(false);
		((Soldier)owner).steering.Flee(false);
		((Soldier)owner).steering.SetTarget(null);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		//Spieler nicht sichtbar
		if(!owner.LineOfSight(owner.player)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.player.collider.bounds.center;
		float distance = owner.DistanceTo(pos);
		//zu nah
		if(distance < Soldier.f_optimum_min)
			owner.MoveFSM.ChangeState(SSoldierFlee.Instance); //zurückgehen
		//zu weit weg
		else if(distance > Soldier.f_optimum_max)
			owner.MoveFSM.ChangeState(SSoldierSeek.Instance); //annähern
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){}
	
	
	
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
