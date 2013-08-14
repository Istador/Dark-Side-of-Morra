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
	/// Position die das Projektil anstrebt
	/// </summary>
	/// <value>
	/// 3D-Koordinate der Zielposition
	/// </value>
	public abstract Vector3 targetPos { get; }
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Projektile`1"/> class.
	/// </summary>
	public Projektile() : base(1) {}
	
	
	
	/// <summary>
	/// Kollisionsbehandlungsroutine:
	/// Kollision mit Spieler -> Schaden verursachen
	/// </summary>
	/// <param name='other'>
	/// Objekt mit dem die Kollision stattfindet
	/// </param>
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player")
			other.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		Death();
	}
	
	
	
	protected override void Start() {
		base.Start();
		steering.Seek(true); //Zielposition anstreben
	}
	
	
	
	/// <summary>
	///  Ziel für Steering Behaviors setzen, Rotiere zum Ziel
	/// </summary>
	protected override void Update() {
		steering.SetTarget(targetPos);
		
		//Vector3 rotate = targetPos - transform.position; //sofort
		Vector3 rotate = rigidbody.velocity; //träge
		
		if(!rotate.Equals(Vector3.zero)){
			//rotiere zum Ziel
			transform.rotation = Quaternion.LookRotation(rotate, zvector);
		
			//drehe Sprite um 90°
			transform.RotateAroundLocal(zvector, deg90);
		}
		
		base.Update();
	}
	
	private static readonly Vector3 zvector = new Vector3(0.0f, 0.0f, -1.0f); //rotation um die Z-Achse
	private static readonly float deg90 = Mathf.PI/2.0f; // = 90°
	
}
