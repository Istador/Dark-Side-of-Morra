using UnityEngine;
using System.Collections;

public class SPatrolLeft<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		owner.Sprite = 0;
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		if(
			((MLeftRight<T>)owner).CanMoveLeft
		){
			((MovableEnemy<T>)owner).MoveLeft();
		} else {
			owner.MoveFSM.ChangeState(SPatrolRight<T>.Instance);
		}
	}
	
	
	
	public override void Exit(Enemy<T> owner){
		((MovableEnemy<T>)owner).StopMoving();
	}
	
	
	
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
