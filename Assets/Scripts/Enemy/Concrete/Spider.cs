using UnityEngine;
using System.Collections;

/// 
/// Der Spinnen-Bossgegner schlüpft aus seinen Kokon aus, und greift den 
/// Spieler im Nahkampf an. Fallen die Trefferpunkte der Spinne auf
/// unter 75%, 50% oder 25% verschwindet sie nach oben und löst ein tödliches
/// Bodenplatten Event aus. Beim Tod droppt sie einen Schlüssel.
/// 
public class Spider : MovableEnemy<Spider> {
	
	
	
	/// <summary>
	/// Richtung in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public override Vector3 Heading { get{
			return IsRight(Player) ? Vector3.right : Vector3.left;
		}
	}
	
	
	
	/// <summary>
	/// Referenz auf den Lebensbalken des Bosses, um ihn zu Beginn des Kampfes 
	/// ein- und mit dem Tod der Spinne auszublenden.
	/// </summary>
	public BossHealthBar healthbar {get; private set;}
	
	/// <summary>
	/// Referenz auf das Platten-Event, um es auslösen zu können.
	/// </summary>
	public Bodenplatten platten  {get; private set;}
	
	/// <summary>
	/// Referenz auf den Player Collider mit dem die Spinne bei Berührung
	/// Schaden verursacht.
	/// Benötigt, um ihn manuell ein und auszuschalten.
	/// </summary>
	public Collider playerCollider {get; private set;}
	
	
	/// <summary>
	/// Angriffsrichtung, je nachdem ob der Spieler links oder rechts von der 
	/// Spinne ist. Es wird nicht Heading verwendet, da die Spinne ihren 
	/// Angriff nicht abbrechen oder umlenken kann. Deshalb wird sich das
	/// Heading zu Beginn der Angriffsanimation gemerkt.
	/// </summary>
	public Vector3 v_attackVector = Vector3.right;
	
	
	
	/// <summary>
	/// Angriffsradius. In diesem Radius um die Angriffsposition (vor der 
	/// Spinne) erleidet der Spieler Schaden sollte er sich dort aufhalten.
	/// </summary>
	public static readonly float f_attackRange = 1.8f;
	
	
	
	/// <summary>
	/// Ein Zähler, um zu speichern wie oft das Plattenevent bereits 
	/// ausgeführt wurde. Um pro 25% Gefälle das Event nur je einmal auszuführen.
	/// </summary>
	public int stage = 0;
	
	
	
	/// <summary>
	/// Schaden den die Spinne pro Schlag verursacht
	/// </summary>
	public static readonly int i_damage = 30;
	
	/// <summary>
	/// Geschwindigkeit mit der die Spinne verschwindet und wieder auftaucht
	/// </summary>
	public static readonly float f_seilSpeed = 2.0f;
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Angreifen
	/// </summary>
	public static readonly float f_outOfRange = 3.0f;
	
	/// <summary>
	/// Entfernung bis zu welcher sich die Spinne dem Spieler annähert
	/// </summary>
	public static readonly float f_seekRange = 2.0f;
	
	/// <summary>
	/// Angriffszeit:
	/// Die Zeit die für den Angriff sowie dessen Animation benötigt wird
	/// </summary>
	public static readonly double d_attackTime = 1.0; // 1 sekunden
	
	
	
	public Spider() : base(2500){
		//Zustandsautomaten initialisieren
		MoveFSM.CurrentState = SSpiderKokon.I;
		
		//Health-Globe Wahrscheinlichkeiten ändern
		f_HealthGlobeProbability = 1.0f; //100% drop, 0% kein drop
		f_HealthGlobeBigProbability = 1.0f; //100% big, 0% small
	}
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 6;
		txtFPS = 8;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 4.2f;
		MaxForce = 9.6f;
		
		//Referenzen laden
		platten = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
		healthbar = (BossHealthBar) GameObject.FindObjectOfType(typeof(BossHealthBar));
		playerCollider = ((PlayerCollider)GameObject.FindObjectOfType(typeof(PlayerCollider))).collider;
		
		//nach Rechts gucken
		Sprite = 1;
		
		//Spinne ist solange unbesiegbar, bis etwas anderes gesagt wird
		Invincible = true;
	}
	
	
	
	//Überschreiben um beim Tod der Spinne den Schlüssel fallen zu lassen
	public override void Death(){
		//Health Bar ausblenden
		healthbar.Hide();
		
		//Schlüssel erstellen
		GameObject key = Instantiate("BossKey", Pos + Vector3.left);
		
		//Schlüssel kurz nach oben bewegen lassen
		key.rigidbody.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
		
		base.Death();
	}
	
	
	
	/// <summary>
	/// Führt die Attack-Animation manuell aus, statt über den Sprite-Controller
	/// </summary>
	/// <returns>
	/// Diese Funktion gibt true zurück, wenn der aktuelle Frame der letzte der Animation war
	/// </returns>
	public bool AttackFrame(){
		//beim erstem Aufruf für diese Animation
		if(!attackStarted){
			//Jetzige Zeit merken
			attackStartTime = Time.time;
			attackStarted = true;
		}
		
		//Frame aus der Zeit berechnen
		double time = Time.time - attackStartTime;
		//bei überschreitung nicht wieder auf den 1. Frame springen, sondern
		//weiterhin kurz den letzten zeigen.
		int attackFrame = System.Math.Min((int) (time * attackFPS), txtCols);
		
		//Textur ändern
		Vector2 offset = renderer.material.mainTextureOffset;
		offset.x = ((float) attackFrame) * (1.0f / txtCols);
		renderer.material.mainTextureOffset = offset;
		
		//war dies der letzte Frame?
		if( time > ( (double)txtCols / attackFPS ) ){
			//Animation zurücksetzen
			attackStarted = false;
			//Der aufrufenden Methode eine Rückmeldung darüber geben
			return true;
		}
		return false;
	}
	//Instanzvariablen für die Animation.
	private bool attackStarted = false;
	private double attackStartTime = 0.0;
	private static double attackFPS = 12.0;
	
	
	
}
