using UnityEngine;
using System.Collections;

public class Stachel : MLeftRight<Stachel> {
	
	
	
	public Stachel() : base(150){}
	
	
	
	protected override void Start(){
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 2;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 6.0f;
		MaxForce = 6.0f;
	}
	
	
}
