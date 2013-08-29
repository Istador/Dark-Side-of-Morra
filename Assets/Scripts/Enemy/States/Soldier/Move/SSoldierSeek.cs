using UnityEngine;
using System.Collections;

public class SSoldierSeek : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){
		((Soldier)owner).steering.Flee(false);
	}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		if(!owner.LineOfSight(owner.player)){
			owner.MoveFSM.ChangeState(SSoldierSeekPosition.Instance);
			return;
		}
		
		//Distanz zum Spieler ermitteln
		Vector3 pos = owner.player.collider.bounds.center;
		float distance = owner.DistanceTo(pos);
		
		//optimale position erreicht
		if(distance <= (Soldier.f_optimum_min + Soldier.f_optimum_max)/2.0f ){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		
		if( ((Soldier)owner).CanMoveTo(pos)   ){
			((Soldier)owner).steering.Seek(true);
			((Soldier)owner).steering.SetTarget(pos);
			
		} else {
			((Soldier)owner).steering.Seek(false);
		}
	}
	
	
	
	public override void Exit(Enemy<Soldier> owner){
		((Soldier)owner).steering.Seek(false);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static SSoldierSeek instance;
	private SSoldierSeek(){}
	public static SSoldierSeek Instance{get{
			if(instance==null) instance = new SSoldierSeek();
			return instance;
		}}
	
	
	
}