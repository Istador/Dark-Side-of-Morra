#pragma strict

var gravity				: float = 0.8;
var maxDownwardSpeed	: float = 20;
var speed				: float = 10;
var jumpPower 			: float = 10;
var velocity			: float = 0;
var InputJump			: boolean = false;
var moveDirection 		: Vector3 = Vector3.zero;
var lookRight 			: boolean = true;
var characterController : CharacterController;


function Start()
{
	characterController = GetComponent(CharacterController);
}

function Update()
{
	InputCheck();
	Move();
}

function InputCheck()
{
	velocity = Input.GetAxis("Horizontal") * speed;
	
	if (velocity > 0)
	{
		lookRight = true;
	}
	else if (velocity < 0)
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
			moveDirection.y = jumpPower;
	}
	
	moveDirection.x = velocity;
	//if(moveDirection.y > -maxDownwardSpeed)
		moveDirection.y -= gravity;
	
	characterController.Move(moveDirection * Time.deltaTime);
}