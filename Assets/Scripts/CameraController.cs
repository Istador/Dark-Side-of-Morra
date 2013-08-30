using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject cameraTarget; 			// object to look at / follow
	public float smoothTime		= 0.1f;			// time for camera dampen
	bool  cameraFollowX	 		= true;			// camera follows on horizontal
	bool  cameraFollowY	 		= false;			// camera follows on vertical
	Vector2 velocity;							// speed of camera movement
	float orthoSize;

	Transform thisTransform; 					// cameras transform
	public float cameraHeightOffset	= 3.0f;		// height offset of camera
	public float playerHeight;

	void  Start ()
	{
		thisTransform = transform;
		orthoSize	  = camera.orthographicSize;
		
		cameraTarget = GameObject.FindWithTag("Player");

		StartCoroutine(WaitAndSetPlayerHeight(0.5f));
		
	}


	void  Update ()
	{
		if (cameraFollowX)
		{
			Vector3 temp = thisTransform.position;
			temp.x = Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
			thisTransform.position = temp;
		}
		
		if (cameraFollowY) // only use for vertical levels
		{
			smoothMoveY();
		}
		else		// normal camera follow, based on initial player height and some math
		{
			if (cameraTarget.transform.position.y - playerHeight > 5) // if player moves 5 higher, camera follows
			{
				StartCoroutine(WaitAndSetPlayerHeight(0.5f));
				smoothMoveY();
			}
			if (cameraTarget.transform.position.y - playerHeight < -0.9) // if player moves 1 lower, camera follows
			{
				StartCoroutine(WaitAndSetPlayerHeight(0.5f));
				smoothMoveY();				
			}
		}

	}

	IEnumerator WaitAndSetPlayerHeight(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		playerHeight = cameraTarget.transform.position.y; // stores initial player height for else of cameraFollowY in Update function
	}

	void smoothMoveY()
	{
		Vector3 temp = thisTransform.position;
		temp.y = Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y + cameraHeightOffset, ref velocity.y, smoothTime);
		thisTransform.position = temp;
	}

}