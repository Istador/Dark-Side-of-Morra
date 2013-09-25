using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour
{



	private static System.Random rnd = new System.Random();
	
	public int index {get; private set;}
	
	//random positive integer -> random animation position for different entities
	public readonly int rndCol = System.Math.Abs(rnd.Next());
	
	
	
	public void  animate (int columnSize, int rowSize, int colFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond)
	{		



		// time control fps
		index = (int) (Time.time * framesPerSecond);

		// modulate
		index %= totalFrames;

		// scale
		Vector2 size = new Vector2( (1.0f / columnSize) , (1.0f / rowSize) );

		// offset
		float u = (index + (rndCol % columnSize )) % columnSize;
		
		//float v = (float) index /  (float) columnSize;
			
		Vector2 offset = new Vector2( (u + colFrameStart) * size.x, (1.0f - size.y) - ( (/*v + */rowFrameStart) * size.y) );
		
		// texture scale
		renderer.material.mainTextureScale = size;

		// texture offset
		renderer.material.mainTextureOffset = offset;



	}



}