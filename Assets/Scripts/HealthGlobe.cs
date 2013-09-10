using UnityEngine;
using System.Collections;

/// 
/// Health-Globes sind Herzen die von sterbenden Gegnern fallengelassen werden,
/// und die beim Spieler Lebenspunkte erneuern wenn sie aufgehoben werden.
/// 
/// Sie existieren in zwei Größen, die unterschiedlich viel Leben heilen.
/// 
/// Werden sie aufgehoben verursachen sie zusätzlich ein Geräusch zur Bestätigung
/// 
public class HealthGlobe : MonoBehaviour {
	
	
	
	/// <summary>
	/// Ob dies ein großer oder kleiner Health-Globe ist
	/// </summary>
	public bool big = true;
	
	
	
	/// <summary>
	/// Die Referenz auf das Geräusch, das beim aufheben gemacht wird.
	/// </summary>
	private static AudioClip ac_pickupsound;
	
	
	
	/// <summary>
	/// HP-Wert um den der Spieler bei kleinen Health-Globes geheilt wird
	/// </summary>
	public static readonly int i_smallHP = 10;
	
	
	
	/// <summary>
	/// HP-Wert um den der Spieler bei großen Health-Globes geheilt wird
	/// </summary>
	public static readonly int i_bigHP = 50;
	
	
	
	
	// Textur / Animation

	/// <summary>
	/// Referenz auf den Sprite-Controller um das Herz zu Animieren
	/// </summary>
	private SpriteController spriteCntrl;
	/// <summary>
	/// Zeile der Animation
	/// </summary>
	private int txtState = 0;
	/// <summary>
	/// Anzahl Spalten (Frames)
	/// </summary>
	private int txtCols = 10;
	/// <summary>
	/// Anzahl Zeilen (Zustände)
	/// </summary>
	private int txtRows = 2;
	/// <summary>
	/// Frames per Second
	/// </summary>
	private int txtFPS = 10;
	
	
	
	void Start () {
		//SpriteController zum Objekt hinzufügen und Referenz merken
		spriteCntrl = gameObject.AddComponent<SpriteController>();
		
		//Referenz auf das Aufhebe-Geräusch laden, falls noch nicht geschehen
		if(ac_pickupsound == null) ac_pickupsound = (AudioClip) Resources.Load("Sounds/healthpickup");
		
		//bei großen Health-Globes den anderen Sprite-Zustand verwenden
		if(big) txtState = 1;
	}
	
	
	
	void Update () {
		//Animation mittels Sprite-Controllers
		spriteCntrl.animate(txtCols, txtRows, 0, txtState, txtCols, txtFPS);
	}
	
	
	
	void OnTriggerEnter(Collider other){
		//Das Health-Globe fällt auf das Level
		if(other.gameObject.layer == 8){
			//Gravitation ausschalten
			rigidbody.useGravity = false;
			//Bewegung ausschalten
			rigidbody.isKinematic = true;
		}
		//Der Spieler löst den Trigger aus
		else if(other.gameObject.tag == "Player"){
			
			//bei großem Health-Globe
			if(big)
				//viel HP zum Spieler schicken
				SendHealthTo(other.gameObject, i_bigHP);
			//bei kleinem Health-Globe
			else 
				//wenig HP zum Spieler schicken
				SendHealthTo(other.gameObject, i_smallHP);
			
			//PickUp-Geräusch abspielen
			AudioSource.PlayClipAtPoint(ac_pickupsound, collider.bounds.center);
			
			//Diesen Health-Globe zerstören
			Destroy(gameObject);
		}
	}
	
	
	
	/// <summary>
	/// Einem Spiel-Objekt Lebenspunkte schicken
	/// </summary>
	/// <param name='other'>
	/// Das Spielobjekt das Lebenspunkte bekommen soll
	/// </param>
	/// <param name='hp'>
	/// Wieviele Lebenspunkte
	/// </param>
	void SendHealthTo(GameObject other, int hp){
		other.SendMessage("ApplyHealth", hp, SendMessageOptions.DontRequireReceiver);
	}
	
	
	
}
