using UnityEngine;
using System.Collections;

/// 
/// Zustand um nach Links zu gehen, und wenn nicht mehr nach Links gegangen
/// werden kann gehe in den nach Rechts gehen Zustand.
/// Dadurch wird abwechselnd nach Links / Rechts Patroliert
/// 
public class SPatrolLeft<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		//Sprite auswählen
		owner.Sprite = 0; //nach links gehen
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		//Wenn nach Links gegangen werden kann
		if( ((MLeftRight<T>)owner).CanMoveLeft )
			//Gehe nach Links
			((MovableEnemy<T>)owner).MoveLeft();
		//kann nicht nach links gehen
		else
			//Wechsel in den nach Rechts gehen Zustand
			owner.MoveFSM.ChangeState(SPatrolRight<T>.I);
	}
	
	
	
	public override void Exit(Enemy<T> owner){
		//anhalten
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
	public static SPatrolLeft<T> I{get{return Instance;}}
}
