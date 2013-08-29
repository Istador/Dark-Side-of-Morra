using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	
	
	public bool big = true;
	
	
	
	private static AudioClip ac_pickupsound;
	
	
	/// <summary>HP Wert von kleinen Health Globes</summary>
	public static readonly int i_smallHP = 10;
	
	/// <summary>HP Wert von großen Health Globes</summary>
	public static readonly int i_bigHP = 50;
	
	
	
	
	/*
	 * Textur / Sprite Controller
	*/
	private SpriteController spriteCntrl;
	/// <summary>Zeile der Animation</summary>
	private int txtState = 0;
	/// <summary>Anzahl Spalten (Frames)</summary>
	private int txtCols = 10;
	/// <summary>Anzahl Zeilen (Zustände)</summary>
	private int txtRows = 2;
	/// <summary>Frames per Second</summary>
	private int txtFPS = 10;
	
	
	
	void Start () {
		//SpriteController hinzufügen
		spriteCntrl = gameObject.AddComponent<SpriteController>();
				
		if(ac_pickupsound == null) ac_pickupsound = (AudioClip) Resources.Load("Sounds/healthpickup");
		
		if(big) txtState = 1;
	}
	
	
	
	void Update () {
		//Animation des Sprite-Controllers
		spriteCntrl.animate(txtCols, txtRows, 0, txtState, txtCols, txtFPS);
	}
	
	
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.layer == 8){ //fällt auf Level
			//Gravitation ausschalten
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		} else if(other.gameObject.tag == "Player"){ //Kollision mit Spieler
			
			//HP zum Spieler schicken
			if(big)
				SendHealthTo(other.gameObject, i_bigHP);
			else 
				SendHealthTo(other.gameObject, i_smallHP);
			
			//PickUp-Geräusch
			AudioSource.PlayClipAtPoint(ac_pickupsound, collider.bounds.center);
			
			Destroy(gameObject);
		}
	}
	
	
	
	void SendHealthTo(GameObject other, int hp){
		other.SendMessage("ApplyHealth", hp, SendMessageOptions.DontRequireReceiver);
	}
	
	
	
}
