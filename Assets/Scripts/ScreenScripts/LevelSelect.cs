using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;
	public Texture2D hintergrundLevel;
	
	void Start ()
	{
		SaveLoad.Load();
	}

	void OnGUI ()
	{
		GUI.BeginGroup(new Rect( 300, 200, 800, 600), new GUIContent(hintergrundLevel));

	

		if (GUI.Button(new Rect( 200,30,buttonWidth,buttonHeight),"Level 1"))
		{
			Application.LoadLevel(3);
		}
		if (GUI.Button(new Rect (200, 100, buttonWidth,buttonHeight),"Main menu"))
		{
			Application.LoadLevel("MainMenu");
		}
		if (SaveData.levelReached >= 4)
		{
			if (GUI.Button(new Rect(200,65,buttonWidth,buttonHeight),"Level 2"))
			{
				Application.LoadLevel(4);
			}
		}
			
		

		GUI.EndGroup();
	}
}
