using UnityEngine;
using System.Collections;

public class SRPGSStay : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){
		((RPGSoldier)owner).steering.Seek(false);
		((RPGSoldier)owner).steering.Flee(false);
		((RPGSoldier)owner).steering.SetTarget(null);
	}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		//Spieler nicht sichtbar
		if(!owner.LineOfSight(owner.player)){
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.player.collider.bounds.center;
		float distance = owner.DistanceTo(pos);
		//zu nah
		if(distance < RPGSoldier.f_optimum_min)
			owner.MoveFSM.ChangeState(SRPGSFlee.Instance); //zurückgehen
		//zu weit weg
		else if(distance > RPGSoldier.f_optimum_max)
			owner.MoveFSM.ChangeState(SRPGSSeek.Instance); //annähern
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSStay instance;
	private SRPGSStay(){}
	public static SRPGSStay Instance{get{
			if(instance==null) instance = new SRPGSStay();
			return instance;
		}}
	
	
	
}
