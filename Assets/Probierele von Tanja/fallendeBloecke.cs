using UnityEngine;
using System.Collections;

/// 
/// Fallende Blöcke, die zunächst unscheinbar sind,
/// aber so bald der Spieler sie betritt werden sie Rot, 
/// und verschwinden nach kurzer Zeit.
/// 
/// Dieses Script wird auf den Trigger des Kindes vom eigtl. Block gezogen
/// 
public class fallendeBloecke : GeneralObject {
	
	
	
	/// <summary>
	/// Zeit in Sekunden, bis der Block verschwindet.
	/// </summary>
	public float zeitBisFall = 1.0f;
	
	/// <summary>
	/// Zeit in Sekunden, doe der Block verschwunden bleibt.
	/// </summary>
	public float zeitVerschwunden = 2.0f;
	
	/// <summary>
	/// Der Block der verschwinden soll, zu dem dierer Trigger gehört
	/// </summary>
	public GameObject owner;
	
	/// <summary>
	/// Automatisch die Textur skalieren, so wie bei den normalen Blöcken.
	/// Ansonsten fallen zu große Fallen offensichtlich auf.
	/// </summary>
	private AutoScale scale;
	
	
	
	//Texturen
	
	/// <summary>
	/// Die Textur vor dem Verschwinden fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[] red;
	/// <summary>
	/// Die normale Textur fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[] normal;
	/// <summary>
	/// Die Textur, welche die Falle im normalfall darstellen soll.
	/// Die Textur die vor dem verschwinden angezeigt werden soll liegt auf owner.
	/// </summary>
	public Material normalBox;
	
	
	
	void Start(){		
		//Texturen laden
		if(red == null) red = new Material[]{owner.renderer.materials[0]};
		if(normal == null) normal = new Material[]{normalBox};
		
		//Normale Textur benutzen
		owner.renderer.materials = normal;
		
		//Textur skalieren
		scale = owner.GetComponent<AutoScale>();
		scale.Rescale();
	}
	
	
	
	void OnTriggerEnter(Collider other) {
		//wenn der Spieler die Falle betritt
		if(other.gameObject.tag == "Player"){
			//nicht erneut mit dem Spieler kollidieren
			collider.enabled = false;
			
			//Nachricht an sich selbst für später
			MessageDispatcher.I.Dispatch(this,"verschwinden",zeitBisFall);
			
			//Rote Textur benutzen
			owner.renderer.materials = red;
			scale.Rescale(); //Textur neu skalieren
			
			//Sound abspielen
			PlaySound("cratefalls", Posi (owner));
		}
	}
	
	
	
	// Methode um eingehende Nachrichten zu verarbeiten
	public override bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			case "verschwinden":
				owner.renderer.enabled = false; //Unsichtbar werden
				owner.collider.enabled = false; //Kollisionen ausschalten
				//Nachricht an sich selbst für später
				MessageDispatcher.I.Dispatch(this, "auftauchen", zeitVerschwunden);
				return true;
			case "auftauchen":
				collider.enabled = true; //Collisionen wieder zulassen
				owner.renderer.materials = normal; //Normale Textur benutzen
				scale.Rescale(); //Textur neu skalieren
				owner.renderer.enabled = true; //Sichtbar werden
				owner.collider.enabled = true; //Kollisionen einschalten
				return true;
			default:
				//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
				return false;
		}
	}
	
	
	
}
