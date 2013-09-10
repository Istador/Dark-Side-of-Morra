using UnityEngine;
using System.Collections;

public class Stachel : MLeftRight<Stachel> {
	
	
	
	public override float maxSpeed { get{return 6.0f;} }
	public override float maxForce { get{return 6.0f;} }
	
	
	
	public Stachel() : base(150){}
	
	
	
	protected override void Start(){
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 2;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
	}
	
	
}
