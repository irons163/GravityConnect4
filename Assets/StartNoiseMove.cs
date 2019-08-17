using UnityEngine;
using System.Collections;

public class StartNoiseMove : MonoBehaviour {
	public GameObject noise1;
	public GameObject noise2;

	private float nt;
	private float bt;
	private bool rigthMove;
	// Use this for initialization
	void Start () {
		//noise1.transform.position = new Vector3 (noise1.transform.position.x, noise1.transform.position.y, noise1.transform.position.z);
		//noise2.transform.position = new Vector3 (noise2.transform.position.x, noise2.transform.position.y, noise2.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if ((nt - bt) >= 0.2f) {
			bt = nt;
			if (rigthMove) {
				rigthMove = false;
				noise1.transform.position = new Vector3 (noise1.transform.position.x+0.3f, noise1.transform.position.y, noise1.transform.position.z);
				noise2.transform.position = new Vector3 (noise2.transform.position.x+0.3f, noise2.transform.position.y, noise2.transform.position.z);
			} else {
				rigthMove = true;
				noise1.transform.position = new Vector3 (noise1.transform.position.x-0.3f, noise1.transform.position.y, noise1.transform.position.z);
				noise2.transform.position = new Vector3 (noise2.transform.position.x-0.3f, noise2.transform.position.y, noise2.transform.position.z);
			}

		}
	}
}
