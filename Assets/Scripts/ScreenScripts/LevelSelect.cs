using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;
	public Texture2D hintergrundLevel;
	public int width;
	public int height;
	
	void Start ()
	{
		SaveLoad.Load();
	}

	void OnGUI ()
	{
		GUI.BeginGroup(new Rect( width, height, Screen.width, Screen.height), new GUIContent(hintergrundLevel));

	

		if (GUI.Button(new Rect( Screen.width/2 +100,Screen.height/2,buttonWidth,buttonHeight),"Level 1"))
		{
			Application.LoadLevel(3);
		}
		if (GUI.Button(new Rect (Screen.width/2 +100, Screen.height/2-100, buttonWidth,buttonHeight),"Main menu"))
		{
			Application.LoadLevel("MainMenu");
		}
		if (SaveData.levelReached >= 4)
		{
			if (GUI.Button(new Rect(Screen.width/2 +100,Screen.height/2 - 65,buttonWidth,buttonHeight),"Level 2"))
			{
				Application.LoadLevel(4);
			}
		}
			
		

		GUI.EndGroup();
	}
}
