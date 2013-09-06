using UnityEngine;
using System.Collections;

public class eingangsTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(
				null,
				(MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>(),
				"roomEntered",
				0.0f,
				null);
			
			collider.enabled = false;
			Destroy(gameObject);
		}
	}
	
}
