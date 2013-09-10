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
	
	
	
	// auf Leitern langsamer Bewegen
	public static float ladderSlowDown = 0.25f;
		
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		if(IsOnLadder){
			if(vIn == Vector3.zero) 
				return vIn;
			
			float a = Mathf.Atan2 (vIn.x, vIn.y) * Mathf.Rad2Deg + 90.0f;
			// -90 links, 90° rechts, 0° oben, 180° unten
			
			
			if( (a >= -45.0f && a < 45.0f) || a >= 315.0f || a < -315.0f)
				return Vector3.left * vIn.magnitude * ladderSlowDown;
			if( (a >=45.0f && a < 135.0f) || (a >= -135.0f && a < -45.0f) )
				return Vector3.up * vIn.magnitude * ladderSlowDown;
			if( (a >= 135.0f && a < 225.0f) || (a >= -225.0f && a < -135.0f) )
				return Vector3.right * vIn.magnitude * ladderSlowDown;
			if( (a >= 225.0f && a < 315.0f) || (a >= -315.0f && a < -225.0f) )
				return Vector3.down * vIn.magnitude * ladderSlowDown;
			return Vector3.zero;
		} else {
			return base.FilterForce(vIn);
		}
	}
	
	
	private bool CanClimbToHeading(Vector3 heading){
		Bounds bb = collider.bounds;
		
		Vector3 pos = bb.center;
		Vector3 direction = pos + heading * bb.size.y/1.95f;
		
		Vector3 left = Vector3.left * bb.size.x/2.15f;
		Vector3 right = Vector3.right * bb.size.x/2.15f;
		Vector3 up = pos + Vector3.up * bb.size.x/2.15f;
		Vector3 down = pos + Vector3.down * bb.size.x/2.15f;
		
		int level = 1<<8;
		int leiter = 1<<12;
		
		//nicht in der Mitte der Leiter?
		if(
			!(
			     Physics.Linecast(pos + left, pos, leiter) //links nach mitte
			  && Physics.Linecast(pos + right, pos, leiter) //rechts nach mitte
			)
			&&
			!(
			     Physics.Linecast(up + left, up, leiter) //links nach mitte
			  && Physics.Linecast(up + right, up, leiter) //rechts nach mitte
			)
			&&
			!(
			     Physics.Linecast(down + left, down, leiter) //links nach mitte
			  && Physics.Linecast(down + right, down, leiter) //rechts nach mitte
			)
		){
			return false;
		}
		
		Debug.DrawLine(pos, direction, Color.red);
		Debug.DrawLine(pos + left, direction + left, Color.red);
		Debug.DrawLine(pos + right, direction + right, Color.red);
		
		//Wand die den Weg nach oben/unten versperrt?
		if(
			   Physics.Linecast(pos + left, direction + left, level) //links
			|| Physics.Linecast(pos, direction, level) //mitte
			|| Physics.Linecast(pos + right, direction + right, level) //rechts
		){
			return false;
		}
		
		
		
		//Position verschieben, damit weiter geklettert werden kann
		Vector3 pos2 = pos - heading * bb.size.y/2.0f;
		Vector3 direction2 = pos2 + heading * bb.size.y/1.95f;
		
		//nicht das Ende der Leiter
		//kollidiert nicht wenn der Line-Ursprung innerhalb der Leiter ist, deshalb umgedrehte Logik
		if(
			//   Physics.Linecast(direction + left, pos + left, leiter) //links
			/*||*/ Physics.Linecast(direction2, pos2, leiter) //mitte
			//|| Physics.Linecast(direction + right, pos + right, leiter) //rechts
		){
			return false;
		}
		
		return true;
	}
	
	
	
	/// <summary>Ob der Gegner weiter nach oben klettern kann (kein Hindernis, nicht das Leiterende)</summary>
	public bool CanClimbUp(){
		return CanClimbToHeading(Vector3.up);
	}
	/// <summary>Ob der Gegner weiter nach unten klettern kann (kein Hindernis, nicht das Leiterende)</summary>
	public bool CanClimbDown(){
		return CanClimbToHeading(Vector3.down);
	}
	
	
	
	//Hilfsmethode
	private bool IsPlatformBelow(Vector3 heading){
		Bounds bb = collider.bounds;
		Vector3 pos = bb.center;
		Vector3 direction = heading * bb.size.x/1.0f;
		Vector3 down = Vector3.down * bb.size.y/1.8f;
		int layer = 1<<8; //Level
		
		
		Debug.DrawLine(pos, pos + direction + down, Color.yellow);
		Debug.DrawLine(pos + down*0.9f, pos + direction + down*0.9f, Color.red);
		Debug.DrawLine(pos + down*0.8f, pos + direction + down*0.8f, Color.red);
		Debug.DrawLine(pos + down*0.6f, pos + direction + down*0.6f, Color.red);
		Debug.DrawLine(pos + down*0.4f, pos + direction + down*0.4f, Color.red);
		Debug.DrawLine(pos + down*0.2f, pos + direction + down*0.2f, Color.red);
		Debug.DrawLine(pos, pos + direction, Color.red);
		
		return Physics.Linecast(pos, pos + direction + down, layer) //Platform unter einem
			//Platform nicht neben einem	
			&& ! Physics.Linecast(pos + down*0.9f, pos + direction + down*0.9f, layer)
			&& ! Physics.Linecast(pos + down*0.8f, pos + direction + down*0.8f, layer)
			&& ! Physics.Linecast(pos + down*0.6f, pos + direction + down*0.6f, layer)
			&& ! Physics.Linecast(pos + down*0.4f, pos + direction + down*0.4f, layer)
			&& ! Physics.Linecast(pos + down*0.2f, pos + direction + down*0.2f, layer)
			&& ! Physics.Linecast(pos, pos + direction, layer)
			;
			 
	}
	
	
	
	/// <summary>Ob Rechts unter dem Gegner eine Platform ist die betreten werden kann</summary>
	public bool IsPlatformRight(){
		return IsPlatformBelow(Vector3.right);
	}
	/// <summary>Ob Links unter dem Gegner eine Platform ist die betreten werden kann</summary>
	public bool IsPlatformLeft(){
		return IsPlatformBelow(Vector3.left);
	}
	
	
	public Vector3 DirectionToLadder(){
		Bounds bb = collider.bounds;
		Vector3 pos = bb.center;
		Vector3 left = Vector3.left * bb.size.x/2.05f;
		Vector3 right = Vector3.right * bb.size.x/2.05f;
		
		int leiter = 1<<12;
		
		//ob in Mitte
		bool leftOk = Physics.Linecast(pos + left, pos, leiter); //links nach mitte
		bool rightOk = Physics.Linecast(pos + right, pos, leiter); //rechts nach mitte
		
		if(leftOk && rightOk) return Vector3.zero;
		if(leftOk && !rightOk) return Vector3.right;
		if(!leftOk && rightOk) return Vector3.left;
		
		//Leiter ist Links
		if( Physics.Linecast(pos, pos + left * 2.0f, leiter) ) return Vector3.left;
		//Leiter ist Rechts
		if( Physics.Linecast(pos, pos + right * 2.0f, leiter) ) return Vector3.right;
		
		return Vector3.down;
	}
	
	
	//Hilfsmethode
	private bool CanClimbLeftRight(Vector3 heading){
		Bounds bb = collider.bounds;
		Vector3 pos = bb.center;
		
		//Vector3 up = Vector3.up * bb.size.y/2.05f;
		//Vector3 down = Vector3.down * bb.size.y/2.2f;
		
		Vector3 vFrom = pos - heading * bb.size.x / 4.0f;
		Vector3 vTo = pos + heading * bb.size.x / 1.0f;
		
		
		//int level = 1<<8; //Level
		int leiter = 1<<12; //Leiter
		
		
		/*
		Debug.DrawLine(pos + up, vTo + up);
		Debug.DrawLine(pos, vTo);
		Debug.DrawLine(pos + down, vTo + down);
		
		//Hindernisse?
		if(
			   Physics.Linecast(pos + up, vTo + up, level)		//Hindernis oben
			|| Physics.Linecast(pos, vTo, level)				//Hindernis mitte
			|| Physics.Linecast(pos + down, vTo + down, level)	//Hindernis unten
		){
			return false;
		}
		*/
		
		//Leiter vorhanden?
		return Physics.Linecast(vFrom, vTo, leiter) || Physics.Linecast(vTo, vFrom, leiter);
	}
	
	
	
	public bool CanClimbLeft(){
		return CanMoveLeft() || CanClimbLeftRight(Vector3.left);
	}
	public bool CanClimbRight(){
		return CanMoveRight() || CanClimbLeftRight(Vector3.right);
	}
	
	
	
}
