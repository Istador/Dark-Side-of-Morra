using UnityEngine;
using System.Collections;

public class Mine : ImmovableEnemy {
	
	public Mine() : base(1) {
		
	}
	
	void Start () {}
	
	void Update () {}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			Debug.Log(this.tag + ": inflicting damage.");
			other.SendMessage("ApplyDamage",25);
			GameObject explosion = (GameObject) Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
			Destroy(gameObject); //mine zerstören
			Destroy (explosion,2.0f); //nach 2 sekunden
		}
	}
}
