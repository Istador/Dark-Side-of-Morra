using UnityEngine;
using System.Collections;

public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 1.5f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_outOfRange = 7.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit zwischen zwei Raketen die zum Nachladen veranschlagt wird.
	/// </summary>
	public static readonly double d_reloadTime = 3.0; // 3 sekunden nachladen
	
	
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	
	
	public AutomGeschuetz() : base(250) { //250 HP
		AttackFSM.SetCurrentState(SAGHoldFire.Instance);
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		//TODO : Soundgeräusch
		
		base.Death();
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
		);
	}
}
