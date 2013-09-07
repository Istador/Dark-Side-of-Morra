﻿using UnityEngine;
using System.Collections;

public class Spider : MLeftRight<Spider> {
	
	
	
	private BossLevel level;
	public bool invincible = true;
	
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 3.0f;} }
	
	
	
	protected override int txtCols { get{return 10;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 6;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 5;} }  //Frames per Second
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Angreifen
	/// </summary>
	public static readonly float f_outOfRange = 2.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit die für den Angriff benötigt wird
	/// </summary>
	public static readonly double d_attackTime = 1.0; // 1,0 sekunden
	
	
	
	
	public Spider() : base(1000){
		MoveFSM.SetCurrentState(SSpiderKokon.Instance);
		f_HealthGlobeProbability = 1.0f; //100% drop, 0% kein drop
		f_HealthGlobeBigProbability = 1.0f; //100% big, 0% small
	}
	
	
	
	protected override void Start(){
		base.Start();
		steering.Seek(false);
		level = GameObject.Find("Level").GetComponent<BossLevel>();
	}
	
	
	
	protected override void Update(){
		base.Update();
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
	public virtual void ApplyDamage(Vector3 damage){
		if(!invincible) base.ApplyDamage(damage);
	}
}
