using UnityEngine;
using System.Collections;

public class cellTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(
				null,
				(MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>(),
				"cellOpened",
				0.0f,
				null);
		
			collider.enabled = false;
			Destroy(gameObject);
		}
	}
	
}
