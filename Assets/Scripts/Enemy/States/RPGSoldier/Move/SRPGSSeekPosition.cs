using UnityEngine;
using System.Collections;

public class SRPGSSeekPosition : State<Enemy<RPGSoldier>> {
	
	
	
	public override void Enter(Enemy<RPGSoldier> owner){}
	
	
	
	public override void Execute(Enemy<RPGSoldier> owner){
		if(owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SRPGSStay.Instance);
			return;
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		Vector3 pos = ((RPGSoldier)owner).LastPos;
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if(distance <= 0.05f ){
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
	private static SRPGSSeekPosition instance;
	private SRPGSSeekPosition(){}
	public static SRPGSSeekPosition Instance{get{
			if(instance==null) instance = new SRPGSSeekPosition();
			return instance;
		}}
	
	
	
}
