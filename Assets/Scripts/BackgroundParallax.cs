using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{



	public CharacterController cc;

	public float speedFactor = 1;

	private Vector3 velocity;



	void  Update ()
	{



		velocity.x = cc.velocity.x * speedFactor;

		transform.Translate(velocity * Time.deltaTime);



	}
}