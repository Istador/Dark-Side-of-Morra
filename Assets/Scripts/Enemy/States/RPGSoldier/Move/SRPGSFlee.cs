using UnityEngine;
using System.Collections;

public class SRPGSFlee : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		if(!owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.PlayerPos;
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if(distance >= (RPGSoldier.f_optimum_min + RPGSoldier.f_optimum_max)/2.0f ){
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
			return;
		}
		
		if(   ((RPGSoldier)owner).CanMoveTo(pos, true)   ){
			((RPGSoldier)owner).steering.Flee(true);
			((RPGSoldier)owner).steering.SetTarget(pos);
		} else {
			((RPGSoldier)owner).steering.Flee(false);
		}
		
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){
		((RPGSoldier)owner).steering.Flee(false);
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
	
	
	
}
