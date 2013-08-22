using UnityEngine;
using System.Collections;

public class fallendeBloecke : MonoBehaviour, MessageReceiver {
	public GameObject owner;
void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(this,this,"enter",1.0f,null);
					
		}
		
}
	public bool HandleMessage(Telegram msg){
	switch(msg.message){
		case "enter": owner.renderer.enabled = false;
					  owner.collider.enabled = false;
					  MessageDispatcher.Instance.Dispatch(this,this,"auftauchen",3.0f,null);
					  return true;
		case "auftauchen": owner.renderer.enabled = true;
						   owner.collider.enabled = true;
					       return true;
			
		default:	return false;
		}
		
	}
}
