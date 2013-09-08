using UnityEngine;
using System.Collections;

public class BossHealthBar : MonoBehaviour {
	
	public Entity boss {get; private set;}
	
	private int height = 34;
	private int top = 20;
	
	private Rect rBox;
	private Rect rBar;
	
	private Color cTransp = new Color(0.0f, 0.0f, 0.0f, 0.0f);
	private Color cBox = Color.grey;
	private Color cBar = Color.Lerp(Color.white, Color.black, 0.3f);
	private Color cHP = Color.Lerp(Color.red, Color.black, 0.3f);
	
	
	
	
	void Start() {
		boss = (Entity) GameObject.FindObjectOfType(typeof(Entity));
	}
	
	
	
	//Smooth show/hide
	private float lerp = 0.0f;
	private bool rising = false;
	private bool sinking = false;
	void FixedUpdate(){
		if(rising) lerp += 0.005f;
		else if(sinking) lerp -= 0.01f;
		MinMax(ref lerp, 0.0f, 1.0f);
		if(lerp >= 1.0f) rising = false;
		if(lerp <= 0.0f) sinking = false;
	}
	
	public void Show(){
		sinking = false;
		rising = true;
	}
	
	public void Hide(){
		sinking = true;
		rising = false;
	}
	
	
	private float hp_lerp = 1.0f;
	private float last_width = 0.0f;
	
	void OnGUI(){
		//Breite abhängig von Bildschirmbreite
		int width = (int) ((double)Screen.width / 4.0 * 3.0); //3/4 Bildschirmbreite
		int left = Screen.width / 2 - width/2;
		
		//Positionen/Breiten/Höhen berechnen
		Rect ra = new Rect(left, top, width, height);
		Rect rb = new Rect(left + 2, top + 2, width - 4, height - 4);
		Rect rc = rb;
		
		//Smooth HP verringern
		float new_width = Mathf.RoundToInt((float)rc.width * boss.healthFactor);
		if(new_width >= last_width){
			rc.width = new_width;
			last_width = new_width;
			hp_lerp = 1.0f;
		}
		else{
			hp_lerp -= 0.025f;
			MinMax(ref hp_lerp, 0.0f, 1.0f);
			rc.width = Mathf.Lerp(new_width, last_width, hp_lerp);
			if(hp_lerp <= 0.0f) last_width = new_width;
		}
		
		//Smooth ein/ausblenden
		Color ca = Color.Lerp(cTransp, cBox, lerp);
		Color cb = Color.Lerp(cTransp, cBar, lerp);
		Color cc = Color.Lerp(cTransp, cHP, lerp);
		
		//Zeichnen
		DrawRectangle(ra, ca);
		DrawRectangle(rb, cb);
		if(boss.healthFactor > 0.0f)
			DrawRectangle(rc, cc);
		
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
	
	
	
	private void MinMax(ref int val, int min, int max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	private void MinMax(ref float val, float min, float max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
}
