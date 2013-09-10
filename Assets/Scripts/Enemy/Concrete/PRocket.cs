using UnityEngine;
using System.Collections;

/// 
/// Raketen werden von RPG-Soldaten und Automatischen Geschützen abgefeuert.
/// Sie verfolgen den Spieler, sofern dieser für sie sichtbar ist.
/// 
public class PRocket : Projektile<PRocket> {
	
	
	
	/// <summary>
	/// Schaden den das Projektil beim Spieler verursacht
	/// </summary>
	/// <value>
	/// in ganzen Trefferpunkten
	/// </value>
	public override int Damage { get{return 40;} }
	
	
	
	/// <summary>
	/// Richtung in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public override Vector3 Heading {get{return TargetPos - Pos;}}
	
	
	
	/// <summary>
	/// Referenz auf das Explosionsgeräusch, das beim Tode abgespielt wird
	/// </summary>
	private static AudioClip ac_explosion;
	
	
	
	/// <summary>
	/// Referenz auf das explosions Prefab
	/// </summary>
	private static Object res_explosion;
	
	
	
	protected override void Start(){
		//Zielposition setzen
		TargetPos = Pos + Heading.normalized * MaxSpeed;
		
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 4;
		txtRows = 1;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 2.4f;
		MaxForce = 2.4f;
		
		//Explosions Prefab laden
		if(res_explosion == null) res_explosion = Resources.Load("prefab kleineExplosion");
		
		//Explosionsgeräusch laden
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
	}
	
	
	
	protected override void Update() {
		//Wenn der Spieler in Sicht ist
		if(LineOfSight(Player)){
			//Strebe die Position des Spielers an
			TargetPos = PlayerPos;
			
			Debug.DrawLine(Pos, TargetPos, Color.green);
		}
		else
			Debug.DrawLine(Pos, TargetPos, Color.red);
		
		//u.a. Rotieren zum Ziel, Bewegung umsetzen
		base.Update();
		
		//Ziel erreicht, möglich wenn z.B. der Spieler nicht mehr sichtbar ist
		if(DistanceTo(TargetPos) < 0.1){
			Death();
		}
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = Instantiate(res_explosion);
		
		//nach 0.5 Sekunden Explosion wieder auflösen
		Destroy(explosion, 0.5f);
		
		//Explosionsgeräusch
		PlaySound(ac_explosion);
		
		//ansonsten normale sterben
		base.Death();
	}
	
	
	
}
