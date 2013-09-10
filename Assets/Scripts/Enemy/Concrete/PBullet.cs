using UnityEngine;
using System.Collections;

public class PBullet : Projektile<PBullet> {
	
	
	
	/// <summary>
	/// Richtung in die sich die Kugel in einer geraden Linie bewegt.
	/// </summary>
	public Vector3 heading;
	
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	
	
	
	public override int damage { get{return 10;} }
	
	
	
	public override float maxSpeed { get{return 7.0f;} }
	public override float maxForce { get{return 7.0f;} }
	
	
		
	private double startTime;
	/// <summary>
	/// Zeit in Sekunden nach der die Kugel automatisch stirbt
	/// </summary>
	public static readonly double timeToLife = 5.0;
	
	
	
	protected override void Start() {
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 8;
		txtRows = 1;
		txtFPS = 8;
		
		//SpriteController einschalten
		Animated = true;
		
		startTime = Time.time;
	}
	
	
	
	protected override void Update() {
		_targetPos = collider.bounds.center + heading.normalized * maxSpeed;
		
		base.Update();
		
		//sterben nach einer bestimmten Zeit
		if(startTime + timeToLife <= Time.time)
			Death();
	}
	
	
	//PBullets können nicht mit anderen PBullets kollidieren
	protected override void OnTriggerEnter(Collider other) {
		PBullet bul = other.gameObject.GetComponent<PBullet>();
		Soldier sol = other.gameObject.GetComponent<Soldier>();
		//wenn kein bullet und kein Soldat
		if(bul==null && sol==null){
			//normales kollidieren
			base.OnTriggerEnter(other);
		}
	}
	
	
}
