using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {
	public Texture backgroundTexture; 
	private string instrctionText = "Win";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(0,0,Screen.width, Screen.height),backgroundTexture);
		GUI.Label (new Rect(10,10,250,200),instrctionText);

		if (Input.anyKeyDown) {
			//Player
			GameScene.lives = 0;
			GameScene.score = 0;
			SceneManager.LoadScene ("1128");
		}
	}
}
