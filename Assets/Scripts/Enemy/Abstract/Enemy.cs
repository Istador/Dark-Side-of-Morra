using UnityEngine;
using System.Collections;

public abstract class Enemy<T> : MonoBehaviour, MessageReceiver {
	
	
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
	
	/**
	 * Zustandsautomaten für Angriff und Bewegung
	*/
	private StateMachine<Enemy<T>> moveFSM;
	private StateMachine<Enemy<T>> attackFSM;
	public StateMachine<Enemy<T>> MoveFSM {get{return moveFSM;}}
	public StateMachine<Enemy<T>> AttackFSM {get{return attackFSM;}}
	
	/*
	 * Konstruktor
	*/
	private Enemy(){}
	public Enemy(int txtCols, int txtRows, int txtFPS, int maxHealth){
		this.maxHealth = maxHealth;
		this.txtCols = txtCols; 
		this.txtRows = txtRows;
		this.txtFPS = txtFPS;
		
		//Zustandsmaschinen erstellen
		moveFSM = new StateMachine<Enemy<T>>(this);
		attackFSM = new StateMachine<Enemy<T>>(this);
	}
	
	
	protected virtual void Start () {
		health = maxHealth;
		//SpriteController hinzufügen
		spriteCntrl = gameObject.AddComponent<SpriteController>();
		
		moveFSM.Start();
		attackFSM.Start();
	}
	
	protected virtual void Update () {
		moveFSM.Update();
		attackFSM.Update();
		//Animation
		spriteCntrl.animate(txtCols, txtRows, 0, txtState, txtCols, txtFPS);
	}
	
	public bool HandleMessage(Telegram msg){
		bool tmp = moveFSM.HandleMessage(msg);
		return attackFSM.HandleMessage(msg) || tmp;
	}
	
	public virtual void ApplyDamage(int damage){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+damage+" dmg received");
		health -= damage;
		if(health <= 0) Death();
	}
	
	public virtual void Death(){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): death");
		Destroy(gameObject);
	}
	
	public void SetSprite(int row){txtState = row;}
	
	
	public void SetVisible(){
		renderer.enabled = true;
	}
	
	public void SetInvisible(){
		renderer.enabled = false;
	}
	
	public float DistanceToPlayer(){
		GameObject player = GameObject.FindWithTag("Player");
		return Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));
	}
}
