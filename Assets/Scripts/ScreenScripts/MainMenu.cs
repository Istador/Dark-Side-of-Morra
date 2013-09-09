using UnityEngine;
using System;
using System.Collections;

public class MainMenu: SceneMenu {
	
	//Array aller Buttons
	private object[,] _scenes = new object[,] {
		{1, "Level auswählen", null},
		{2, "Credits", null},
		{0, "Spiel Beenden", (Action<int>)((int id)=>{
			Application.Quit(); //nicht innerhalb des Unity-Editors möglich
			UnityEditor.EditorApplication.isPlaying = false; //beendet das Spielen des Editors
			//UnityEditor.EditorApplication.Exit(0); //Beendet dem Editor ohne speichern !!!
		}) }
	};
	
	protected override object[,] scenes {get{return _scenes;}}
	
}
