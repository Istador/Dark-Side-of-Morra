using UnityEngine;
using System.Collections;

/// 
/// Trigger der das Ausschlüpfen der Spinne aus dem Kokon startet
/// 
public class BossTrigger : MonoBehaviour {
	
	
	/// <summary>
	/// Referenz auf die Spinne
	/// </summary>
	MessageReceiver spinne;
	
	/// <summary>
	/// Referenz auf den Kokon
	/// </summary>
	MessageReceiver kokon;
	
	
	
	void Start () {
		//Referenzen einmal zu Beginn laden
		spinne = GameObject.Find("Spider").GetComponent<Spider>();
		kokon = GameObject.Find("Kokon").GetComponent<Kokon>();
	}
	
	
	
	void OnTriggerEnter(Collider hit){
		//Trigger ausgelöst von Spieler oder Bullet vom Spieler
		if(hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet"){
		
			//Kokon öffnen (Referenz auf Spinne weiterreichen)
			MessageDispatcher.I.Dispatch(spinne, kokon, "start", 0.0f, null);
			//Zustandsübergang bei der Spinne bewirken. nicht mehr unsichtbar
			MessageDispatcher.I.Dispatch(spinne, "ausschluepfen");
			
			//Diesen Trigger ausschalten
			collider.enabled = false;
		}
	}
	
}
