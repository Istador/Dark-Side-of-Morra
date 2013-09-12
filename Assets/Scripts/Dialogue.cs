using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
		public GUIText talkTextGUI;
		public string[] talkLines;
		public int textScrollSpeed;
	
		private bool talking;
		private bool textIsScrolling;
		
		private int currentLine;

	
	void Start () {
		
	}
	void 	Update () {
   		Speek();
	}
	
		void  OnTriggerEnter ( Collider hit  )
		{
				// Ist der Spieler beim Einhorn ?
			if (hit.gameObject.CompareTag ("Unicorn"))
			{
				
			    // nun kann der Text abgespielt werden
				talking = true;
				currentLine = 0;
			   	startScrolling();
			}
		}


	void Speek()
	{
		if(talking){
			
        if(Input.GetButtonDown("Fire1")){
				// mit diesem Butten wird die nächste Zeile des ausgegebenen Textes aufgerufen
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
         // talkLines[currentLine][i] = GUI.TextField (Rect (10, 10, 200, 20), talkLines[currentLine][i], 25);
				
          }
         else{
            return;
          }
     }

     textIsScrolling = false;
}
	

}