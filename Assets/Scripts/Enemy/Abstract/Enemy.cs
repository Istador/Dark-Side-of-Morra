using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für alle Gegner und Projektile von Gegnern
/// 
/// Abstrahieren von Verhalten das für die meisten Unterklassen gleich ist:
/// - HP, Schaden, Sterben
/// - SpriteController ansteuern
/// - Bewegungs- und Angriffs-Zustandsautomat erstellen
/// - Nachrichtensystem: einghende Nachrichten an Zustände weitergeben
/// 
/// Generisches T, um die konkrete Klasse des Gegners referenzieren zu können
/// (für die Zustandsautomaten)
/// 
public abstract class Enemy<T> : Entity {
	
	
	
	// Spieler
	
	/// <summary>
	/// Referenz auf das Spieler-Objekt
	/// </summary>
	public GameObject Player {get{
		//wenn die Referenz noch nicht besteht
		if(_player == null)
			//Lade sie
			_player = GameObject.FindWithTag("Player");
		return _player;
		}
	}
	/// <summary>
	/// Spielerposition
	/// </summary>
	public Vector3 PlayerPos {get{
			return Player.collider.bounds.center;
		}
	}
	
	private static GameObject _player; //Instanzvariable die von der Property verwendet wird
	/// <summary>
	/// Entfernung des Gegners zum Spieler
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Spieler
	/// </returns>
	public float DistanceToPlayer{ get{
			return DistanceTo(Player);
		}
	}
	
	
	
	// Health Globes
	// 0,3 * ( 0,3 * 50 + 0,7 * 10 ) = 6,6 HP on average
	
	/// <summary>
	/// Wahrscheinlichkeit, dass ein Health-Globe beim Tode des Gegners 
	/// fallengelassen wird.
	/// 
	/// Wertebereich: 0.0 bis 1.0
	/// Default: 0.3 (=> 30 % drop, 70 % kein drop)
	/// </summary>
	protected float f_HealthGlobeProbability = 0.3f;
	
	/// <summary>
	/// Wahrscheinlichkeit, dass der fallengelassene Health-Globe ein großer ist.
	/// 
	/// Wertebereich: 0.0 bis 1.0
	/// Default: 0.3 (=> 30 % groß, 70 % klein)
	/// </summary>
	protected float f_HealthGlobeBigProbability = 0.3f;
	
	/// <summary>
	/// Lebenszeit von Health-Globes die von Gegnern fallengelassen werden.
	/// Wie lange Health Globes angezeigt werden bevor sie verschwinden.
	/// 
	/// Default: 10.0 (10 Sekunden)
	/// </summary>
	protected float f_HealthGlobeLifetime = 10.0f;
	
	
	
	// Zustandsautomaten
	
	/// <summary>
	/// Zustandsautomat für die Bewegung.
	/// </summary>
	public readonly StateMachine<Enemy<T>> MoveFSM;
	/// <summary>
	/// Zustandsautomat für den Angriff
	/// </summary>
	public readonly StateMachine<Enemy<T>> AttackFSM;
	
	
	
	// Konstruktor
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Enemy`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public Enemy(int maxHealth) : base(maxHealth) {
		//Zustandsautomaten erstellen
		MoveFSM = new StateMachine<Enemy<T>>(this);
		AttackFSM = new StateMachine<Enemy<T>>(this);
	}
	
	
	
	// Start
	
	protected override void Start() {
		base.Start();
		
		//Zustandsautomaten starten (Enter)
		MoveFSM.Start();
		AttackFSM.Start();
	}
	
	
	
	// Update
	
	/// <summary>
	/// Zustandsautomaten, Animation
	/// </summary>
	protected override void Update() {
		//Update der Zustandsautomaten
		MoveFSM.Update();
		AttackFSM.Update();
		
		//Animation
		base.Update();
	}
	
	
	
	// HandleMessage
	
	/// <summary>
	/// Nachricht empfangen, deligieren an BEIDE Zustandsautomaten
	/// </summary>
	/// <returns>
	/// ob die Nachricht von einem der Automaten angenommen wurde
	/// </returns>
	/// <param name='msg'>
	/// Die Nachricht
	/// </param>
	public override bool HandleMessage(Telegram msg){
		//Move-Zustandsautomat
		bool tmp = MoveFSM.HandleMessage(msg);
		//Angriffs-Zustandsautomat
		return AttackFSM.HandleMessage(msg) || tmp;
	}
	
	
	
	/// <summary>
	/// Death-Methode überschrieben, um beim sterben Health-Globes fallen 
	/// zu lassen
	/// </summary>
	public override void Death(){
		
		//soll ein Health Globe droppen?
		if(rnd.NextDouble() <= f_HealthGlobeProbability ){
			string res;
			
			//soll es ein großer oder kleiner Health Globe sein?
			if(rnd.NextDouble() <= f_HealthGlobeBigProbability)
				//groß
				res = "bigHP";
			else 
				//klein
				res = "smallHP";
			
			//Health Globe erstellen
			GameObject obj = Instantiate(res);
			HealthGlobe hg = obj.GetComponent<HealthGlobe>();
			
			//Geräusch machen
			hg.PlaySound("healthfall");
			
			//Health Globe kurz nach oben bewegen lassen
			hg.rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
			
			//Health Globe nach einer bestimmten Zeit sterben lassen
			Destroy(obj, f_HealthGlobeLifetime);
		}
		
		base.Death();
	}
	
	
	
}
