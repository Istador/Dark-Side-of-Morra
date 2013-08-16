using UnityEngine;
using System.Collections;

public class RPGSoldier : MLeftRight<RPGSoldier> {
	
	
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 3.0f;} }
	
	
	
	protected override int txtCols { get{return 8;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 6;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 4;} }  //Frames per Second
	
	
	
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
	
	
	public RPGSoldier() : base(100) {
		MoveFSM.SetGlobalState(SRPGSPatrol.Instance);
		AttackFSM.SetCurrentState(SRPGSHoldFire.Instance);
		
	}
	
	
	
	protected override void Start(){
		base.Start();
		_lastKnownPosition = player.collider.bounds.center;
		_lastTimeVisited = Time.time;
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
			&& distance >= f_closeRange
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
		}	
		//Debug.DrawLine(collider.bounds.center, _lastKnownPosition, Color.red);
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
	public override void ApplyDamage(int damage){
		//Zustandsautomaten informieren
		MessageDispatcher.Instance.Dispatch(new Telegram(this, "damage"));
		
		//Position merken
		_lastKnownPosition = player.collider.bounds.center;
		_lastTimeVisited = Time.time;
		
		//Schaden anwenden
		base.ApplyDamage(damage);
	}
	
	
	
	public Vector3 Heading(){
		if(MoveFSM.GetCurrentState() == SPatrolLeft<RPGSoldier>.Instance)
			return Vector3.left;
		else if(MoveFSM.GetCurrentState() == SPatrolRight<RPGSoldier>.Instance)
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
	
}