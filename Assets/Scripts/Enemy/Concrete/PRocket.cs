using UnityEngine;
using System.Collections;

public class PRocket : Projektile<PRocket> {
		
	private Vector3 _targetPos = Vector3.zero;
	public override Vector3 targetPos { get{return _targetPos;} }
	
	public override int damage { get{return 40;} }
	
	public override float maxSpeed { get{return 2.0f;} }
	public override float maxForce { get{return 2.0f;} }
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	protected override void Update() {
		_targetPos = player.transform.position;
		base.Update();
	}
	
	//beim Tod explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		base.Death();
	}
}
