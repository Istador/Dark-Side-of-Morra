using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	
	
	/*
	 * Textur / Sprite Controller
	*/
	private SpriteController spriteCntrl;
	private int txtState = 0; //Zeile der Animation
	private int txtCols; //Anzahl Spalten (Frames)
	private int txtRows; //Anzahl Zeilen (Zustände)
	private int txtFPS;  //Frames per Second
	
	
	/*
	 * Health
	*/
	private int health;
	private int maxHealth;
	
	
	/*
	 * Konstruktor
	*/
	private Enemy(){}
	public Enemy(int txtCols, int txtRows, int txtFPS, int maxHealth){
		this.maxHealth = maxHealth;
		this.txtCols = txtCols; 
		this.txtRows = txtRows;
		this.txtFPS = txtFPS;
	}
	
	
	protected virtual void Start () {
		health = maxHealth;
		//SpriteController hinzufügen
		spriteCntrl = gameObject.AddComponent<SpriteController>();
	}
	
	protected virtual  void Update () {
		//Animation
		spriteCntrl.animate(txtCols, txtRows, 0, txtState, txtCols, txtFPS);
	}
	
	protected virtual  void ApplyDamage(int damage){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+damage+" dmg received");
		health -= damage;
		if(health <= 0) Death();
	}
	
	protected virtual  void Death(){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): death");
		Destroy(gameObject);
	}
	
	protected void SetSprite(int row){txtState = row;}
}
