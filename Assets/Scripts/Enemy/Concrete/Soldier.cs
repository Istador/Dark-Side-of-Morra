using UnityEngine;
using System.Collections;

public class Soldier : MLeftRightClimb<Soldier> {
	
	
	
	public static AudioClip ac_shoot;
	
	
	
	public override float maxSpeed { get{return 8.0f;} }
	public override float maxForce { get{return 8.0f;} }
	
	
	
	protected override int txtCols { get{return 10;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 10;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 12;} }  //Frames per Second
	
	
		
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Untere Grenze
	/// </summary>
	public static readonly float f_optimum_min = 2.5f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Obere Grenze
	/// </summary>
	public static readonly float f_optimum_max = 6.5f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 8.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly double d_reloadTime = 0.8; // 0,5 sekunden nachladen
	
	/// <summary>
	/// Gedächtnis:
	/// Die Zeit die der Gegner sich noch an den Spieler erinnert.
	/// </summary>
	public static readonly double d_memoryTime = 10.0; // 5 sekunden
	
	/// <summary>
	/// Sichtweite des Gegners. Bis zu dieser Distanz kann der Gegner sehen.
	/// </summary>
	public static readonly float f_visibleRange = 10.0f;
	
	
	public Soldier() : base(150) {
		MoveFSM.SetGlobalState(SSoldierPatrol.Instance);
		AttackFSM.SetCurrentState(SSoldierHoldFire.Instance);
	}
	
	
	
	protected override void Start(){
		base.Start();
		_lastKnownPosition = player.collider.bounds.center;
		_lastTimeVisited = Time.time;
		if(ac_shoot == null) ac_shoot = (AudioClip) Resources.Load("Sounds/shoot2");
	}
	
	
	//bool once = true;
	protected override void Update(){
		SetSprite(DetermineSprite());
		
		base.Update();
		
		//Debug.DrawLine(collider.bounds.center, LastKnownPosition(), Color.yellow);
		//Debug.DrawLine(collider.bounds.center, collider.bounds.center + rigidbody.velocity, Color.green);
		
		//Debug.Log(MoveFSM.GetCurrentState());
	}
	
	
	
	/// <summary>
	/// Ob der Spieler vor dem Gegner ist (in seiner Blickrichtung).
	/// </summary>
	public bool IsPlayerInfront(){
		//Debug.DrawLine(Vector3.zero, player.collider.bounds.center - collider.bounds.center, Color.yellow);
		//Debug.Log(Vector3.Dot((player.collider.bounds.center - collider.bounds.center), Heading()) );
		return Vector3.Dot((player.collider.bounds.center - collider.bounds.center), Heading()) > 0.0f;
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Sichtweite und in Blickrichtung ist, sowie keine geometrie
	/// zwischen Spieler und Gegner liegen.
	/// </summary>
	public bool IsPlayerVisible(){
		float distance = DistanceToPlayer();
		return ( 
			   distance < f_visibleRange 
			&& LineOfSight(player)
			&& (IsOnLadder || IsPlayerInfront() )
		);
			
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Schussreichweite ist.
	/// </summary>
	public bool IsPlayerInFireRange(){
		float distance = DistanceToPlayer();
		return ( 
			   distance <= f_outOfRange		//in Reichweite
			&& LineOfSight(player)			//Sicht frei
			&& IsPlayerInfront()			//vor dem Gegner
			&& IsHeightOk(player.collider.bounds.center) //Höhenunterschied nicht zu groß
		);
	}
	
	
	/// <summary>
	/// Ob die Höhe der Position innerhalb der des Gegners ist (ob grob auf selber Ebene)
	/// </summary>
	public bool IsHeightOk(Vector3 pos){
		return Mathf.Abs(collider.bounds.center.y - pos.y) < (collider.bounds.size.y / 2.0f);
	}
	
	
	/// <summary>
	/// Die Position ist direkt über oder unter dem Gegner
	/// </summary>
	public bool DirectlyAboveOrUnder(Vector3 pos){
		return Mathf.Abs(collider.bounds.center.x - pos.x) < (collider.bounds.size.x / 2.0f);
	}
	
	
	private Vector3 _lastKnownPosition;
	private double _lastTimeVisited;
	private Vector3 _lastHeading;
	
	public void RememberNow(){
		_lastKnownPosition = player.collider.bounds.center;
		_lastTimeVisited = Time.time;
		if( ! DirectlyAboveOrUnder(_lastKnownPosition))
			_lastHeading = Heading();
	}
	
	public void DeterminePlayerPosition(){
		if(IsPlayerVisible()){
			RememberNow();
			//Debug.DrawLine(collider.bounds.center, _lastKnownPosition, Color.green);
		} else {
			//Debug.DrawLine(collider.bounds.center, _lastKnownPosition, Color.red);
		}
		
	}
	
	public Vector3 LastKnownPosition(){
		return _lastKnownPosition;
	}
	
	public bool IsRememberingPlayer(){
		return _lastTimeVisited + d_memoryTime >= Time.time;
	}
	
	
	
	
	/// <summary>
	/// Schaden erhalten, der die HP verringert, und zum Tode führen kann.
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Gegner zugefügt wird
	/// </param>
	public override void ApplyDamage(Vector3 damage){
		//Zustandsautomaten informieren
		MessageDispatcher.Instance.Dispatch(new Telegram(this, "damage"));
		
		//Position merken
		RememberNow();
		
		//Schaden anwenden
		base.ApplyDamage(damage);
	}
	
	
	public Vector3 LastHeading(){
		return _lastHeading;
	}
	
	public Vector3 Heading(){
		if(MoveFSM.GetCurrentState() == SPatrolLeft<Soldier>.Instance)
			return Vector3.left;
		else if(MoveFSM.GetCurrentState() == SPatrolRight<Soldier>.Instance)
			return Vector3.right;
		else if(IsRight(_lastKnownPosition))
			return Vector3.right;
		else
			return Vector3.left;
	}
	
	
	
	public Vector3 Moving(){
		Vector3 f = steering.Calculate();
		if(f == Vector3.zero)
			return Vector3.zero;
		
		if(IsOnLadder){
			float a = Mathf.Atan2 (f.x, f.y) * Mathf.Rad2Deg + 90.0f;
			
			if( (a >= -45.0f && a < 45.0f) || a >= 315.0f || a < -315.0f)
				return Vector3.left;
			if( (a >=45.0f && a < 135.0f) || (a >= -135.0f && a < -45.0f) )
				return Vector3.up;
			if( (a >= 135.0f && a < 225.0f) || (a >= -225.0f && a < -135.0f) )
				return Vector3.right;
			if( (a >= 225.0f && a < 315.0f) || (a >= -315.0f && a < -225.0f) )
				return Vector3.down;
			return Vector3.zero;
		}
		
		if(IsRight(collider.bounds.center + rigidbody.velocity))
			return Vector3.right;
		else
			return Vector3.left;
	}
	
	
	
	public int DetermineSprite(){
		Vector3 h = Heading();
		Vector3 m = Moving();
		
		//auf Leiter
		if(IsOnLadder){
			if(m == Vector3.up) return 7;	//nach oben
			if(m == Vector3.down) return 8; //nach unten
			return 6;						//stehen/links/rechts
		}
		
		//keine Bewegung
		if(m == Vector3.zero){
			if(h == Vector3.left) return 2;	//nach links gucken
			else return 3;					//nach rechts gucken
		}
		
		//Bewegung == Blickrichtung
		if(m == h){ 
			if(h == Vector3.left) return 0;	//nach links
			else return 1;					//nach rechts
		}
		//Bewegung != Blickrichtung
		else {
			if(h == Vector3.left) return 4;	//nach rechts gehen, links gucken
			else return 5;					//nach links gehen, rechts gucken
		}
	}
	
	
	
	public bool CanMoveTo(Vector3 pos, bool invertDirection = false){
		if(IsRight(pos) ^ invertDirection) //ist das Ziel rechts vom Gegner?
			return CanMoveRight();
		else 
			return CanMoveLeft();
	}
	
	public bool CanClimbTo(Vector3 pos, bool invertDirection = false){
		if(IsRight(pos) ^ invertDirection) //ist das Ziel rechts vom Gegner?
			return CanClimbRight();
		else 
			return CanClimbLeft();
	}
}
