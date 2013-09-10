using UnityEngine;
using System.Collections;

/// 
/// Dieses Script sorgt dafür, dass der Spieler sich beim Start des Levels
/// aus dem Eingang heraus nach rechts bewegt
/// 
public class PlayerEnter : MonoBehaviour {
	
	
	
	/// <summary>
	/// Referenz auf den Spieler
	/// </summary>
	private PlayerController pc;
	private CharacterController cc;
	
	
	
	/// <summary>
	/// Die Distanz die der Spieler gehen soll bevor er wieder die Kontrolle erhält
	/// </summary>
	public float distance = 3.0f;
	
	
	
	/// <summary>
	/// Position des Spielers zu beginn des Spieles
	/// </summary>
	private Vector3 initialPosition;
	
	
	
	void Start () {
		//Referenzen laden
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		cc = pc.GetComponent<CharacterController>();
		
		//PlayerController auschalten
		pc.enabled = false;
		
		//Position des Spielers merken
		initialPosition = pc.transform.position;
	}
	
	
	void Update () {
		//Bewege Spieler nach Rechts
		cc.Move(Vector3.right * pc.runSpeed * 0.75f * Time.deltaTime);
		
		//Animation des Spielers
		pc.anim((int)PlayerController.AnimationTypes.moveRight);
		
		//wenn die gewünschte Distanz gelaufen wurde
		if(Vector3.Distance(initialPosition, pc.transform.position) > distance){
			//dieses Script ausschalten
			enabled = false;
			
			//Dem Spieler wieder die Kontrolle über die Spielfigur geben
			pc.enabled = true;
		}
	}
	
	
	
}
