using UnityEngine;
using System.Collections;

/// 
/// Zentrales Skript für das gesamte Boss-Level, um die jeweiligen
/// Trigger für den Story-Ablauf ein- und auszuschalten.
/// 
public class BossLevel : MonoBehaviour, MessageReceiver {
	
	
	
	// Referenzen auf GameObjekte im Level
	
	/// <summary>
	/// Die 1. Tür, welche anfangs geschlossen ist, und nach dem Gespräch mit
	/// dem blauem Einhorn aufgeht, und sich wieder schließt wenn der Spieler
	/// den Raum der Spinne betritt.
	/// </summary>
	private GameObject door1;
	
	/// <summary>
	/// Die 2. Tür, welche geschlossen bleibt bis der Spieler sie mit dem
	/// Schlüssel öffnet um das Pinke Einhorn aus dem Käfig zu befreien.
	/// </summary>
	private GameObject door2;
	
	/// <summary>
	/// Der Trigger der das Öffnen von Tür 2 mittels Schlüssel auslöst.
	/// </summary>
	private GameObject cellTrigger;
	
	/// <summary>
	/// Prefab des Schlüssels, den der Boss fallen lässt.
	/// </summary>
	public GameObject keyPrefab;
	
	/// <summary>
	/// Nicht der Boss, sondern die Referenz auf den Trigger, der wenn er
	/// betätigt wird das Ausschlüpfen der Spinne startet.
	/// </summary>
	public GameObject boss;
	
	
	
	/// <summary>
	/// Geräusch einer öffnenden/schließenden Tür
	/// </summary>
	public AudioClip ac_door;
	
	
	
	/// 
	/// Einmal zu Beginn die Referenzen Laden.
	/// 
	void Start(){
		door1 = GameObject.Find("door1");
		door2 = GameObject.Find("door2");
		cellTrigger = GameObject.Find("cellTrigger");
		boss = GameObject.Find("Boss");
	}
	
	
	
	// Methode um eingehende Nachrichten zu verarbeiten
	public bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			
			//Das 1. Gespräch, mit dem blauen Einhorn, ist vorrüber
			case "dialog1":
				//Tür 1 öffnen
				door1.collider.enabled = false;
				door1.renderer.enabled = false;
				//Tür-Geräusch
				AudioSource.PlayClipAtPoint(ac_door, door1.collider.bounds.center);
				return true;
			
			//Spieler hat den Raum betreten
			case "roomEntered":
				//Tür 1 schließen
				door1.collider.enabled = true;
				door1.renderer.enabled = true;
				//Tür-Geräusch
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
				//Tür 1 öffnen
				door1.renderer.enabled = false;
				door1.collider.enabled = false;
				//Tür-Geräusch
				AudioSource.PlayClipAtPoint(ac_door, door1.collider.bounds.center);
				//Tür 2 öffnen
				door2.renderer.enabled = false;
				door2.collider.enabled = false;
				//Tür-Geräusch
				AudioSource.PlayClipAtPoint(ac_door, door2.collider.bounds.center);
				return true;
			
			//Das 3. Gespräch, mit dem pinken Einhorn, ist vorrüber
			case "dialog3":
				//Spiel speichern
				if(SaveData.levelReached == Application.loadedLevel){
					SaveData.levelReached++;
				}
				SaveLoad.Save();
			
				//Credits laden
				MessageDispatcher.I.EmptyQueue();
				Application.LoadLevel(2);	
			
				return true;
			
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
			default:
				return false;
		}
	}
	
	
	
}
