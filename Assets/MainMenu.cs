using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public GameObject bg; 
	public GameObject startGame;
	public GameObject option;
	public GameObject exit;
	public GameObject startNoise;

	private string instrctionText = "HI";
	private bool isFadingIn;
	private float nt;
	private float bt;
	// Use this for initialization
	void Start () {
		isFadingIn = true;
		Color color = startGame.GetComponent<Renderer>().material.color;
		color.a = 0f;
		color = option.GetComponent<Renderer>().material.color;
		color.a = 0f;
		color = exit.GetComponent<Renderer>().material.color;
		color.a = 0f;
		startNoise.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
		else if (isFadingIn && (nt - bt) >= 0.2f) {
			bt = nt;
			Color color = startGame.GetComponent<Renderer>().material.color;
			color.a += 0.05f;
			startGame.GetComponent<Renderer> ().material.color = color;
			option.GetComponent<Renderer> ().material.color = color;
			exit.GetComponent<Renderer> ().material.color = color;
			if(color.a >= 1.0f){
				isFadingIn = false;
				//startNoise.GetComponent<Renderer> ().enabled = true;
				startNoise.SetActive(true);
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
				}else if(hitInfo.transform.gameObject.tag=="option"){
					SceneManager.LoadScene ("Option");
				} else if(hitInfo.transform.gameObject.tag=="exit"){
					Application.Quit();
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
