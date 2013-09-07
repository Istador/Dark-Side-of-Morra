using UnityEngine;
using System.Collections;

public class Spider : MovableEnemy<Spider> {
	
	
	
	public BossLevel level {get; private set;}
	public Bodenplatten platten  {get; private set;}
	
	/// <summary>Boss erleidet keinen Schaden</summary>
	public bool invincible {
		get{return this._invincible;}
		set{
			this._invincible = value;
			pc.collider.enabled = !value;
		}
	}
	private bool _invincible;
	private PlayerCollider pc;
	
	/// <summary>Angriffsrichtung, je nachdem ob der Spieler links oder rechts von der Spinne ist</summary>
	public Vector3 v_attackVector = Vector3.right;
	/// <summary>Angriffsradius</summary>
	public static readonly float f_attackRange = 1.8f;
	
	
	
	/// <summary>Wie oft bereits die Platten eingesetzt wurden</summary>
	public int stage = 0;
	
	public override float maxSpeed { get{return 3.5f;} }
	public override float maxForce { get{return 8.0f;} }
	
	
	
	protected override int txtCols { get{return 10;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 6;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 8;} }  //Frames per Second
	
	
	
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
		MoveFSM.SetCurrentState(SSpiderKokon.Instance);
		f_HealthGlobeProbability = 1.0f; //100% drop, 0% kein drop
		f_HealthGlobeBigProbability = 1.0f; //100% big, 0% small
	}
	
	
	
	protected override void Start(){
		base.Start();
		steering.Seek(false);
		level = GameObject.Find("Level").GetComponent<BossLevel>();
		platten = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
		SetSprite(1);
		pc = GetComponentInChildren<PlayerCollider>();
		invincible = true;
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
