using UnityEngine;
using System.Collections;

public class SPatrolRight<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		owner.Sprite = 1;
		((MovableEnemy<T>)owner).steering.Seek(true);
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		if(
			((MLeftRight<T>)owner).CanMoveRight()
		){
			Vector3 direction = owner.collider.bounds.center + Vector3.right * ((MovableEnemy<T>)owner).maxSpeed;
			((MovableEnemy<T>)owner).steering.SetTarget(direction);
		} else {
			owner.MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		}	
	}
	
	
	
	public override void Exit(Enemy<T> owner){}
	
	
	
	/**
	 * Singleton
	*/
	private static SPatrolRight<T> instance;
	private SPatrolRight(){}
	public static SPatrolRight<T> Instance{get{
			if(instance==null) instance = new SPatrolRight<T>();
			return instance;
		}}
}
