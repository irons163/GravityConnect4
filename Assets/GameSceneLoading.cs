using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSceneLoading : MonoBehaviour {
	public Texture backgroundTexture;
	public Texture backgroundTexture2;
	public Texture doorLeftTexture;
	public Texture doorRightTexture;
	public GameObject plane;
	public GameObject whiteLightPlane;
	public AudioSource audioSource;

	GameObject obj;
	private float nt;
	private float bt;

	private bool isStartRotation;
	private bool isStartOpen;
	private int rotateCount;

	private float doorLeftPositionX, doorRightPositionX;
	public Light lt;
	// Use this for initialization
	void Start () {
		doorLeftPositionX = gameObject.transform.position.x;
		doorRightPositionX = 0;
		//lt = GetComponent<Light>();
		lt.enabled = false;

		nt=Time.time;
		bt=nt;
		StartCoroutine (waitAndStartRotation ());
		audioSource.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//lt.color -= Color.white / 2.0F * Time.deltaTime;


		nt=Time.time;
		if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene ("Launch"); }
		else if ((nt - bt) >= 0.1 && isStartRotation) {
			bt = nt;
			Transform t = gameObject.transform.GetChild (0);
			if (rotateCount < 3) {
				if (t.rotation.eulerAngles.x<1)
					rotateCount++;
				t.Rotate(new Vector3 (0, -15, 0));
			} else {
				isStartRotation = false;
				StartCoroutine (waitAndStartOpen ());
			}


		} else if(isStartOpen){
			if ((nt - bt) >= 0.1) {
				bt = nt;
				if (doorLeftPositionX + 4 >= doorRightPositionX) {
					if (doorLeftPositionX + 7 >= doorRightPositionX) {
						Quaternion q = new Quaternion ();
						q.x = 370;
						lt.gameObject.transform.rotation = q;
						lt.enabled = true;
					}

					doorLeftPositionX -= 0.2f;


					plane.transform.position = new Vector3 (doorLeftPositionX, plane.transform.position.y, plane.transform.position.z);
					//doorRightPositionX--;
				} else {
					
					StartCoroutine (load ());

				}
			}
		}

	}

	void OnGUI(){
		//GUI.DrawTexture (new Rect(0,0,Screen.width-47, Screen.height-40),backgroundTexture);
		//GUI.Label (new Rect(10,10,250,200),instrctionText);

		//GUI.DrawTexture (new Rect(doorRightPositionX,0,Screen.width, Screen.height),doorRightTexture);
		//GUI.DrawTexture (new Rect(doorLeftPositionX,20,Screen.width, Screen.height-40),doorLeftTexture);

//		if (Input.anyKeyDown)
//			SceneManager.LoadScene ("1128");
	}

	IEnumerator waitAndStartRotation()
	{
		yield return new WaitForSeconds(2);    
		isStartRotation = true;

	}

	IEnumerator waitAndStartOpen()
	{
		yield return new WaitForSeconds(2);    
		isStartOpen = true;
		whiteLightPlane.SendMessage("DoWhiteLightByOpenDoor");
		audioSource.enabled = true;
	}

	IEnumerator load()
	{
		yield return new WaitForSeconds(3);    //注意等待时间的写法
		SceneManager.LoadScene ("1128");
	}
}
