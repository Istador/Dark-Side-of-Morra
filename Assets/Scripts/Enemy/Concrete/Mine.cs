using UnityEngine;
using System.Collections;

public class Mine : ImmovableEnemy<Mine> {
	
	public static readonly float f_yellowRange = 6.0f;
	public static readonly float f_redRange = 3.0f;
	public static readonly float f_explosionRadius = 4.0f;
	public static readonly float f_explosionDamage = 75.0f;
	
	protected override int txtCols { get{return 2;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 3;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 5;} }  //Frames per Second
	
	public Mine() : base(1) { //1 HP
		AttackFSM.SetCurrentState(SMineInvisible.Instance);
	}
	
	void OnTriggerEnter(Collider other) {
		//Kollision nur mit Spieler
		if(other.tag == "Player"){
			Explode();
			GetComponent<BoxCollider>().enabled = false;
		}
	}
	
	public override void ApplyDamage(int damage){
		//Schaden immer
		Explode();
	}
	
	
	private void Explode(){
		//sende eine Nachricht an den Zustandsautomat um in den roten zustand zu gehen.
		MessageDispatcher.Instance.Dispatch(new Telegram(this, "explode"));
	}
}
