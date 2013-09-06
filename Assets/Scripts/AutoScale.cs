using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour
{
	public Vector3 scale;


	// Use this for initialization
	void Start ()
	{
		Rescale();
	}
	
	public void Rescale(){
		Vector3 s = transform.localScale;
		renderer.material.mainTextureScale = new Vector2( s.x, s.y );
	}
}
