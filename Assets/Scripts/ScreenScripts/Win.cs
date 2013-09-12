using UnityEngine;
using System.Collections;

public class Win : SceneMenu
{
	/// <summary>
	/// Array aller Buttons die angezeigt werden sollen
	/// </summary>
	private object[,] _scenes = new object[,]
	{
		{3, "Nochmal", null},
		{2, "Credits", null},
		{0, "Zum Hauptmenü"}
	};
	
	/// <summary>
	/// Methode der Oberklasse um auf die Szenen zuzugreifen
	/// </summary>
	protected override object[,] scenes {get{return _scenes;}}
}