using UnityEngine;
using System.Collections;

public class cellTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			MessageReceiver level = (MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "cellOpened", 0.0f, null);
		
			collider.enabled = false;
			Destroy(gameObject);
		}
	}
	
}
