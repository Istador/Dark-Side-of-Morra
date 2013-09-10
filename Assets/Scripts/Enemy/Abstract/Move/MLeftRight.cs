using UnityEngine;
using System.Collections;

public abstract class MLeftRight<T> : MovableEnemy<T> {
	
	
	
	/// <summary>
	/// Richtung in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public override Vector3 Heading { get{
			if(MoveFSM.IsInState(SPatrolLeft<T>.Instance))
				return Vector3.left;
			else if(MoveFSM.IsInState(SPatrolRight<T>.Instance))
				return Vector3.right;
			return  Vector3.zero;
		}
	}
	
	
	
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
	
	
	
	private bool CanMoveToHeading(Vector3 heading){
		Bounds bb = collider.bounds;
		Vector3 pos = bb.center;
		
		Vector3 direction = bb.center + heading * bb.size.x/1.95f;
		Vector3 direction2 = bb.center + heading * bb.size.x/4.0f;
		
		Vector3 up = Vector3.up * bb.size.y/2.05f;
		Vector3 down = Vector3.down * bb.size.y/2.1f;
		Vector3 down2 = Vector3.down * bb.size.y/1.8f;
		
		Debug.DrawLine(pos, direction, Color.red);
		Debug.DrawLine(pos + up, direction + up, Color.red);
		Debug.DrawLine(pos + down, direction + down, Color.red);
		Debug.DrawLine(direction, direction + down * 1.2f, Color.blue);
		Debug.DrawLine(direction2, direction2 + down * 1.2f, Color.blue);
		
		int layer = 1<<8; //Layer 8: Level (also  Kollision mit Level-Geometrie)
		//Kollision mit Level (z.B. Wand)
		if(
			   Physics.Linecast(pos + up, direction + up, layer) //oben
			|| Physics.Linecast(pos, direction, layer) //mitte
			|| Physics.Linecast(pos + down, direction + down, layer) //unten
			|| ! Physics.Linecast(direction, direction + down2, layer) //boden vorhanden
			|| ! Physics.Linecast(direction2, direction2 + down2, layer) //boden vorhanden
		){
			return false;
		} else {
			return true;
		}
	}
	
		
	
	public bool CanMoveLeft(){
		return CanMoveToHeading(Vector3.left);
	}
	
	
	
	public bool CanMoveRight(){
		return CanMoveToHeading(Vector3.right);
	}
	
	
	
}
