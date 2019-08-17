using UnityEngine;
using System.Collections;

public class EyeCheck : MonoBehaviour {
	public GameObject camera;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y <= -10 && gameObject.GetComponent<Light> ().enabled != true && !MyConfig.isNotDieable) {
			camera.SendMessage("LoseOneLive");
		}
	}
}
