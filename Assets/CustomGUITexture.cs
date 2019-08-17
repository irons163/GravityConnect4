using UnityEngine;
using System.Collections;

public class CustomGUITexture: MonoBehaviour {
	//define here the original resolution 
	private float origW = 480f;
	private float origH = 800f;
	// Use this for initialization
	void Start () {
		float scaleX = Screen.width / origW; //your scale x
		float scaleY = Screen.height / origH; //your scale y

		//Find all GUIText object on your scene
		GUIText[] texts =  FindObjectsOfType(typeof(GUIText)) as GUIText[]; 

		foreach(GUIText myText in texts) { //find your element of text

			Vector2 pixOff = myText.pixelOffset; //your pixel offset on screen
			int origSizeText = myText.fontSize;
			myText.pixelOffset = new Vector2(pixOff.x*scaleX, pixOff.y*scaleY); //new position
			float floatFontSize = origSizeText * scaleX; //new size font in a float
			myText.fontSize = (int)Mathf.RoundToInt(floatFontSize); // Closest value of fontSize

		}
		/*
		//Find all GUITexture on your scene
		GUITexture[] textures =  FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
		foreach(GUITexture myTexture in textures) { //find your element of texture
			
			Rect pixIns = myTexture.pixelInset; //your dimention of texture
			//Change size pixIns for our screen
			pixIns.x = pixIns.x * scaleX;
			pixIns.y = pixIns.y * scaleY;
			pixIns.width = pixIns.width * scaleX;
			pixIns.height = pixIns.height * scaleY;
			//Sets new rectangle for our texture
			myTexture.pixelInset = pixIns;
		}
		*/



			GUITexture[] guis = FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
			Vector2 size = new Vector2();
		size.x = Screen.width / origW;
		size.y = Screen.height / origH;
			foreach(GUITexture gui in guis)
			{
				gui.pixelInset = new Rect(gui.pixelInset.x * size.x, gui.pixelInset.y * size.y, gui.pixelInset.width * size.x, gui.pixelInset.height * size.y);
			}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
