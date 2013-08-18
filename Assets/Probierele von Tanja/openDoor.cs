using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {
	
	private int key = 0;
	private CharacterController characterController;
	public GameObject magicDoor;
	public GameObject magicKey;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
		void  OnTriggerEnter ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Key"))
		{
		
			key = 1;
			Debug.Log("Juhuu du hast den Schlüssel gefunden, nun kannst du gewinnen");
			Destroy(magicKey);
			// character is on the trigger
		}
		if (characterController.gameObject.CompareTag ("Door")){
			Debug.Log ("Rainbowsparkle beinhaltet die Antwort");
		}
		if (characterController.gameObject.CompareTag ("Door") && key == 1){
			Debug.Log("jetzt kann es knallen");
			
		
			Destroy(magicDoor);
			
		}
	}
}
