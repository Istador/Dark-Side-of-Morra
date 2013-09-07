using UnityEngine;
using System.Collections;

public class Bodenplatten : MonoBehaviour, MessageReceiver {
	
	//Zufallsgenerator
	private System.Random rnd = new System.Random();
	
	//Ist der Spieler auf der sicheren Bodenplatte?
	private bool playerSafe = false;
	
	//Array aller Bodenplatten
	private Bodenplatte[] platten;
	
	//Bossreferenz
	private MessageReceiver spinne;
	
	//Texturen für die Bodenplatten
	public Material[] mats;
	
	void Start () {
		//alle Bodenplatten abrufen
		platten = gameObject.GetComponentsInChildren<Bodenplatte>();
		
		//Boss abrufen
		spinne = GameObject.Find("Spider").GetComponent<Spider>();
	}
	
	
	
	private void Begin(){
		playerSafe = false;
		
		//zufällig eine Platte auswählen
		int good = rnd.Next(0, platten.Length);
		
		//für alle Platten
		for(int i=0; i < platten.Length; i++){
			//grüne Platte
			if(i==good)
				MessageDispatcher.Instance.Dispatch(this, platten[i], "green", 0.0f, null);
			//rote Platten
			else
				MessageDispatcher.Instance.Dispatch(this, platten[i], "red", 0.0f, null);
		}
		
		//in 5 Sekunden muss der Spieler auf der Platform sein
		MessageDispatcher.Instance.Dispatch(this, this, "fire", 5.0f, null);
	}
	
	
	
	private void Fire(){
		//Spieler töten wenn nicht auf grüner Platte
		if(!playerSafe)
			GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyDamage", Vector3.down * 1000.0f, SendMessageOptions.DontRequireReceiver);
		else {
			playerSafe = false;
		
			//alle Bodenplatten normalisieren
			foreach(Bodenplatte p in platten)
				MessageDispatcher.Instance.Dispatch(this, p, "normal", 0.0f, null);
			
			//Boss eine Nachricht schicken damit er wieder auftaucht
			MessageDispatcher.Instance.Dispatch(this, spinne, "auftauchen", 1.0f, null);
		}
	}
	
	
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			//Neue Platten Runde
			case "begin":
				Begin();
				return true;
			//Spieler betritt sichere Platte
			case "enter":
				playerSafe = true;
				return true;
			//Spieler verlässt die sichere Platte
			case "leave":
				playerSafe = false;
				return true;
			//die Zeit für den Spieler ist um
			case "fire":
				Fire();
				return true;
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden	
			default:
				return false;
		}
	}
	
	
	
}
