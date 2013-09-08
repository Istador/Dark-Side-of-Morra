using UnityEngine;
using System.Collections;

public class unicorn2 : MonoBehaviour {
	
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
		
			//TODO: Dialog starten, und erst nach dem Dialog die Nachricht verschicken.
			
			MessageReceiver level = (MessageReceiver)GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "dialog2", 0.0f, null);
		
			collider.enabled = false;
		}
	}
	
}
