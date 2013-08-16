// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject cameraTarget; 			// object to look at / follow
	public float smoothTime		= 0.2f;			// time for camera dumpen
	bool  cameraFollowX	 		= true;			// camera follows on horizontal
	bool  cameraFollowY	 		= true;			// camera follows on vertical
	Vector2 velocity;							// speed of camera movement
	public float orthoSize;

	private Transform thisTransform; 			// cameras transform
	private float cameraHeight	= 1.8f;			// height of camera

	void  Start ()
	{
		thisTransform = transform;
		orthoSize	  = camera.orthographicSize;
	}


	void  Update ()
	{
		if (cameraFollowX)
		{
			Vector3 temp = thisTransform.position;
			temp.x = Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, ref velocity.x, smoothTime);
			thisTransform.position = temp;
		}
		
		if (cameraFollowY)
		{
			//if(cameraTarget.transform.position.y >= orthoSize-3 && cameraTarget.transform.position.y <= orthoSize+3 )
			//{
				Vector3 temp = thisTransform.position;
				temp.y = Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y + cameraHeight, ref velocity.y,smoothTime);
				thisTransform.position = temp;
			//}
		}
	}

}