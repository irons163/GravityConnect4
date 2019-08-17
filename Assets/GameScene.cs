using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {
	public static int lives;
	public static int score;
	public GUIText gText;
	private float timer = 180;
	public Texture timeTitle;

	private string instrctionText = "Lives:";

	// Use this for initialization
	void Start () {
		//gText.pixelOffset = new Vector2(Screen.width/2,Screen.height/2-timeTitle.height);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) timer = 0; // clamp the timer to zero
		int seconds = (int)(timer % 60); // calculate the seconds
		int minutes = (int)(timer / 60); // calculate the minutes
		gText.text = minutes + ":" + seconds;

		if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene ("Launch"); }
	}

	void OnGUI(){
		//GUI.DrawTexture (new Rect(0,0,Screen.width, Screen.height),backgroundTexture);
		//GUI.Label (new Rect(10,10,250,200),instrctionText+lives);
		//GUI.DrawTexture (new Rect(Screen.width-timeTitle.width,0,200,70),timeTitle);

	}

	void LoseLive(){
		Debug.Log("LoseLive, GameScene receive");
	}


}
