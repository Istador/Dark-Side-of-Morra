using UnityEngine;
using System.Collections;

public abstract class Projektile<T> : MovableEnemy<T> {
		
	public abstract int damage { get; }
	public abstract Vector3 targetPos { get; }
	
	public Projektile() : base(1) {}
	
	//Kollision -> Schaden verursachen
	void OnTriggerEnter(Collider other) {
		other.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		Death();
	}
	
	protected override void Start() {
		base.Start();
		steering.Seek(true);
	}
	
	protected override void Update() {
		steering.SetTarget(targetPos);
		Vector3 relPos = targetPos - transform.position;
		
		//rotiere zum Ziel
		transform.rotation = Quaternion.LookRotation(relPos, new Vector3(0.0f, 0.0f, 1.0f));
		//drehe nochmal um 90°
		transform.RotateAroundLocal(new Vector3(0.0f, 0.0f, 1.0f), 1.575f);
		base.Update();
	}
	
}
