using UnityEngine;
using System.Collections;

public class Kokon : MonoBehaviour, MessageReceiver {
	
	int frame = 0;
	int max_frame = 10;
	
	MessageReceiver spinne;
	
	public bool HandleMessage(Telegram msg){
		switch(msg.message){
			case "start":
				MessageDispatcher.Instance.Dispatch(this, this, "frame", 0.25f, (object)(frame+1));
				spinne = msg.sender;
				return true;
			case "frame":
				frame = (int)msg.extraInfo;
				
				//Textur ändern
				Vector2 offset = new Vector2( ((float) frame) * (1.0f / max_frame), 0.0f );
				renderer.material.mainTextureOffset = offset;
				
				//nächste Textur
				if(frame >= max_frame-1){
					MessageDispatcher.Instance.Dispatch(this, this, "over", 0.25f, null);
				} else {
					MessageDispatcher.Instance.Dispatch(this, this, "frame", 0.25f, (object)(frame+1));
				}
				return true;
			case "over":
				renderer.enabled = false;
				MessageDispatcher.Instance.Dispatch(null, spinne, "kokon_offen", 0.0f, null);
				Destroy(gameObject);
				return true;
			default:
				return false;
		}
	}
	
}
