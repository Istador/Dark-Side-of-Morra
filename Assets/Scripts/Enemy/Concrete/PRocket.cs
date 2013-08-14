using UnityEngine;
using System.Collections;

public class PRocket : Projektile<PRocket> {
	
	
	
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	
	
	
	public override int damage { get{return 40;} }
	
	
	
	public override float maxSpeed { get{return 0.8f;} }
	public override float maxForce { get{return 0.8f;} }
	
	
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	
	
	protected override void Update() {
		//Wenn der Spieler in Sicht ist
		if(LineOfSight(player)){
			//Strebe die Position des Spielers an
			_targetPos = player.collider.bounds.center;
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.green);
		}
		else
			Debug.DrawLine(collider.bounds.center, _targetPos, Color.red);
		
		base.Update();
	}
	
	
	
	//Überschreiben um beim Tod zu explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		//TODO : Soundgeräusch
		
		base.Death();
	}
	
	
	
}
