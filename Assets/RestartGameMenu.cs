using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartGameMenu : MonoBehaviour {
	private float nt;
	private float bt;
	private bool startToShowRestartMenu;
	private Vector3 originalPosition;
	public GameObject reportPlane;

	public GameObject camera;
	// Use this for initialization
	void Start () {
		nt=Time.time;
		bt=nt;
		originalPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!startToShowRestartMenu)
			return;
		nt=Time.time;
		if ((nt - bt) >= 0.02 && gameObject.transform.position.y < -0.9) {
			bt = nt;
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
			if (gameObject.transform.position.y >= -0.9) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, -0.9f, gameObject.transform.position.z);
			}
		} else if(gameObject.transform.position.y >= -0.9){
			if(Input.GetMouseButton(0)){
				RaycastHit hitInfo = new RaycastHit ();
				bool hit = Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if (hit) {
					if(hitInfo.transform.gameObject.tag=="again"){
						camera.SendMessage("RestartGame");
						startToShowRestartMenu = false;
						gameObject.transform.position = originalPosition;
					}else if(hitInfo.transform.gameObject.tag=="exit"){
						SceneManager.LoadScene ("Launch");
					}
				}
			}
		}

	}

	void ShowRestartMenu(){
		Debug.Log("ShowRestartMenu, receive");
		startToShowRestartMenu = true;

		reportPlane.SendMessage ("showRecode", Score.score);
	}
}
