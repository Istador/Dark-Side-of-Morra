using UnityEngine;
using System.Collections;

public class TextureMirror : MonoBehaviour
{
	public bool mirrored = false;

	// Use this for initialization
	void Start ()
	{
		if(mirrored){
			//Textur vertikal spiegeln
			Vector2 tmp = renderer.material.mainTextureScale;
			tmp = new Vector2(-tmp.x, tmp.y);
			renderer.material.mainTextureScale = tmp;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
