using UnityEngine;
using System.Collections;

public abstract class MovableEnemy<T> : Enemy<T> {
	
	public abstract float maxSpeed { get; }
	public abstract float maxForce { get; }
	
	public readonly SteeringBehaviors<T> steering;
	
	public MovableEnemy(int maxHealth) : base(maxHealth){
		this.steering = new SteeringBehaviors<T>(this);
	}
	
	protected override void Update () {
		base.Update();
		
		Vector3 f = steering.Calculate();
		
		rigidbody.AddForce(f);
		
		if(rigidbody.velocity.magnitude > maxSpeed)
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
	}
}
