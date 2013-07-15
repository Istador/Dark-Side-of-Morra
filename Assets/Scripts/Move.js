#pragma strict

var gravity			: float = 0.8;
var speed			: float = 10;
var jumpPower 		: float = 10;
var velocity		: Vector2 = Vector2.zero;
var moveDirection	: Vector2 = Vector2.zero;
var InputJump		: boolean = false;
var isOnLadder		: boolean = false;
var lookRight 		: boolean = true;
var characterController : CharacterController;
private var climbSpeed		= 30;

function Start()
{
	characterController = GetComponent(CharacterController);
}

function Update()
{
	InputCheck();
	Move();
	//Climb();
}

function InputCheck()
{
	velocity.x = Input.GetAxis("Horizontal") * speed;
	velocity.y = Input.GetAxis("Vertical") * speed;

	if (velocity.x > 0)
	{
		lookRight = true;
	}

	if (velocity.x < 0)
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
	if (characterController.isGrounded)
		{
			if (InputJump)
			{
				moveDirection.y = jumpPower;
			}
		}

	if (isOnLadder)
	{
		moveDirection.y = velocity.y;
	}
	else
	{
		if (moveDirection.y > -20)
		{
			moveDirection.y -= gravity;
		}
	}

	moveDirection.x = velocity.x;

	characterController.Move(moveDirection * Time.deltaTime);	
}

/*
function Climb()
{
	if (Input.GetButtonDown("Vertical")&& isOnLadder)
	{
		moveDirection = Vector3(0,0,Input.GetAxis("Vertical"));

       	moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= climbSpeed;

		characterController.Move(moveDirection * Time.deltaTime*climbSpeed);	// move character
	}

}
*/

function OnTriggerEnter(characterController : Collider)
{
	if (characterController.gameObject.CompareTag ("Ladder"))
	{
	
		isOnLadder = true;
		// character is on the trigger
	}
}

function OnTriggerExit(characterController : Collider)
{
	if (characterController.gameObject.CompareTag ("Ladder"))
	{
		isOnLadder = false;
		// character is out of the trigger
	}
}