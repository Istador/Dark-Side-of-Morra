using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour
{
	
	
	
	private static System.Random rnd = new System.Random();
	
	public int index {get; private set;}
	//public int index {public get{return _index;}}
	
	
	//random positive integer -> random animation position for different entities
	public readonly int rndCol = System.Math.Abs(rnd.Next());
	
	
	
	public void  animate (int columnSize, int rowSize, int colFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond)
	{
		/*
		 * TODO
		 * 
		 * Ist columnSize nicht eigtl. das selbe wie totalFrames ?
		 * 
		 * Ist colFrameStart nicht überflüssig weil es immer 0 ist ?
		 */
		
		// time control fps
		index = (int) (Time.time * framesPerSecond);
		// modulate
		index %= totalFrames;

		// scale
		Vector2 size = new Vector2( (1.0f / columnSize) , (1.0f / rowSize) );
		// offset
		float u = (index + (rndCol % columnSize )) % columnSize;
		
		//float v = index / columnSize; //v is always 0 because both are int and index < columnSize
		//it also makes no sense to animate on the X axis AND on the Y axis simultaneously
				
		Vector2 offset = new Vector2( (u + colFrameStart) * size.x, (1.0f - size.y) - ( (rowFrameStart) * size.y) );
		
		// texture scale
		renderer.material.mainTextureScale = size;
		// texture offset
		renderer.material.mainTextureOffset = offset;
	}
}