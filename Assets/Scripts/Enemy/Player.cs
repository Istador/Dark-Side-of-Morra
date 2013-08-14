using UnityEngine;
using System.Collections;

/*
 * Eine mögliche Beispielimplementation des Spielers mit rigidbody.
 * 
 * Einfach gehalten zum Testen der K.I.
 * 
*/
public class Player : MonoBehaviour {
	
	
	
	/// <summary>Update is called once per frame</summary>
	void Update() {
		/*
		Vector3 pos = collider;
		float height = (renderer.bounds.size.y/2.0f+0.0001f);
		float width = renderer.bounds.size.x/2.0f;
		Debug.DrawRay(pos, Vector3.down*height, Color.red);
		Debug.DrawRay(pos + Vector3.left*width, Vector3.down*height, Color.red);
		Debug.DrawRay(pos + Vector3.right*width, Vector3.down*height, Color.red);
		*/
		
		float f = Input.GetAxisRaw("Horizontal");
		rigidbody.AddForce(Vector3.right * f * 4.0f, ForceMode.Acceleration);
	
		
		if(Input.GetButtonDown("Jump") && IsGrounded())
			rigidbody.AddForce(Vector3.up * 7.0f, ForceMode.Impulse);
		
	}
	
	
	
	/// <summary>
	/// Determines whether the player is grounded.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is grounded; otherwise, <c>false</c>.
	/// </returns>
	bool IsGrounded(){
		/*
		 * Diese Methode erstellt 3 RayCasts um zu überprüfen ob eine davon mit 
		 * dem Boden kollidiert.
		*/
		int layer = 1<<8 | 1<<9; //nur mit Level und Enemy
		float height = (renderer.bounds.size.y/2.0f+0.0001f);
		float width = renderer.bounds.size.x/2.0f;
		
		
		return Physics.Raycast(transform.position, Vector3.down, height, layer)
			|| Physics.Raycast(transform.position + Vector3.left*width, Vector3.down, height, layer)
			|| Physics.Raycast(transform.position + Vector3.right*width, Vector3.down, height, layer)
			;
	}
	
	
	
	/// <summary>
	/// Schaden erhalten, der die HP verringert, und zum Tode führen kann
	/// </summary>
	/// <param name='damage'>
	/// Schaden der dem Spieler zugefügt wird
	/// </param>
	void ApplyDamage(int damage){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+damage+" dmg received");
	}
	
	
	
}
