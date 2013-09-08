using UnityEngine;
using System.Collections;

public class cellTrigger : MonoBehaviour {
	
	void Start(){
		Dialog d = GetComponent<Dialog>();
		
		//Code der ausgeführt wird vor dem Dialog
		d.preDialog = (GameObject obj) => {
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "cellOpened", 0.0f, null);
		};
		
		//Code der ausgeführt wird nach dem Dialog
		d.postDialog = (GameObject obj) => {
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "dialog3", 0.0f, null);
		};
	}
	
}
