using UnityEngine;
using System.Collections;

public class MsgDispatcher : MonoBehaviour {

	void LateUpdate(){
		MessageDispatcher.Instance.DispatchDelayedMessages();
	}
}
