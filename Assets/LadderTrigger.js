#pragma strict


var characterController		: CharacterController;

private var climbSpeed		= 10;
private var moveDirection 	: Vector3 = Vector3.zero;
var isClimbing				: boolean;

function Start()
{
	characterController = GetComponent(CharacterController);
}
function Update () {
				
	/*if (Input.GetButtonDown("Vertical")){
			Debug.Log("TasteGedrückt");								// check if Button is pushed
				if(isClimbing) {
							Debug.Log("TasteGedrückt und true");	// check if button is bushed and isClimbing is true
							
							
							moveDirection = Vector3(0,Input.GetAxis("Vertical"), 0);
				
           					moveDirection = transform.TransformDirection(moveDirection);
            				moveDirection *= climbSpeed;
							
							characterController.Move(moveDirection * Time.deltaTime*climbSpeed);	// move character
		}
	}*/
	
}


function OnTriggerEnter(characterController : Collider){
		
	
			if (characterController.gameObject.CompareTag ("Ladder")){
			
			isClimbing = true;
			
				Debug.Log("isClimbing = true");					// character is on the trigger
	}
	
}

function OnTriggerExit(characterController : Collider){
	  
	if (characterController.gameObject.CompareTag ("Ladder")){
		
		isClimbing = false;
		Debug.Log("Weg vom Trigger");							// character is out of the trigger
	}
}

