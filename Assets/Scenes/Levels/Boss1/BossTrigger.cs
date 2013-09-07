using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {
	
	MessageReceiver spinne;
	MessageReceiver kokon;
	
	
	
	void Start () {
		spinne = GameObject.Find("Spider").GetComponent<Spider>();
		kokon = GameObject.Find("Kokon").GetComponent<Kokon>();
	}
	
	
	
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet"){
		
			MessageDispatcher.Instance.Dispatch(spinne, kokon, "start", 0.0f, null);
			MessageDispatcher.Instance.Dispatch(null, spinne, "ausschluepfen", 0.0f, null);
			
			collider.enabled = false;
		}
	}
	
}
