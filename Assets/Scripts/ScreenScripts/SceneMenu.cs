using UnityEngine;
using System.Collections;
using System;

/// 
/// Spezialisierung der generellen Menu-Klasse um dessen Verwendung zu vereinfachen.
/// 
/// Diese Klasse geht davon aus, dass alle Buttons Übergänge zu anderen Szenen
/// sind, die mit Application.LoadLevel geladen werden wenn auf sie geklickt wird.
/// 
public abstract class SceneMenu : Menu {
	
	
	
	protected override void Start (){
		base.Start();
		
		//Laden des gespeicherten Spielzustandes
		SaveLoad.Load();
		
		CreateButtons();
	}
	
	
	
	/// <summary>
	/// Alle Szenen für die Buttons angezeigt werden sollen.
	/// Eine Szene wird nicht angezeigt, wenn dessen Szenen-ID noch nicht vom 
	/// Spieler erreicht wurde.
	/// 
	/// Jede Array-Zeile ist ein Button.
	/// Jede Array-Spalte ist eine Button-Eigenschaft
	/// 
	/// Struktur: 
	/// 0: int, ID der Szene die geladen werden soll
	/// 1: string, Beschriftung des Buttons für diese Szene
	/// (optional) 3: Action<int> action = (id) => {Alternativ zum Level-Laden eine beliebige andere Aktion}
	/// </summary>
	protected abstract object[,] scenes {get;}
	
	
	
	/// <summary>
	/// Array aller anzuzeigenden Buttons
	/// </summary>
	protected object[,] _buttons;
	
	
	
	/// <summary>
	/// Methode der Oberklasse um auf die Buttons zuzugreifen
	/// </summary>
	protected override object[,] buttons {get{
		//wenn sie noch nicht vorhanden sind, erstelle sie aus scenes
		if(_buttons == null) CreateButtons();
		return _buttons;
	}}
	
	
	
	/// <summary>
	/// Erstellen der Button-Struktur für die Menu-Oberklasse, aus der Scenes-Struktur
	/// von den konkreten Unterklassen.
	/// </summary>
	protected void CreateButtons(){
		//Bedingung um den Button anzuzeigen ist, dass das Level bereits erreicht wurde
		Func<int,bool> pre = (id) => SaveData.levelReached >= id ;
		
		//wenn es erreicht wurde, soll das Level auch geladen werden bei einem Button-Klick
		Action<int> action = (id) => Application.LoadLevel(id);
		
		//Anzahl Szenen
		int n = scenes.GetLength(0);
		
		//Array-Struktur erstellen
		_buttons = new object[n,5];
		
		//für alle Szenen
		for(int i=0; i<n; i++){
			//ID zuweisen
			_buttons[i,0] = scenes[i,0];
			//Beschriftung zuweisen
			_buttons[i,1] = scenes[i,1];
			//Vorbedingung zuweisen
			_buttons[i,2] = pre;
			//keine Nachbedingung
			_buttons[i,3] = null;
			//Ist von der Unterklasse eine andere Aktion angegeben?
			if(scenes.GetLength(1) == 3 && scenes[i,2] != null)
				//Benutzerdefinierte-Aktion zuweisen
				_buttons[i,4] = scenes[i,2];
			else
				//Aktion: Level laden
				_buttons[i,4] = action;
		}
	}
	
	
	
}
