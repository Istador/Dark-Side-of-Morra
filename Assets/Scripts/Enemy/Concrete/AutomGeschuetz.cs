using UnityEngine;
using System.Collections;

public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	
	
	/// <summary>Explosionsgeräusch</summary>
	public static AudioClip ac_explosion;
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu Dicht ist zum Starten von Raketen
	/// </summary>
	public static readonly float f_closeRange = 2.0f;
	
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
		
		f_HealthGlobeProbability = 0.8f; //80% drop, 20% kein drop
		f_HealthGlobeBigProbability = 0.8f; //80% big, 20% small
		// 0,8 * ( 0,8 * 50 + 0,2 * 10 ) = 33,6 HP on average
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		//Explosionsgeräusch
		AudioSource.PlayClipAtPoint(ac_explosion, collider.bounds.center);
		
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
