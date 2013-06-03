using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
	public GameObject[] PuzzlePieces {get{return puzzlePieces;}}
	GameObject[] puzzlePieces;
	Texture2D puzzleTexture;
	float time;
	int texWidth;
	int texHeight;

	void Start () {
		texWidth=Preferences.TilesX;
		texHeight=Preferences.TilesY;
		puzzlePieces = new GameObject[texWidth*texHeight];

		Debug.Log("load texture");
		puzzleTexture = (Texture2D)Resources.Load("image");
		Debug.Log("texture loaded  " + puzzlePieces.Length + " -- " + puzzleTexture.height);

		for (int i = 0; i < puzzlePieces.Length; i++) {
			puzzlePieces[i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			puzzlePieces[i].transform.localScale = new Vector3(400/texWidth,400/texHeight,100);
			puzzlePieces[i].transform.Rotate (0f,0f,180f);
			puzzlePieces[i].transform.Translate (Random.Range(-512,512)-50,Random.Range(-384,384)-50,0);
			puzzlePieces[i].renderer.material.mainTexture = puzzleTexture;
			puzzlePieces[i].renderer.material.mainTextureScale = new Vector2(1f/texWidth, 1f/texHeight);
			puzzlePieces[i].AddComponent("PieceLogic");
			PieceLogic mp = puzzlePieces[i].GetComponent<PieceLogic>();
			mp.x = i % texWidth;
			mp.y = i / texWidth;
			
			float x = i % texWidth;
			float y = i / texWidth;
			
			puzzlePieces[i].renderer.material.mainTextureOffset = new Vector2(
				x/(float)texWidth,
				y/(float)texHeight);
    	}
	}
	
	void Update () {
		/* for animated texture
		time += Time.deltaTime;
		while(time > 1.0) {
			time-=1.0f;
		    puzzleTexture.SetPixel(0, 0, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(1, 0, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(2, 0, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(0, 1, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(1, 1, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(2, 1, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(0, 2, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(1, 2, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
		    puzzleTexture.SetPixel(2, 2, new Color(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f), 1.0f));
			puzzleTexture.Apply();
		}
		*/
	}
}