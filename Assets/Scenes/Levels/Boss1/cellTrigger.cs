using UnityEngine;
using System.Collections;

/// 
/// Trigger der das Öffnen der Zellentür mittels Schlüssel durch den Spieler ausführt
/// 
/// Implementiert als Dialog:
///   Vor dem Dialog wird die Zelle geöffnet, 
///   und danach wird dieses Level beendet.
/// 
public class cellTrigger : MonoBehaviour {
	
	void Start(){
		//Referenz auf Dialog laden
		Dialog d = GetComponent<Dialog>();
		
		
		//Code der ausgeführt wird vor dem Dialog
		d.preDialog = (GameObject obj) => {
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht ans Level, dass die Zelle geöffnet werden soll
			MessageDispatcher.I.Dispatch(level, "cellOpened");
		};
		
		
		//Code der ausgeführt wird nach dem Dialog
		d.postDialog = (GameObject obj) => {
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht ans Level, dass der Dialog vorbei ist
			MessageDispatcher.I.Dispatch( level, "dialog3");
			//sich selbst löschen
			Destroy(obj);
		};
	}
	
}
