using UnityEngine;
using System.Collections;

/// 
/// Abstrakte Oberklasse für bewegliche Gegner
/// 
public abstract class MovableEnemy<T> : Enemy<T> {
	
	
	
	/// <summary>
	/// maximale Geschwindigkeit
	/// </summary>
	public abstract float maxSpeed { get; }
	
	
	
	/// <summary>
	/// maximale Kraft der Steering Behaviors
	/// </summary>
	public abstract float maxForce { get; }
	
	
	
	/// <summary>
	/// Komponente die den "Wunsch nach Bewegung" ausdrückt
	/// </summary>
	public readonly SteeringBehaviors<T> steering;
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MovableEnemy`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MovableEnemy(int maxHealth) : base(maxHealth){
		this.steering = new SteeringBehaviors<T>(this);
	}
	
	
	
	/// <summary>
	/// Steering Behaviors berechnen und anwenden
	/// </summary>
	protected override void Update () {
		base.Update();
		
		//resultierende Kraft der verschiedenen Steering Behaviors berechnen
		Vector3 f = steering.Calculate();
		
		//Kraft auf die Unity-Physik-Engine übertragen, um Bewegung zu erzeugen
		rigidbody.AddForce(f);
				
		//Bewegungsgeschwindigkeit limitieren
		if(rigidbody.velocity.magnitude > maxSpeed)
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
	}
	
	
}
