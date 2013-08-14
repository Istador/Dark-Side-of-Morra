using UnityEngine;
using System.Collections;

public class SPatrolRight<T> : State<Enemy<T>> {
	
	
	
	public override void Enter(Enemy<T> owner){
		owner.SetSprite(1);
	}
	
	
	
	public override void Execute(Enemy<T> owner){
		Vector3 direction = owner.transform.position + Vector3.right * 1.2f;
		Vector3 down = direction + Vector3.down * 1.2f;
		
		Debug.DrawLine(owner.transform.position, direction, Color.red);
		Debug.DrawLine(direction, down, Color.blue);
		
		int layer = 1<<8; //Layer 8: Level (also  Kollision mit Level-Geometrie)
		//Kollision mit Level (z.B. Wand)
		if(
			Physics.Linecast(owner.transform.position, direction, layer)
			|| ! Physics.Linecast(direction, down, layer)
		){
			owner.MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		} else {
			((MovableEnemy<T>)owner).steering.SetTarget(direction);
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
