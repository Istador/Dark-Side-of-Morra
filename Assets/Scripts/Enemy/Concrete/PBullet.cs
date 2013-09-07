using UnityEngine;
using System.Collections;

public class PBullet : Projektile<PRocket> {
	
	
	
	/// <summary>
	/// Richtung in die sich die Kugel in einer geraden Linie bewegt.
	/// </summary>
	public Vector3 heading;
	
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	
	
	
	public override int damage { get{return 10;} }
	
	
	
	public override float maxSpeed { get{return 4.0f;} }
	public override float maxForce { get{return 4.0f;} }
	
	
	
	protected override int txtCols { get{return 8;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 8;} }  //Frames per Second
	
	
	
	private double startTime;
	/// <summary>
	/// Zeit in Sekunden nach der die Kugel automatisch stirbt
	/// </summary>
	public static readonly double timeToLife = 5.0;
	
	
	
	protected override void Start() {
		base.Start();
		startTime = Time.time;
	}
	
	
	
	protected override void Update() {
		_targetPos = collider.bounds.center + heading.normalized * maxSpeed;
		
		base.Update();
		
		//sterben nach einer bestimmten Zeit
		if(startTime + timeToLife <= Time.time)
			Death();
	}
	
	
	
}
