using UnityEngine;
using System.Collections;

public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 5.0f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 25.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly double d_reloadTime = 2.0; // 2 sekunden nachladen
	
	
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	
	
	public AutomGeschuetz() : base(250) { //250 HP
		AttackFSM.SetCurrentState(SAGIdle.Instance);
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		//TODO : Soundgeräusch
		
		base.Death();
	}
	
	
	
	//Zustandsvariable für das Nachladen
	private float _reloadStart = 0.0f;
	/// <summary>
	/// Zeitpunkt an dem begonnen wurde Nachzuladen
	/// </summary>
	/// <value>
	/// Zeit in Sekunden seit Spielstart
	/// </value>
	public float reloadStart { get{return _reloadStart;} }
	/// <summary>
	/// Setzt die aktuelle Zeit als Zeitpunkt des Nachladebeginns
	/// </summary>
	public void BeginReload(){
		_reloadStart = Time.time;
	}
	
}
