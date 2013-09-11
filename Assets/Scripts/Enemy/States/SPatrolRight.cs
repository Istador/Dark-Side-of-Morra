using UnityEngine;
using System.Collections;

/// 
/// Zustand um nach Rechts zu gehen, und wenn nicht mehr nach Rechts gegangen
/// werden kann gehe in den nach Links gehen Zustand.
/// Dadurch wird abwechselnd nach Links / Rechts Patroliert
/// 
public class SPatrolRight<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		//Sprite auswählen
		owner.Sprite = 1; //nach rechts gehen
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		//Wenn nach Rechts gegangen werden kann
		if( ((MLeftRight<T>)owner).CanMoveRight )
			//Gehe nach Rechts
			((MovableEnemy<T>)owner).MoveRight();
		else
			//Wechsel in den nach Links gehen Zustand
			owner.MoveFSM.ChangeState(SPatrolLeft<T>.I);
	}
	
	
	
	public override void Exit(Enemy<T> owner){
		//anhalten
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
	public static SPatrolRight<T> I{get{return Instance;}}
}
