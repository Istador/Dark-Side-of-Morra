using UnityEngine;
using System.Collections;

public class keyTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(
				null,
				(MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>(),
				"keyPickup",
				0.0f,
				null);
		
			collider.enabled = false;
			
			//TODO Soundgeräusch fürs Schlüssel aufheben
			
			Destroy(gameObject);
		}
	}
	
}
