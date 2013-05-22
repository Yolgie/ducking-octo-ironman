using UnityEngine;
using System.Collections;

public class managescript : MonoBehaviour {
	public GameObject[] puzzlePieces;
	Texture2D puzzleTexture;
	float time;
	int texWidth;
	int texHeight;

	void Start () {
		// todo: adjust the size of the blocks depending on the number of tiles
		texWidth=Preferences.TilesX;
		texHeight=Preferences.TilesY;
		puzzlePieces = new GameObject[texWidth*texHeight];

		Debug.Log("load texture");
		puzzleTexture = (Texture2D)Resources.Load("image");
		Debug.Log("texture loaded  " + puzzlePieces.Length + " -- " + puzzleTexture.height);

		
		// todo: fix error when tilesX != tilesY
		for (int i = 0; i < puzzlePieces.Length; i++) {
			puzzlePieces[i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			puzzlePieces[i].transform.localScale = new Vector3(100,100,100);
			puzzlePieces[i].transform.Rotate (0f,0f,180f);
			puzzlePieces[i].transform.Translate (Random.Range(-800,800),Random.Range(-400,400),0);
			puzzlePieces[i].AddComponent("MovePoint2");
			puzzlePieces[i].renderer.material.mainTexture = puzzleTexture;
			puzzlePieces[i].renderer.material.mainTextureScale = new Vector2(1f/texWidth, 1f/texHeight);
			puzzlePieces[i].renderer.material.mainTextureOffset = new Vector2((float)(i%3)/3,(float)(i/3)/3);
			puzzlePieces[i].renderer.material.mainTextureOffset = new Vector2((float)(i%texHeight)/(float)texWidth,(float)(i/texHeight)/(float)texHeight);
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
