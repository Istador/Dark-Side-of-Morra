using UnityEngine;
using System.Collections;

/// 
/// Trigger auf dem Schlüssel prefab, der ausgeführt wird, wenn der
/// Spieler nach dem Tod der Spinne den Schlüssel aufhebt
/// 
public class keyTrigger : MonoBehaviour {
	
	
	
	/// <summary>
	/// Geräusch: Schlüssel aufheben
	/// </summary>
	public AudioClip ac_pickup;
	
	
	
	/// <summary>
	/// Geräusch: Schlüssel fallenlassen
	/// </summary>
	public AudioClip ac_drop;
	
	
	
	void Start(){
		//Drop-Geräusch abspielen
		AudioSource.PlayClipAtPoint(ac_drop, collider.bounds.center);
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
			AudioSource.PlayClipAtPoint(ac_pickup, collider.bounds.center);
			
			//Trigger ausschalten
			collider.enabled = false;
			
			//sich selbst löschen
			Destroy(gameObject);
		}
	}
	
	
	
}
