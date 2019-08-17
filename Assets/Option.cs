using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {
	public GameObject bg;
	public GameObject optionChallenge;
	public GameObject optionUnlimited;
	public GameObject optionRecord;
	public GameObject optionProductionTeam;
	public GameObject optionExit;

	private string instrctionText = "HI";
	private bool isFadingIn;
	private float nt;
	private float bt;
	// Use this for initialization
	void Start () {
		isFadingIn = true;
		Color color = optionChallenge.GetComponent<Renderer>().material.color;
		color.a = 0f;
		color = optionUnlimited.GetComponent<Renderer>().material.color;
		color.a = 0f;
		color = optionRecord.GetComponent<Renderer>().material.color;
		color.a = 0f;
	}

	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene ("Launch"); }
		else if (isFadingIn && (nt - bt) >= 0.2f) {
			bt = nt;
			Color color = optionChallenge.GetComponent<Renderer>().material.color;
			color.a += 0.05f;
			optionChallenge.GetComponent<Renderer> ().material.color = color;
			optionUnlimited.GetComponent<Renderer> ().material.color = color;
			optionRecord.GetComponent<Renderer> ().material.color = color;
			if(color.a >= 1.0f){
				isFadingIn = false;
			}
		}
		else if(!isFadingIn && Input.GetMouseButton(0)){
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) {
				if(hitInfo.transform.gameObject.tag=="startSCP"){
					//camera.SendMessage("RestartGame");
					//startToShowRestartMenu = false;
					//gameObject.transform.position = originalPosition;
					SceneManager.LoadScene ("GameSceneLoading");
				}else if(hitInfo.transform.gameObject.tag=="recode"){
					SceneManager.LoadScene ("Recode");
				} else if(hitInfo.transform.gameObject.tag=="productTeam"){
					SceneManager.LoadScene ("ProductTeam");
				}else if(hitInfo.transform.gameObject.tag=="exit"){
					SceneManager.LoadScene ("Launch");
				}
			}
		}
	}

	void OnGUI(){
		/*
		GUI.DrawTexture (new Rect(0,0,Screen.width, Screen.height),backgroundTexture);
		GUI.Label (new Rect(10,10,250,200),instrctionText);

		if (Input.anyKeyDown)
			SceneManager.LoadScene ("GameSceneLoading");*/
	}
}

