using UnityEngine;
using System.Collections;

public abstract class MLeftRight<T> : MovableEnemy<T> {
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MLeftRight`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MLeftRight(int maxHealth) : base(maxHealth){
		//zufällig nach links/rechts patrouillieren
		if(Enemy<T>.rnd.Next(0,2) == 0){
			MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		}
		else {
			MoveFSM.ChangeState(SPatrolRight<T>.Instance);
		}
	}
	
	
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		if(vIn == Vector3.zero) 
			return vIn;
		
		float a = Vector3.Angle(Vector3.left, vIn);
		//Debug.DrawLine(transform.position, transform.position + vIn * 3.0f, Color.green);
		//Debug.DrawLine(transform.position, transform.position + Vector3.left * 2.0f, Color.red);
		//Debug.Log("Angle: "+a);
		if(a < 90.000f)
			return Vector3.left * vIn.magnitude;
		else if(a > 90.000f)
			return Vector3.right * vIn.magnitude;
		return Vector3.zero;
	}
	
	
	
	private bool CanMoveToDirection(Vector3 direction){
		Bounds bb = collider.bounds;
		
		Vector3 up = Vector3.up * bb.size.y/2.05f;
		Vector3 down = Vector3.down * bb.size.y/2.1f;
		
		Debug.DrawLine(bb.center, direction, Color.red);
		Debug.DrawLine(bb.center + up, direction + up, Color.red);
		Debug.DrawLine(bb.center + down, direction + down, Color.red);
		Debug.DrawLine(direction, direction + down * 1.2f, Color.blue);
		
		int layer = 1<<8; //Layer 8: Level (also  Kollision mit Level-Geometrie)
		//Kollision mit Level (z.B. Wand)
		if(
			   Physics.Linecast(bb.center+up, direction+up, layer) //oben
			|| Physics.Linecast(bb.center, direction, layer) //mitte
			|| Physics.Linecast(bb.center+down, direction+down, layer) //unten
			|| ! Physics.Linecast(direction, direction + down * 1.2f, layer) //boden vorhanden
		){
			return false;
		} else {
			return true;
		}
	}
	
	
	
	private bool CanMoveToHeading(Vector3 heading){
		Bounds bb = collider.bounds;
		Vector3 direction = bb.center + heading * bb.size.x/1.95f;
		return CanMoveToDirection(direction);
	}
	
	
	
	public bool CanMoveLeft(){
		return CanMoveToHeading(Vector3.left);
	}
	
	
	
	public bool CanMoveRight(){
		return CanMoveToHeading(Vector3.right);
	}
	
	
	
}
