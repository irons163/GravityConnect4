using UnityEngine;
using System.Collections;

public class LoseLive : MonoBehaviour {
	private float nt;
	private float bt;
	private bool isLoseLive;
	private bool isGotoCloseEye;
	private bool isGotoOpenEye;
	public static bool isGameover;
	private float closeEyeUntilKeepEyeOepnMindistance;
	private int imageIndex;

	public GameObject topBlackplane;
	public GameObject bottomBlackplane;
	public GameObject eyeCloseBlackAlphaplane;
	public GameObject camera;
	public GameObject scp;
	public GameObject scp1;
	public GameObject scp2;
	public GameObject scp3;
	public GameObject scp4;
	public GameObject glass;
	public GameObject flashPlane;
	public GameObject EyeAllClosePlane;
	public TextMesh scoreText;
	// Use this for initialization
	void Start () {
		nt=Time.time;
		bt=nt;
		closeEyeUntilKeepEyeOepnMindistance = 0.75f;
		isGameover = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLoseLive)
			return;
		if(isGotoCloseEye){
			Debug.Log("LoseLive, CloseEye");
			nt=Time.time;
			if ((nt - bt) >= 0.02) {
				bt = nt;

				if (topBlackplane.transform.position.y < bottomBlackplane.transform.position.y + bottomBlackplane.GetComponent<Renderer>().bounds.size.y + closeEyeUntilKeepEyeOepnMindistance) {
					//scp.SendMessage("LoseLive");
					//eyeCloseBlackAlphaplane.GetComponent<Renderer>().enabled = true;
					foreach (GameObject gos in GameObject.FindGameObjectsWithTag("eye")) {
						if (gos.name.Contains ("(Clone)")) {
							//Destroy (gos);
							Renderer r = gos.GetComponent<Renderer>();
							r.enabled = false;
						}
					}
					isGotoCloseEye = false;
					if (closeEyeUntilKeepEyeOepnMindistance <= 0f) {
						StartCoroutine (waitAndThenOpenEyeByExplode ());
					} else {
						StartCoroutine (waitAndThenOpenEye ());
					}
				
				} else {
					topBlackplane.transform.position = new Vector3 (topBlackplane.transform.position.x, topBlackplane.transform.position.y - 0.5f, topBlackplane.transform.position.z);
					bottomBlackplane.transform.position = new Vector3 (bottomBlackplane.transform.position.x, bottomBlackplane.transform.position.y + 0.5f, bottomBlackplane.transform.position.z);
				}
			}
		}else if(isGotoOpenEye){
			Debug.Log("LoseLive, OpenEye");

			nt=Time.time;
			if ((nt - bt) >= 0.02) {
				bt = nt;

				if (topBlackplane.transform.position.y > 12.05f) {
					isGotoOpenEye = false;
					isLoseLive = false;
					if (closeEyeUntilKeepEyeOepnMindistance <= 0f) {
						Color color = EyeAllClosePlane.GetComponent<Renderer>().material.color;
						EyeAllClosePlane.SendMessage ("StartExplosion");
						color.a = 0f;
						EyeAllClosePlane.GetComponent<Renderer> ().material.color = color;
					}
					StartCoroutine (waitAndStartGame ());
				} else {
					topBlackplane.transform.position = new Vector3 (topBlackplane.transform.position.x, topBlackplane.transform.position.y + 0.7f, topBlackplane.transform.position.z);
					bottomBlackplane.transform.position = new Vector3 (bottomBlackplane.transform.position.x, bottomBlackplane.transform.position.y - 0.7f, bottomBlackplane.transform.position.z);
				}
			}
		}

	}

	private void move(){
		
	}

	IEnumerator waitAndThenOpenEye()
	{
		yield return new WaitForSeconds(0.5f);
		isGotoOpenEye = true;
		//eyeCloseBlackAlphaplane.GetComponent<Renderer>().enabled = false;

		if(!MyConfig.isInfinity)
			scpChnage ();
		else
			scpChnage ();
	}

	IEnumerator waitAndThenOpenEyeByExplode()
	{
		yield return new WaitForSeconds(0.5f);
		isGotoOpenEye = true;
		Color color = EyeAllClosePlane.GetComponent<Renderer>().material.color;
		color.a = 1.0f;
		EyeAllClosePlane.GetComponent<Renderer> ().material.color = color;
		scpChnage ();
	}

	IEnumerator waitAndChangeSCPByFlash()
	{
		yield return new WaitForSeconds(1.0f);
		isGotoOpenEye = true;
		//eyeCloseBlackAlphaplane.GetComponent<Renderer>().enabled = false;
		scpChnage ();
		flashPlane.SendMessage ("SCPChanged");
	}

	IEnumerator waitAndStartGame()
	{
		camera.SendMessage ("playHeavyBreatheSound");

		yield return new WaitForSeconds(2); 

		foreach (GameObject gos in GameObject.FindGameObjectsWithTag("eye")) {
			if (gos.name.Contains ("(Clone)")) {
				Destroy (gos);
			}
		}

		if(!isGameover)
			camera.SendMessage("StartGame");


	}

	void scpChnage(){
		Debug.Log("SCP, Change");
		if (imageIndex == 0) {
			scp.GetComponent<Renderer> ().enabled = false;
			scp1.GetComponent<Renderer> ().enabled = true;
		} else if (imageIndex == 1) {
			scp1.GetComponent<Renderer> ().enabled = false;
			scp2.GetComponent<Renderer> ().enabled = true;
		} else if (imageIndex == 2) {
			scp2.GetComponent<Renderer> ().enabled = false;
			scp3.GetComponent<Renderer> ().enabled = true;
		} else if (imageIndex == 3) {
			scp3.GetComponent<Renderer> ().enabled = false;
			scp4.GetComponent<Renderer> ().enabled = true;
			glass.SendMessage ("GameOver");
			isGameover = true;
		} else if (imageIndex == 4) {
			scp4.GetComponent<Renderer> ().enabled = false;
			scp1.GetComponent<Renderer> ().enabled = true;
			imageIndex = 0;
			glass.SendMessage("RestartGame");
		}
		imageIndex++;
	}

	void LoseOneLive(){
		Debug.Log("LoseOneLive, receive");
		isLoseLive = true;
		checkIsGameOver ();
		if(isGameover)
			closeEyeUntilKeepEyeOepnMindistance = 0f;

		camera.SendMessage("LoseLive");

		foreach(GameObject gos in GameObject.FindGameObjectsWithTag("eye"))
		{
			if(gos.name.Contains("(Clone)"))
			{
				Destroy (gos);
				/*
					gos.hideFlags = HideFlags.HideInHierarchy;
					//gos.SetActive(false);
					Collider c = gos.GetComponent<Collider>();
					c.isTrigger = true;
					c.attachedRigidbody.isKinematic = true;
					c.attachedRigidbody.useGravity = false;
					*/
			}
		}
		isGotoCloseEye = true;
	}

	void LoseOneLiveByFlash(){
		Debug.Log("LoseOneLive, receive");
		isLoseLive = true;
		checkIsGameOver ();

		camera.SendMessage("LoseLive");

		foreach(GameObject gos in GameObject.FindGameObjectsWithTag("eye"))
		{
			if(gos.name.Contains("(Clone)"))
			{
				Destroy (gos);
			}
		}

		StartCoroutine (waitAndChangeSCPByFlash());
	}

	void checkIsGameOver(){
		if(imageIndex==3){
			isGameover = true;
		}
	}

	void RestartGame(){
		Debug.Log("RestartGame, receive");
		Combo.comboCount=0;
		Score.score = 0;
		scoreText.text = "Score:"+Score.score;
		isGotoCloseEye = false;
		isGotoOpenEye = false;
		isGameover = false;
		closeEyeUntilKeepEyeOepnMindistance = 0.75f;
		isGotoCloseEye = true;
		isLoseLive = true;
		waitAndThenOpenEye();
		//scpChnage ();

		//camera.SendMessage("StartGame");
	}

	//void StartGameByFirstGoIntoRoom(){
	//	StartCoroutine (waitAndStartGame ());
	//}
}
