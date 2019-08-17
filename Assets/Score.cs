using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public static int score;
	public TextMesh scoreText;
	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ScoreIncrease(){
		Debug.Log("Score, receive");
		score++;
		scoreText.text = "Score:"+score;
	}
}
