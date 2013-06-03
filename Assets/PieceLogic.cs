using UnityEngine;
using System.Collections;

public class PieceLogic : MonoBehaviour {
	// todo: use methods instead of public variables
	public ArrayList puzzlePieces;
	public int x;
	public int y;
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 oldPos;
	
	public PieceLogic()
	{
		puzzlePieces = new ArrayList();
	}

    void OnMouseDown() 
    { 
		if(!Preferences.HasWon) {
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	 
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	 
			Screen.showCursor = false;
			
			oldPos = gameObject.transform.position;
		}
		
    }
 
    void OnMouseDrag() 
    { 
		if(!Preferences.HasWon) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
	 
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
	 
			transform.position = curPosition;
	 
			foreach(GameObject go in puzzlePieces)
			{
				// todo: insert movement of joined blocks
				go.transform.position -= oldPos-transform.position;
			}
			oldPos=transform.position;
		}
    }
 
    void OnMouseUp()
    {
		if(!Preferences.HasWon) {
			CheckTouchingObjects();
	
			if(puzzlePieces.Count == (Preferences.TilesX * Preferences.TilesY - 1)) {
				GameObject winsound = GameObject.Find("winsound");
				AudioSource audio = winsound.GetComponent<AudioSource>();
				audio.Play();
				Preferences.HasWon = true;
				
			}
			
			Screen.showCursor = true;
		}
    }
	
	void CheckTouchingObjects()
	{
		int numPieces = puzzlePieces.Count;
		
		GameObject startup = GameObject.Find("Startup");
        MainGame ms = startup.GetComponent<MainGame>();
		
		// todo: check also the childs
		foreach(GameObject go2 in ms.PuzzlePieces) {
			CheckTouchingObject(gameObject, go2);
			foreach(GameObject go1 in puzzlePieces) {
				CheckTouchingObject(go1, go2);
			}		
		}
		
		if(numPieces != puzzlePieces.Count) {
			GameObject clicksound = GameObject.Find("clicksound");
			AudioSource audio = clicksound.GetComponent<AudioSource>();
			audio.Play();
		}
		
	}
	
	void CheckTouchingObject(GameObject go1, GameObject go2) {
		if(go2 != go1 && !puzzlePieces.Contains(go2)) {
	        PieceLogic mp1 = go1.GetComponent<PieceLogic>();
	        PieceLogic mp2 = go2.GetComponent<PieceLogic>();
			
			if(mp1.x > 0) {
				if(mp1.y == mp2.y && mp1.x-1 == mp2.x) {
					if(IsInRange(go2.transform.position.y,go1.transform.position.y) &&
						IsInRange(go2.transform.position.x,go1.transform.position.x - 400/Preferences.TilesX)) {
						SnapTo(go2);
						return;
					}
				}
			}
			
			if(mp1.x < Preferences.TilesX) {
				if(mp1.y == mp2.y && mp1.x+1 == mp2.x) {
					if(IsInRange(go2.transform.position.y,go1.transform.position.y) &&
						IsInRange(go2.transform.position.x,go1.transform.position.x + 400/Preferences.TilesX)) {
						SnapTo(go2);
						return;
					}
				}
			}
			if(mp1.y > 0) {
				if(mp1.x == mp2.x && mp1.y-1 == mp2.y) {
					if(IsInRange(go2.transform.position.x,go1.transform.position.x) &&
						IsInRange(go2.transform.position.y,go1.transform.position.y - 400/Preferences.TilesY)) {
						SnapTo(go2);
						return;
					}
				}
			}
			
			if(mp1.y < Preferences.TilesY) {
				if(mp1.x == mp2.x && mp1.y+1 == mp2.y) {
					if(IsInRange(go2.transform.position.x,go1.transform.position.x) &&
						IsInRange(go2.transform.position.y,go1.transform.position.y + 400/Preferences.TilesY)) {
						SnapTo(go2);
						return;
					}
				}
			}
		}
	}
	
	// add a piece(+connected pieces) to this piece
	private void SnapTo(GameObject go) {
        PieceLogic pl = go.GetComponent<PieceLogic>();
		
		AddPiece(go);
		AddPieces(pl.puzzlePieces);
		pl.AddPiece(gameObject);
		
		foreach(GameObject o in puzzlePieces) {
			PieceLogic pl2 = o.GetComponent<PieceLogic>();
			pl2.AddPiece(gameObject);
			pl2.AddPieces(puzzlePieces);
			AdjustPosition(o);
		}		
	}
	
	// add an array of pieces to this piece
	public void AddPieces(ArrayList puzzlePieces) {
		foreach(GameObject go in puzzlePieces) {
			AddPiece(go);
		}
	}
	
	// adds 1 piece to this piece
	public void AddPiece(GameObject go) {
		if(go != gameObject && !puzzlePieces.Contains(go)) {
			puzzlePieces.Add(go);
		}
	}
	
	// recalculate the position of the piece in relation to this piece
	public void AdjustPosition(GameObject go) {
        PieceLogic mp = go.GetComponent<PieceLogic>();
		go.transform.position=gameObject.transform.position + new Vector3((mp.x-x)*400/Preferences.TilesX,(mp.y-y)*400/Preferences.TilesY,0);
	}
							
	// check if the piece is in snap-range
	private bool IsInRange(float a, float b) {
		if(Mathf.Abs(a-b) <= Preferences.SnapRange)
			return true;
		return false;
	}
	
}