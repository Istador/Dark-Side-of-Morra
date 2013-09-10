using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Das Pausen-Menü wird aktiviert wenn man im Spiel Escape drückt (oder 
/// vergleichbare Buttons auf anderen Plattformen).
/// 
/// Das Spiel wird angehalten, und der Spieler kann entweder das Spiel wieder
/// fortsetzen, oder das laufende Spiel beenden und zum Hauptmenü zurückkehren
/// </summary>
public class Pause : Menu {
	
	
	
	/// <summary>
	/// Array aller anzuzeigenden Buttons.
	/// Nicht hier initialisiert, weil die Instanz-Referenz benötigt wird.
	/// </summary>
	private object[,] _buttons;
	
	
	
	/// <summary>
	/// Methode der Oberklasse um auf die Buttons zuzugreifen
	/// </summary>
	protected override object[,] buttons {get{return _buttons;}}
	
	
	
	/// <summary>
	/// Alle Key-Codes die das Spiel pausieren können.
	/// Z.B. die Escape-Taste.
	/// </summary>
	private static HashSet<KeyCode> keys;
	
	
	
	/// <summary>
	/// Referenz auf den Spieler, um ihn deaktivieren zu können.
	/// Ansonsten bekommt er noch Befehle vom Spieler.
	/// </summary>
	private PlayerController pc;
	
	
	
	protected override void Start(){
		base.Start ();
		
		//Spiel fortsetzen falls noch pausiert
		Time.timeScale = 1.0f;
		paused = false;
		
		//Referenz auf Spieler laden
		pc = GetComponent<PlayerController>();
		
		//Button-Array erstellen
		_buttons = new object[,] {
			{1, "Spiel fortsetzen", null, null, (Action<int>)((int id)=>{ResumeGame();}) },
			{2, "Zum Hauptmenü",    null, null, (Action<int>)(
				(int id)=>{
					//Nachrichtenwarteschlange leeren
					MessageDispatcher.Instance.EmptyQueue();
					//Hauptmenü laden
					Application.LoadLevel(0);
					//Spiel fortsetzen
					ResumeGame();
				})
			}
		};
		
		//KeyCode Set erstellen wenn noch nicht vorhanden
		if(keys == null){
			keys = new HashSet<KeyCode>();
			
			keys.Add(KeyCode.Escape);
			keys.Add(KeyCode.Pause);
			keys.Add(KeyCode.Menu);
			keys.Add(KeyCode.Break);
		}
	}
	
	
	
	void Update() {
		//Für jeden Key-Code der das Spiel pausieren soll
		foreach(KeyCode kc in keys){
			//überorüfen ob er gedrückt wurde, und wenn ja
			if(Input.GetKeyDown(kc)){
				//prüfen ob das Spiel pausiert
				if(!paused) PauseGame();
				//oder fortgesetzt werden soll
				else ResumeGame();
				break;
			}
		}
	}
	
	
	
	/// <summary>
	/// Ist das Spiel jetzt gerade pausiert?
	/// </summary>
	private bool paused = false;
	
	
	
	/// <summary>
	/// Pausiere das Spiel
	/// </summary>
	private void PauseGame(){
		paused = true;
		
		//PlayerController deaktivieren
		pc.enabled = false;
		
		//Zeit anhalten
		Time.timeScale = 0.0f;
	}
	
	
	
	/// <summary>
	/// Setze das pausierte Spiel fort
	/// </summary>
	private void ResumeGame(){
		paused = false;
		
		//PlayerController aktivieren
		pc.enabled = true;
		
		//Zeit weiterlaufen lassen
		Time.timeScale = 1.0f;
	}
	
	
	
	//GUI zeichnen
	protected override void OnGUI(){
		//nur wenn das Spiel pausiert wurde den Pausen-Bildschirm zeichnen
		if(paused) base.OnGUI();
	}
	
	
	
}
