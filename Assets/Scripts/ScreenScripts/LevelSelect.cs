using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;

	void Start ()
	{
		SaveLoad.Load();
	}

	void OnGUI ()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));

		GUI.Box(new Rect(0, 0, 800, 600), " ");

		if (GUI.Button(new Rect( (Screen.width / 2) - (buttonWidth / 2),30,buttonWidth,buttonHeight),"Level 1"))
		{
			Application.LoadLevel(3);
		}

		if (SaveData.levelReached >= 4)
		{
			if (GUI.Button(new Rect( (Screen.width / 2) - (buttonWidth / 2),65,buttonWidth,buttonHeight),"Level 2"))
			{
				Application.LoadLevel(4);
			}
		}
			
		

		GUI.EndGroup();
	}
}
