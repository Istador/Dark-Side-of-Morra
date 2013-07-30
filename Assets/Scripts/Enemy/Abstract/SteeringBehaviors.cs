using UnityEngine;
using System.Collections;

public class SteeringBehaviors<T> {
	
	
	private MovableEnemy<T> owner;
	
	private MovableEnemy<T> target;
	private Vector3 targetPos;
	
	public SteeringBehaviors(MovableEnemy<T> owner){
		this.owner = owner;
	}
	
	
	
	//Anstreben
	private Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(targetPos - owner.transform.position).normalized * owner.maxSpeed;
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	//Fliehen
	private Vector3 Flee(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(owner.transform.position - targetPos).normalized * owner.maxSpeed;
		
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	//Abfangen
	private Vector3 Pursuit(MovableEnemy<T> evader){
		Vector3 toEvader = evader.transform.position - owner.transform.position;
		
		/*
		if(
			( Vector3.Dot(toEvader, owner.transform.rotation.eulerAngles) > 0 )
			&& 
			( Vector3.Dot(owner.transform.rotation, evader.transform.rotation.eulerAngles) < -0.95f )
			){
			return Seek(evader.transform.position);
		}
		*/
						
		float LAT = toEvader.magnitude / ( owner.maxSpeed + evader.rigidbody.velocity.magnitude );
		return Seek(evader.transform.position + evader.rigidbody.velocity * LAT);
	}
	
	//Ausweichen
	private Vector3 Evade(MovableEnemy<T> persuer){
		Vector3 toPersuer = persuer.transform.position - owner.transform.position;
		float LAT = toPersuer.magnitude / ( owner.maxSpeed + persuer.rigidbody.velocity.magnitude );
		return Flee(persuer.transform.position + persuer.rigidbody.velocity * LAT);
	}
	
	
	private bool seeking = false;
	public void Seek(bool on){seeking = on;}
	
	private bool fleeing = false;
	public void Flee(bool on){fleeing = on;}
	
	private bool pursuing = false;
	public void Pursuit(bool on){pursuing = on;}
	
	private bool evading = false;
	public void Evade(bool on){evading = on;}
	
	
	public void SetTarget(Vector3 targetPos){
		this.targetPos = targetPos;
	}
	
	public void SetTarget(MovableEnemy<T> target){
		this.target = target;
	}
	
	// berechnet die resultierende Kraft aller Steering Behaviours
	public Vector3 Calculate(){
		Vector3 f = Vector3.zero;
		
		if(seeking) f += Seek(targetPos);
		if(fleeing) f += Flee(targetPos);
		if(pursuing) f+= Pursuit(target);
		if(evading) f+= Evade(target);
		
		//truncat
		if(f.magnitude > owner.maxForce)
			f = f.normalized * owner.maxForce;
		
		return f;
	}
}
