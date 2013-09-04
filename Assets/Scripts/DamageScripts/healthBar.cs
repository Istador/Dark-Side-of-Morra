using UnityEngine;
using System.Collections;

public class healthBar : MonoBehaviour {

	// Rezept
	// GUI der Healthbar darstellt
	// 10 verschiedene Texturen, die immer in 10er Schritten ein neues darstellt
	public Texture2D healthBar1;
	public Texture2D healthBar2;
	public Texture2D healthBar3;
	public Texture2D healthBar4;
	public Texture2D healthBar5;
	public Texture2D healthBar6;
	public Texture2D healthBar7;
	public Texture2D healthBar8;
	public Texture2D healthBar9;
	public Texture2D healthBar10;
	public Texture2D healthBar11;
	/*void OnGUI () {
		GUI.Label (new Rect (25, 25, 100, 30), new GUIContent("healthbar1"));
	}
	 */
	// Health vom Spieler aufrufen
	
	private PlayerController pc;
	
	void Start(){
	 pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	
	
	
	void OnGUI (){
		
		int hp = pc.currentHealth;
		
		int factor = (int) ( ((float)hp) / ((float)PlayerController.MAX_HEALTH) * 10.0f );
		
		Texture txt = null;
		switch(factor){
			case 10: default:
			case  9: txt = healthBar1; break;
			case  8: txt = healthBar2; break;
			case  7: txt = healthBar3; break;
			case  6: txt = healthBar4; break;
			case  5: txt = healthBar5; break;
			case  4: txt = healthBar6; break;
			case  3: txt = healthBar7; break;
			case  2: txt = healthBar8; break;
			case  1: txt = healthBar9; break;
			case  0: txt = healthBar10; break;
			
		}
		
		GUI.Label(new Rect(100, 100, 100, 30), txt);
		
		/*
		if (MAX_HEALTH <= hp && hp>=90){
			GUI.Label(new Rect(100, 100, 100, 100),healthBar1); // bin mir nicht sicher, ob mit new GUIContent oder einfach nur so, geht aber beides nicht :D
		}
		if (89 <= hp && hp>=80){
			
			GUI.Label(new Rect(100, 100, 100, 30),healthBar2);
		}
		if (79 <= hp && hp>=70){
			
			GUI.Label(new Rect(100,100, 100, 30),healthBar3);
		}
		if (69 <= hp && hp>=60){
			
			GUI.Label(new Rect(100, 100, 100, 30),healthBar4);
		}
		if (59 <= hp && hp>=50){
			
			GUI.Label(new Rect(100, 100, 100, 30),healthBar5);
		}
		if (49 <= hp && hp>=40){
			
			GUI.Label(new Rect(100, 100, 100, 30),healthBar6);
		}
		if (39 <= hp && hp>=30){
		
			GUI.Label(new Rect(100, 100, 100, 30),healthBar8);
		}
		if (29 <= hp && hp>=20){
		
			GUI.Label(new Rect(100, 100, 100, 30),healthBar9);
		}
		if (19 <= hp && hp>=10){
		
			GUI.Label(new Rect(100, 100, 100, 30),healthBar10);
		}
		if (9 <= hp && hp>=1){
		
			GUI.Label(new Rect(100, 100, 100, 30),healthBar11);
		}
		*/
		
	}
}
