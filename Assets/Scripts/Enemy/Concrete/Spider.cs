using UnityEngine;
using System.Collections;

public class Spider : MovableEnemy<Spider> {
	
	
	
	public BossLevel level {get; private set;}
	public Bodenplatten platten  {get; private set;}
	
	/// <summary>Boss erleidet keinen Schaden</summary>
	public bool invincible = true;
	
	/// <summary>Wie oft bereits die Platten eingesetzt wurden</summary>
	public int stage = 0;
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 8.0f;} }
	
	
	
	protected override int txtCols { get{return 10;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 6;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 5;} }  //Frames per Second
	
	
	
	/// <summary>Geschwindigkeit mit der die Spinne verschwindet und wieder auftaucht</summary>
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
		MoveFSM.SetCurrentState(SSpiderKokon.Instance);
		f_HealthGlobeProbability = 1.0f; //100% drop, 0% kein drop
		f_HealthGlobeBigProbability = 1.0f; //100% big, 0% small
	}
	
	
	
	protected override void Start(){
		base.Start();
		steering.Seek(false);
		level = GameObject.Find("Level").GetComponent<BossLevel>();
		platten = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
	}
	
	
	
	protected override void Update(){
		base.Update();
		Debug.DrawLine(collider.bounds.center, collider.bounds.center + steering.Calculate(), Color.green);
		Debug.DrawLine(collider.bounds.center, collider.bounds.center + rigidbody.velocity, Color.red);
	}
	
	
	
	//Überschreiben um beim Tod den Schlüssel fallen zu lassen
	public override void Death(){
		GameObject key = (GameObject)Instantiate(level.keyPrefab, transform.position + Vector3.left, transform.rotation);
		
		//Schlüssel kurz nach oben bewegen lassen
		key.rigidbody.AddForce(Vector3.up * 6.0f, ForceMode.Impulse);
		
		base.Death();
	}
	
	
	
	/// <summary>
	/// überschrieben für unbesiegbarkeit
	/// </summary>
	public override void ApplyDamage(Vector3 damage){
		if(!invincible) base.ApplyDamage(damage);
	}
}
