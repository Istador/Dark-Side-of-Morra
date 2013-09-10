using UnityEngine;
using System.Collections;

public class MsgDispatcher : MonoBehaviour {
	
	/// <summary>
	/// Liefert zeitverzögerte Nachrichten aus
	/// </summary>
	void LateUpdate(){
		MessageDispatcher.I.DispatchDelayedMessages();
	}
}
