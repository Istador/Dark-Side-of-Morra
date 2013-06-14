#pragma strict

var cameraTarget			: GameObject; // object to look at / follow


var smoothTime 				: float		 = 0.1;				// time for camera dumpen
var cameraFollowX			: boolean	 = true;			// camera follows on horizontal
var cameraFollowY			: boolean	 = true;			// camera follows on vertical
var cameraHeight			: float		 = 2.5;				//  height of camera, adjustable in the inspector
var velocity 				: Vector2;						// speed of camera movement
private var thisTransform	: Transform; 					// cameras transform


function Start () {
	thisTransform = transform;
	
}


function Update () {
	if (cameraFollowX)
	{
		
			
		thisTransform.position.x = Mathf.SmoothDamp(thisTransform.position.x, cameraTarget.transform.position.x, velocity.x,smoothTime);
	}
	
	if (cameraFollowY)
	{
		if(cameraTarget.transform.position.y >= 3){
			thisTransform.position.y = Mathf.SmoothDamp(thisTransform.position.y, cameraTarget.transform.position.y, velocity.y,smoothTime);
		}
	}
	

}