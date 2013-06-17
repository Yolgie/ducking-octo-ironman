using UnityEngine;
using System.Collections;

public class MainGame : MonoBehaviour {
	public GameObject[] PuzzlePieces {get{return puzzlePieces;}}
	GameObject[] puzzlePieces;
	Texture2D puzzleTexture;
	float time;
	int texWidth;
	int texHeight;
	
	float[,] mapR;
	float[,] mapG;
	float[,] mapB;
	Texture2D[] puzzleTextures;
	int actTex=0;
	int[] rComp = {2,3,2,1,0,0,0,0,1};
	int[] bComp = {1,0,0,0,0,1,2,3,2};
	int[] gComp = {0,0,1,2,3,2,1,0,0};

	void Start () {
		texWidth=Preferences.TilesX;
		texHeight=Preferences.TilesY;
		
		puzzlePieces = new GameObject[texWidth*texHeight];
		
		
		for (int i = 0; i < puzzlePieces.Length; i++) {
			puzzlePieces[i] = GameObject.CreatePrimitive (PrimitiveType.Cube);
			puzzlePieces[i].transform.localScale = new Vector3(400/texWidth,400/texHeight,100);
			puzzlePieces[i].transform.Rotate (0f,0f,180f);
			puzzlePieces[i].transform.Translate (Random.Range(-512,512)-50,Random.Range(-384,384)-50,0);
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

		switch(Preferences.Image)
		{
		case 0:
			puzzleTexture = (Texture2D)Resources.Load("image");
			for (int i = 0; i < puzzlePieces.Length; i++) {
				puzzlePieces[i].renderer.material.mainTexture = puzzleTexture;	    	}
			break;
		case 1:
			TextureGenerator texGen = new TextureGenerator();
			mapR = texGen.Generate (512, 512, 2);
			mapG = texGen.Generate (512, 512, 2);
			mapB = texGen.Generate (512, 512, 2);
			
			puzzleTextures = new Texture2D[90];
			for(int i=0; i<90; i++)
			{
				puzzleTextures[i] = new Texture2D(512, 512, TextureFormat.ARGB32, false);
				
				for(int x=0; x<512; x++)
				{
					for(int y=0; y<512; y++)
					{
						float c1=(rComp[i%9] * (i%9)/9.0f + rComp[(i+1)%9] * (1.0f - (i%9)/9.0f));
						float c2=(gComp[i%9] * (i%9)/9.0f + gComp[(i+1)%9] * (1.0f - (i%9)/9.0f));
						float c3=(bComp[i%9] * (i%9)/9.0f + bComp[(i+1)%9] * (1.0f - (i%9)/9.0f));
						float r = (c1*mapR[x,y] + c2*mapG[x,y] + c3*mapB[x,y])/3.0f;
						float g = (c3*mapR[x,y] + c1*mapG[x,y] + c2*mapB[x,y])/3.0f;
						float b = (c2*mapR[x,y] + c3*mapG[x,y] + c1*mapB[x,y])/3.0f;
						puzzleTextures[i].SetPixel(x, y, new Color(r, g, b));
					}
				}
				puzzleTextures[i].Apply();
			}			
			for (int i = 0; i < puzzlePieces.Length; i++) {
				puzzlePieces[i].renderer.material.mainTexture = puzzleTextures[0];
	    	}
			break;
		}
	}
	
	void Update () {
		switch(Preferences.Image)
		{
		case 0:
			break;
		case 1:
			time += Time.deltaTime*10.0f;
			while(time > 1.0) {
				time-=1.0f;
				actTex = (actTex+1)%90;
				
				for (int i = 0; i < puzzlePieces.Length; i++) {
					puzzlePieces[i].renderer.material.mainTexture = puzzleTextures[actTex];
		    	}
			}
			break;
		}
	}
}