using UnityEngine;
using System.Collections;

public class WhiteLightByOpenDoor : MonoBehaviour {
	private float nt;
	private float bt;
	private bool isLightTwinkleToWhite;

	// Use this for initialization
	void Start () {
		Color color = gameObject.GetComponent<Renderer>().material.color;
		color.a = 0.5f;
		gameObject.GetComponent<Renderer> ().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if(isLightTwinkleToWhite){
			nt=Time.time;
			Color color = gameObject.GetComponent<Renderer>().material.color;
			if (color.a < 1.0f && (nt - bt) >= 0.2f) {
				bt = nt;
				color.a += 0.07f;
				gameObject.GetComponent<Renderer> ().material.color = color;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.1f);
			}else if(color.a >= 1.0f){
				isLightTwinkleToWhite = false;
			}
		}
	}

	void DoWhiteLightByOpenDoor(){
		isLightTwinkleToWhite = true;
	}
}
