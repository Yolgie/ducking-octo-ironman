using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BoxCollider))]


public class MovePoint2 : MonoBehaviour {
	public ArrayList puzzlePieces;
	private Vector3 screenPoint;
	private Vector3 offset;
	
	public MovePoint2()
	{
		puzzlePieces = new ArrayList();
	}

    void OnMouseDown() 
    { 
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 
		Screen.showCursor = false;
		
    }
 
    void OnMouseDrag() 
    { 
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
 
		transform.position = curPosition;
 
		foreach(GameObject go in puzzlePieces)
		{
			// todo: insert movement
		}
    }
 
    void OnMouseUp()
    {
		CheckTouchingObjects();
		Screen.showCursor = true;
    }
	
	void CheckTouchingObjects()
	{
		// todo: check if the right objects are touching, if yes -> add them to the list
	}
}
