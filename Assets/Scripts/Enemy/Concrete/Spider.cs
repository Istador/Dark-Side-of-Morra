using UnityEngine;
using System.Collections;

public class Spider : MovableEnemy<Spider> {
	
	
	
	/// <summary>
	/// Richtung in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public override Vector3 Heading { get{
			return IsRight(Player) ? Vector3.right : Vector3.left;
		}
	}
	
	
	
	public BossLevel level {get; private set;}
	public BossHealthBar healthbar {get; private set;}
	public Bodenplatten platten  {get; private set;}
	
	
	
	/// <summary>Angriffsrichtung, je nachdem ob der Spieler links oder rechts von der Spinne ist</summary>
	public Vector3 v_attackVector = Vector3.right;
	/// <summary>Angriffsradius</summary>
	public static readonly float f_attackRange = 1.8f;
	
	
	
	/// <summary>Wie oft bereits die Platten eingesetzt wurden</summary>
	public int stage = 0;
	
	
	
	/// <summary>
	/// Schaden den die Spinne pro Schlag macht
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
	/// Entfernung bis zu welcher angenähert wird
	/// </summary>
	public static readonly float f_seekRange = 2.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit die für den Angriff benötigt wird
	/// </summary>
	public static readonly double d_attackTime = 1.0; // 1,0 sekunden
	
	
	
	public Spider() : base(2000){
		MoveFSM.CurrentState = SSpiderKokon.Instance;
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
		MaxSpeed = 3.5f;
		MaxForce = 8.0f;
		
		//Referenzen laden
		level = GameObject.Find("Level").GetComponent<BossLevel>();
		platten = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
		healthbar = (BossHealthBar) GameObject.FindObjectOfType(typeof(BossHealthBar));
		
		//nach Rechts gucken
		Sprite = 1;
		
		//Unbesiegbar
		Invincible = true;
	}
	
	
	
	protected override void Update(){
		base.Update();
		Debug.DrawLine(Pos, Pos + Steering.Calculate(), Color.green);
		Debug.DrawLine(Pos, Pos + rigidbody.velocity, Color.red);
	}
	
	
	
	//Überschreiben um beim Tod den Schlüssel fallen zu lassen
	public override void Death(){
		//Health Bar ausblenden
		healthbar.Hide();
		
		//Schlüssel erstellen
		GameObject key = (GameObject)Instantiate(level.keyPrefab, transform.position + Vector3.left, transform.rotation);
		
		//Schlüssel kurz nach oben bewegen lassen
		key.rigidbody.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
		
		base.Death();
	}
	
	
	
	/// <summary>
	/// Führt die Attack-Animation aus
	/// </summary>
	/// <returns>
	/// ob dies der letzte Frame ist
	/// </returns>
	public bool AttackFrame(){
		//Jetzige Zeit merken
		if(!attackStarted){
			attackStartTime = Time.time;
			attackStarted = true;
		}
		
		//Frame berechnen
		double time = Time.time - attackStartTime;
		int attackFrame = System.Math.Min((int) (time * attackFPS), txtCols);
		
		//Textur ändern
		Vector2 offset = renderer.material.mainTextureOffset;
		offset.x = ((float) attackFrame) * (1.0f / txtCols);
		renderer.material.mainTextureOffset = offset;
		
		//war dies der letzte Frame?
		if( time > ( (double)txtCols / attackFPS ) ){
			attackStarted = false;
			return true;
		}
		return false;
	}
	private bool attackStarted = false;
	private double attackStartTime = 0.0;
	private static double attackFPS = 12.0;
}
