using UnityEngine;
using System.Collections;

public class GuiPlay : MonoBehaviour {
	private bool showMenu;
	
	void Start () {
		showMenu = false;
	}
		
	void OnGUI () {
		if(Preferences.HasWon) {
				GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 -100,200,100), "YOU WON !!!");
				
				// back to menu button
				if(GUI.Button(new Rect(Screen.width/2 - 75,Screen.height/2 - 50, 150, 20), "Exit to Main Menu")) {
					Application.LoadLevel(0);
				}			
		} else {
			
			// check for escape key
			if (Event.current.Equals (Event.KeyboardEvent ("escape"))) {
				showMenu = !showMenu;
			}
			if(showMenu) {
				GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 -100,200,100), "Menu");
				
				// back to menu button
				if(GUI.Button(new Rect(Screen.width/2 - 75,Screen.height/2 - 50, 150, 20), "Exit to Main Menu")) {
					Application.LoadLevel(0);
				}
			}
		}

	}
}
