using UnityEngine;
using System.Collections;

/*
 * Abstrakte Klasse für Projektile von Gegnern
 * Gemeinsam für alle Projektile:
 * - Schaden Verursachen 
 * - Sterben wenn sie selbst Schaden erleiden (1 HP)
 * - Rotation zu Flugrichtung
 * 
*/
public abstract class Projektile<T> : MovableEnemy<T> {
	
	
	
	/// <summary>
	/// Schaden den das Projektil beim Spieler verursacht
	/// </summary>
	/// <value>
	/// in ganzen Trefferpunkten
	/// </value>
	public abstract int damage { get; }
	
	
	
	/// <summary>
	/// GameObject, dass dieses Projektil abgeschossen hat.
	/// </summary>
	public GameObject owner;
	
	
	/// <summary>
	/// Position die das Projektil anstrebt
	/// </summary>
	/// <value>
	/// 3D-Koordinate der Zielposition
	/// </value>
	public abstract Vector3 targetPos { get; }
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Projektile`1"/> class.
	/// </summary>
	public Projektile() : base(1) {
		f_HealthGlobeProbability = 0.01f; //1% drop, 99% kein drop
		f_HealthGlobeBigProbability = 0.1f; //10% big, 90% small
		// 0,01 * ( 0,1 * 50 + 0,9 * 10 ) = 0,14 HP on average
	}
	
	
	
	/// <summary>
	/// Kollisionsbehandlungsroutine:
	/// Kollision mit Spieler -> Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Objekt mit dem die Kollision stattfindet
	/// </param>
	protected virtual void OnTriggerEnter(Collider other) {
		//nicht null, wenn Projektil von selben Typ
		Projektile<T> p = other.gameObject.GetComponent<Projektile<T>>();
		
		//Nicht mit dem Besitzer dieses Projektils oder eines seiner Projektile kollidieren
		if(other.gameObject != owner && (p==null || p.owner != owner ) ){
		
			//Kollision mit Spieler?
			if(other.gameObject.tag == "Player")
				//Schaden verursachen
				DoDamageTo(other.gameObject, damage);
			//auch bei Kollisionen die nicht mit dem Spieler sind sterben
			Death();
		}
	}
	
	
	
	protected override void Start() {
		base.Start();
		steering.Seek(true); //Zielposition anstreben
		SetInvisible();
		
		//Kollision mit Spieler einschalten
		Physics.IgnoreCollision(collider, player.collider, false);
		Physics.IgnoreCollision(player.collider, collider, false);
		
		transform.Rotate(-90.0f, 0.0f, -90.0f);
	}
	
	
	
	/// <summary>
	///  Ziel für Steering Behaviors setzen, Rotiere zum Ziel
	/// </summary>
	protected override void Update() {
		steering.SetTarget(targetPos);
		
		rotate();
		
		SetVisible();
		base.Update();
	}
	
	float inc = 0.0f;
	protected void rotate(){
		//Vector3 rotate = targetPos - transform.position; //sofort
		Vector3 rotate = rigidbody.velocity; //träge
		
		if(!rotate.Equals(Vector3.zero)){
			//rotiere zum Ziel
			transform.rotation = Quaternion.LookRotation(rotate, zvector);
			//Quaternion rot = Quaternion.LookRotation(rotate, zvector);
			//float speed = 0.5f;
			//transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed*Time.deltaTime);
		
			inc += 0.02f;
			//drehe Sprite um 90°
			transform.Rotate(-90.0f, 0.0f, -90.0f);
		}
	}
	
	private static readonly Vector3 zvector = new Vector3(0.0f, 0.0f, 1.0f); //rotation um die Z-Achse
	
}
