using UnityEngine;
using System.Collections;

public class PBullet : Projektile<PRocket> {

	public Vector3 heading;
	
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	
	public override int damage { get{return 10;} }
	
	public override float maxSpeed { get{return 4.0f;} }
	public override float maxForce { get{return 4.0f;} }
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	private double startTime;
	public static readonly double timeToLife = 5.0;
	
	protected override void Start() {
		base.Start();
		startTime = Time.time;
	}
	
	protected override void Update() {
		_targetPos = transform.position + heading.normalized * maxSpeed;
		base.Update();
		
		//sterben nach einer bestimmten Zeit
		if(startTime + timeToLife <= Time.time)
			Death();
	}
	
}
