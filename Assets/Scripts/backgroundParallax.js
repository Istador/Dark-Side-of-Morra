#pragma strict

var cc : CharacterController;
var speedFactor : float = 1;

var velocity : Vector3;

function Update()
{
	velocity.x = cc.velocity.x * speedFactor;
	transform.Translate(velocity * Time.deltaTime);
}