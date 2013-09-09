﻿using UnityEngine;
using System.Collections;

/// 
/// Dieses Script erstellt den Lebensbalken vom Bossgegner
/// 
/// Features:
/// - Smooth Ein-/Ausblenden
/// - Mittig zentriert
/// - Änderung an der HP springen nicht, sondern Bewegen sich smooth
/// 
public class BossHealthBar : MonoBehaviour {
	
	
	
	/// <summary>
	/// Referenz auf den Boss von dem die HP angezeigt wird
	/// </summary>
	public Entity boss {get; private set;}
	
	
	
	/// <summary>
	/// Höhe des Lebensbalkens in absoluten Pixeln
	/// </summary>
	private static int height = 34;
	/// <summary>
	/// Abstand vom oberen Bildschirmrand zum Lebensbalken
	/// </summary>
	private static int top = 20;
	
	
	
	//konstante Farben
	
	/// <summary>
	/// 100% durchsichtig
	/// </summary>
	private static Color cTransp = new Color(0.0f, 0.0f, 0.0f, 0.0f);
	
	/// <summary>
	/// Grauer Rand des Lebensbalkens
	/// </summary>
	private static Color cBox = Color.grey;
	/// <summary>
	/// weißer Hintergrund, für Leben das der Boss bereits verloren hat
	/// </summary>
	private static Color cBar = Color.Lerp(Color.white, Color.black, 0.3f);
	
	/// <summary>
	/// Grünes Leben
	/// </summary>
	private static Color cGreen = Color.Lerp(Color.green, Color.black, 0.3f);
	/// <summary>
	/// Rotes Leben
	/// </summary>
	private static Color cRed = Color.Lerp(Color.red, Color.black, 0.3f);
	
	/// <summary>
	/// Ob der Lebensbalken mit sinkendem Leben von Grün ins Rot über gehen 
	/// soll (true), oder die ganze Zeit rot sein soll (false).
	/// </summary>
	public bool GreenToRed = false;
	
	
	
	void Start() {
		//Referenz des Bosses holen
		boss = (Entity) GameObject.FindObjectOfType(typeof(Entity));
	}
	
	
	
	//Variablen fürs Smooth Ein-/Ausblenden
	
	/// <summary>
	/// Smooth Ein-/Ausblenden Übergangsfaktor zwischen 0.0 und 1.0
	/// </summary>
	private float lerp = 0.0f;
	/// <summary>
	/// Ob der Lebensbalken gerade dabei ist eingeblendet zu werden
	/// </summary>
	private bool rising = false;
	/// <summary>
	/// Ob der Lebensbalken gerade dabei ist ausgeblendet zu werden
	/// </summary>
	private bool sinking = false;
	
	
	
	/// <summary>
	/// In festen Zeitintervallen das Ein-/Ausblenden umsetzen
	/// </summary>
	void FixedUpdate(){
		//wenn einblenden
		if(rising) lerp += 0.005f;
		//wenn ausblenden
		else if(sinking) lerp -= 0.01f;
		
		//Beschränken auf Wertebereich
		MinMax(ref lerp, 0.0f, 1.0f);
		
		//ein-/ausblenden wieder ausschalten wenn fertig
		if(lerp >= 1.0f) rising = false;
		if(lerp <= 0.0f) sinking = false;
	}
	
	
	
	/// <summary>
	/// Lebensbalken einblenden
	/// </summary>
	public void Show(){
		sinking = false;
		rising = true;
	}
	
	
	
	/// <summary>
	/// Lebensbalken ausblenden
	/// </summary>
	public void Hide(){
		sinking = true;
		rising = false;
	}
	
	
	
	//Variablen fürs Smoothe Verringern der HP
	
	private float hp_lerp = 1.0f;
	private float last_width = 0.0f;
	
	
	
	//GUI-Zeichnen
	void OnGUI(){
		
		//Lebensbalkenbreite abhängig von der Bildschirmbreite berechnen
		int width = (int) ((double)Screen.width / 4.0 * 3.0); //3/4 Bildschirmbreite
		int left = Screen.width / 2 - width/2; //Mittig zentriert
		
		//Positionen/Breiten/Höhen der Rechtecke berechnen
		Rect ra = new Rect(left, top, width, height);
		Rect rb = new Rect(left + 2, top + 2, width - 4, height - 4);
		Rect rc = rb;
		
		
		//Smoothes HP verringern
		
		//Breite des aktuellen HP-Wertes
		float new_width = Mathf.RoundToInt((float)rc.width * boss.healthFactor);
		
		//keine HP-Änderung
		if(new_width >= last_width){
			rc.width = new_width;
			last_width = new_width;
			hp_lerp = 1.0f;
		}
		//HP hat sich verändert
		else{
			hp_lerp -= 0.025f;
			MinMax(ref hp_lerp, 0.0f, 1.0f);
			rc.width = Mathf.Lerp(new_width, last_width, hp_lerp);
			if(hp_lerp <= 0.0f) last_width = new_width;
		}
		
		
		//Smoothes Ein-/Ausblenden des Lebensbalkens
		Color ca = Color.Lerp(cTransp, cBox, lerp);
		Color cb = Color.Lerp(cTransp, cBar, lerp);
		//Übergang von Grün zu Rot
		Color cHP = Color.Lerp(cRed, cGreen, GreenToRed ? boss.healthFactor : 0.0f);
		Color cc = Color.Lerp(cTransp, cHP, lerp);
		
		
		//Zeichnen
		DrawRectangle(ra, ca);
		DrawRectangle(rb, cb);
		if(boss.healthFactor > 0.0f)
			DrawRectangle(rc, cc);
		
	}
	
	
	
	/// <summary>
	/// Zeichnet ein farbiges Rechteck auf die GUI
	/// </summary>
	/// <param name='position'>
	/// Position und Ausmaße des Rechteckes
	/// </param>
	/// <param name='c'>
	/// Füll-Farbe des Rechteckes
	/// </param>
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
	private void MinMax(ref int val, int min, int max){
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
	private void MinMax(ref float val, float min, float max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	
	
}
