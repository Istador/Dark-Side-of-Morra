using UnityEngine;
using System.Collections;

/// 
/// Einfach wiederverwendbarer Dialog
/// 
/// Dialogsfortsetzung durch:
/// - Schießen
/// - Klick mit der Maus auf den Weiter-Button
/// - (optional) nach einer bestimmten Zeit
/// 
/// Benötigt zur Verwendung wird das String-Array text.
/// Jedes Array-Element ist ein Dialogsschritt.
/// 
/// Optional um Aktionen vor und nach dem Dialog ergänzbar
/// 
public class Dialog : MonoBehaviour {
	
	
	
	/// <summary>
	/// Die Dialogsbestandteile (Texte für die einzelnen Dialogsschritte)
	/// </summary>
	public string[] text;
	
	/// <summary>
	/// Anzeigedauer eines Dialogsschrittes bevor es automatisch zum nächsten geht
	/// </summary>
	public double displayTime = 10.0;
	
	/// <summary>
	/// Soll automatisch nach einer bestimmten Zeit 'displayTime' zum nächsten Dialogsschritt gegangen werden?
	/// </summary>
	public bool autoSkip = false;
	
	
	
	/// <summary>
	/// aktuelller Dialogsschrittsindex
	/// </summary>
	private int i = 0;
	/// <summary>
	/// Wurde der Dialog bereits begonnen?
	/// </summary>
	private bool started = false;
	/// <summary>
	/// Wurde der Dialog bereits beendet?
	/// </summary>
	private bool finished = false;
	/// <summary>
	/// Wann wurde der aktuelle Dialogsschritt gestartet?
	/// </summary>
	private double startTime;
	
	
	
	/// <summary>
	/// Referenz auf den Spieler
	/// </summary>
	private PlayerController pc;
	
	
	
	/// <summary>
	/// Höhe der GUI-Dialogsanzeige in Pixel
	/// </summary>
	private static int height = 30;
	
	
	
	/// <summary>
	/// Methode die vor dem Dialog ausgeführt wird
	/// </summary>
	public System.Action<GameObject> preDialog {get; set;}
	
	
	
	/// <summary>
	/// Methode die nach dem Dialog ausgeführt wird
	/// </summary>
	public System.Action<GameObject> postDialog {get; set;}
	
	
	
	protected virtual void Start(){
		//Player referenz holen
		pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	
	
	protected virtual void Update(){
		//Dialog gestartet und noch nicht beendet
		if(started && !finished){
			//der aktueller Text wurde lange genug angezeigt
			//oder der Spieler überspringt mittels Schießen
			if(
				   ( autoSkip &&( (Time.time - startTime) > displayTime ) )
				|| Input.GetButtonDown("Fire1")
			){
				//Nächster Text
				NextText();
			}
		}
	}
	
	
	
	protected virtual void OnTriggerEnter(Collider hit){
		//Der Dialog wurde bisher noch nicht abgespielt 
		//und der Spieler betritt den Trigger
		if(!started && hit.gameObject.tag == "Player"){
			
			//Startzeitpunkt merken
			startTime = Time.time;
			
			//Dialog hat begonnen
			started = true;
			
			//trigger ausschalten
			collider.enabled = false;
			
			//Spieler unverwundbar machen
			pc.godMode = true;
			//Spieler anhalten
			pc.CanMove = false;
			//Schießen des Spielers unterbinden
			pc.CanShoot = false;
			
			//Falls eine pre-Dialog-Aktion vorhanden ist
			if(preDialog != null)
				preDialog(gameObject); //führe sie aus
		}
	}
	
	
	
	//GUI-Zeichnen
	protected virtual void OnGUI(){
		//wenn der Dialog gestartet und noch nicht beendet wurde
		if(started && !finished){
			
			//DialogBoxBreite abhängig von der Bildschirmbreite berechnen
			int width = (int) ((double)Screen.width * 5.0 / 6.0 ); //5/6 Bildschirmbreite
			int left = Screen.width / 2 - width/2;
			//int top = Screen.height / 2 - height/2; //mittig
			int top = Screen.height - 20 - height; //unten
			
			//Positionen/Breiten/Höhen der Rechtecke berechnen
			Rect border = new Rect(left, top, width, height);
			Rect box = new Rect(left+2, top+2, width-4, height-4);
			Rect content = new Rect(left+4, top+4, width-8, height-8);
			Rect weiter = new Rect(left+width-4-50, top+4, 50, height-8);
			
			//Zeichnen
			DrawRectangle(border, Color.grey);
			DrawRectangle(box, Color.black);
			DrawText(content, text[i], Color.white);
			
			//Buttonbeschriftung "Weiter", oder beim letztem Text "Ende".
			string s = ( i == text.Length-1 ? "Ende" : "Weiter" );
			
			//Weiter-Button zeichnen
			if(GUI.Button(weiter, s)){
				//Wenn Weiter gedrückt, nächster Text
				NextText();
			}
		}
	}
	
	
	
	/// <summary>
	/// Inkrementiere den Dialogschittindex, setze die Zeit zurück und
	/// schaue ob das der letzte Schritt war.
	/// </summary>
	private void NextText(){
		//nächster Text
		i++;
		
		//Zeit zurücksetzen
		startTime = Time.time;
		
		//Fertig mit den Texten?
		if(i >= text.Length){
			//Dialog beenden
			Finish();
		}
	}
	
	
	
	/// <summary>
	/// Beende den Dialog und gebe die Kontrolle an den Spieler zurück
	/// </summary>
	private void Finish(){
		
		//Dialog ist beendet
		finished = true;
		
		//Spieler wieder unverwundbar
		pc.godMode = false;
		//Spieler darf sich wieder bewegen
		pc.CanMove = true;
		//Spieler kann wieder schießen
		pc.CanShoot = true;
		
		//Falls eine post-Dialog-Aktion vorhanden ist
		if(postDialog != null)
			postDialog(gameObject); //führe sie aus
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
	private void DrawText(Rect pos, string text, Color c){
		Color tmp = GUI.skin.label.normal.textColor;
		GUI.skin.label.normal.textColor = c;
		GUI.Label(pos, text);
		GUI.skin.label.normal.textColor = tmp;
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
