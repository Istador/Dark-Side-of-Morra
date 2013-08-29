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
public abstract class Enemy<T> : MonoBehaviour, MessageReceiver {
	
	
	/*
	 * Referenz auf das Spieler-Objekt
	*/
	private static GameObject _player;
	/// <summary>
	/// Referenz auf das Spieler-Objekt
	/// </summary>
	public GameObject player {get{
		if(_player == null) _player = GameObject.FindWithTag("Player");
		return _player;
	}}
	
	
	/*
	 * Textur / Sprite Controller
	*/
	public SpriteController spriteCntrl {get; private set;}
	/// <summary>Zeile der Animation</summary>
	private int txtState = 0;
	/// <summary>Anzahl Spalten (Frames)</summary>
	protected abstract int txtCols  { get; }
	/// <summary>Anzahl Zeilen (Zustände)</summary>
	protected abstract int txtRows { get; }
	/// <summary>Frames per Second</summary>
	protected abstract int txtFPS { get; }
	
	
	
	/*
	 * Health
	*/
	private int health; //aktuell
	private int maxHealth; //maximal
	private static AudioClip ac_healthdrop; //Health Globe Drop Sound
	
	
	
	/// <summary>Wahrscheinlichkeit das ein Health Globe droppt</summary>
	protected float f_HealthGlobeProbability = 0.3f; //30% drop, 70% kein drop
	
	/// <summary>Wahrscheinlichkeit das der gedroppte Health Globe groß ist</summary>
	protected float f_HealthGlobeBigProbability = 0.3f; //30% big, 70% small
	
	/// <summary>Lebenszeit von Health Globes. Wie lange Health Globes vorhanden bleiben.</summary>
	protected float f_HealthGlobeLifetime = 10.0f; //10 Sekunden
	
	// 0,3 * ( 0,3 * 50 + 0,7 * 10 ) = 6,6 HP on average
	
	
	
	/**
	 * Zustandsautomaten für Angriff und Bewegung
	*/
	/// <summary>
	/// Zustandsautomat für die Bewegung.
	/// </summary>
	public readonly StateMachine<Enemy<T>> MoveFSM;
	/// <summary>
	/// Zustandsautomat für den Angriff
	/// </summary>
	public readonly StateMachine<Enemy<T>> AttackFSM;
	
	
	
	public readonly static System.Random rnd = new System.Random();
	
	
	/*
	 * Konstruktor
	*/
	private Enemy(){}
	/// <summary>
	/// Initializes a new instance of the <see cref="Enemy`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public Enemy(int maxHealth){
		this.maxHealth = maxHealth;
		//Zustandsautomaten erstellen
		MoveFSM = new StateMachine<Enemy<T>>(this);
		AttackFSM = new StateMachine<Enemy<T>>(this);
	}
	
	
	
	protected virtual void Start () {
		health = maxHealth;
		
		//SpriteController hinzufügen
		spriteCntrl = gameObject.AddComponent<SpriteController>();
				
		if(ac_healthdrop == null) ac_healthdrop = (AudioClip) Resources.Load("Sounds/healthfall");
		
		//Zustandsautomaten starten (Enter)
		MoveFSM.Start();
		AttackFSM.Start();
	}
	
	
	/// <summary>
	/// Zustandsautomaten, Animation
	/// </summary>
	protected virtual void Update() {
		//Update der Zustandsautomaten
		MoveFSM.Update();
		AttackFSM.Update();
		//Animation des Sprite-Controllers
		spriteCntrl.animate(txtCols, txtRows, 0, txtState, txtCols, txtFPS);
	}
	
	
	
	/// <summary>
	/// Nachricht empfangen, deligieren an BEIDE Zustandsautomaten
	/// </summary>
	/// <returns>
	/// ob die Nachricht von einem der Automaten angenommen wurde
	/// </returns>
	/// <param name='msg'>
	/// Die Nachricht
	/// </param>
	public bool HandleMessage(Telegram msg){
		bool tmp = MoveFSM.HandleMessage(msg);
		return AttackFSM.HandleMessage(msg) || tmp;
	}
	
	
	
	/// <summary>
	/// Schaden erhalten, der die HP verringert, und zum Tode führen kann
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Gegner zugefügt wird
	/// </param>
	public virtual void ApplyDamage(Vector3 damage){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+damage.magnitude+" dmg received");
		health -= (int)damage.magnitude;
		if(health <= 0) Death();
	}
	
	
	
	/// <summary>
	/// Heilung erhalten, überschreitet nicht das maximale Leben
	/// </summary>
	/// <param name='hp'>
	/// Trefferpunkte die geheilt werden
	/// </param>
	public virtual void ApplyHeal(int hp){
		health += hp;
		if(health > maxHealth) health = maxHealth;
	}
	
	
	
	/// <summary>
	/// Lässt den Gegner sterben, z.B. weil die HP auf 0 gefallen sind
	/// </summary>
	public virtual void Death(){
		//Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): death");
		
		//soll ein Health Globe droppen?
		if(rnd.NextDouble() <= f_HealthGlobeProbability ){
			UnityEngine.Object res;
			
			//soll es ein großer oder kleiner Health Globe sein?
			if(rnd.NextDouble() <= f_HealthGlobeBigProbability){
				//groß
				res = Resources.Load("bigHP");
			} else { 
				//klein
				res = Resources.Load("smallHP");
			}
			
			//Health Globe erstellen
			GameObject obj = (GameObject)UnityEngine.Object.Instantiate(res, transform.position, new Quaternion(0.0f, 0.7f, -0.7f, 0.0f));
			
			//Geräusch machen
			AudioSource.PlayClipAtPoint(ac_healthdrop, obj.collider.bounds.center);
			
			//Health Globe kurz nach oben bewegen lassen
			obj.rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
			
			Destroy(obj, f_HealthGlobeLifetime);
		}
		
		Destroy(gameObject);
	}
	
	
	
	/// <summary>
	/// Ändert bei Gegnern mit kontextabhängigen Sprite welche 
	/// Zeile angezeigt werden soll
	/// </summary>
	/// <param name='row'>
	/// Die Zeile in der Textur die für die Animation verwendet werden soll
	/// </param>
	public void SetSprite(int row){txtState = row;}
	
	
	
	/// <summary>
	/// Macht den Gegner Sichtbar
	/// </summary>
	public void SetVisible(){
		renderer.enabled = true;
	}
	
	
	
	/// <summary>
	/// Macht den Gegner Unsichtbar
	/// </summary>
	public void SetInvisible(){
		renderer.enabled = false;
	}
	
	
	
	/// <summary>
	/// Entfernung des Gegners zu einem Spielobjekt bestimmen
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Objekt
	/// </returns>
	/// <param name='obj'>
	/// Das Objekt zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(GameObject obj){
		return Mathf.Abs(Vector3.Distance(collider.bounds.center, obj.collider.bounds.center));
	}
	
	
	
	/// <summary>
	/// Entfernung des Gegners zu einer Position bestimmen
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zur Position
	/// </returns>
	/// <param name='obj'>
	/// Die Position zu dem die Distanz ermittelt werden soll
	/// </param>
	public float DistanceTo(Vector3 pos){
		return Mathf.Abs(Vector3.Distance(collider.bounds.center, pos));
	}
	
	
	
	/// <summary>
	/// Entfernung des Gegners zum Spieler
	/// </summary>
	/// <returns>
	/// Die absolute Distanz zum Spieler
	/// </returns>
	public float DistanceToPlayer(){
		return Mathf.Abs(Vector3.Distance(collider.bounds.center, player.collider.bounds.center));
	}
	
	
	
	/// <summary>
	/// ist das Objekt Sichtbar für den Gegner
	/// dies prüft nicht ob sich das Objekt in einem bestimmten Winkel
	/// zum Gegner befindet (z.B. ob es vor ihm ist).
	/// </summary>
	/// <returns>
	/// false: wenn zw. Gegner und Ziel eine Wand oder Platform ist
	/// </returns>
	/// <param name='target'>
	/// Das Objekt zu dem LOS geprüft werden soll
	/// </param>
	public bool LineOfSight(GameObject target){
		RaycastHit hit; //wenn kollision, dann steht hier womit
		int layer = 0; //kollidiert mit nichts
		layer += 1<<8; //Layer 8: Level (also  Kollision mit Level-Geometrie)
		if(Physics.Linecast(collider.bounds.center, target.collider.bounds.center, out hit, layer))
			return hit.collider.gameObject == target;
		return true;
	}
	
	
	public void DoDamageTo(GameObject other, int damage){
		Vector3 dmg = (other.collider.bounds.center - collider.bounds.center).normalized * damage;
		other.SendMessage("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
	}
	
	
}
