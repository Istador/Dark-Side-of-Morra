using UnityEngine;
using System.Collections;

public abstract class MLeftRightClimb<T> : MLeftRight<T> {
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MLeftRightClimb`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MLeftRightClimb(int maxHealth) : base(maxHealth){}
	
	
	
	/// <summary>Ob der Gegner sich auf einer Leiter befindet. Wird von außen verändert</summary>
	public bool IsOnLadder = false;
	
	
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		if(IsOnLadder){
			if(vIn == Vector3.zero) 
			return vIn;
		
			float a = Vector3.Angle(Vector3.left, vIn);
			
			if( (a >= -45.0f && a < 45.0f) || a >= 315.0f || a < -315.0f)
				return Vector3.left * vIn.magnitude;
			if( (a >=45.0f && a < 135.0f) || (a >= -135.0f && a < -45.0f) )
				return Vector3.up * vIn.magnitude;
			if( (a >= 135.0f && a < 225.0f) || (a >= -225.0f && a < -135.0f) )
				return Vector3.right * vIn.magnitude;
			if( (a >= 225.0f && a < 315.0f) || (a >= -315.0f && a < -225.0f) )
				return Vector3.down * vIn.magnitude;
			return Vector3.zero;
		} else {
			return base.FilterForce(vIn);
		}
	}
	
	
		
	public bool CanClimbToDirection(Vector3 direction){
		Bounds bb = collider.bounds;
		Vector3 pos = bb.center;
		Vector3 left = Vector3.left * bb.size.x/2.05f;
		Vector3 right = Vector3.right * bb.size.x/2.05f;
		
		Debug.DrawLine(pos, direction, Color.red);
		Debug.DrawLine(pos + left, direction + left, Color.red);
		Debug.DrawLine(pos + right, direction + right, Color.red);
		
		int level = 1<<8;
		int leiter = 1<<12;
		
		//keine Wand
		if(
			   Physics.Linecast(pos + left, direction + left, level) //links
			|| Physics.Linecast(pos, direction, level) //mitte
			|| Physics.Linecast(pos + right, direction + right, level) //rechts
		){
			return false;
		}
		
		//nicht das Ende der Leiter
		//kollidiert nicht wenn der Line-Ursprung innerhalb der Leiter ist, deshalb umgedrehte Logik
		if(
			   Physics.Linecast(direction + left, pos + left, leiter) //links
			|| Physics.Linecast(direction, pos, leiter) //mitte
			|| Physics.Linecast(direction + right, pos + right, leiter) //rechts
		){
			return false;
		}
		
		return true;
	}
	
	
	
	private bool CanClimbToHeading(Vector3 heading){
		Bounds bb = collider.bounds;
		Vector3 direction = bb.center + heading * bb.size.x/1.95f;
		return CanClimbToDirection(direction);
	}
	
	
	
	public bool CanClimbUp(){
		return CanClimbToHeading(Vector3.up);
	}
	
	
	
	public bool CanClimbDown(){
		return CanClimbToHeading(Vector3.down);
	}
	
	
	
	public bool CanClimbLeft(){
		return CanMoveLeft();
	}
	
	
	
	public bool CanClimbRight(){
		return CanMoveRight();
	}
	
	
	
}
