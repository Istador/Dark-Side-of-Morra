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
	
	
	
	protected override void Start(){
		base.Start();
		
		//Sprite-Eigenschaften
		txtCols = 4;
		txtRows = 1;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		if(ac_explosion == null) ac_explosion = (AudioClip) Resources.Load("Sounds/explode");
	}
	
	
	
	protected override void Update() {
		//Wenn der Spieler in Sicht ist
		if(LineOfSight(Player)){
			//Strebe die Position des Spielers an
			_targetPos = PlayerPos;
			//heading = (_targetPos - collider.bounds.center).normalized;
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.green);
		}
		else
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.red);
		
		base.Update();
		
		//Ziel erreicht, wenn Spieler z.B. nicht mehr sichtbar
		if(DistanceTo(_targetPos) < 0.1){
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
