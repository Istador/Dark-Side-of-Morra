using UnityEngine;
using System.Collections;

public class SSoldierSeekPosition : State<Enemy<Soldier>> {
	
	
	
	public override void Enter(Enemy<Soldier> owner){}
	
	
	
	public override void Execute(Enemy<Soldier> owner){
		if(owner.LineOfSight(owner.player)){
			owner.MoveFSM.ChangeState(SSoldierStay.Instance);
			return;
		}
		
		//Distanz zur letzt bekannten Spielerposition ermitteln
		Vector3 pos = ((Soldier)owner).LastKnownPosition();
		float distance = owner.DistanceTo(pos);
		
		//position erreicht
		if(distance <= 0.05f ){
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
	private static SSoldierSeekPosition instance;
	private SSoldierSeekPosition(){}
	public static SSoldierSeekPosition Instance{get{
			if(instance==null) instance = new SSoldierSeekPosition();
			return instance;
		}}
	
	
	
}
