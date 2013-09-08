using UnityEngine;
using System.Collections;

public class unicorn2 : MonoBehaviour {
	
	void Start(){
		Dialog d = GetComponent<Dialog>();
		
		//Code der ausgeführt wird nach dem Dialog
		d.postDialog = (GameObject obj) => {
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			MessageDispatcher.Instance.Dispatch(null, level, "dialog2", 0.0f, null);
		};
	}
	
}
