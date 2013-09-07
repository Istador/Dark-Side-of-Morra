using UnityEngine;
using System.Collections;

public class keyTrigger : MonoBehaviour {
	
	
	
	//Schlüssel aufheben
	public AudioClip ac_pickup;
	//Schlüssel fallenlassen
	public AudioClip ac_drop;
	
	
	
	void Start(){
		//Drop-Geräusch
		AudioSource.PlayClipAtPoint(ac_drop, collider.bounds.center);
	}
	
	
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.layer == 8){ //fällt auf Level
			//Gravitation ausschalten
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		else if(hit.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(
				null,
				(MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>(),
				"keyPickup",
				0.0f,
				null);
		
			collider.enabled = false;
			
			//PickUp-Geräusch
			AudioSource.PlayClipAtPoint(ac_pickup, collider.bounds.center);
			
			Destroy(gameObject);
		}
	}
	
}
