﻿using UnityEngine;
using System.Collections;

/// 
/// Trigger der den zweiten Dialog mit dem pinken Einhorn startet,
/// sowie anschließend dem Level bescheid gibt die Spinne 
/// ausschlüpfen lassen zu können.
/// 
public class unicorn2 : MonoBehaviour {
	
	void Start(){
		//Referenz auf Dialog laden
		Dialog d = GetComponent<Dialog>();
		
		//Code der ausgeführt wird nach dem Dialog
		d.postDialog = (GameObject obj) => {
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht ans Level, dass der Dialog beendet ist
			MessageDispatcher.I.Dispatch(level, "dialog2");
		};
	}
	
}
