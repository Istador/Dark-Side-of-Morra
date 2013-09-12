using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{



	public int levelToLoadNext = 0;



	void OnTriggerEnter ( Collider hit )
	{



		if (hit.gameObject.tag == "Player")
		{

			//wenn das erste mal dieses Level bestanden wurde
			if( SaveData.levelReached == Application.loadedLevel )
			{

				//Verändere den Wert
				SaveData.levelReached++;

			}
			
			//Speichern
			SaveLoad.Save();
			
			//Nachrichtensystem zurücksetzen
			MessageDispatcher.I.EmptyQueue();
			
			//zur Level-Auswahl
			Application.LoadLevel(levelToLoadNext);
			
			Debug.Log("Level zu Ende");

		}



	}



}