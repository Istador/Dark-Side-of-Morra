var columnSize		: int = 10;
var rowSize			: int = 15;
var colFrameStart	: int =  0;
var rowFrameStart	: int =  0;
var totalFrames		: int = 10;
var framesPerSecond	: int = 12;

var move 			: Move;
var velo 			: float;
var lookRight 		: boolean;

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
			anim(AnimationTypes.stayRight);
		}
		else
		{
			anim(AnimationTypes.stayLeft);
		}
	}

	if (velocity < 0)
	{
		anim(AnimationTypes.moveLeft);
	}

	if (velocity > 0)
	{
		anim(AnimationTypes.moveRight);
	}

	if (!move.characterController.isGrounded)
	{
		if (lookRight)
		{
			anim(AnimationTypes.jumpRight);
		}
		else
		{
			anim(AnimationTypes.jumpLeft);
		}
	}
}

enum AnimationTypes
{
	// Zahl ist für rowFrameStart, d.h. Auswahl der Reihe in welcher die Sprites liegen.
	stayRight	=  0,
	stayLeft	=  1,
	moveRight	=  2,
	moveLeft	=  3,
	jumpRight	=  4,
	jumpLeft	=  5,
	attackRight	=  6,
	attackLeft	=  7,
	anim1Right	=  8,
	anim1Left	=  9,
	anim2Right	= 10,
	anim2Left	= 11,
	other1		= 12,
	other2		= 13,
	other3		= 14
}

function anim(animType)
{
	var spritePlay = GetComponent("spriteController");

	spritePlay.spriteController(columnSize, rowSize, colFrameStart, animType, totalFrames, framesPerSecond);
}