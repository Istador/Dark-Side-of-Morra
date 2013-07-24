using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

	void OnTriggerEnter ( Collider characterController )
	{
		if (characterController.gameObject.CompareTag("Player1"))
		{
			if ( SaveData.levelReached == Application.loadedLevel)
			{
				SaveData.levelReached++;
			}

			SaveLoad.Save();

			Application.LoadLevel(1);
			Debug.Log("Level zu Ende");
		}
	}

}
