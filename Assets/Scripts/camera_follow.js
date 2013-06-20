#pragma strict

var cameraTarget			: GameObject; // object to look at / follow


var smoothTime 				: float		 = 0.2;				// time for camera dumpen
var cameraFollowX			: boolean	 = true;			// camera follows on horizontal
var cameraFollowY			: boolean	 = true;			// camera follows on vertical
private var cameraHeight	: float		 = 1.8;				//  height of camera
var velocity 				: Vector2;						// speed of camera movement
private var thisTransform	: Transform; 					// cameras transform
var orthoSize				: float;


function Start () {
	thisTransform = transform;
	orthoSize	  = camera.orthographicSize;
	
}


function Update () {
	if (cameraFollowX)
	{
		
			
		thisTransform.position.x = Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, velocity.x,smoothTime);
	}
	
	if (cameraFollowY)
	{
		if(cameraTarget.transform.position.y >= orthoSize-3 && cameraTarget.transform.position.y <= orthoSize+3 ){
			thisTransform.position.y = Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y + cameraHeight, velocity.y,smoothTime);
		}
	}
	

}