using UnityEngine;
using System.Collections;

public class GuiPlay : MonoBehaviour {
	void OnGUI () 
	{
		// http://docs.unity3d.com/Documentation/Components/gui-Basics.html
		GUI.Box(new Rect(10,10,100,90), "Menu");
		
		// back to menu button
		if(GUI.Button(new Rect(20,40,80,20), "Exit to Main Menu")) {
			Application.LoadLevel(0);
		}

	}
}
