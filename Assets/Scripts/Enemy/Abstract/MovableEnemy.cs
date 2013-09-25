using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für bewegliche Gegner
/// 
public abstract class MovableEnemy<T> : Enemy<T> {
	
	
	
	/// <summary>
	/// Richtung (Links/Rechts) in die der Gegner guckt.
	/// Lokales Koordinatensystem.
	/// </summary>
	public virtual Vector3 Heading {get{return Moving;}}
	
	/// <summary>
	/// Richtung (Links/Rechts/Oben/Unten) in die sich der Gegner bewegt.
	/// </summary>
	public Vector3 Moving {
		get{
			//wenn kein Moving angegeben
			if(_Moving == Vector3.zero && Mathf.Abs(rigidbody.velocity.magnitude) > 0.5f)
				//ermittel das aus der Geschwindigkeit
				return Utility.ToHeading(rigidbody.velocity);
			//sonst die Instanzvariable
			return _Moving;
		}
		private set{_Moving = value;}
	}
	private Vector3 _Moving;
	
	//Hilfsmethode
	private void Move(Vector3 heading){
		Moving = heading;
		Steering.DoSeek(Pos + heading * MaxSpeed);
	}
	
	/// <summary>
	/// Bewegung nach oben
	/// </summary>
	public void MoveUp(){ Move(Vector3.up); }
	/// <summary>
	/// Bewegung nach unten
	/// </summary>
	public void MoveDown(){ Move(Vector3.down); }
	/// <summary>
	/// Bewegung nach links
	/// </summary>
	public void MoveLeft(){ Move(Vector3.left); }
	/// <summary>
	/// Bewegung nach rechts
	/// </summary>
	public void MoveRight(){ Move(Vector3.right); }
	/// <summary>
	/// Hört auf sich zu bewegen
	/// </summary>
	public void StopMoving(){
		//anhalten
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		Steering.Stop();
		Moving = Vector3.zero;
	}
	
	
	
	/// <summary>
	/// maximale Geschwindigkeit
	/// </summary>
	public float MaxSpeed { get; protected set; }
	
	
	
	/// <summary>
	/// maximale Kraft der Steering Behaviors
	/// </summary>
	public float MaxForce { get; protected set; }
	
	
	
	/// <summary>
	/// Steering Behavior Komponente, die den "Wunsch nach Bewegung" ausdrückt
	/// </summary>
	public readonly SteeringBehaviors<T> Steering;
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MovableEnemy`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MovableEnemy(int maxHealth) : base(maxHealth){
		//Steering Behavior Komonente erstellen
		Steering = new SteeringBehaviors<T>(this);
		
		MaxSpeed = 1.0f;
		MaxForce = 1.0f;
	}
	
	
	
	/// <summary>
	/// Filtert den Kraftvektor vor der Anwendung auf den rigidbody
	/// </summary>
	/// <returns>
	/// Die gefilterte Kraft
	/// </returns>
	/// <param name='vin'>
	/// Eingehende Kraft der Steering Behaviour Komponente
	/// </param>
	protected virtual Vector3 FilterForce(Vector3 vIn){
		return vIn;
	}
	
	
	
	/// <summary>
	/// Steering Behaviors berechnen und anwenden
	/// </summary>
	protected virtual void FixedUpdate () {
		
		//resultierende Kraft der verschiedenen Steering Behaviors berechnen
		Vector3 f = FilterForce(Steering.Calculate());
		
		if(f != Vector3.zero){
			
			//Kraft auf die Unity-Physik-Engine übertragen, um Bewegung zu erzeugen
			//rigidbody.AddRelativeForce(f); //nicht für Projektile
			rigidbody.AddForce(f);
				
			//Bewegungsgeschwindigkeit limitieren
			if(Mathf.Abs(rigidbody.velocity.magnitude) > MaxSpeed)
				rigidbody.velocity = rigidbody.velocity.normalized * MaxSpeed;
		}
	}
	
	

	
	
	
}
