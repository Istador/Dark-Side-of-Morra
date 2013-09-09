using UnityEngine;
using System.Collections;

/// <summary>
/// Dieses Skript sendet dem Bodenplatten-Skript Nachrichten falls der 
/// Spieler den Trigger für die sichere grüne Bodenplatte betritt oder 
/// verlässt.
/// </summary>
public class BodenplattenTrigger : MonoBehaviour {
	
	
	
	/// <summary>
	/// Referenz auf das Skript das für das ganze Platten-Event zuständig 
	/// ist, um ihm Nachrichten schicken zu können.
	/// </summary>
	private static Bodenplatten plattenController;
	
	/// <summary>
	/// Ob der Spieler den Trigger bereits betreten hat, um überflüssig 
	/// redundante Nachrichtenübermittlung zu verhindern.
	/// </summary>
	public bool entered = false;
	
	
	
	void Start(){
		//Referenz holen
		if(plattenController == null)
			plattenController = GameObject.Find("Bodenplatten").GetComponent<Bodenplatten>();
	}
	
	
	
	void OnTriggerEnter(Collider hit){
		//wenn der Spieler den Trigger betritt
		if(!entered && hit.gameObject.tag == "Player"){
			entered = true;
			MessageDispatcher.I.Dispatch(plattenController, "enter");
		}
	}
	
	
	
	void OnTriggerStay(Collider hit){
		//wenn der Spieler auf dem Trigger stehen bleibt
		if(!entered && hit.gameObject.tag == "Player"){
			entered = true;
			MessageDispatcher.I.Dispatch(plattenController, "enter");
		}
	}
	
	
	
	void OnTriggerExit(Collider hit){
		//wenn der Spieler den Trigger verlässt
		if(entered && hit.gameObject.tag == "Player"){
			entered = false;
			MessageDispatcher.I.Dispatch(plattenController, "leave");
		}
	}
	
	
	
}
