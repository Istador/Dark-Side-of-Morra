using UnityEngine;
using System.Collections;

public class AutomGeschuetz : ImmovableEnemy<AutomGeschuetz> {
	
	//zu Dicht zum angreifen
	public static readonly float f_closeRange = 5.0f;
	
	//zu weit entfernt zum angreifen
	public static readonly float f_outOfRange = 25.0f;
	
	//zeit zwischen neuen Raketen
	public static readonly double reloadTime = 2.0; // 2 sekunden nachladen
	
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	
	
	public AutomGeschuetz() : base(250) { //250 HP
		AttackFSM.SetCurrentState(SAGIdle.Instance);
	}
	
	//beim Tod explodieren
	public override void Death(){
				
		//Explosionsanzeige
		GameObject explosion = (GameObject) UnityEngine.Object.Instantiate(Resources.Load("prefab Explosion"), transform.position, transform.rotation);
		UnityEngine.Object.Destroy(explosion, 0.5f); //nach 0.5 sekunden explosion weg
		
		base.Death();
	}
	
	private float _reloadStart = 0.0f;
	public float reloadStart { get{return _reloadStart;} }
	public void BeginReload(){
		_reloadStart = Time.time;
	}
	
}
