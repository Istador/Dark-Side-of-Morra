using UnityEngine;
using System;
using System.Collections;

/// 
/// Diese Abstrakte-Klasse dient dazu ein Menü auf der GUI anzuzeigen,
/// das aus Buttons besteht, die horizontal und vertikal Zentriert auf
/// dem Bildschirm angezeigt werden. Die Buttons werden zusammen mit dem 
/// Hintergrundbild entsprechend der Bildschirmgröße gestreckt.
/// 
public abstract class Menu : MonoBehaviour {
	
	
	
	/// <summary>
	/// Das Hintergrundbild für dieses Menü.
	/// Es wird als Referenz für die Bildschirmauflösung benutzt.
	/// </summary>
	public Texture2D hintergrund;
	
	
	
	/// <summary>
	/// Faktor zw. Breite und Höhe des Hintergrundbildes
	/// </summary>
	float factor1;
	
	
	/// <summary>
	/// Breite eines Buttons in der Referenzauflösung
	/// </summary>
	public int buttonWidth = 300;
	
	
	
	/// <summary>
	/// Höhe eines Buttons in der Referenzauflösung
	/// </summary>
	public int buttonHeight = 50;
	
	
	
	/// <summary>
	/// Die, aufgrund der Bildschirmgröße, berechnete Buttonbreite in diesem Frame
	/// </summary>
	private float btnWidth;
	
	
	
	/// <summary>
	/// Die, aufgrund der Bildschirmgröße, berechnete Buttonhöhe in diesem Frame
	/// </summary>
	private float btnHeight;
	
	
	
	/// <summary>
	/// Array aller anzuzeigenden Buttons
	/// 
	/// Jede Array-Zeile ist ein Button.
	/// Jede Array-Spalte ist eine Button-Eigenschaft
	/// 
	/// Spalten-Struktur:
	/// 0: int, ID des Buttons
	/// 1: string, Beschriftung des Buttons
	/// 2: Func<int,bool> pre = (id) => {Vorbedingung um den Button zu zeichnen} 
	/// 3: Func<int,bool> post = (id) => {Nachbedingung die für die Aktion erfüllt sein muss beim Button-Click}
	/// 4: Action<int> action = (id) => {Aktion die ein Klick auf den Button auslöst}
	/// </summary>
	protected abstract object[,] buttons {get;}
	
	
	
	protected virtual void Start(){
		//Verhältnis aus Breite und Höhe des Hintergrundbildes berechnen
		factor1 = (float)hintergrund.width / (float)hintergrund.height;
	}
	
	
	
	/// <summary>
	/// Methode um einen der Buttons zu zeichnen
	/// </summary>
	/// <param name='i'>
	/// Index des zu zeichnenden Buttons
	/// </param>
	/// <returns>
	/// Wahrheitswert ob der Button gedrückt wurde
	/// </returns>
	private bool Button(int i){
		return GUI.Button(new Rect(0, (btnHeight+10.0f)*i, btnWidth, btnHeight), (string)buttons[i,1]);
	}
	
	
	
	//Zeichnen der GUI
	protected virtual void OnGUI(){
		
		//Bildschirm schwarz ausfüllen
		Utility.DrawRectangle(new Rect(0,0,Screen.width, Screen.height), Color.black);
		
		
		
		//Berechnung der Button-Höhe und -Breite abhängig von der Bildschirmgröße, und dem Aspektratio
		
		//Verhältnis von Breite und Höhe des Bildschirmes
		float factor2 = (float)Screen.width / (float)Screen.height;
		
		//Der Verhältnis ist größer oder gleich wie beim Referenzbildschirm
		if(factor1 <= factor2){
			//Berechne die Button-Höhe aus der Referenzhöhe und der aktuellen Bildschirmhöhe
			btnHeight = (float)Screen.height / (float)hintergrund.height * (float)buttonHeight;
			//Berechne die Button-Breite ausgehend von der berechneten Höhe
			btnWidth = btnHeight / (float)buttonHeight * (float)buttonWidth;
		} else {
			//Berechne die Button-Breite aus der Referenzbreite und der aktuellen Bildschirmbreite
			btnWidth = (float)Screen.width / (float)hintergrund.width * (float)buttonWidth;
			//Berechne die Button-Höhe ausgehend von der berechneten Breite
			btnHeight = btnWidth / (float)buttonWidth * (float)buttonHeight;
		}
		
		
		
		//Hintergrundgrafik zeichnen (Skalierung übernimmt Unity)
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height), hintergrund, ScaleMode.ScaleToFit);
		
		
		
		//Zentrierte Gruppe von Buttons erstellen, so dass die Buttons anhand 
		//des Lokalen-Koordinatensystem dieser Gruppe platziert werden
		float centerLeft = (float)Screen.width/2.0f - btnWidth / 2.0f;
		float centerTop =  (float)Screen.height/2.0f - (btnHeight+10.0f) * buttons.GetLength(0) / 2.0f;
		GUI.BeginGroup(new Rect(centerLeft, centerTop, btnWidth, (btnHeight+10.0f) * buttons.GetLength(0) ));
		
		
		
		//für alle Buttons
		for(int i = 0; i < buttons.GetLength(0); i++){
			//ID des Buttons
			int id = (int) buttons[i,0];
			//Vorbedingung
			Func<int, bool> pre = (Func<int, bool>) buttons[i,2];
			//Nachbedingung
			Func<int, bool> post = (Func<int, bool>) buttons[i,3];
			//Aktion
			Action<int> action = (Action<int>) buttons[i,4];
			
			if( 
				// Wenn keine Vorbedingung vorhanden ist oder sie erfüllt ist
				   ( pre==null || pre(id) ) 
				// Zeige den Button an. wenn der Button gedrückt wurde
				&& Button(i)
				/// Wenn keine Nachbedingung besteht oder sie erfüllt ist
				&& ( post==null || post(id) )
				// und wenn eine Aktion vorhanden ist
				&& (action!=null)
			){
				//führe die Aktion aus
				action(id);
			}
		}
		
		//GUI-Gruppe beenden
		GUI.EndGroup();
	}
	
	
	
}
