using UnityEngine;
using System.Collections;

//TODO: Credits

public class Credits : SceneMenu
{
	/// <summary>
	/// Array aller Buttons die angezeigt werden sollen
	/// </summary>
	private object[,] _scenes = new object[,]
	{
		{1, "Level auswählen", null},
		{2, "Credits", null},
		{0, "Spiel beenden", null}
	};

	/// <summary>
	/// Methode der Oberklasse um auf die Szenen zuzugreifen
	/// </summary>
	protected override object[,] scenes {get{return _scenes;}}
}
