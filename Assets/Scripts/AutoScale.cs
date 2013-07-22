using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour
{
	public Vector3 scale;


	// Use this for initialization
	void Start ()
	{
		scale = transform.localScale;
		renderer.material.mainTextureScale = new Vector2( scale.x, scale.y );
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
