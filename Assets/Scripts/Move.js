#pragma strict

var gravity			: float = 0.8;
var speed			: float = 10;
var jumpPower 		: float = 10;
var velocity		: float = 0;
var InputJump		: boolean = false;
var moveDirection 	: Vector3 = Vector3.zero;
var lookRight 		: boolean = true;
var characterController : CharacterController;
private var climbSpeed		= 30;
var isClimbing				: boolean;


function Start()
{
	characterController = GetComponent(CharacterController);
}

function Update()
{
	InputCheck();
	Move();
	Climb();
}

function InputCheck()
{
	velocity = Input.GetAxis("Horizontal") * speed;
	
	if (velocity > 0)
	{
		lookRight = true;
	}

	if (velocity < 0)
	{
		lookRight = false;
	}

	if (Input.GetButtonDown("Jump"))
	{
		InputJump = true;
	}

	else
	{
		InputJump = false;
	}

}

function Move()
{
	if (characterController.isGrounded && !isClimbing)
	{
		if (InputJump)
			moveDirection.y = jumpPower;
	}
	
	moveDirection.x = velocity;
	moveDirection.y -= gravity;
	
	characterController.Move(moveDirection * Time.deltaTime);
}
function Climb(){
	if (Input.GetButtonDown("Vertical")&& isClimbing){
						
			moveDirection = Vector3(0,0,Input.GetAxis("Vertical"));
				
           	moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= climbSpeed;
            				
							
			characterController.Move(moveDirection * Time.deltaTime*climbSpeed);	// move character
		
	}

}
function OnTriggerEnter(characterController : Collider){
		
	
			if (characterController.gameObject.CompareTag ("Ladder")){
			
			isClimbing = true;
			gravity = 0;
							// character is on the trigger
	}
	
}

function OnTriggerExit(characterController : Collider){
	  
	if (characterController.gameObject.CompareTag ("Ladder")){
		
		isClimbing = false;
		gravity = 0.8;
									// character is out of the trigger
	}
}
