using UnityEngine;
using System.Collections;

public class Bodenplatte : MonoBehaviour, MessageReceiver {
	
	//Trigger-Collider des Child-Objektes
	private BodenplattenTrigger child;
	private Collider trigger;
	
	private static Material[] normal;
	private static Material[] red;
	private static Material[] green;
	public Material[] mats;
	
	
	void Start(){
		//Collider des Spielertriggers
		child = transform.FindChild("Spielertrigger").GetComponent<BodenplattenTrigger>();
		trigger = child.collider;
		trigger.enabled = false;
		
		//Texturen laden
		if(normal == null) normal = new Material[]{mats[0]};
		if(red == null) red = new Material[]{mats[1]};
		if(green == null) green = new Material[]{mats[2]};
		
		//Normale Textur benutzen
		renderer.materials = normal;
	}
	
	
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			case "green":
				renderer.materials = green;
				trigger.enabled = true;
				
				//Health Globe laden
				UnityEngine.Object res = Resources.Load("bigHP");
				//Health Globe erstellen
				GameObject obj = (GameObject)UnityEngine.Object.Instantiate(res, transform.position+Vector3.up, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
				//Health Globe kurz nach oben bewegen lassen
				obj.rigidbody.AddForce(Vector3.up * 4.0f, ForceMode.Impulse);
			
				return true;
			case "red":
				renderer.materials = red;
				trigger.enabled = false;
				return true;
			case "normal":
				renderer.materials = normal;
				trigger.enabled = false;
				
				return true;
			default:
				//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
				return false;
		}
	}
	
	
	
}
