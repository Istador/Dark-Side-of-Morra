using UnityEngine;
using System.Collections;

public class Mine : ImmovableEnemy<Mine> {
	
	
	
	/// <summary>Reichweite in der die Mine beginnt Gelb zu blinken.</summary>
	public static readonly float f_yellowRange = 3.0f;
	
	/// <summary>Reichweite in der die Mine beginnt Rot zu blinken und bald zu explodieren.</summary>
	public static readonly float f_redRange = 1.0f;
	
	/// <summary>Explosionsradius der Mine.</summary>
	public static readonly float f_explosionRadius = 1.5f;
	
	/// <summary>Schaden den die Explosion maximal verursachen kann</summary>
	public static readonly float f_explosionDamage = 75.0f;
	
	
	/// <summary>Explosionsgeräusch</summary>
	public static AudioClip ac_explosion;
	/// <summary>Tickgeräusch</summary>
	public static AudioClip ac_tick;
	public bool ticked = false; //für den roten zustand, ob bereits getickt wurde
	
	
	protected override int txtCols { get{return 10;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 3;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 20;} }  //Frames per Second
	
	
	
	public Mine() : base(1) { //1 HP
		AttackFSM.SetGlobalState(SMineInvisible.Instance);
		AttackFSM.SetCurrentState(SMineIdle.Instance);
	}
	
	
	
	protected override void Start(){
		base.Start();
		
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
			Explode();
			GetComponent<SphereCollider>().enabled = false;
		}
	}
	
	
	
	/// <summary>
	/// Schaden führt zu Rot blinken und baldigem Explodieren
	/// </summary>
	/// <param name='damage'>
	/// Schadenswert.
	/// </param>
	public override void ApplyDamage(Vector3 damage){
		//Schaden führt immer zur Explosion
		Explode();
	}
	
	
	
	/// <summary>
	/// Mine sendet sich selbst die Nachricht das sie explodieren soll.
	/// </summary>
	private void Explode(){
		//sende eine Nachricht an den Zustandsautomat um in den roten zustand zu gehen.
		MessageDispatcher.Instance.Dispatch(new Telegram(this, "explode"));
	}
	
	
	
}
