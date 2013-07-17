using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float f = Input.GetAxisRaw("Horizontal");
		this.rigidbody.AddForce(-Vector3.right * f * 5.0f, ForceMode.Acceleration);
	
		
		if(Input.GetButtonDown("Jump") && IsGrounded())
			this.rigidbody.AddForce(Vector3.up * 10.0f, ForceMode.Impulse);
		
	}
	
	bool IsGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, 3.000001f);
	}
	
	void ApplyDamage(int damage){
		Debug.Log(this.tag + ": "+damage+" dmg received");
	}
}
