using UnityEngine;
using System.Collections;

public class SPatrolLeft<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		owner.SetSprite(0);
		((MovableEnemy<T>)owner).steering.Seek(true);
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		if(
			((MLeftRight<T>)owner).CanMoveLeft()
		){
			Vector3 direction = owner.collider.bounds.center + Vector3.left * ((MovableEnemy<T>)owner).maxSpeed;
			((MovableEnemy<T>)owner).steering.SetTarget(direction);
		} else {
			owner.MoveFSM.ChangeState(SPatrolRight<T>.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<T> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SPatrolLeft<T> instance;
	private SPatrolLeft(){}
	public static SPatrolLeft<T> Instance{get{
			if(instance==null) instance = new SPatrolLeft<T>();
			return instance;
		}}
}
