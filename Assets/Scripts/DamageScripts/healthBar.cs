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
	/*void OnGUI () {
		GUI.Label (new Rect (25, 25, 100, 30), new GUIContent("healthbar1"));
	}
	 */
	// Health vom Spieler aufrufen
	
	public GameObject player;
	PlayerController playerController;
	
	void Start(){
	 playerController = player.GetComponent<PlayerController>();
	}
	private int MAX_HEALTH = 100;
	
	
	
	void OnGUI (){
		
		int hp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().currentHealth;
		
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
		
			GUI.Label(new Rect(100, 100, 100, 30),healthBar7);
		}
	}
}
