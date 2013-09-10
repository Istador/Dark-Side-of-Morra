using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour
{	
	public void Rescale()
	{
		// nimm die Skalierung vom Gameobject
		Vector3 s = transform.localScale;
		// und skalier anhand dessen die Textur
		renderer.material.mainTextureScale = new Vector2( s.x, s.y );
	}
}
