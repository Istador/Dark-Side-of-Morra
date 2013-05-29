function spriteController(columnSize, rowSize, colFrameStart, rowFrameStart, totalFrames, framesPerSecond)
{
	// time control fps
	var index : int = Time.time * framesPerSecond;
	// modulate
	index = index % totalFrames;

	// scale
	var size = Vector2(1.0 / columnSize, 1.0 / rowSize);
	// offset
	var u : int = index % columnSize;
	var v : int = index / columnSize;
	var offset = Vector2( (u + colFrameStart) * size.x, (1 - size.y) - ( (v + rowFrameStart) * size.y) );
	
	// texture scale
	renderer.material.mainTextureScale = size;
	// texture offset
	renderer.material.mainTextureOffset = offset;
}