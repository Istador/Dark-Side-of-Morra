using UnityEngine;
using System.Collections;

public class Mine : ImmovableEnemy<Mine> {
	
	public readonly float f_yellowRange = 6.0f;
	public readonly float f_redRange = 3.0f;
	public readonly float f_explosionRadius = 4.0f;
	public readonly float f_explosionDamage = 75.0f;
	
	public Mine() : base(2, 3, 5, 1) {
		AttackFSM.SetCurrentState(SMineInvisible.Instance);
	}
	
	protected override void Update() {
		base.Update();
	}
	
	void OnTriggerEnter(Collider other) {
		//Kollision nur mit Spieler
		if(other.tag == "Player"){
			Explode();
			GetComponent<BoxCollider>().enabled = false;
		}
	}
	
	public override void ApplyDamage (int damage)
	{
		//Schaden immer
		Explode();
	}
	
	
	private void Explode(){
		MessageDispatcher.Instance.Dispatch(new Telegram(this, "explode"));
		
		/*
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): inflict damage.");
		Collider[] cs = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider c in cs){
			if(c.gameObject.tag == "Enemy" || c.gameObject.tag == "Player" ){
				c.gameObject.SendMessage("ApplyDamage",explosionDamage);
			}
		}
		*/
	}
}
