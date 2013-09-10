using UnityEngine;
using System.Collections;

public class LevelSelect : SceneMenu {
	
	
	
	/// <summary>
	/// Array aller Szenen die geladen werden können
	/// </summary>
	private object[,] _scenes = new object[,] {
		{3, "Level 1"},
		{4, "Level 2"},
		{5, "Boss"},
		{0, "Zum Hauptmenü"}
	};
	
	
	
	/// <summary>
	/// Methode der Oberklasse um auf die Szenen zuzugreifen
	/// </summary>
	protected override object[,] scenes {get{return _scenes;}}
	
	
	
}
