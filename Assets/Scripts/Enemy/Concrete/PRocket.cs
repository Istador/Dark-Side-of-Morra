using UnityEngine;
using System.Collections;

public class PRocket : Projektile<PRocket> {
	
	
	//private Vector3 heading = Vector3.zero;
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	//public override Vector3 targetPos { get{return collider.bounds.center + heading * maxSpeed;} }
	
	
	
	public override int damage { get{return 40;} }
	
	
	
	/// <summary>Explosionsgeräusch</summary>
	public static AudioClip ac_explosion;
	
	
	
	public override float maxSpeed { get{return 2.4f;} }
	public override float maxForce { get{return 2.4f;} }
	
	
	
	protected override int txtCols { get{return 4;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 6;} }  //Frames per Second
	
	
	
	protected override void Start(){
		base.Start();
		
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
	}
	
	
	
	protected override void Update() {
		//Wenn der Spieler in Sicht ist
		if(LineOfSight(player)){
			//Strebe die Position des Spielers an
			_targetPos = player.collider.bounds.center;
			//heading = (_targetPos - collider.bounds.center).normalized;
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.green);
		}
		else
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.red);
		
		base.Update();
		
		//Ziel erreicht, wenn Spieler z.B. nicht mehr sichtbar
		if(DistanceTo(_targetPos) < 0.01){
			Death();
		}
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab kleineExplosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		//Explosionsgeräusch
		AudioSource.PlayClipAtPoint(ac_explosion, collider.bounds.center);
		
		base.Death();
	}
	
	
	
}
