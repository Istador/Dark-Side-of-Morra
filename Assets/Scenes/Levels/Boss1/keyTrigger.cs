using UnityEngine;
using System.Collections;

/// 
/// Trigger auf dem Schlüssel prefab, der ausgeführt wird, wenn der
/// Spieler nach dem Tod der Spinne den Schlüssel aufhebt
/// 
public class keyTrigger : GeneralObject {
	
	
	
	void Start(){		
		//Drop-Geräusch abspielen
		PlaySound("keydrop");
	}
	
	
	
	void OnTriggerEnter(Collider hit){
		//Wenn der Schlüssel das Level berührt
		if(hit.gameObject.layer == 8){
			//Gravitation und Bewegung ausschalten
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		//Wenn der Schlüssel den Spieler berührt
		if(hit.gameObject.tag == "Player"){
			
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht ans Level, dass der Schlüssel aufgehoben wurde
			MessageDispatcher.I.Dispatch(level, "keyPickup");
			
			//PickUp-Geräusch abspielen
			PlaySound("keypickup");
			
			//Trigger ausschalten
			collider.enabled = false;
			
			//sich selbst löschen
			Destroy(gameObject);
		}
	}
	
	
	
}
