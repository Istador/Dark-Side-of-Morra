using UnityEngine;
using System.Collections;

public class BodenplattenTrigger : MonoBehaviour {
	
	private static Bodenplatten plattenController;
	
	public bool entered = false;
	
	void Start(){
		if(plattenController == null)
			plattenController = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
	}
	
	void OnTriggerEnter(Collider hit){
		if(!entered && hit.gameObject.tag == "Player"){
			entered = true;
			MessageDispatcher.Instance.Dispatch(null, plattenController, "enter", 0.0f, null);
		}
	}
	
	void OnTriggerStay(Collider hit){
		if(!entered && hit.gameObject.tag == "Player"){
			entered = true;
			MessageDispatcher.Instance.Dispatch(null, plattenController, "enter", 0.0f, null);
		}
	}
	
	void OnTriggerExit(Collider hit){
		if(entered && hit.gameObject.tag == "Player"){
			entered = false;
			MessageDispatcher.Instance.Dispatch(null, plattenController, "leave", 0.0f, null);
		}
	}
}
