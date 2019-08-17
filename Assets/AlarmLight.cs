using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {
	private float nt;
	private float bt;
	private bool isStartRotation;
	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3 (0, gameObject.transform.position.y, gameObject.transform.position.z);
		isStartRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if ((nt - bt) >= 0.07 && isStartRotation) {
			bt = nt;
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x - 1.5f, gameObject.transform.position.y, gameObject.transform.position.z);
		}else if(gameObject.transform.position.x < -20f){
			gameObject.transform.position = new Vector3 (20f, gameObject.transform.position.y, gameObject.transform.position.z);
			isStartRotation = false;
			StartCoroutine (waitAndStartRotation ());
		}
	}

	IEnumerator waitAndStartRotation()
	{
		yield return new WaitForSeconds(3);
		isStartRotation = true;

	}
}
