using UnityEngine;
using System.Collections;

public class fallendeBloecke : MonoBehaviour, MessageReceiver {
	public float zeitBisFall = 1.0f;
	public GameObject owner;
	
	
	private static Material[] red;
	private static Material[] normal;
	public Material normalBox;
	
	
	void Start(){
		if(red == null) red = new Material[]{owner.renderer.materials[0]};
		if(normal == null) normal = new Material[]{normalBox};
		owner.renderer.materials = normal;
	}
	
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player"){
			MessageDispatcher.Instance.Dispatch(this,this,"verschwinden",zeitBisFall,null);
			owner.renderer.materials = red;
		}
		
	}
	
	
	public bool HandleMessage(Telegram msg){
		switch(msg.message){
			case "verschwinden":
				owner.renderer.enabled = false;
				owner.collider.enabled = false;
				MessageDispatcher.Instance.Dispatch(this,this,"auftauchen",3.0f,null);
				return true;
			case "auftauchen":
				owner.renderer.materials = normal;
				owner.renderer.enabled = true;
				owner.collider.enabled = true;
				return true;
			default:
				return false;
		}
	}
	
	
}
