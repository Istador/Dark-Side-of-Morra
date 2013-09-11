using UnityEngine;
using System.Collections;

/// 
/// Eine Mine ist ein unbeweglicher Gegner, der Explodiert wenn er ausgelöst 
/// wurde. Ausgelöst wird er durch annäherung des Spielers, Kollision mit dem 
/// Spieler oder durch Schaden vom Spieler. Beim Explodieren verursacht sie 
/// Schaden bei allen Gegnern innerhalb eines Explosionsradius. Die Schadenshöhe
/// berechnet sich anhand der Entfernung zum Explosionsursprung.
/// 
/// Zunächst ist die Mine unsichtbar, und wird erst sichtbar, wenn der Spieler
/// sich ihr nähert und sie sehen kann. Kommt der Spieler ihr zu nahe blinkt 
/// sie Gelb. Wird sie ausgelöst blinkt sie rot und tickt synchron dazu, um 
/// kurze zeit später zu explodieren.
/// 
public class Mine : ImmovableEnemy<Mine> {
	
	
	
	/// <summary>
	/// Reichweite in der die Mine beginnt Gelb zu blinken.
	/// </summary>
	public static readonly float f_yellowRange = 3.0f;
	
	/// <summary>
	/// Reichweite in der die Mine beginnt Rot zu blinken und bald zu explodieren.
	/// </summary>
	public static readonly float f_redRange = 1.0f;
	
	/// <summary>
	/// Explosionsradius der Mine.
	/// </summary>
	public static readonly float f_explosionRadius = 2.0f;
	
	/// <summary>
	/// Schaden den die Explosion maximal verursachen kann
	/// </summary>
	public static readonly float f_explosionDamage = 75.0f;
	
	
	
	/// <summary>
	/// Referenz auf das Explosionsgeräusch
	/// </summary>
	public static AudioClip ac_explosion;
	
	/// <summary>
	/// Referenz auf das Tickgeräusch, das Synchron zum rot blinken abgespielt wird
	/// </summary>
	public static AudioClip ac_tick;
	
	
	
	/// <summary>
	/// Boolscher-Schalter, um nicht mehrmals innerhalb des gleichen Animations-
	/// Frames den Sound abzuspielen.
	/// </summary>
	public bool ticked = false;
	
	
	
	public Mine() : base(1) { //1 HP
		//Zustandsautomaten initialisieren
		AttackFSM.GlobalState = SMineInvisible.Instance;
		AttackFSM.CurrentState = SMineIdle.Instance;
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 3;
		txtFPS = 20;
		
		//SpriteController einschalten
		Animated = true;
		
		//Sound-Referenzen laden
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
		if(ac_tick == null) ac_tick = (AudioClip) Resources.Load("Sounds/minetick");
	}
	
	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Rot blinken und baldigem Explodieren
	/// </summary>
	/// <param name='other'>
	/// Kollisionsobjekt
	/// </param>
	void OnTriggerEnter(Collider other) {
		//Kollision nur mit Spieler
		if(other.tag == "Player"){
			//Explodieren
			Explode();
		}
	}
	
	
	
	/// <summary>
	/// Schaden führt zu Rot blinken und baldigem Explodieren
	/// </summary>
	/// <param name='damage'>
	/// Schadenswert.
	/// </param>
	public override void ApplyDamage(Vector3 damage){
		//Schaden führt immer zur Explosion, egal von wem
		Explode();
	}
	
	
	
	/// <summary>
	/// Mine sendet sich selbst die Nachricht das sie explodieren soll.
	/// </summary>
	private void Explode(){
		if(collider.enabled){
			//sende eine Nachricht an den Zustandsautomat um in den roten zustand zu gehen.
			MessageDispatcher.I.Dispatch(this, "explode");
		
			//Collider ausschalten, damit das nicht erneut ausgelöst wird
			collider.enabled = false;
		}
	}
	
	
	
}
