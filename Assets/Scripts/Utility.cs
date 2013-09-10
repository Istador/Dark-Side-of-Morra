using UnityEngine;
using System.Collections;

public static class Utility {
	
	
	
	/// <summary>
	/// Zeichnet ein farbiges Rechteck auf die GUI
	/// </summary>
	/// <param name='position'>
	/// Position und Ausmaße des Rechteckes
	/// </param>
	/// <param name='c'>
	/// Füll-Farbe des Rechteckes
	/// </param>
	public static void DrawRectangle(Rect position, Color c){
		Texture2D t = new Texture2D(1,1);
		t.SetPixel(0,0, c);
		t.wrapMode = TextureWrapMode.Repeat;
		t.Apply();
		
		Texture2D tmp = GUI.skin.box.normal.background;
		GUI.skin.box.normal.background = t;
		GUI.Box(position, GUIContent.none);
		GUI.skin.box.normal.background = tmp;
	}
	
	
	
	/// <summary>
	/// Zeichnet einen farbigen Text auf die GUI
	/// </summary>
	/// <param name='pos'>
	/// Position des Textes.
	/// </param>
	/// <param name='text'>
	/// Der Text der gezeichnet werden soll.
	/// </param>
	/// <param name='c'>
	/// Die Farbe des Textes.
	/// </param>
	public static void DrawText(Rect pos, string text, Color c){
		Color tmp = GUI.skin.label.normal.textColor;
		GUI.skin.label.normal.textColor = c;
		GUI.Label(pos, text);
		GUI.skin.label.normal.textColor = tmp;
	}
	
	
	
	/// <summary>
	/// Beschränkt einen Wert auf einen bestimmten Wertebereich
	/// </summary>
	/// <param name='val'>
	/// Wert der eingegrenzt wird
	/// </param>
	/// <param name='min'>
	/// Minimaler Wert
	/// </param>
	/// <param name='max'>
	/// Maximaler Wert
	/// </param>
	public static void MinMax(ref int val, int min, int max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	
	
	/// <summary>
	/// Beschränkt einen Wert auf einen bestimmten Wertebereich
	/// </summary>
	/// <param name='val'>
	/// Wert der eingegrenzt wird
	/// </param>
	/// <param name='min'>
	/// Minimaler Wert
	/// </param>
	/// <param name='max'>
	/// Maximaler Wert
	/// </param>
	public static void MinMax(ref float val, float min, float max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	
	
}
