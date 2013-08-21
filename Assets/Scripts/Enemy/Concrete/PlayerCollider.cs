using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {
	
	/// <summary>Schadenswert pro Beruehrung</summary>
	public int damage;
	
	/// <summary>minimale Zeit in Sekunden zwischen Schaden</summary>
	public float tickLength;
	
	
	
	void Start(){
		lastPlayerDamage = Time.time;
	}
	
	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	void OnTriggerEnter(Collider other) {
		DoPlayerDamage(other);
	}

	
	
	/// <summary>
	/// Kollision mit dem Spieler führt zu Schaden beim Spieler
	/// </summary>
	void OnTriggerStay(Collider other) {
		DoPlayerDamage(other);
	}
	
	
	
	//<summary>wann das letzte mal Schaden beim Spieler verursacht wurde</summary>
	private float lastPlayerDamage;
	
	
	
	void DoPlayerDamage(Collider other){
		//Kollision nur mit Spieler
		if(other.gameObject.tag == "Player"){
			//nur in bestimmten Intervallen
			if(lastPlayerDamage + tickLength < Time.time){
				Vector3 dmg = (other.bounds.center - collider.bounds.center).normalized * damage;
				other.gameObject.SendMessage("ApplyDamage", dmg, SendMessageOptions.DontRequireReceiver);
				lastPlayerDamage = Time.time;
			}
		}
	}
	
	
	
}
