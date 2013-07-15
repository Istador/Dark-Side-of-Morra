using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour
{
	public void  animate (int columnSize, int rowSize, int colFrameStart, int rowFrameStart, int totalFrames, int framesPerSecond)
	{
		// time control fps
		int index = (int) (Time.time * framesPerSecond);
		// modulate
		index = index % totalFrames;

		// scale
		Vector2 size = new Vector2( (1.0f / columnSize) , (1.0f / rowSize) );
		// offset
		float u = index % columnSize;
		float v = index / columnSize;
		//Debug.Log(u + " " + v);

		Vector2 offset = new Vector2( (u + colFrameStart) * size.x, (1.0f - size.y) - ( (v + rowFrameStart) * size.y) );
		
		// texture scale
		renderer.material.mainTextureScale = size;
		// texture offset
		renderer.material.mainTextureOffset = offset;
	}
}