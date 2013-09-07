using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour
{
	public float teleportRangeUpDown = 9f;
	public float teleportRangeRL = 0f;
	public bool up = true;
	public bool down;
	public bool right;
	public bool left;
	private GameObject player;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	void OnTriggerEnter ( Collider characterController )
	{
		if (characterController.gameObject.CompareTag("Player"))
		{
			if (up)
			{
				Vector3 temp = player.transform.position;
				temp.y += teleportRangeUpDown;
				player.transform.position = temp;
			}
			else
			if (down)
			{
				Vector3 temp = player.transform.position;
				temp.y -= teleportRangeUpDown;
				player.transform.position = temp;	
			}

			if (right)
			{
				Vector3 temp = player.transform.position;
				temp.x += teleportRangeRL;
				player.transform.position = temp;
			}
			else
			if (left)
			{
				Vector3 temp = player.transform.position;
				temp.x -= teleportRangeRL;
				player.transform.position = temp;
			}
		}
	}
}
