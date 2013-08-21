using UnityEngine;
using System.Collections;

public class Stachel : MLeftRight<Stachel> {
	
	
	
	public override float maxSpeed { get{return 2.5f;} }
	public override float maxForce { get{return 2.5f;} }
	
	
	
	protected override int txtCols { get{return 8;} } //Anzahl Spalten (Frames)
	protected override int txtRows { get{return 2;} } //Anzahl Zeilen (Zustände)
	protected override int txtFPS { get{return 4;} }  //Frames per Second
	
	
	
	public Stachel() : base(100){}
	
	
	
}
