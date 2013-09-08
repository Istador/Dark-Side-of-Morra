using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
	
	public string[] text;
	public double displayTime = 10.0;
	public bool autoSkip = false;
	
	
	private int i = 0;
	private bool started = false;
	private bool finished = false;
	private double startTime;
	private PlayerController pc;
	
	private static int height = 30;
	
	
	/// <summary>
	/// Methode die vor dem Dialog ausgeführt wird
	/// </summary>
	public System.Action<GameObject> preDialog {get; set;}
	
	/// <summary>
	/// Methode die nach dem Dialog ausgeführt wird
	/// </summary>
	public System.Action<GameObject> postDialog {get; set;}
	
	
	void Start(){
		//Player referenz holen
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	
	
	void Update(){
		if(started && !finished){
			//aktueller Text lange genug angezeigt
			//oder der Spieler überspringt es
			if(
				   ( autoSkip &&( (Time.time - startTime) > displayTime ) )
				|| Input.GetButtonDown("Fire1")
			){
				NextText();
			}
		}
	}
	
	
	
	void OnTriggerEnter(Collider hit){
		if(!started && hit.gameObject.tag == "Player"){
			//Startzeitpunkt merken
			started = true;
			startTime = Time.time;
			
			//trigger ausschalten
			collider.enabled = false;
			
			//Spieler anhalten
			pc.godMode = true;
			pc.CanMove = false;
			pc.CanShoot = false;
			
			if(preDialog != null)
				preDialog(gameObject);
		}
	}
	
	
	
	void OnGUI(){
		if(started && !finished){
			int width = (int) ((double)Screen.width * 5.0 / 6.0 ); //5/6 Bildschirmbreite
			int left = Screen.width / 2 - width/2;
			//int top = Screen.height / 2 - height/2; //mittig
			int top = Screen.height - 20 - height; //unten
			
			Rect border = new Rect(left, top, width, height);
			Rect box = new Rect(left+2, top+2, width-4, height-4);
			Rect content = new Rect(left+4, top+4, width-8, height-8);
			Rect weiter = new Rect(left+width-4-50, top+4, 50, height-8);
			
			DrawRectangle(border, Color.grey);
			DrawRectangle(box, Color.black);
			DrawText(content, text[i], Color.white);
			
			if(GUI.Button(weiter, "Weiter")){
				NextText();
			}
		}
	}
	
	
	
	private void NextText(){
		//nächster Text
		i++;
		//Zeit zurücksetzen
		startTime = Time.time;
		
		//Fertig mit dem Text?
		if(i >= text.Length){
			Finish();
		}
	}
	
	
	private void Finish(){
		finished = true;
		pc.godMode = false;
		pc.CanMove = true;
		pc.CanShoot = true;
		
		if(postDialog != null)
			postDialog(gameObject);
	}
	
	
	private void DrawText(Rect pos, string text, Color c){
		Color tmp = GUI.skin.label.normal.textColor;
		GUI.skin.label.normal.textColor = c;
		GUI.Label(pos, text);
		GUI.skin.label.normal.textColor = tmp;
	}
	
	
	private void DrawRectangle(Rect pos, Color c){
		Texture2D t = new Texture2D(1,1);
		t.SetPixel(0,0, c);
		t.wrapMode = TextureWrapMode.Repeat;
		t.Apply();
		
		Texture2D tmp = GUI.skin.box.normal.background;
		GUI.skin.box.normal.background = t;
		GUI.Box(pos, GUIContent.none);
		GUI.skin.box.normal.background = tmp;
	}
	
	
	private void MinMax(ref int val, int min, int max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	
	private void MinMax(ref float val, float min, float max){
		val = System.Math.Max(System.Math.Min(val, max), min);
	}
	
	
	
}
