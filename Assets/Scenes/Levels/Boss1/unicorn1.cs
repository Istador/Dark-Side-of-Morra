using UnityEngine;
using System.Collections;

public class unicorn1 : MonoBehaviour {
	
	void Start(){
		Dialog d = GetComponent<Dialog>();
		
		//Code der ausgeführt wird nach dem Dialog
		d.postDialog = (GameObject obj) => {
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "dialog1", 0.0f, null);
		};
	}
	
}
