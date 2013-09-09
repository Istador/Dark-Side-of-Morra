using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {
	// hier geht es darum, dass man erst 2 Schlüssel einsammeln muss, bevor die Tür verschwindet
	
	// dabei muss man die Game Objects noch von Hand zuweißen 	!! Wichtig !!
	private int key = 0;
	//private CharacterController characterController; //unused
	public GameObject magicDoor;
	public GameObject magicKey1;
	public GameObject magicSparcle1;
	public GameObject magicKey2;
	public GameObject magicSparcle2;
	public GameObject magicFalle;
	void Start () {
		//characterController = GetComponent<CharacterController>();
	}
	

	void Update () {
	
	}
	
		void  OnTriggerEnter ( Collider other  )
	{
		if (other.gameObject.CompareTag ("Key1"))
		{
		
			key++;
			Debug.Log("Juhuu du hast einen Schlüssel gefunden");
			Destroy(magicKey1);
			Destroy(magicSparcle1);
		
		}
		if (other.gameObject.CompareTag ("Key2")){
			key ++;
			Debug.Log("Juhuu, du hast einen Schlüssel gefunden");
			Destroy(magicKey2);
			Destroy(magicSparcle2);
		}
		if (other.gameObject.CompareTag ("Lock")){
			Debug.Log ("Rainbowsparkle beinhaltet die Antwort");
		}
		if (other.gameObject.CompareTag ("Lock") && key == 1){
			
			Debug.Log ("Da fehlt noch was");	
		}
		if (other.gameObject.CompareTag ("Lock") && key == 2){
			
			Destroy(magicDoor);	
		}
		
	}
}
