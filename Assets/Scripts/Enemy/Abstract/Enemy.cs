using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	
	private int health;
	private int maxHealth;
		
	private Enemy(){}
	public Enemy(int maxHealth){
		this.maxHealth = maxHealth;
		this.health = maxHealth;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ApplyDamage(int damage){
		Debug.Log(this.tag + ": "+damage+" dmg received");
	}
}
