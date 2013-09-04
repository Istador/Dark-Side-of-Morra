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
	/*void OnGUI () {
		GUI.Label (new Rect (25, 25, 100, 30), new GUIContent("healthbar1"));
	}
	 */
	// Health vom Spieler aufrufen
	private int hp;
	void Start(){
	 hp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().currentHealth;
	}
	private int MAX_HEALTH = 100;
	
	void OnGUI (){
		if (MAX_HEALTH <= hp && hp>=90){
			GUI.Label(new Rect(100, 1100, 100, 100),healthBar1); // bin mir nicht sicher, ob mit new GUIContent oder einfach nur so, geht aber beides nicht :D
		}
		if (89 <= hp && hp>=80){
			GUI.Label(new Rect(25, 25, 100, 30),healthBar2);
		}
		if (79 <= hp && hp>=70){
			GUI.Label(new Rect(25, 25, 100, 30),healthBar3);
		}
		if (69 <= hp && hp>=60){
			GUI.Label(new Rect(25, 25, 100, 30), new GUIContent("healthBar4"));
		}
	}
}
