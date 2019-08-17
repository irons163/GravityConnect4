using UnityEngine;
using System.Collections;

public class SCP : MonoBehaviour {
	public Material m1;
	public Material m2;
	public Material m3;
	public Material m4;

	private int imageIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LoseLive(){
		Debug.Log("LoseLive, SCP receive");
		//Material mm = new Material(@"032 SCP 173001");
		//Material m = new Material (Shader.Find(@"032 SCP 173001"));
		if (imageIndex==0) {
			GetComponent<Renderer> ().material = m1;
		} else if (imageIndex==1) {
			GetComponent<Renderer> ().material = m2;
		} else if (imageIndex==2) {
			GetComponent<Renderer> ().material = m3;
		} else if (imageIndex==3) {
			GetComponent<Renderer> ().material = m4;
		}
		imageIndex++;
	}
}
