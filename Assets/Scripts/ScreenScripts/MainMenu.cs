using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;

	// Use this for initialization
	void OnGUI ()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));
        
        GUI.Box(new Rect(0, 0, 800, 600), " ");

		if (GUI.Button(new Rect( (Screen.width / 2) - (buttonWidth / 2),30,buttonWidth,buttonHeight),"Level Select"))
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