using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
		public GUIText talkTextGUI;
		public string[] talkLines;
		public int textScrollSpeed;
	
		private bool talking;
		private bool textIsScrolling;
		private CharacterController characterController;
		private int currentLine;

	
	void Start () {
		characterController = GetComponent<CharacterController>();
	}
	void 	Update () {
   		Speek();
	}
	
		void  OnTriggerEnter ( Collider characterController  )
		{
			if (characterController.gameObject.CompareTag ("Unicorn"))
			{
			    Debug.Log ("Trigger Unicorn");
				talking = true;
				currentLine = 0;
			    talkTextGUI.text = talkLines[currentLine];
				
				
				startScrolling();
			}
		}


	void Speek()
	{
		if(talking){
        if(Input.GetButtonDown("Fire1")){
           if(textIsScrolling){
              talkTextGUI.text = talkLines[currentLine];
              textIsScrolling = false;
            }
           else{
              if(currentLine < talkLines.Length - 1){
              currentLine++;
              
              startScrolling();
             }
            else{
               currentLine = 0;
               talkTextGUI.text = "";
               talking = false;
           
             }
          }
       }
    }
	}

void startScrolling(){
     textIsScrolling = true;
     int startLine = currentLine;
     string displayText = "";

     for(int i = 0; i < talkLines[currentLine].Length; i++){
          if(textIsScrolling && currentLine == startLine){
          displayText += talkLines[currentLine][i];
          talkTextGUI.text = displayText;
          //talkLines[currentLine][i] = GUI.TextField (Rect (10, 10, 200, 20), talkLines[currentLine][i], 25);
				
          }
         else{
            return;
          }
     }

     textIsScrolling = false;
}
}