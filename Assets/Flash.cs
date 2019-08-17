using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {
	private float nt;
	private float bt;
	private bool isRedLightTwinkle;
	private bool isLightTwinkleToWhite;
	private bool isGameover;

	public GameObject mainCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isRedLightTwinkle){
			nt=Time.time;
			Color color = gameObject.GetComponent<Renderer>().material.color;
			if (!isLightTwinkleToWhite && color.a >= 1.0f && (nt - bt) >= 0.3f) {
				bt = nt;
				color.a -= 0.25f;
				gameObject.GetComponent<Renderer> ().material.color = color;

				if (LoseLive.isGameover) {
					isRedLightTwinkle = false;
					isGameover = false;
					StartExplosion();
					color.a = 0f;
					gameObject.GetComponent<Renderer> ().material.color = color;
				}

			} else if (isLightTwinkleToWhite && color.a < 1.0f && (nt - bt) >= 0.05f) {
				bt = nt;
				color.a += 0.15f;
				gameObject.GetComponent<Renderer> ().material.color = color;
			} else if (!isLightTwinkleToWhite && (nt - bt) >= 0.05f) {
				bt = nt;
				color.a -= 0.15f;
				gameObject.GetComponent<Renderer> ().material.color = color;
				if(color.a <= 0f){
					isRedLightTwinkle = false;
				}
			}
		}
	}

	void RedLight(){
		isRedLightTwinkle = true;
		isLightTwinkleToWhite = true;
		mainCamera.SendMessage("LoseOneLiveByFlash");
	}

	void SCPChanged(){
		Debug.Log ("SCPChanged, Flash receive");
		isLightTwinkleToWhite = false;
	}

	void StartExplosion() {
		BroadcastMessage ("Explode");
	}
}
