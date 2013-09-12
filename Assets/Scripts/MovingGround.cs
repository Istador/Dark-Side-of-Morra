using UnityEngine;
using System.Collections;

public class MovingGround : MonoBehaviour
{
	public float leftLimit = 0.0f;
	public float rightLimit = 10.0f;
	public float movingSpeed = 2.0f;
	private bool moveLeft = true;
	private bool PlayerIsOnGround = false;

	// Spieler Controller Variablen
	private PlayerController playerController;
	private CharacterController characterController;
	
	void Start ()
	{
		// Zuweisung des Spielers
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		characterController = playerController.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (transform.position.x > leftLimit && moveLeft == true)
		{
			transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
		}
		else
		{
			moveLeft = false;
			if (transform.position.x < rightLimit && moveLeft == false)
			{
				transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
			}
			else
			{
				moveLeft = true;
			}
		}

		if (PlayerIsOnGround)
		{
			if(moveLeft)
			{
				characterController.Move(Vector3.left * movingSpeed * Time.deltaTime);
			}
			else
			{
				characterController.Move(Vector3.right * movingSpeed * Time.deltaTime);	
			}
		}

		Debug.Log(PlayerIsOnGround);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerIsOnGround = true;	
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerIsOnGround = false;	
		}
	}
}
