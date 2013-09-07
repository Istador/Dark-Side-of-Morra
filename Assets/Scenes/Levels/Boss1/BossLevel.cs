using UnityEngine;
using System.Collections;

public class BossLevel : MonoBehaviour, MessageReceiver {
	
	private GameObject door1;
	private GameObject door2;
	private GameObject cellTrigger;
	public GameObject keyPrefab;
	public GameObject boss;
	
	public AudioClip ac_door;
	
	void Start(){
		door1 = GameObject.Find("door1");
		door2 = GameObject.Find("door2");
		cellTrigger = GameObject.Find("cellTrigger");
		boss = GameObject.Find("Boss");
		
		//geringere Gravitation, damit Boss leichter die Schräge hoch kann
		Physics.gravity = new Vector3(0.0f, -4.0f, 0.0f);
	}
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			//Das 1. Gespräch, mit dem blauen Einhorn, ist vorrüber
			case "dialog1":
				//Tür 1 öffnen
				door1.collider.enabled = false;
				door1.renderer.enabled = false;
				//Geräusch
				AudioSource.PlayClipAtPoint(ac_door, door1.collider.bounds.center);
				return true;
			//Spieler hat den Raum betreten
			case "roomEntered":
				//Tür 1 schließen
				door1.collider.enabled = true;
				door1.renderer.enabled = true;
				//Geräusch
				AudioSource.PlayClipAtPoint(ac_door, door1.collider.bounds.center);
				return true;
			//Das 2. Gespräch, mit dem pinken Einhorn, ist vorrüber
			case "dialog2":
				//Boss kann nun engaged werden
				boss.collider.enabled = true;
				return true;
			//Der Spieler hat den Schlüssel aufgehoben
			case "keyPickup":
				//Tür von Gefängniszelle öffnet sich wenn der Spieler näher kommt
				cellTrigger.collider.enabled = true;
				return true;
			//Der Spieler erreicht mit dem Schlüssel die Zellentür
			case "cellOpened":
				//Tür 2 öffnen
				door2.renderer.enabled = false;
				door2.collider.enabled = false;
				AudioSource.PlayClipAtPoint(ac_door, door2.collider.bounds.center);
				//TODO Ende-Dialog und Spiel beenden
				return true;
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
			default:
				return false;
		}
	}
	
}
