function Update()
{
	var spritePlay = GetComponent("spriteController");

	if (Input.GetKey("d"))
	{
		spritePlay.spriteController(10,1,0,0,10,12);
	}
}