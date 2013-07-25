using UnityEngine;
using System.Collections;

public class MsgDispatcher : MonoBehaviour {

	void LateUpdate () {
		Debug.Log("LateUpdate");
		MessageDispatcher.Instance.DispatchDelayedMessages();
	}
}
