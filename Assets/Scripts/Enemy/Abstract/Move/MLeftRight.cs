using UnityEngine;
using System.Collections;

public abstract class MLeftRight<T> : MovableEnemy<T> {
	
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="MLeftRight`1"/> class.
	/// </summary>
	/// <param name='maxHealth'>
	/// Maximale Trefferpunkte des Gegners. Bei 0 HP stirbt der Gegner.
	/// </param>
	public MLeftRight(int maxHealth) : base(maxHealth){
		//zufällig nach links/rechts patrouillieren
		if(new System.Random().Next(0,2) == 0)
			MoveFSM.ChangeState(SPatrolLeft<T>.Instance);
		else
			MoveFSM.ChangeState(SPatrolRight<T>.Instance);
	}
	
	
	
	/// <summary>
	/// Beschränkt die Bewegung auf Links/Rechts durch Drehung
	/// </summary>
	protected override Vector3 FilterForce(Vector3 vIn){
		if(vIn == Vector3.zero) 
			return vIn;
		
		float a = Vector3.Angle(Vector3.left, vIn);
		//Debug.DrawLine(transform.position, transform.position + vIn * 3.0f, Color.green);
		//Debug.DrawLine(transform.position, transform.position + Vector3.left * 2.0f, Color.red);
		//Debug.Log("Angle: "+a);
		if(a < 90.000f)
			return Vector3.left * vIn.magnitude;
		else if(a > 90.000f)
			return Vector3.right * vIn.magnitude;
		return Vector3.zero;
	}
	
	
	
}
