using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{

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
			Application.LoadLevel(1);
			
			Debug.Log("Level zu Ende");
		}
	}
}