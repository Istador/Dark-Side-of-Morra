using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;
	public Texture2D button;

	void OnGUI ()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));

		GUI.Box(new Rect(0, 0, 800, 600), "Main Menu ");

		if (GUI.Button(new Rect( (Screen.width / 2) - (buttonWidth / 2),30,buttonWidth,buttonHeight),new GUIContent("Level Select", button)))
		{
			Application.LoadLevel("LevelSelect");
		}
		if (GUI.Button(new Rect( (Screen.width / 2) - (buttonWidth / 2),65,buttonWidth,buttonHeight),"Credits"))
		{
			Application.LoadLevel("Credits");
		}

		GUI.EndGroup();
	}
}
