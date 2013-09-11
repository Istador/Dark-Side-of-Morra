using UnityEngine;
using System.Collections;

/// 
/// Diese Klasse Beschränkt die Bewegung auf die Horizontale,
/// also Bewegung ist nur nach Links oder Rechts möglich
/// 
public abstract class MLeftRight<T> : MovableEnemy<T> {
	
	
	
	// Konstruktor
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MLeftRight`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MLeftRight(int maxHealth) : base(maxHealth){
		//zufällig nach links oder rechts patroullieren
		if(GeneralObject.rnd.Next(0,2) == 0)
			//nach Links
			MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		else
			//nach Rechts
			MoveFSM.ChangeState(SPatrolRight<T>.Instance);
	}
	
	protected override void Start(){
		base.Start();
		
		//Letzt bekannte Spielerposition und Zeit merken
		LastPos = PlayerPos;
		IsRememberingPlayer = false;
	}
	
	
	// FilterForce
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung des Vectors
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		//Keine Bewegung
		if(vIn == Vector3.zero) 
			return vIn;
		
		//Winkel zwischen Vector nach Links und der Kraft
		float a = Vector3.Angle(Vector3.left, vIn);
		
		//Winkel kleiner 90° bedeutet die Kraft geht eher nach Links
		if(a < 90.000f)
			//also die Bewegung nach Links drehen
			return Vector3.left * vIn.magnitude;
		//Winkel größer 90° bedeutet die Kraft geht eher nach Rechts
		else if(a > 90.000f)
			//also die Bewegung nach Rechts drehen
			return Vector3.right * vIn.magnitude;
		//Weder Links noch Rechts
		return Vector3.zero;
	}
	
	
	
	/// <summary>
	/// Ob der Spieler vor dem Gegner ist (in seiner Blickrichtung).
	/// </summary>
	public bool IsPlayerInfront{ get{
			return Vector3.Dot((PlayerPos - Pos), Heading) > 0.0f;
		}
	}
	
	
	
	/// <summary>
	/// Sichtweite des Gegners. Bis zu dieser Distanz kann der Gegner 
	/// den Spieler erkennen.
	/// </summary>
	public static readonly float f_visibleRange = 10.0f;
	
	/// <summary>
	/// Ob der Spieler in Sichtweite und in Blickrichtung ist, sowie keine 
	/// geometrie zwischen Spieler und Gegner liegen.
	/// </summary>
	public virtual bool IsPlayerVisible{ get{
			return ( 
				   //nicht zu weit weg
				   DistanceToPlayer < f_visibleRange 
				   //LoS besteht
				&& LineOfSight(Player)
				   //in Blickrichtung oder gerade noch gesehen
				&& (IsPlayerInfront || IsRememberingPlayer)
			);
		}
	}
	
	
	
	/// <summary>
	/// Gedächtnis:
	/// Die Zeit die der Gegner sich noch an den Spieler erinnert, wenn er ihn nicht mehr sieht.
	/// </summary>
	public static readonly double d_memoryTime = 7.0; // 7 sekunden
	
	/// <summary>
	/// Der Zeitpunkt an dem der Gegner den Spieler zu letzt sah
	/// </summary>
	private double d_lastTimeVisited;
	
	/// <summary>
	/// Die letzte Position an welcher der Gegner den Spieler gesehen hat
	/// </summary>
	public Vector3 LastPos {
		get{return _LastPos;}
		private set{
			//setze  instanzvariable
			_LastPos = value;
			//merke zeit
			IsRememberingPlayer = true;
			//merke heading nur wenn nicht über/unter dem Spieler
			if( ! DirectlyAboveOrUnder(_LastPos) )
				LastHeading = Heading;
		}
	}
	private Vector3 _LastPos; //verwendete Instanzvariable
	public Vector3 LastHeading { get; private set;}
	
	/// <summary>
	/// Ob der Gegner sich an den Spieler erinnert
	/// </summary>
	/// <value>
	/// <c>true</c> wenn der Zeitpunkt der letzten Sichtung noch nicht zu weit entfernt ist; ansonsten, <c>false</c>.
	/// </value>
	public bool IsRememberingPlayer{ get{
			return d_lastTimeVisited + d_memoryTime >= Time.time;
		}
		private set{
			if(value) d_lastTimeVisited = Time.time;
			else d_lastTimeVisited = -1.0;
		}
	}
	
	/// <summary>
	/// Merke sich jetzt aktuelle Position des Spielers, sowie die aktuelle Zeit
	/// </summary>
	public void RememberNow(){
		LastPos = PlayerPos;
	}
	
	
	
	/// <summary>
	/// Merkt sich die aktuelle Position des Spielers und die aktuelle Zeit,
	/// aber nur wenn der Spieler sichtbar ist.
	/// 
	/// Diese Methode wird pro Update nur einmal von dem globalem Zustand 
	/// aufgerufen.
	/// </summary>
	public void DeterminePlayerPosition(){
		//wenn sichtbar
		if(IsPlayerVisible)
			RememberNow(); //merke Position und Zeit
	}
	
	
	
	/// <summary>
	/// Richtung in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public override Vector3 Heading { get{
			//Nach Links Patrolierend
			if( MoveFSM.IsInState(SPatrolLeft<T>.Instance) )
				return Vector3.left;
			//nach Rechts Patrolierend
			else if( MoveFSM.IsInState(SPatrolRight<T>.Instance) )
				return Vector3.right;
			//Ansonsten die gemerkte Position verwenden
			else if( IsRight(LastPos) )
				return Vector3.right;
			else
				return Vector3.left;
		}
	}
	
	
	
	
	
	

	
	
	/// <summary>
	/// Methode ob in der Bewegungsrichtung ein Hindernis ist.
	/// Funktioniert sowohl mit Links, Rechts, Oben sowie Unten.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn kein Hindernis vor einem ist; ansonsten, <c>false</c>.
	/// </returns>
	/// <param name='heading'>
	/// Die Richtung Vector3.left, Vector3.right, Vector3.up oder Vector3.down
	/// </param>
	protected bool NoObstacle(Vector3 heading){
		Vector3 direction;
		Vector3 side;
		
		//Links/Rechts
		if(heading == Vector3.left || heading == Vector3.right){
			direction = Pos + heading * Width / 1.95f;
			side = Vector3.up * Height / 2.15f;
		}
		//Oben/Unten
		else if(heading == Vector3.up || heading == Vector3.down){
			direction = Pos + heading * Height / 1.95f;
			side = Vector3.left * Width / 2.15f;
		}
		//Unbekannte Richtung
		else {
			return false;
		}
		
		Debug.DrawLine(Pos, direction, Color.red);
		Debug.DrawLine(Pos + side, direction + side, Color.red);
		Debug.DrawLine(Pos - side, direction - side, Color.red);
		
		//alle drei Kollisionstests schlagen fehl
		return  ! Linecast(Pos, direction, Layer.Level) 
			&&  ! Linecast(Pos + side, direction + side, Layer.Level) 
			&&  ! Linecast(Pos - side, direction - side, Layer.Level)
		;
	}
	
	
	
	/// <summary>
	/// Ob der Gegner sich in die Richtung (Links/Rechts) bewegen kann.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn kein Hindernis und unter einem Boden; ansonsten, <c>false</c>.
	/// </returns>
	/// <param name='heading'>
	/// Die Richtung Vector3.left oder Vector3.right
	/// </param>
	private bool CanMoveToHeading(Vector3 heading){
		
		Vector3 direction = Pos + heading * Width / 1.95f; // aus dem Collider heraus
		Vector3 direction2 = Pos + heading * Width / 4.0f; //nicht aus dem Collider heraus
		Vector3 down = Vector3.down * Height / 1.8f; // aus dem Collider heraus
	
		Debug.DrawLine(direction, direction + down, Color.blue);
		Debug.DrawLine(direction2, direction2 + down, Color.blue);
		
		return 
			//vor einem ist kein Hindernis
			NoObstacle(heading)
			//und vor einem ist Boden auf dem gestanden werden kann
			&& Linecast(direction, direction + down, Layer.Level)
			&& Linecast(direction2, direction2 + down, Layer.Level)
		;
	}
	
	
	
	/// <summary>
	/// Ob der Spieler nach Links gehen kann.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn kein Hindernis den Weg blockiert und Boden vorhanden ist; ansonsten, <c>false</c>.
	/// </returns>
	public bool CanMoveLeft{ get{
			return CanMoveToHeading(Vector3.left);
		}
	}
	
	
	
	/// <summary>
	/// Ob der Spieler nach Rechts gehen kann.
	/// </summary>
	/// <returns>
	/// <c>true</c> wenn kein Hindernis den Weg blockiert und Boden vorhanden ist; ansonsten, <c>false</c>.
	/// </returns>
	public bool CanMoveRight{ get{
			return CanMoveToHeading(Vector3.right);
		}
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Richtung (Links/Rechts) der Position gehen kann.
	/// </summary>
	/// <param name='pos'>
	/// Die Position die überprüft werden soll
	/// </param>
	/// <param name='invertDirection'>
	/// Ob das Ergebnis invertiert werden soll (z.B. Flee statt Seek)
	/// </param>
	public bool CanMoveTo(Vector3 pos, bool invertDirection = false){
		if(IsRight(pos) ^ invertDirection) //ist das Ziel rechts vom Gegner?
			return CanMoveRight;
		else 
			return CanMoveLeft;
	}
	
	
	
}
