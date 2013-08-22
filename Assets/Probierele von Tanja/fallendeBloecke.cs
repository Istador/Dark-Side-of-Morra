using UnityEngine;
using System.Collections;

public class fallendeBloecke : MonoBehaviour, MessageReceiver {
	public float zeitBisFall = 1.0f;
	public GameObject owner;
void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
		
			MessageDispatcher.Instance.Dispatch(this,this,"enter",zeitBisFall,null);
					
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
