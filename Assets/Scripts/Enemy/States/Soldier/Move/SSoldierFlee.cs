using UnityEngine;
using System.Collections;

public class SSoldierFlee : State<Enemy<Soldier>> {
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		if(!owner.LineOfSight(owner.Player)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.PlayerPos;
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if(distance >= (((Soldier)owner).f_optimum_min + ((Soldier)owner).f_optimum_max)/2.0f ){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		if( ((Soldier)owner).CanMoveTo(pos, true)   ){
			((Soldier)owner).Steering.DoFlee(pos);
		} else {
			((Soldier)owner).Steering.Fleeing = false;
		}
		
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		((Soldier)owner).Steering.Fleeing = false;
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
	
	
	
}
