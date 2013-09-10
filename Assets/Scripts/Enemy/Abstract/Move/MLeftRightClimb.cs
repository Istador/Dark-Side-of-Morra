using UnityEngine;
using System.Collections;

/// 
/// Diese Klasse Beschränkt die Bewegung auf die Horizontale,
/// also Bewegung ist nur nach Links oder Rechts möglich.
/// Außer der Gegnerbefindet sich auf einer Leiter, dann ist auch
/// Begung nach Oben oder Unten möglich.
/// 
public abstract class MLeftRightClimb<T> : MLeftRight<T> {
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MLeftRightClimb`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MLeftRightClimb(int maxHealth) : base(maxHealth){}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Zwischenberechnungen für IsPlatformBelow
		d10 = Vector3.down * Height/1.8f;
		d09 = d10*0.9f;
		d08 = d10*0.8f;
		d06 = d10*0.6f;
		d04 = d10*0.4f;
		d02 = d10*0.2f;
	}
	
	
	
	/// <summary>
	/// Ob der Gegner sich auf einer Leiter befindet. Wird von außen verändert
	/// </summary>
	public bool IsOnLadder = false;
	
	
	
	/// <summary>
	/// Der Faktor, mit dem sich auf Leitern langsamer Bewegt wird
	/// </summary>
	public static float f_ladderSlowDown = 0.25f;
		
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		//Wenn der Gegner sich nicht auf einer Leiter befindet
		if(!IsOnLadder){
			//benutze die einfachere Methode aus MLeftRight
			return base.FilterForce(vIn);
		}
		//Wenn der Gegner sich auf einer Leiter befindet genauere Betrachtung 
		//mit zusätzlich Oben und Unten.
		else{
			//Keine Bewegung
			if(vIn == Vector3.zero) 
				return vIn;
			
			//genauerer Winkel mit +/- um die ganzen 360° abzudecken
			float a = Mathf.Atan2 (vIn.x, vIn.y) * Mathf.Rad2Deg + 90.0f;
			// Normal: -90° links, 0° oben, 90° rechts, 180° unten
			// +90°  : 0° links, 90° oben, 180° rechts, 270° unten
			
			Vector3 heading;
			
			//Links:	315° ...  45°
			if( (a >= -45.0f && a < 45.0f) || a >= 315.0f || a < -315.0f)
				heading = Vector3.left;
			//Oben:		 45° ... 135°
			else if( (a >=45.0f && a < 135.0f) || (a >= -135.0f && a < -45.0f) )
				heading = Vector3.up;
			//Rechts:	135° ... 225°
			else if( (a >= 135.0f && a < 225.0f) || (a >= -225.0f && a < -135.0f) )
				heading = Vector3.right;
			//Unten:	225° ... 315°
			else if( (a >= 225.0f && a < 315.0f) || (a >= -315.0f && a < -225.0f) )
				heading = Vector3.down;
			//Fehlerfall der nie auftreten sollte
			else{
				Debug.LogError("Fehler: Winkelberechnung");
				return Vector3.zero;
			}
			
			//Geschwindigkeit auf Leitern verlangsamen
			return heading * vIn.magnitude * f_ladderSlowDown;
		}
	}
	
	
	
	/// <summary>
	/// Richtung zur Leitermitte, damit der Gegner weiß in welche Richtung
	/// er sich bewegen muss.
	/// </summary>
	/// <value>
	/// Vector3.zero: Ist bereits in Leitermitte.
	/// Vector3.left oder Vector3.right: In die Richtung gehen.
	/// Vector3.down: Unbekannte Richtung
	/// </value>
	public Vector3 DirectionToLadder{ get{
			Vector3 left = Vector3.left * Width/2.15f;
			Vector3 right = Vector3.right * Width/2.15f;
			Vector3 up = Pos + Vector3.up * Height/2.15f;
			Vector3 down = Pos + Vector3.down * Height/2.15f;
			
			Debug.DrawLine(Pos+left, Pos, Color.green);
			Debug.DrawLine(up+left, up, Color.green);
			Debug.DrawLine(down+left, down, Color.green);
			
			//nur so viele Linecasts wie nötig
			bool leftOk =
				   //Mitte
				   Linecast(Pos + left, Pos, Layer.Ladder)
				   //Oben
				|| Linecast(up + left, up, Layer.Ladder)
				   //Unten
				|| Linecast(down + left, down, Layer.Ladder)
			;
			
			Debug.DrawLine(Pos+right, Pos, Color.green);
			Debug.DrawLine(up+right, up, Color.green);
			Debug.DrawLine(down+right, down, Color.green);
			
			//nur so viele Linecasts wie nötig
			bool rightOk = 
				   //Mitte
				   Linecast(Pos + right, Pos, Layer.Ladder)
				   //Oben
				|| Linecast(up + right, up, Layer.Ladder)
				   //Unten
				|| Linecast(down + right, down, Layer.Ladder)
			;
			
			//Leiter ist in Mitte
			if(leftOk && rightOk)
				return Vector3.zero;
			
			//Leiter ist Rechts
			if(leftOk && !rightOk)
				return Vector3.right;
			
			//Leiter ist Links
			if(!leftOk && rightOk)
				return Vector3.left;
			
			//Leiter ist weiter weg, weitere Linecasts nötig:
			
			Debug.DrawLine(Pos, Pos + left * 2.0f, Color.blue);
			
			//Leiter ist Links
			if( Linecast(Pos, Pos + left * 2.0f, Layer.Ladder) )
				return Vector3.left;
			
			Debug.DrawLine(Pos, Pos + right * 2.0f, Color.blue);
			
			//Leiter ist Rechts
			if( Linecast(Pos, Pos + right * 2.0f, Layer.Ladder) )
				return Vector3.right;
			
			//Unbekannt in welche Richtung die Leiter ist (zu weit weg)
			return Vector3.down;
		}
	}
	
	
	
	/// <summary>
	/// Ob der Gegner in der horizontalen Leitermitte ist, und folglich nach 
	/// Oben und Unten klettern kann.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn der Gegner in der Mitte ist; sonst, <c>false</c>.
	/// </returns>
	private bool IsInLadderCenter{ get{
			return DirectionToLadder == Vector3.zero;
		}
	}
	
	
	
	/// <summary>
	/// Ob der Gegner in die Richtung (Oben/Unten) klettern kann.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn kein Hindernis vorhanden ist, der Gegner sich in der Leitermitte befindet, und es nicht das Ende der Leiter ist; ansonsten, <c>false</c>.
	/// </returns>
	/// <param name='heading'>
	/// Die Richtung Vector3.up oder Vector3.down
	/// </param>
	private bool CanClimbToHeading(Vector3 heading){
		
		//nicht in der Mitte der Leiter?
		if( ! IsInLadderCenter ){
			return false;
		}
		
		//Wand die den Weg nach oben/unten versperrt?
		if( ! NoObstacle(heading) ){
			return false;
		}
		
		//Position verschieben, damit weiter geklettert werden kann
		Vector3 pos = Pos - heading * Height/2.0f;
		Vector3 direction = pos + heading * Height/1.95f;
		
		//nicht das Ende der Leiter
		//kollidiert nicht wenn der Line-Ursprung innerhalb der Leiter ist, deshalb umgedrehte Richtung
		return ! Linecast(direction, pos, Layer.Ladder);
	}
	
	
	
	/// <summary>
	/// Ob der Gegner weiter nach oben klettern kann
	/// </summary>
	/// <value>
	/// <c>true</c> wenn kein Hindernis vorhanden ist, der Gegner sich in der Leitermitte befindet, und es nicht das Ende der Leiter ist; ansonsten, <c>false</c>.
	/// </value>
	public bool CanClimbUp{ get{
			return CanClimbToHeading(Vector3.up);
		}
	}
	
	
	
	/// <summary>
	/// Ob der Gegner weiter nach unten klettern kann
	/// </summary>
	/// <value>
	/// <c>true</c> wenn kein Hindernis vorhanden ist, der Gegner sich in der Leitermitte befindet, und es nicht das Ende der Leiter ist; ansonsten, <c>false</c>.
	/// </value>
	public bool CanClimbDown{ get{
			return CanClimbToHeading(Vector3.down);
		}
	}
	
	
	
	/// <summary>
	/// Ob in der Richtung (Links/Rechts) sich eine Platform befindet.
	/// </summary>
	/// <returns>
	/// <c>true</c> unter einem ein Level-Hindernis ist, aber nicht Neben einen; ansonsten, <c>false</c>.
	/// </returns>
	/// <param name='heading'>
	/// Richtung die geprüft wird. Vector3.left oder Vector3.right
	/// </param>
	private bool IsPlatformBelow(Vector3 heading){
		Vector3 direction = Pos + heading * Width;
		
		Debug.DrawLine(Pos, direction + d10, Color.yellow);
		Debug.DrawLine(Pos + d09, direction + d09, Color.red);
		Debug.DrawLine(Pos + d08, direction + d08, Color.red);
		Debug.DrawLine(Pos + d06, direction + d06, Color.red);
		Debug.DrawLine(Pos + d04, direction + d04, Color.red);
		Debug.DrawLine(Pos + d02, direction + d02, Color.red);
		Debug.DrawLine(Pos, direction, Color.red);
		
		return 
			//Platform unter einem
			Linecast(Pos, direction + d10, Layer.Level)
			//Platform nicht neben einem	
			&& ! Linecast(Pos + d09, direction + d09, Layer.Level)
			&& ! Linecast(Pos + d08, direction + d08, Layer.Level)
			&& ! Linecast(Pos + d06, direction + d06, Layer.Level)
			&& ! Linecast(Pos + d04, direction + d04, Layer.Level)
			&& ! Linecast(Pos + d02, direction + d02, Layer.Level)
			&& ! Linecast(Pos, direction, Layer.Level)
		;
			 
	}
	//Temporäre Zwischenergebnisse nur einmal beim Start berechnen
	private static Vector3 d10;
	private static Vector3 d09;
	private static Vector3 d08;
	private static Vector3 d06;
	private static Vector3 d04;
	private static Vector3 d02;
	
	
	
	/// <summary>
	/// Ob Rechts unter dem Gegner eine Platform ist die betreten werden kann
	/// </summary>
	/// <value>
	/// <c>true</c> wenn rechts eine Plattform ist; ansonsten, <c>false</c>.
	/// </value>
	public bool IsPlatformRight{ get{
			return IsPlatformBelow(Vector3.right);
		}
	}
	
	
	
	/// <summary>
	/// Ob Links unter dem Gegner eine Platform ist die betreten werden kann
	/// </summary>
	/// <value>
	/// <c>true</c> wenn links eine Plattform ist; ansonsten, <c>false</c>.
	/// </value>
	public bool IsPlatformLeft{ get{
			return IsPlatformBelow(Vector3.left);
		}
	}
	
	
	
	/// <summary>
	/// Ob in der Richtung (Links/Rechts) eine Leiter ist.
	/// </summary>
	/// <returns>
	/// <c>true</c> in der Richtung eine Leiter ist; ansonsten, <c>false</c>.
	/// </returns>
	/// <param name='heading'>
	/// Richtungsvektor Vector.right oder Vector.left
	/// </param>
	private bool CanClimbLeftRight(Vector3 heading){
		Vector3 vFrom = Pos - heading * Width / 4.0f;
		Vector3 vTo = Pos + heading * Width;
		
		//Leiter vorhanden?
		return Linecast(vFrom, vTo, Layer.Ladder) 
			|| Linecast(vTo, vFrom, Layer.Ladder);
	}
	
	
	
	/// <summary>
	/// Ob Links eine Leiter ist.
	/// </summary>
	public bool CanClimbLeft{ get{
			return CanMoveLeft || CanClimbLeftRight(Vector3.left);
		}
	}
	
	
	
	/// <summary>
	/// Ob Rechts eine Leiter ist.
	/// </summary>
	public bool CanClimbRight{ get{
			return CanMoveRight || CanClimbLeftRight(Vector3.right);
		}
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Richtung (Links/Rechts) der Position klettern kann.
	/// </summary>
	/// <param name='pos'>
	/// Die Position die überprüft werden soll
	/// </param>
	/// <param name='invertDirection'>
	/// Ob das Ergebnis invertiert werden soll (z.B. Flee statt Seek)
	/// </param>
	public bool CanClimbTo(Vector3 pos, bool invertDirection = false){
		if(IsRight(pos) ^ invertDirection) //ist das Ziel rechts vom Gegner?
			return CanClimbRight;
		else 
			return CanClimbLeft;
	}
	
	
	
}
