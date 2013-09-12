using UnityEngine;
using System.Collections;

/// 
/// Dieses Skript ist der Zentrale Ansprechpartner für andere Komponenten
/// um das Bodenplatten-Event auszulösen. Es steuert die einzelnen Bodenplatten an,
/// und meldet sich nach Beendigung wieder bei dem Bossgegner zurück.
/// 
public class Bodenplatten : MonoBehaviour, MessageReceiver {
	
	
	
	/// <summary>
	/// Zufallsgenerator um zufällig eine Bodenplatte auswählen zu können die grün wird.
	/// </summary>
	private static System.Random rnd = new System.Random();
	
	/// <summary>
	/// Ist der Spieler auf der sicheren grünen Bodenplatte?
	/// </summary>
	private bool playerSafe = false;
	
	/// <summary>
	/// Array aller Bodenplatten, um über alle iterieren zu können.
	/// </summary>
	private Bodenplatte[] platten;
	
	/// <summary>
	/// Nicht-Bodenplatten, die ebenfalls rot werden sollen, damit der
	/// Spieler erkennt das er überall sterben wird, außer auf der grünen
	/// Bodenplatte.
	/// 
	/// Z.B die schrägen Rampen
	/// </summary>
	private AutoScale[] boden;
	
	/// <summary>
	/// Referenz auf den Spinnenboss, um ihm Nachrichten schicken zu können.
	/// </summary>
	private MessageReceiver spinne;
	
	/// <summary>
	/// Zeit die der Spieler hat um auf die richtige Bodenplatte zu kommen.
	/// </summary>
	public static readonly float f_duration = 4.0f;
	
	
	
	void Start () {
		//alle Bodenplatten abrufen
		platten = gameObject.GetComponentsInChildren<Bodenplatte>();
		
		//Boss abrufen
		spinne = GameObject.Find("Spider").GetComponent<Spider>();
		
		//Rampen und Boden abrufen um auch rot zu werden
		boden = (AutoScale[])GameObject.FindObjectsOfType(typeof(AutoScale));
	}
	
	
	
	/// <summary>
	/// Platten-Event starten
	/// </summary>
	private void Begin(){
		//variable ob der Spieler sicher ist zurücksetzen
		playerSafe = false;
		
		//zufällig eine Platte auswählen
		int good = rnd.Next(0, platten.Length);
		
		//für alle Platten
		for(int i=0; i < platten.Length; i++){
			//für die eine grüne Platte
			if(i==good)
				MessageDispatcher.I.Dispatch(platten[i], "green");
			//für die anderen, roten Platten
			else
				MessageDispatcher.I.Dispatch(platten[i], "red");
		}
		
		Material[] red = Bodenplatte.red;
		//den restlichen Boden
		foreach(AutoScale o in boden){
			//ebenfalls Rot machen
			o.renderer.materials = red;
			//Textur Neu skalieren
			o.Rescale();
		}
		
		//in 5 Sekunden muss der Spieler auf der Platform sein
		MessageDispatcher.I.Dispatch(this, "fire", f_duration);
	}
	
	
	
	/// <summary>
	/// Die Zeit für den Spieler ist um, jetzt wird geguckt ob er auf der
	/// grünen Platte ist und weiterleben darf, oder nicht.
	/// </summary>
	private void Fire(){
		//Spieler töten wenn nicht auf grüner Platte
		if(!playerSafe){
			// 1000 HP Schaden zum Spieler
			GameObject.FindGameObjectWithTag("Player").SendMessage("ApplyDamage", Vector3.down * 1000.0f, SendMessageOptions.DontRequireReceiver);
		}
		//Spieler befand sich auf der grünen Platte
		else {
			//variable ob der Spieler sicher ist zurücksetzen
			playerSafe = false;
		
			//alle Bodenplatten normalisieren
			foreach(Bodenplatte p in platten)
				MessageDispatcher.I.Dispatch(p, "normal");
			//den restlichen Boden normalisieren
			Material[] normal = Bodenplatte.normal;
			foreach(AutoScale o in boden){
				//Textur normalisieren
				o.renderer.materials = normal;
				//Textur neu skalieren
				o.Rescale();
			}
			
			//Dem Boss eine Nachricht schicken, damit er weiß dass das Event 
			//vorbei ist und er wieder auftauchen kann.
			MessageDispatcher.I.Dispatch(spinne, "auftauchen", 1.0f);
		}
	}
	
	
	
	// Methode um eingehende Nachrichten zu verarbeiten
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			
			//Neue Platten Runde
			case "begin":
				Begin();
				return true;
			
			//Spieler betritt die sichere grüne Platte
			case "enter":
				playerSafe = true;
				return true;
			
			//Spieler verlässt die sichere grüne Platte
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
