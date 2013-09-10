using UnityEngine;
using System.Collections;

public class SRPGSSeek : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		if(!owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SRPGSSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.PlayerPos;
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if(distance <= (RPGSoldier.f_optimum_min + RPGSoldier.f_optimum_max)/2.0f ){
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
			return;
		}
		
		
		if(   ((RPGSoldier)owner).CanMoveTo(pos)   ){
			((RPGSoldier)owner).Steering.DoSeek(pos);
			
		} else {
			((RPGSoldier)owner).Steering.Seeking = false;
		}
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){
		((RPGSoldier)owner).Steering.Seeking = false;
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSSeek instance;
	private SRPGSSeek(){}
	public static SRPGSSeek Instance{get{
			if(instance==null) instance = new SRPGSSeek();
			return instance;
		}}
	
	
	
}
