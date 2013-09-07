using UnityEngine;
using System.Collections;

public class Stair : MonoBehaviour
{
	public bool upstairs = true;
	private float quaternionAngleW = 0.9230264f;
	private float quaternionAngleZ = 0.3847366f;

	// Use this for initialization
	void Start ()
	{
		if (upstairs)
		{
			Quaternion temp = transform.rotation;
			temp.w = quaternionAngleW;
			temp.z = -quaternionAngleZ;
			transform.rotation = temp;
			Debug.Log(transform.rotation);

			Vector3 tempPos = transform.position;
			tempPos.x += 0.750939f;
			tempPos.y += 0.099053f;
			transform.position = tempPos;
		}
		else
		{
			Quaternion temp = transform.rotation;
			temp.w = quaternionAngleW;
			temp.z = quaternionAngleZ;
			transform.rotation = temp;

			Vector2 tmp = renderer.material.mainTextureScale;
			tmp = new Vector2(-tmp.x, tmp.y);
			renderer.material.mainTextureScale = tmp;

			Vector3 tmpPos = transform.position;
			tmpPos.x += 0.207352f;
			tmpPos.y += 0.083782f;
			transform.position = tmpPos;
		}
	}
}
