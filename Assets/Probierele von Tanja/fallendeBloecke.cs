using UnityEngine;
using System.Collections;

public class fallendeBloecke : MonoBehaviour, MessageReceiver {
	
	public float zeitBisFall = 1.0f;
	
	public GameObject owner; //Block zu dem dieser Collider gehört
	
	private bool touched = false; //damit der Spieler nicht mehrfach OnTriggerEnter ausführt
	
	//Sound fürs bröckeln des Blocks
	private static AudioClip ac_disapear; 
	
	//Texturen
	private static Material[] red;
	private static Material[] normal;
	public Material normalBox;
	
	void Start(){
		//Sound laden
		if(ac_disapear == null) ac_disapear = (AudioClip) Resources.Load("Sounds/cratefalls");
		
		//Texturen laden
		if(red == null) red = new Material[]{owner.renderer.materials[0]};
		if(normal == null) normal = new Material[]{normalBox};
		
		//Normale Textur benutzen
		owner.renderer.materials = normal;
	}
	
	
	void OnTriggerEnter(Collider other) {
		if(!touched && other.gameObject.tag == "Player"){
			touched = true;
			//Nachricht an sich selbst für später
			MessageDispatcher.Instance.Dispatch(this,this,"verschwinden",zeitBisFall,null);
			//Rote Textur benutzen
			owner.renderer.materials = red;
			//Sound abspielen
			AudioSource.PlayClipAtPoint(ac_disapear, owner.collider.bounds.center);
		}
		
	}
	
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			case "verschwinden":
				owner.renderer.enabled = false; //Unsichtbar werden
				owner.collider.enabled = false; //Kollisionen ausschalten
				//Nachricht an sich selbst für später
				MessageDispatcher.Instance.Dispatch(this,this,"auftauchen",3.0f,null);
				return true;
			case "auftauchen":
				touched = false;
				owner.renderer.materials = normal; //Normale Textur benutzen
				owner.renderer.enabled = true; //Sichtbar werden
				owner.collider.enabled = true; //Kollisionen einschalten
				return true;
			default:
				//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
				return false;
		}
	}
	
	
}
