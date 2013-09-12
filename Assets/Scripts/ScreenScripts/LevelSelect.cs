using UnityEngine;
using System.Collections;

public class LevelSelect : SceneMenu {
	
	
	
	/// <summary>
	/// Array aller Szenen die geladen werden können
	/// </summary>
	private object[,] _scenes = new object[,] {
		{2, "Level 1"},
		{3, "Level 2"},
		{4, "Boss"},
		{0, "Zum Hauptmenü"}
	};
	
	
	
	/// <summary>
	/// Methode der Oberklasse um auf die Szenen zuzugreifen
	/// </summary>
	protected override object[,] scenes {get{return _scenes;}}
	
	
	
}
