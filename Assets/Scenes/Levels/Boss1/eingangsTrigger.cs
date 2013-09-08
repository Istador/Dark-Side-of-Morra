using UnityEngine;
using System.Collections;

public class eingangsTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			MessageReceiver level = (MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "roomEntered", 0.0f, null);
			
			collider.enabled = false;
			Destroy(gameObject);
		}
	}
	
}
