using UnityEngine;
using System.Collections;

public class MainMenu: MonoBehaviour {

	public int buttonWidth = 200;
	public int buttonHeight = 50;
	public Texture2D button;
	public Texture2D hintergrund;
	public int a;
	public int b;
	void Start ()
	{
		SaveLoad.Load();
	}
	
	void OnGUI ()
	{
		GUI.BeginGroup(new Rect(a, b, 800, 600), new GUIContent(hintergrund));

		//GUI.Box(new Rect(0, 0, 800, 600), "Main Menu ");

		if (GUI.Button(new Rect( 200,200,buttonWidth,buttonHeight),new GUIContent("Level Select", button)))
		{
			Application.LoadLevel("LevelSelect");
		}
		if (GUI.Button(new Rect( 200,250,buttonWidth,buttonHeight),"Credits"))
		{
			Application.LoadLevel("Credits");
		}
	
		GUI.EndGroup();
	}
}
