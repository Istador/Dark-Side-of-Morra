using UnityEngine;
using System;
using System.Collections;

public class MainMenu: SceneMenu {
	
	
	
	/// <summary>
	/// Array aller Buttons die angezeigt werden sollen
	/// </summary>
	private object[,] _scenes = new object[,] {
		{1, "Level auswählen", null},
		{2, "Credits", null},
		{0, "Spiel beenden", (Action<int>)(
			(int id) => {
				
				//Beendet das Spiel
				//innerhalb des Unity-Editors ohne Wirkung
				Application.Quit();
				
				//beendet das Spielen innerhalb des Unity-Editors
				//TODO: Auskommentieren für Release-Build !
				//UnityEditor.EditorApplication.isPlaying = false;
			}
		)}
	};
	
	
	
	/// <summary>
	/// Methode der Oberklasse um auf die Szenen zuzugreifen
	/// </summary>
	protected override object[,] scenes {get{return _scenes;}}
	
	
	
	protected override void Start (){
		base.Start();
		
		//Quit nicht im Editor oder WebPlayer zeigen
		_buttons[2,2] = (Func<int,bool>)((id) => !Application.isWebPlayer && !Application.isEditor) ;
	}
	
}
