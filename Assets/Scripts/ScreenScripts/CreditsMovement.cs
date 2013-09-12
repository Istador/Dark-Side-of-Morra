using UnityEngine;
using System.Collections;

public class CreditsMovement : MonoBehaviour
{


	
	private float verticalLimit = 3.5f;
	
	private float horizontalLimit = 6.5f;

	private Quaternion rotation;


	void Start()
	{
		rotation = rigidbody.rotation;
	}



	void FixedUpdate()
	{



		rigidbody.rotation = rotation;

		if (rigidbody.position.y > verticalLimit)
		{

			rigidbody.AddForce(Vector3.down * 10);

		}

		if (rigidbody.position.y < -verticalLimit)
		{

			rigidbody.AddForce(Vector3.up * 10);

		}		

		if (rigidbody.position.y > horizontalLimit)
		{

			rigidbody.AddForce(Vector3.left * 10);

		}

		if (rigidbody.position.y < -horizontalLimit)
		{

			rigidbody.AddForce(Vector3.right * 10);

		}



	}
}
