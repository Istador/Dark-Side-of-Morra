using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {
	
	private int key = 0;
	private CharacterController characterController;
	public GameObject magicDoor;
	public GameObject magicKey1;
	public GameObject magicSparcle1;
	public GameObject magicKey2;
	public GameObject magicSparcle2;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
		void  OnTriggerEnter ( Collider characterController  )
	{
		if (characterController.gameObject.CompareTag ("Key1"))
		{
		
			key++;
			Debug.Log("Juhuu du hast einen Schlüssel gefunden");
			Destroy(magicKey1);
			Destroy(magicSparcle1);
			// character is on the trigger
		}
		if (characterController.gameObject.CompareTag ("Key2")){
			key ++;
			Debug.Log("Juhuu, du hast einen Schlüssel gefunden");
			Destroy(magicKey2);
			Destroy(magicSparcle2);
		}
		if (characterController.gameObject.CompareTag ("Lock")){
			Debug.Log ("Rainbowsparkle beinhaltet die Antwort");
		}
		if (characterController.gameObject.CompareTag ("Lock") && key == 1){
			
			Debug.Log ("Da fehlt noch was");	
		}
		if (characterController.gameObject.CompareTag ("Lock") && key == 2){
			
			Destroy(magicDoor);	
		}
	}
}
