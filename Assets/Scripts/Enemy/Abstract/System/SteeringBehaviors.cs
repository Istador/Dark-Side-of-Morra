using UnityEngine;
using System.Collections;

/*
 * Steering Behaviors System
 * 
 * Ein zentrales Objekt um als Gegner aus einer Vielzahl von unterschiedlichen
 * Steering Behaviors wählen zu können, und bei mehreren ausgewählten diese
 * gemeinsam zu einem resultierenden Kraft-Vektor zu berechnen.
 * 
 * Quelle:
 * Mat Buckland - Programming Game AI by Example
*/
public class SteeringBehaviors<T> {
	
	
	
	/// <summary>
	/// Besitzer dieser Instanz
	/// </summary>
	private MovableEnemy<T> owner;
	
	/// <summary>
	/// Ziel das für einige Behaviors benötigt wird
	/// </summary>
	private MovableEnemy<T> target;
	
	/// <summary>
	/// Zielkoordinaten
	/// </summary>
	private Vector3 targetPos;
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SteeringBehaviors`1"/> class.
	/// </summary>
	/// <param name='owner'>
	/// Besitzer dieser Instanz
	/// </param>
	public SteeringBehaviors(MovableEnemy<T> owner){
		this.owner = owner;
	}
	
	
	
	/// <summary>
	/// Anstreben der Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	private Vector3 Seek(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(targetPos - owner.collider.bounds.center).normalized * owner.maxForce;
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	
	
	/// <summary>
	/// Fliehen vor den Zielkoordinaten mit maximaler Geschwindigkeit
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	private Vector3 Flee(Vector3 targetPos){
		Vector3 desiredVelocity = 
			(owner.collider.bounds.center - targetPos).normalized * owner.maxForce;
		
		return desiredVelocity - owner.rigidbody.velocity;
	}
	
	
	
	/// <summary>
	/// Abfangen eines Objektes anhand dessen vorraussichtlich zukünftigen Position.
	/// </summary>
	/// <param name='evader'>
	/// Objekt das abgefangen werden soll
	/// </param>
	private Vector3 Pursuit(MovableEnemy<T> evader){
		Vector3 toEvader = evader.collider.bounds.center - owner.collider.bounds.center;
		
		/*
		if(
			( Vector3.Dot(toEvader, owner.transform.rotation.eulerAngles) > 0 )
			&& 
			( Vector3.Dot(owner.transform.rotation, evader.transform.rotation.eulerAngles) < -0.95f )
			){
			return Seek(evader.transform.position);
		}
		*/
						
		float LAT = toEvader.magnitude / ( owner.maxForce + evader.rigidbody.velocity.magnitude );
		return Seek(evader.collider.bounds.center + evader.rigidbody.velocity * LAT);
	}
	
	
	
	/// <summary>
	/// Ausweichen anhand der vorraussichtlich zukünftigen Position eines Verfolgers.
	/// </summary>
	/// <param name='evader'>
	/// Der Verfolger dem man asuweichen will
	/// </param>
	private Vector3 Evade(MovableEnemy<T> persuer){
		Vector3 toPersuer = persuer.collider.bounds.center - owner.collider.bounds.center;
		float LAT = toPersuer.magnitude / ( owner.maxForce + persuer.rigidbody.velocity.magnitude );
		return Flee(persuer.collider.bounds.center + persuer.rigidbody.velocity * LAT);
	}
	
	
	
	private bool seeking = false;
	/// <summary>
	/// Anstreben ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public void Seek(bool on){seeking = on;}
	
	private bool fleeing = false;
	/// <summary>
	/// Fliehen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public void Flee(bool on){fleeing = on;}
	
	private bool pursuing = false;
	/// <summary>
	/// Abfangen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public void Pursuit(bool on){pursuing = on;}
	
	private bool evading = false;
	/// <summary>
	/// Ausweichen ein-/ausschalten
	/// </summary>
	/// <param name='on'>
	/// true=ein, false=aus
	/// </param>
	public void Evade(bool on){evading = on;}
	
	
	
	/// <summary>
	/// Setzt die Zielkoordinaten
	/// </summary>
	/// <param name='targetPos'>
	/// Zielkoordinaten
	/// </param>
	public void SetTarget(Vector3 targetPos){
		this.targetPos = targetPos;
	}
	
	
	
	/// <summary>
	/// Setzt das Ziel das für einige Behaviors benötigt wird
	/// </summary>
	/// <param name='targetPos'>
	/// Ziel das für einige Behaviors benötigt wird
	/// </param>
	public void SetTarget(MovableEnemy<T> target){
		this.target = target;
	}
	
	
	
	/// <summary>
	/// berechnet die resultierende Kraft aller Steering Behaviours
	/// </summary>
	/// <returns>
	/// Kraft-Vektor zu der sich bewegt werden soll
	/// </returns>
	public Vector3 Calculate(){
		Vector3 f = Vector3.zero;
		
		if(seeking) f += Seek(targetPos);
		if(fleeing) f += Flee(targetPos);
		if(pursuing && target != null) f+= Pursuit(target);
		if(evading && target != null) f+= Evade(target);
		
		//truncat
		if(f != Vector3.zero && f.magnitude > owner.maxForce)
			f = f.normalized * owner.maxForce;
		
		return f;
	}
	
	
	
}
