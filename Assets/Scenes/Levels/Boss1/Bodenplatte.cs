using UnityEngine;
using System.Collections;

public class Bodenplatte : MonoBehaviour, MessageReceiver {
	
	//Trigger-Collider des Child-Objektes
	private BodenplattenTrigger trigger;
	
	private static Material[] normal;
	private static Material[] red;
	private static Material[] green;
	
	private static Material[] mats;
	
	
	void Start(){
		//Collider des Spielertriggers
		trigger = transform.FindChild("Spielertrigger").GetComponent<BodenplattenTrigger>();
		trigger.collider.enabled = false;
		
		//Texturen laden
		if(mats == null) mats = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>().mats;
		if(normal == null) normal = new Material[]{mats[0]};
		if(red == null) red = new Material[]{mats[1]};
		if(green == null) green = new Material[]{mats[2]};
		
		//Normale Textur benutzen
		renderer.materials = normal;
	}
	
	
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			//dies soll eine grüne Bodenplatte sein
			case "green":
				renderer.materials = green;
				trigger.collider.enabled = true;
				
				//Health Globe laden
				UnityEngine.Object res = Resources.Load("bigHP");
				//Health Globe erstellen
				GameObject obj = (GameObject)UnityEngine.Object.Instantiate(res, transform.position+Vector3.up, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
				//Health Globe kurz nach oben bewegen lassen
				obj.rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
			
				return true;
			//dies soll eine rote Bodenplatte sein
			case "red":
				renderer.materials = red;
				return true;
			//Bodenplatte zurücksetzen
			case "normal":
				renderer.materials = normal;
				trigger.collider.enabled = false;
				trigger.entered = false;
				return true;
			default:
				//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
				return false;
		}
	}
	
	
	
}
