using UnityEngine;
using System.Collections;

/// 
/// Health-Globes sind Herzen die von sterbenden Gegnern fallengelassen werden,
/// und die beim Spieler Lebenspunkte erneuern wenn sie aufgehoben werden.
/// 
/// Sie existieren in zwei Größen, die unterschiedlich viel Leben heilen.
/// 
/// Werden sie aufgehoben verursachen sie zusätzlich ein Geräusch zur Bestätigung
/// 
public class HealthGlobe : GeneralObject {
	
	
	
	/// <summary>
	/// Ob dies ein großer oder kleiner Health-Globe ist
	/// </summary>
	public bool big = true;
	
	
	
	/// <summary>
	/// HP-Wert um den der Spieler bei kleinen Health-Globes geheilt wird
	/// </summary>
	public static readonly int i_smallHP = 10;
	
	
	
	/// <summary>
	/// HP-Wert um den der Spieler bei großen Health-Globes geheilt wird
	/// </summary>
	public static readonly int i_bigHP = 50;
	
	
	
	// Start
	
	void Start() {
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 2;
		txtFPS = 10;
		
		//SpriteController einschalten
		Animated = true;
		
		//bei großen Health-Globes den anderen Sprite-Zustand verwenden
		if(big) Sprite = 1;
	}
	
	
	
	void OnTriggerEnter(Collider other){
		
		//Das Health-Globe fällt auf das Level
		if(other.gameObject.layer == 8){
			//Gravitation ausschalten
			rigidbody.useGravity = false;
			//Bewegung ausschalten
			rigidbody.isKinematic = true;
		}
		
		//Der Spieler löst den Trigger aus
		else if(other.gameObject.tag == "Player"){
			
			//bei großem Health-Globe
			if(big)
				//viel HP zum Spieler schicken
				DoHeal(other, i_bigHP);
			//bei kleinem Health-Globe
			else 
				//wenig HP zum Spieler schicken
				DoHeal(other, i_smallHP);
			
			//PickUp-Geräusch abspielen
			PlaySound("healthpickup");
			
			//Diesen Health-Globe zerstören
			Destroy(gameObject);
		}
	}
	
	
	
}
