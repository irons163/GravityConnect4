using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class Glass : MonoBehaviour {
	public Material glass1;
	public Material glass2;
	public Texture gameoverTexture;
	public GameObject restartMenu;

	// Use this for initialization
	void Start () {
		Material currentGlass = glass1;
		int r = Random.Range (0, 1);

		if(r==0){
			currentGlass = glass1;
		}else if(r==1){
			currentGlass = glass2;
		}

		GetComponent<Renderer> ().material = currentGlass;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		//GUI.DrawTexture (new Rect(0,0,Screen.width-47, Screen.height-40),gameoverTexture);
		//GUI.Label (new Rect(10,10,250,200),instrctionText);

		//GUI.DrawTexture (new Rect(doorRightPositionX,0,Screen.width, Screen.height),doorRightTexture);
		//GUI.DrawTexture (new Rect(doorLeftPositionX,20,Screen.width, Screen.height-40),doorLeftTexture);

		//		if (Input.anyKeyDown)
		//			SceneManager.LoadScene ("1128");
	}

	void GameOver(){
		Debug.Log("GameOver, Glass receive");
		GetComponent<Renderer> ().enabled = true;

		StartCoroutine (waitAndThenShowGameoverTitle ());

	}

	IEnumerator waitAndThenShowGameoverTitle()
	{
		Debug.Log("GameOver, ShowGameoverTitle");
		yield return new WaitForSeconds(1);
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach(Renderer renderer in renderers){
			renderer.enabled = true;
		}
		StartCoroutine (waitAndThenShowRestartMenu ());
	}

	IEnumerator waitAndThenShowRestartMenu()
	{
		yield return new WaitForSeconds (2);
		restartMenu.SendMessage("ShowRestartMenu");
	}

	void RestartGame(){
		Debug.Log("RestartGame, receive");
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach(Renderer renderer in renderers){
			renderer.enabled = false;
		}
	}
}
