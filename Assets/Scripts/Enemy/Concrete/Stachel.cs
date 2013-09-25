using UnityEngine;
using System.Collections;


/// 
/// Der Stachel bewegt sich immer von Links nach Rechts auf einer Platform
/// ohne jegliche Intelligenz.
/// 
/// Diese Klasse ist so leer, weil das Patroulieren als Standard-Verhalten
/// in der MLeftRight Klasse implementier ist, und der Stachel selbst keinen 
/// Schaden macht, sondern diesen über seinen PlayerCollider verursacht.
/// 
public class Stachel : MLeftRight<Stachel> {
	
	
	
	public Stachel() : base(150){} //150 HP
	
	
	
	protected override void Start(){
		//Sprite-Eigenschaften
		txtCols = 10;
		txtRows = 2;
		txtFPS = 6;
		
		//SpriteController einschalten
		Animated = true;
		
		//Geschwindigkeit setzen
		MaxSpeed = 7.2f;
		MaxForce = 7.2f;
	}
	
	
	
}
