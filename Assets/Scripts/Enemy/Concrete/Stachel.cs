using UnityEngine;
using System.Collections;

public class Stachel : MLeftRight<Stachel> {
	
	public static readonly int damage = 5; //Schaden pro beruehrung
	public static readonly float tickLength = 0.5f; //minimale Zeit in Sekunden zwischen Schaden
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 3.0f;} }
	
	
	
	protected override int txtCols { get{return 8;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 2;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 4;} }  //Frames per Second
	
	
	
	public Stachel() : base(100){}
	
	
	
	protected override void Start(){
		base.Start();
		//Physics.IgnoreCollision(collider, player.collider);
		lastPlayerDamage = Time.time;
	}
	
	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	/// <param name='other'>
	/// Kollisionsobjekt
	/// </param>
	void OnCollisionEnter(Collision other) {
		//Kollision nur mit Spieler
		if(other.gameObject.tag == "Player"){
			DoPlayerDamage();
		}
	}

	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	/// <param name='other'>
	/// Kollisionsobjekt
	/// </param>
	void OnCollisionStay(Collision other) {
		//Kollision nur mit Spieler
		if(other.gameObject.tag == "Player"){
			DoPlayerDamage();
		}
	}
	
	
	
	//wann das letzte mal Schaden beim Spieler verursacht wurde
	private float lastPlayerDamage;
	
	void DoPlayerDamage(){
		if(lastPlayerDamage + tickLength < Time.time){
			DoDamageTo(player, damage);
			lastPlayerDamage = Time.time;
		}
	}
	
	
	
}
