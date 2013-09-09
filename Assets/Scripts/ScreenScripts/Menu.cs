using UnityEngine;
using System.Collections;

public abstract class Menu : MonoBehaviour {
	
	public int buttonWidth = 300;
	public int buttonHeight = 50;
	
	private float btnWidth;
	private float btnHeight;
	
	public Texture2D hintergrund;
	
	
	//Array aller Buttons
	//Struktur: 
	// 0: int id
	// 1: string name
	// 2: Func<int,bool> x = (id) => {precondition to draw} 
	// 3: Func<int,bool> x = (id) => {postcondition to fire}
	// 4: Action<int> x = (id) => {action}
	protected abstract object[,] buttons {get;}

	
	
	
	void Start ()
	{
		SaveLoad.Load();
	}
	
	
	
	//Button zeichnen
	private bool Button(int i){
		return GUI.Button(new Rect(0, (btnHeight+10.0f)*i, btnWidth, btnHeight), (string)buttons[i,1]);
	}
	
	
	
	protected virtual void OnGUI()
	{
		//Bild schwarz ausfüllen
		this.DrawRectangle(new Rect(0,0,Screen.width, Screen.height), Color.black);
		
		
		//Berechnung der Button höhe und breite abhängig von der Bildschirmgröße, und dem Aspektratio
		
		float factor1 = (float)hintergrund.width / (float)hintergrund.height;
		float factor2 = (float)Screen.width / (float)Screen.height;
		
		if(factor1 <= factor2){
			btnHeight = (float)Screen.height / (float)hintergrund.height * (float)buttonHeight;
			btnWidth = btnHeight / (float)buttonHeight * (float)buttonWidth;
		} else {
			btnWidth = (float)Screen.width / (float)hintergrund.width * (float)buttonWidth;
			btnHeight = btnWidth / (float)buttonWidth * (float)buttonHeight;
			//Debug.Log( btnWidth + " * " + (float)hintergrundLevel.height + " / " + (float)buttonHeight + " = " + btnHeight );
		}
		
		
		//Hintergrundgrafik zeichnen
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height), hintergrund, ScaleMode.ScaleToFit);
		
		//Zentrierte Gruppe
		float centerLeft = (float)Screen.width/2.0f - btnWidth / 2.0f;
		float centerTop =  (float)Screen.height/2.0f - (btnHeight+10.0f) * buttons.GetLength(0) / 2.0f;
		
		GUI.BeginGroup(new Rect(centerLeft, centerTop, btnWidth, (btnHeight+10.0f) * buttons.GetLength(0) ));
		
		//für alle Buttons
		for(int i = 0; i < buttons.GetLength(0); i++){
			int id = (int)buttons[i,0];
			System.Func<int, bool> pre = (System.Func<int, bool>) buttons[i,2];
			System.Func<int, bool> post = (System.Func<int, bool>) buttons[i,3];
			System.Action<int> action = (System.Action<int>) buttons[i,4];
			
			//Wenn bereits erreicht anzeigen
			if( ( pre==null || pre(id) ) && Button(i) && ( post==null || post(id) ) ){
				//wenn angeklickt
				action(id);
			}
		}
		
		GUI.EndGroup();
	}
	
	
	
	private void DrawRectangle(Rect position, Color c){
		Texture2D t = new Texture2D(1,1);
		t.SetPixel(0,0, c);
		t.wrapMode = TextureWrapMode.Repeat;
		t.Apply();
		
		Texture2D tmp = GUI.skin.box.normal.background;
		GUI.skin.box.normal.background = t;
		GUI.Box(position, GUIContent.none);
		GUI.skin.box.normal.background = tmp;
	}
}
