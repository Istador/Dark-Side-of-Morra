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
	
	
	
	protected override int txtCols { get{return 2;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 3;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 5;} }  //Frames per Second
	
	
	
	public Mine() : base(1) { //1 HP
		AttackFSM.SetCurrentState(SMineInvisible.Instance);
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
			GetComponent<BoxCollider>().enabled = false;
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
