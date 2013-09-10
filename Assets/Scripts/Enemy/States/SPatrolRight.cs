using UnityEngine;
using System.Collections;

public class SPatrolRight<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		owner.Sprite = 1;
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		if(
			((MLeftRight<T>)owner).CanMoveRight()
		){
			((MovableEnemy<T>)owner).MoveRight();
		} else {
			owner.MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		}	
	}
	
	
	
	public override void Exit(Enemy<T> owner){
		((MovableEnemy<T>)owner).StopMoving();
	}
	
	
	
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
