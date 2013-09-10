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
			transform.Translate(Vector3.left * Time.deltaTime);
		}
		else
		{
			moveLeft = false;
			if (transform.position.x < rightLimit && moveLeft == false)
			{
				transform.Translate(Vector3.right * Time.deltaTime);
			}
			else
			{
				moveLeft = true;
			}
		}
	}
}
