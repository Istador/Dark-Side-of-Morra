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
	
			
		
	 				if (Input.GetButtonDown("Vertical")){
	 					Debug.Log("Hoch ist gedrückt.");
							if(isClimbing) {
								Debug.Log("climbing in move");
							moveDirection.y = climbSpeed;
							characterController.Move(moveDirection * Time.deltaTime);	
		}
	}
	
}


function OnTriggerEnter(characterController : Collider){
	Debug.Log("Spieler berührt Trigger");
	
	
			if (characterController.gameObject.CompareTag ("Ladder")){
			
			isClimbing = true;
			
				Debug.Log("isClimbing = true");
	}
	
}

function OnTriggerExit(characterController : Collider){
	
	if (characterController.gameObject.CompareTag ("Ladder")){
		
		isClimbing = false;
		Debug.Log("Weg vom Trigger");
	}
}
