using UnityEngine;
using System.Collections;

public class EinhornAnimation : MonoBehaviour {
	
	public int columnSize		= 10;
	public int rowSize			= 15;
	public int colFrameStart	=  0;
	private int animType 		= 0;
	public int totalFrames		= 11;
	public int framesPerSecond	= 12;
	
	
	
	void Update()
	{
	
	
		SpriteController spritePlay;
		spritePlay = GetComponent<SpriteController>();
		spritePlay.animate(columnSize, rowSize, colFrameStart, animType, totalFrames, framesPerSecond);
	}

}