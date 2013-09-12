using UnityEngine;
using System.Collections;

/// 
/// Zentrales Skript für das gesamte Boss-Level, um die jeweiligen
/// Trigger für den Story-Ablauf ein- und auszuschalten.
/// 
public class BossLevel : GeneralObject {
	
	
	
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
	/// Nicht der Boss, sondern die Referenz auf den Trigger, der wenn er
	/// betätigt wird das Ausschlüpfen der Spinne startet.
	/// </summary>
	public GameObject boss;
	
	
	
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
	public override bool HandleMessage(Telegram msg){
		//Nachrichteneingang, je nach Nachricht etwas anderes tun
		switch(msg.message){
			
			//Das 1. Gespräch, mit dem blauen Einhorn, ist vorrüber
			case "dialog1":
				//Tür 1 öffnen
				door1.collider.enabled = false;
				door1.renderer.enabled = false;
				//Tür-Geräusch
				PlaySound("door", Posi(door1));
				return true;
			
			//Spieler hat den Raum betreten
			case "roomEntered":
				//Tür 1 schließen
				door1.collider.enabled = true;
				door1.renderer.enabled = true;
				//Tür-Geräusch
				PlaySound("door", Posi(door1));
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
				PlaySound("door", Posi(door1));
				//Tür 2 öffnen
				door2.renderer.enabled = false;
				door2.collider.enabled = false;
				//Tür-Geräusch
				PlaySound("door", Posi(door2));
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
				Application.LoadLevel(7);	
			
				return true;
			
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden
			default:
				return false;
		}
	}
	
	
	
}
