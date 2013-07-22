using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter ( Collider characterController )
	{
		if (characterController.gameObject.CompareTag("Player1"))
		{
			Application.LoadLevel(1);
			Debug.Log("Level zu Ende");
		}
	}
}
