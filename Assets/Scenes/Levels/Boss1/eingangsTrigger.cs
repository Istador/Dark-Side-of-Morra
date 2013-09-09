using UnityEngine;
using System.Collections;

/// 
/// Trigger der ausgelöst wird, wenn der Spieler den Raum betritt.
/// Gibt dem Level bescheid um die Tür zu schließen.
/// 
public class eingangsTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		//wenn Trigger ausgelöst vom Spieler
		if(hit.gameObject.tag == "Player"){
		
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht an Level, dass Spieler den Raum betreten hat
			MessageDispatcher.I.Dispatch(level, "roomEntered");
			
			//Trigger ausschalten
			collider.enabled = false;
			//sich selbst löschen
			Destroy(gameObject);
		}
	}
	
}
