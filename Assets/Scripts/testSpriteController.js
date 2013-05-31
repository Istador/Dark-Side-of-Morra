var columnSize : int = 10;
var rowSize : int = 15;
var colFrameStart : int = 0;
var rowFrameStart : int = 0;
var totalFrames : int = 10;
var framesPerSecond : int = 12;

var move : Move;
var velo : float;
var lookRight : boolean;

function Start()
{
	move = GetComponent(Move);	
}

function Update()
{
	velocity = move.velocity;
	lookRight = move.lookRight;
	
	if (velocity == 0)
	{
		if(lookRight)
		{
			anim("stayRight");
		}
		else
		{
			anim("stayLeft");
		}
	}

	if (velocity < 0)
	{
		anim("moveLeft");
	}

	if (velocity > 0)
	{
		anim("moveRight");
	}

	if (!move.characterController.isGrounded)
	{
		if (lookRight)
		{
			anim("jumpRight");
		}
		else
		{
			anim("jumpLeft");
		}
	}
}

function anim(animType)
{
	var spritePlay = GetComponent("spriteController");

	if (animType == "stayRight")
	{
		rowFrameStart = 0;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "stayLeft")
	{
		rowFrameStart = 1;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);	
	}
	else if (animType == "moveRight")
	{
		rowFrameStart = 2;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "moveLeft")
	{
		rowFrameStart = 3;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "jumpRight")
	{
		rowFrameStart = 4;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "jumpLeft")
	{
		rowFrameStart = 5;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "atackRight")
	{
		rowFrameStart = 6;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else if (animType == "attackLeft")
	{
		rowFrameStart = 7;
		totalFrames = 10;
		spritePlay.spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond);
	}
	else
	{
		Debug.Log("unbekannter animType");
	}
}