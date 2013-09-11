using UnityEngine;
using System.Collections;

/// 
/// Dieses Skript ist für eine einzelne Bodenplatte bestimmt.
/// 
/// Die Bodenplatte kann rot oder grün werden.
/// Dieses Skript empfangt Nachrichten vom Bodenplatten Skript, 
/// und aktiviert den Trigger für den sicheren Standort wenn es eine Grüne 
/// Platte ist.
/// 
public class Bodenplatte : MonoBehaviour, MessageReceiver {
	
	
	
	/// <summary>
	/// Trigger-Collider des Child-Objektes, das im Falle einer grünen Platte
	/// die Sicherheit des Spielers signalisiert.
	/// </summary>
	private BodenplattenTrigger trigger;
	
	
	
	/// <summary>
	/// Die normale Textur die außerhalb des Platten-Events angezeigt wird.
	/// Fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[] normal;
	/// <summary>
	/// Die rote Textur die wärend des Platten-Events angezeigt wird für Platten auf denen der Spieler sterben würde.
	/// Fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[] red;
	/// <summary>
	/// Die grüne Textur die wärend des Platten-Events angezeigt wird, auf der der Spieler sicher ist.
	/// Fertig zum Anwenden auf den Renderer.
	/// </summary>
	private static Material[] green;
	/// <summary>
	/// Ein Array der drei zu verwendenen Texturen.
	/// </summary>
	private static Material[] mats;
	
	
	
	void Start(){
		//Triggers holen
		trigger = transform.FindChild("Spielertrigger").GetComponent<BodenplattenTrigger>();
		//Trigger ausschalten
		trigger.collider.enabled = false;
		
		//Texturen laden falls noch nicht geschehen
		if(mats == null) mats = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>().mats;
		if(normal == null) normal = new Material[]{mats[0]};
		if(red == null) red = new Material[]{mats[1]};
		if(green == null) green = new Material[]{mats[2]};
		
		//Normale Textur benutzen
		renderer.materials = normal;
	}
	
	
	
	// Methode um eingehende Nachrichten zu verarbeiten
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			
			//dies soll eine grüne Bodenplatte sein
			case "green":
				//Grüne Textur benutzen
				renderer.materials = green;
				//Trigger einschalten
				trigger.collider.enabled = true;
				
				//Health Globe laden
				Object res = Resource.Prefab["bigHP"];
				//Health Globe erstellen
				GameObject obj = (GameObject)Object.Instantiate(res, transform.position+Vector3.up, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
				//Health Globe kurz nach oben bewegen lassen
				obj.rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
			
				return true;
			
			//dies soll eine rote Bodenplatte sein
			case "red":
				//Rote Textur benutzen
				renderer.materials = red;
				return true;
			
			//Bodenplatte zurücksetzen
			case "normal":
				//Normale Textur benutzen
				renderer.materials = normal;
				//Trigger ausschalten
				trigger.collider.enabled = false;
				//Trigger-Zustand zurücksetzen
				trigger.entered = false;
				return true;
			
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
			default:
				return false;
		}
	}
	
	
	
}
