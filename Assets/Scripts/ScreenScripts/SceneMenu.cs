using UnityEngine;
using System.Collections;

public abstract class SceneMenu : Menu {
	
	//Struktur: 
	// 0: int id
	// 1: string name
	protected abstract object[,] scenes {get;}
	
	
	private object[,] _buttons;
	protected override object[,] buttons {get{
		if(_buttons == null) CreateButtons();
		return _buttons;
	}}
	
	private void CreateButtons(){
		System.Func<int,bool> pre = (id) => SaveData.levelReached >= id ;
		System.Action<int> action = (id) => Application.LoadLevel(id);
		
		int n = scenes.GetLength(0);
		_buttons = new object[n,5];
		for(int i=0; i<n; i++){
			_buttons[i,0] = scenes[i,0];
			_buttons[i,1] = scenes[i,1];
			_buttons[i,2] = pre;
			_buttons[i,3] = null;
			if(scenes.GetLength(1) == 3 && scenes[i,2] != null)
				_buttons[i,4] = scenes[i,2];
			else
				_buttons[i,4] = action;
		}
	}
}
