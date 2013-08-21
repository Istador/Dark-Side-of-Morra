using UnityEngine;
using System.Collections;

public class SRPGSSeekPosition : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		if(owner.LineOfSight(owner.player)){
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
			return;
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		Vector3 pos = ((RPGSoldier)owner).LastKnownPosition();
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if(distance <= 0.05f ){
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
			return;
		}
		
		
		if(   ((RPGSoldier)owner).CanMoveTo(pos)   ){
			((RPGSoldier)owner).steering.Seek(true);
			((RPGSoldier)owner).steering.SetTarget(pos);
			
		} else {
			((RPGSoldier)owner).steering.Seek(false);
		}
	}
	
	
	
	public override void Exit(Enemy<RPGSoldier> owner){
		((RPGSoldier)owner).steering.Seek(false);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SRPGSSeekPosition instance;
	private SRPGSSeekPosition(){}
	public static SRPGSSeekPosition Instance{get{
			if(instance==null) instance = new SRPGSSeekPosition();
			return instance;
		}}
	
	
	
}
