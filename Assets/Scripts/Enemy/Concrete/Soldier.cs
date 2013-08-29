using UnityEngine;
using System.Collections;

public class Soldier : MLeftRight<Soldier> {
	
	
	
	public static AudioClip ac_shoot;
	
	
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 3.0f;} }
	
	
	
	protected override int txtCols { get{return 8;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 10;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 4;} }  //Frames per Second
	
	
		
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
	
	
	
	protected override void Update(){
		SetSprite(DetermineSprite());
		base.Update();
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
	/// Ob die Position rechts vom Gegner ist.
	/// </summary>
	public bool IsRight(Vector3 pos){
		return Vector3.Dot((pos - collider.bounds.center), Vector3.right) > 0.0f;
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
			&& IsPlayerInfront()
		);
			
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Schussreichweite ist.
	/// </summary>
	public bool IsPlayerInFireRange(){
		float distance = DistanceToPlayer();
		return ( 
			   distance <= f_outOfRange
			&& LineOfSight(player)
			&& IsPlayerInfront()
		);
	}
	
	
	private Vector3 _lastKnownPosition;
	private double _lastTimeVisited;
	
	public void RememberNow(){
		_lastKnownPosition = player.collider.bounds.center;
		_lastTimeVisited = Time.time;
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
		else if(IsRight(collider.bounds.center + rigidbody.velocity))
			return Vector3.right;
		else
			return Vector3.left;
	}
	
	
	
	public int DetermineSprite(){
		Vector3 h = Heading();
		Vector3 m = Moving();
		//keine Bewegung
		if(m == Vector3.zero){
			if(h == Vector3.left) return 2;
			else return 3;
		} else if(m == h){
			if(h == Vector3.left) return 0;
			else return 1;
		} else {
			if(h == Vector3.left) return 4;
			else return 5;
		}
	}
	
	
	
	public bool CanMoveTo(Vector3 pos, bool invertDirection = false){
		if(IsRight(pos) ^ invertDirection) //ist das Ziel rechts vom Gegner?
			return CanMoveRight();
		else 
			return CanMoveLeft();
	}
}
