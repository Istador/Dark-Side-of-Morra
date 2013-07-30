using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float f = Input.GetAxisRaw("Horizontal");
		rigidbody.AddForce(-Vector3.right * f * 5.0f, ForceMode.Acceleration);
	
		
		if(Input.GetButtonDown("Jump") && IsGrounded())
			rigidbody.AddForce(Vector3.up * 10.0f, ForceMode.Impulse);
		
	}
	
	bool IsGrounded(){
		float height = (renderer.bounds.size.y/2.0f+0.0001f);
		float width = renderer.bounds.size.x/2.0f;
		return Physics.Raycast(transform.position, Vector3.down, height)
			|| Physics.Raycast(transform.position + Vector3.left*width, Vector3.down, height)
			|| Physics.Raycast(transform.position + Vector3.right*width, Vector3.down, height)
			;
	}
	
	void ApplyDamage(int damage){
		Debug.Log(name+"<"+tag+">("+GetInstanceID()+"): "+damage+" dmg received");
	}
}
