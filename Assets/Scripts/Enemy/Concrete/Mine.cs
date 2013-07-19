using UnityEngine;
using System.Collections;

public class Mine : ImmovableEnemy {
	
	float explosionRadius = 5.0f;
	int explosionDamage = 75;
	
	public Mine() : base(2, 3, 5, 1) {
		
	}
	
	protected override void Update() {
		base.Update();
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			Explode();
		}
	}
	
	protected override void ApplyDamage (int damage)
	{
		Explode();
	}
	
	protected override void Death (){}
	
	private void Explode(){
		/*
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): inflict damage.");
		Collider[] cs = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach(Collider c in cs){
			if(c.gameObject.tag == "Enemy" || c.gameObject.tag == "Player" ){
				c.gameObject.SendMessage("ApplyDamage",explosionDamage);
			}
		}
		*/
		//Explosionsanzeige
		GameObject explosion = (GameObject) Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		//scale explosion
		explosion.particleEmitter.minSize = 0.5f;
		explosion.particleEmitter.maxSize = 2.0f;
		explosion.GetComponent<ParticleRenderer>().lengthScale = 2.0f;
		
		SetSprite(2);	
		Destroy(gameObject, 2.0f); //mine zerstören
		Destroy(explosion,2.0f); //nach 2 sekunden explosion entfernen
	}
}
