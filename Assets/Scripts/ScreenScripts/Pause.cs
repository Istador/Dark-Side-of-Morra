using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Pause : Menu {
	
	
	
	//Array aller Buttons
	private object[,] _buttons;
	
	protected override object[,] buttons {get{return _buttons;}}
	
	
	
	private static HashSet<KeyCode> keys;
	
	
	
	private PlayerController pc;
	
	
	void Start(){
		Time.timeScale = 1.0f;
		paused = false;
		
		pc = GetComponent<PlayerController>();
		
		_buttons = new object[,] {
			{1, "Spiel Fortsetzen", null, null, (Action<int>)((int id)=>{ResumeGame();}) },
			{2, "Zum Hauptmenü",    null, null, (Action<int>)((int id)=>{
					MessageDispatcher.Instance.EmptyQueue();
					Application.LoadLevel(0);
					ResumeGame();
				})
			}
			
		};
		
		if(keys == null){
			keys = new HashSet<KeyCode>();
			
			keys.Add(KeyCode.Escape);
			keys.Add(KeyCode.Menu);
			keys.Add(KeyCode.Break);
		}
	}
	
	
	
	void Update() {
		foreach(KeyCode kc in keys){
			if(Input.GetKeyDown(kc)){
				if(!paused) PauseGame();
				else ResumeGame();
				break;
			}
		}
	}
	
	
	
	private bool paused = false;
	
	private void PauseGame(){
		pc.enabled = false;
		paused = true;
		Time.timeScale = 0.0f;
	}
	
	private void ResumeGame(){
		pc.enabled = true;
		paused = false;
		Time.timeScale = 1.0f;
	}
	
	
	
	protected override void OnGUI(){
		if(paused) base.OnGUI();
	}
	
	
	
}
