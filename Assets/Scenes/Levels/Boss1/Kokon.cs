using UnityEngine;
using System.Collections;

/// 
/// Skript um das aufgehen des Kokons zu steuern
/// 
public class Kokon : MonoBehaviour, MessageReceiver {
	
	
	
	/// <summary>
	/// Anzahl aller Frames dieser Animation
	/// </summary>
	int max_frame = 10;
	
	
	
	/// <summary>
	/// Referenz auf die Spinne um ihr mitzuteilen wann der Kokon offen ist
	/// </summary>
	MessageReceiver spinne;
	
	
	
	// Methode um eingehende Nachrichten zu verarbeiten
	public bool HandleMessage(Telegram msg){
		switch(msg.message){
			
			//Befehl mit dem aufgehen des Kokons zu beginnen
			case "start":
				MessageDispatcher.I.Dispatch(this, "frame", 0.25f, (object)0);
				spinne = msg.sender;
				return true;
			
			//Lade den nächsten Frame der Animation
			case "frame":
				//aktueller Frameindex der Animation
				int frame = (int)msg.extraInfo;
				
				//Textur ändern
				Vector2 offset = new Vector2( ((float) frame) * (1.0f / max_frame), 0.0f );
				renderer.material.mainTextureOffset = offset;
				
				//wenn dies der letzte Frame ist
				if(frame >= max_frame-1){
					//Die Animation beenden
					MessageDispatcher.I.Dispatch(this, "over", 0.25f);
				}
				//wenn noch weitere Frames kommen
				else {
					//nächsten Frame später laden
					MessageDispatcher.I.Dispatch(this, "frame", 0.25f, (object)(frame+1));
				}
				return true;
			
			//Beende die Animation
			case "over":
				//Dieses Objekt ausblenden
				renderer.enabled = false;
			
				//Spinne benachrichtigen, dass sie frei ist
				MessageDispatcher.I.Dispatch(spinne, "kokon_offen");
			
				//sich selbst löschen
				Destroy(gameObject);
				return true;
			
			//Nachrichtentyp unbekannt, konnte nicht verarbeitet werden	
			default:
				return false;
		}
	}
	
}
