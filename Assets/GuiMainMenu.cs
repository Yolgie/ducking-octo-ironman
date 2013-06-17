using UnityEngine;
using System.Collections;

public class GuiMainMenu : MonoBehaviour {
	
	void Start ()
	{
		Preferences.TilesX=3;
		Preferences.TilesY=3;
		Preferences.MinimumTiles = 2;
		Preferences.SnapRange = 10;
		Preferences.Image = 0;
	}
	
	void OnGUI () 
	{
		// frame of the menu
		GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 -100,200,200), "Main Menu");
		
		if(Preferences.TilesX < Preferences.MinimumTiles || Preferences.TilesY < Preferences.MinimumTiles) {
			// disable the startbutton if one of the dimensions is < 3
			GUI.enabled = false;
			
			// text if a dimension is < 3
			GUI.Label(new Rect(Screen.width/2 - 75,Screen.height/2+15,150,25), "ERROR: X or Y < " + Preferences.MinimumTiles);

		}
		
		// startbutton
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 -50,100,25), "Play")) {
			Preferences.Image = 1;
			Preferences.HasWon = false;
			Application.LoadLevel(1);
		}
		
		// re-enable the following gui elements
		GUI.enabled = true;
		
		// number of columns
		string text = GUI.TextField(new Rect(Screen.width/2 - 75,Screen.height/2-10,50,25), Preferences.TilesX.ToString());
		int temp = 0;
		if (int.TryParse(text, out temp))
		{
			Preferences.TilesX = temp;
		}
		else if (text == "") Preferences.TilesX = Preferences.MinimumTiles;
		
		// number of rows
		text = GUI.TextField(new Rect(Screen.width/2 + 25,Screen.height/2-10,50,25), Preferences.TilesY.ToString());
		temp = 0;
		if (int.TryParse(text, out temp))
		{
			Preferences.TilesY = temp;
		}
		else if (text == "") Preferences.TilesY = Preferences.MinimumTiles;
		
		// exitbutton
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 + 50 ,100,25), "Exit")) {
			Application.Quit();
		}
	}
}