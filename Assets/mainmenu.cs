using UnityEngine;
using System.Collections;

public class mainmenu : MonoBehaviour {
	
	void Start ()
	{
		Preferences.TilesX=3;
		Preferences.TilesY=3;
	}
	
	void OnGUI () 
	{
		// http://docs.unity3d.com/Documentation/Components/gui-Basics.html
		GUI.Box(new Rect(Screen.width/2 - 100,Screen.height/2 -100,200,200), "Main Menu");
		
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 -50,100,25), "Play")) {
			Application.LoadLevel(1);
		}
		
		string text = GUI.TextField(new Rect(Screen.width/2 - 75,Screen.height/2,50,25), Preferences.TilesX.ToString());
		int temp = 0;
		if (int.TryParse(text, out temp))
		{
			Preferences.TilesX = temp;
		}
		else if (text == "") Preferences.TilesX = 3;
		
		text = GUI.TextField(new Rect(Screen.width/2 + 25,Screen.height/2,50,25), Preferences.TilesY.ToString());
		temp = 0;
		if (int.TryParse(text, out temp))
		{
			Preferences.TilesY = temp;
		}
		else if (text == "") Preferences.TilesY = 3;		
		
		if(GUI.Button(new Rect(Screen.width/2 - 50,Screen.height/2 + 50 ,100,25), "Exit")) {
			Application.Quit();
		}
	}
}
