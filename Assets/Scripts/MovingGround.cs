using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour
{
	public float leftLimit = 0.0f;
	public float rightLimit = 10.0f;
	private bool moveLeft = true;
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (transform.position.x > leftLimit && moveLeft == true)
		{
			//transform.Translate(Vector3.left * Time.deltaTime);
			rigidbody.AddForce(Vector3.left * 10);
		}
		else
		{
			moveLeft = false;
			if (transform.position.x < rightLimit && moveLeft == false)
			{
				//transform.Translate(Vector3.right * Time.deltaTime);
				rigidbody.AddForce(Vector3.right * 10);
			}
			else
			{
				moveLeft = true;
			}
		}
		if (transform.position.y > 20)
		{
			rigidbody.AddForce(Vector3.down * 100);
		}	
	}
}
