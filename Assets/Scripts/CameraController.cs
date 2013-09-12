using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	


	public float smoothTimeX		= 0.1f;			// time for camera dampen
	
	public float smoothTimeY		= 0.3f;			// time for camera dampen
	
	public float cameraHeightOffset	= 3.0f;			// height offset of camera
	
	public int tier0 = 0;
	
	public int tier1 = 10;
	
	public int tier2 = 20;

	private GameObject player;						// object to look at / follow

	private Vector2 velocity;						// speed of camera movement

	private Transform thisTransform;	 			// cameras transform

	

	void  Start ()
	{



		thisTransform = transform;

		player = GameObject.FindWithTag("Player");



	}


	void  Update ()
	{



		smoothMoveX();



		if (player.transform.position.y > tier0 && player.transform.position.y < tier0 + 5)
		{

			setCameraHeight(Mathf.SmoothDamp(thisTransform.position.y, tier0 + 2, ref velocity.y, smoothTimeY));	

		}

		else
		if (player.transform.position.y > tier1 && player.transform.position.y < tier1 + 5)
		{

			setCameraHeight(Mathf.SmoothDamp(thisTransform.position.y, tier1 + 2, ref velocity.y, smoothTimeY));	

		}

		else
		if (player.transform.position.y > tier2 && player.transform.position.y < tier2 + 5)
		{

			setCameraHeight(Mathf.SmoothDamp(thisTransform.position.y, tier2 + 2, ref velocity.y, smoothTimeY));	

		}

		else
		{

			smoothMoveY();

		}
	}




	void smoothMoveX()
	{



		Vector3 temp = thisTransform.position;
		
		temp.x = Mathf.SmoothDamp(thisTransform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		
		thisTransform.position = temp;



	}




	void smoothMoveY()
	{



		Vector3 temp = thisTransform.position;
		
		temp.y = Mathf.SmoothDamp(thisTransform.position.y, player.transform.position.y + cameraHeightOffset, ref velocity.y, smoothTimeY);
		
		thisTransform.position = temp;
	


	}




	void setCameraHeight(float height)
	{



		Vector3 temp = thisTransform.position;

		temp.y = height;

		thisTransform.position = temp;



	}



}