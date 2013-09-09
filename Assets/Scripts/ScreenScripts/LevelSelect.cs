using UnityEngine;
using System.Collections;

public class LevelSelect : SceneMenu {
	
	//Array aller Buttons
	private object[,] _scenes = new object[,] {
		{3, "Level 1"},
		{4, "Level 2"},
		{5, "Boss"},
		{0, "Zum Hauptmenü"}
	};
	
	protected override object[,] scenes {get{return _scenes;}}
	
}
