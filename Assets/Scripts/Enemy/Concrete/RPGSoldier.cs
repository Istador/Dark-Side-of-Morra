using UnityEngine;
using System.Collections;

public class RPGSoldier : MLeftRight<RPGSoldier> {
	
	
	
	public Vector3 bulletSpawn { get{
			return collider.bounds.center 
				+ Heading * collider.bounds.size.x/4.0f
				+ Vector3.up * collider.bounds.size.y/8.0f
			;
		}
	}
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 2.0f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Untere Grenze
	/// </summary>
	public static readonly float f_optimum_min = 3.5f;
	
	/// <summary>
	/// Optimale Entfernung in der stehen geblieben wird. Obere Grenze
	/// </summary>
	public static readonly float f_optimum_max = 4.5f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 6.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly double d_reloadTime = 3.0; // 3 sekunden nachladen
	
	/// <summary>
	/// Gedächtnis:
	/// Die Zeit die der Gegner sich noch an den Spieler erinnert.
	/// </summary>
	public static readonly double d_memoryTime = 5.0; // 5 sekunden
	
	/// <summary>
	/// Sichtweite des Gegners. Bis zu dieser Distanz kann der Gegner sehen.
	/// </summary>
	public static readonly float f_visibleRange = 8.0f;
	
	
	public RPGSoldier() : base(150) {
		MoveFSM.GlobalState = SRPGSPatrol.Instance;
		AttackFSM.CurrentState = SRPGSHoldFire.Instance;
		
		f_HealthGlobeProbability = 0.6f; //60% drop, 40% kein drop
		f_HealthGlobeBigProbability = 0.6f; //60% big, 40% small
		// 0,6 * ( 0,6 * 50 + 0,4 * 10 ) = 20,4 HP on average
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 6;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 5.0f;
		MaxForce = 5.0f;
		
		_lastKnownPosition = PlayerPos;
		_lastTimeVisited = Time.time;
	}
	
	
	
	protected override void Update(){
		Sprite = DetermineSprite();
		
		base.Update();
		//Debug.Log(MoveFSM.GetCurrentState());
	}
	
	
	
	/// <summary>
	/// Ob der Spieler vor dem Gegner ist (in seiner Blickrichtung).
	/// </summary>
	public bool IsPlayerInfront(){
		//Debug.DrawLine(Vector3.zero, player.collider.bounds.center - collider.bounds.center, Color.yellow);
		//Debug.Log(Vector3.Dot((player.collider.bounds.center - collider.bounds.center), Heading()) );
		return Vector3.Dot((PlayerPos - Pos), Heading) > 0.0f;
	}
	
	
	
	/// <summary>
	/// Ob der Spieler in Sichtweite und in Blickrichtung ist, sowie keine geometrie
	/// zwischen Spieler und Gegner liegen.
	/// </summary>
	public bool IsPlayerVisible(){
		float distance = DistanceToPlayer();
		return ( 
			   distance < f_visibleRange 
			&& LineOfSight(Player)
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
			&& distance >= f_closeRange
			&& LineOfSight(Player)
			&& IsPlayerInfront()
		);
	}
	
	
	private Vector3 _lastKnownPosition;
	private double _lastTimeVisited;
	
	public void RememberNow(){
		_lastKnownPosition = PlayerPos;
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
		MessageDispatcher.I.Dispatch(this, "damage");
		
		//Position merken
		RememberNow();
		
		//Schaden anwenden
		base.ApplyDamage(damage);
	}
	
	
	
	public override Vector3 Heading { get{
		if(MoveFSM.IsInState(SPatrolLeft<RPGSoldier>.Instance))
			return Vector3.left;
		else if(MoveFSM.IsInState(SPatrolRight<RPGSoldier>.Instance))
			return Vector3.right;
		else if(IsRight(_lastKnownPosition))
			return Vector3.right;
		else
			return Vector3.left;
	}}
	
	
	
	public Vector3 Moving(){
		Vector3 f = Steering.Calculate();
		if(f == Vector3.zero)
			return Vector3.zero;
		else if(IsRight(collider.bounds.center + rigidbody.velocity))
			return Vector3.right;
		else
			return Vector3.left;
	}
	
	
	
	public int DetermineSprite(){
		Vector3 h = Heading;
		Vector3 m = Moving();
		//keine Bewegung
		if(m == Vector3.zero){
			if(h == Vector3.left) return 2;
			else return 3;
		} else if(m == h){
			if(h == Vector3.left) return 0;
			else return 1;
		} else {
			if(h == Vector3.left) return 5;
			else return 4;
		}
	}
	
	
	
	
}
