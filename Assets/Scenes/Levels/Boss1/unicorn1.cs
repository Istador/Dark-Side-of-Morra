﻿using UnityEngine;
using System.Collections;

/// 
/// Trigger der den ersten Dialog mit dem blauem Einhorn startet,
/// sowie anschließend dem Level bescheid gibt um die Tür zum Raum zu öffnen
/// 
public class unicorn1 : Dialog {
	
	protected override void Start(){
		base.Start();
		
		
		//Code der ausgeführt wird nach dem Dialog
		postDialog = (GameObject obj) => {
			//Referenz aufs Level
			MessageReceiver level = (MessageReceiver) GameObject.Find("Level").GetComponent<BossLevel>();
			//Nachricht ans Level, dass der Dialog beendet ist
			MessageDispatcher.I.Dispatch(level, "dialog1");
		};
		
		
	}
	
}
