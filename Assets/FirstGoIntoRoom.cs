using UnityEngine;
using System.Collections;

public class FirstGoIntoRoom : MonoBehaviour {
	private float nt;
	private float bt;
	private bool isGotoCloseEye;
	private bool isGotoOpenEye;
	private float closeEyeUntilKeepEyeOepnMindistance;
	private Vector3 originalTopBlackplanePosition;
	private Vector3 originalBottomBlackplanePosition;
	private bool isWaitForOpenEye;
	private Vector3 originalBgPosition;
	private Vector3 originalScpPosition;
	private bool isArriveRoomRight;
	private bool isReadyToGameStart;

	public GameObject topBlackplane;
	public GameObject bottomBlackplane;
	public GameObject mainCamera;
	public GameObject bg;
	public GameObject scp;
	//public AudioSource audioSource;
	// Use this for initialization
	void Start () {
		originalTopBlackplanePosition = topBlackplane.transform.position;
		originalBottomBlackplanePosition = bottomBlackplane.transform.position;
		originalBgPosition = bg.transform.position;
		originalScpPosition = scp.transform.position;
		topBlackplane.transform.position = new Vector3 (topBlackplane.transform.position.x, topBlackplane.transform.position.y - 12.09f, topBlackplane.transform.position.z);
		bottomBlackplane.transform.position = new Vector3 (bottomBlackplane.transform.position.x, bottomBlackplane.transform.position.y + 12.09f, bottomBlackplane.transform.position.z);
		bg.transform.position = new Vector3 (bg.transform.position.x  - 8.3f, bg.transform.position.y, bg.transform.position.z);
		scp.transform.position = new Vector3 (scp.transform.position.x - 8.3f, scp.transform.position.y, scp.transform.position.z);
		isGotoOpenEye = true;
		isWaitForOpenEye = true;
		nt=Time.time;
		bt = nt;
	}
	
	// Update is called once per frame
	void Update () {
		if (isReadyToGameStart)
			return;

			Debug.Log("LoseLive, OpenEye");

			nt=Time.time;
			if ((nt - bt) >= 2.0f && isWaitForOpenEye) {
				isWaitForOpenEye = false;
			}else if(isWaitForOpenEye){
				return;
			}else if(!isWaitForOpenEye && !isGotoOpenEye){
				if ((bg.transform.position.x >= 5.8f)) {
					isArriveRoomRight = true;
				} else if(!isArriveRoomRight){
					bg.transform.position = new Vector3 (bg.transform.position.x + 0.2f, bg.transform.position.y, bg.transform.position.z);
					scp.transform.position = new Vector3 (scp.transform.position.x + 0.2f, scp.transform.position.y, scp.transform.position.z);
				}

				if (isArriveRoomRight && (bg.transform.position.x - 0.2f <= originalBgPosition.x)) {
					bg.transform.position = originalBgPosition;
					scp.transform.position = originalScpPosition;
					isReadyToGameStart = true;
					StartCoroutine (waitAndStartGame ());
				} else if(isArriveRoomRight){
					bg.transform.position = new Vector3 (bg.transform.position.x - 0.2f, bg.transform.position.y, bg.transform.position.z);
					scp.transform.position = new Vector3 (scp.transform.position.x - 0.2f, scp.transform.position.y, scp.transform.position.z);
				}

				
				return;
			}
				
			if ((nt - bt) >= 0.02) {
				bt = nt;

				if (topBlackplane.transform.position.y > 12.05f) {
					isGotoOpenEye = false;
					//isLoseLive = false;
					if (closeEyeUntilKeepEyeOepnMindistance <= 0f) {

					}
					//StartCoroutine (waitAndStartGame ());
					isGotoOpenEye = false;
					topBlackplane.transform.position = originalTopBlackplanePosition;
					bottomBlackplane.transform.position = originalBottomBlackplanePosition;
				} else {
					topBlackplane.transform.position = new Vector3 (topBlackplane.transform.position.x, topBlackplane.transform.position.y + 0.3f, topBlackplane.transform.position.z);
					bottomBlackplane.transform.position = new Vector3 (bottomBlackplane.transform.position.x, bottomBlackplane.transform.position.y - 0.3f, bottomBlackplane.transform.position.z);
				}
			}
		}

	IEnumerator waitAndStartGame()
	{
		yield return new WaitForSeconds (0.5f);
		//mainCamera.SendMessage("StartGame");
		mainCamera.SendMessage("LoseOneLive");
	}
}
