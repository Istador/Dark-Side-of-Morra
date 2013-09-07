using UnityEngine;
using System.Collections;

public class Spider : MLeftRight<Spider> {
	
	
	
	public override float maxSpeed { get{return 3.0f;} }
	public override float maxForce { get{return 3.0f;} }
	
	
	
	protected override int txtCols { get{return 1;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 1;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 1;} }  //Frames per Second
	
	
	
	/// <summary>
	/// Entfernung bei welcher der Spieler zu weit entfernt ist zum Angreifen
	/// </summary>
	public static readonly float f_outOfRange = 2.0f;
	
	/// <summary>
	/// Nachladezeit:
	/// Die Zeit die für den Angriff benötigt wird
	/// </summary>
	public static readonly double d_attackTime = 1.0; // 1,0 sekunden
	
	
	
	
	public Spider() : base(1000){
		MoveFSM.SetCurrentState(SSpiderKokon.Instance);
	}
	
	
	
	protected override void Start(){
		base.Start();
	}
}
