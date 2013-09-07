using UnityEngine;
using System.Collections;

public class BossLevel : MonoBehaviour, MessageReceiver {
	
	private GameObject door1;
	private GameObject door2;
	private GameObject cellTrigger;
	public GameObject keyPrefab;
	public GameObject boss;
	
	void Start(){
		door1 = GameObject.Find("door1");
		door2 = GameObject.Find("door2");
		cellTrigger = GameObject.Find("cellTrigger");
		boss = GameObject.Find("Boss");
	}
	
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			//1. Gespräch mit blauem Einhorn vorrüber
			case "dialog1":
				door1.collider.enabled = false;
				door1.renderer.enabled = false;
				//TODO Soundgeräusch für öffnende Tür
				return true;
			case "roomEntered":
				door1.collider.enabled = true;
				door1.renderer.enabled = true;
				
				//TODO Soundgeräusch für schließende Tür
				return true;
			case "dialog2":
				boss.collider.enabled = true;
				return true;
			case "keyPickup":
				cellTrigger.collider.enabled = true;
				return true;
			case "cellOpened":
				door2.renderer.enabled = false;
				door2.collider.enabled = false;
				//TODO Soundgeräusch für öffnende Tür
				//TODO Ende-Dialog und Spiel beenden
				return true;
			default:
				//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
				return false;
		}
	}
	
}
