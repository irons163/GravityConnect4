using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	public AudioClip[] sounds;
	// Use this for initialization
	void Start () {
		playBGSound ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playHeavyBreatheSound(){
		//gameObject.GetComponent<AudioSource> ().clip = sounds [0];
		gameObject.GetComponent<AudioSource> ().PlayOneShot (sounds [0]);
	}

	public void playEyeHitSound(){
		int r = Random.Range (1, 4);
		//gameObject.GetComponent<AudioSource> ().clip = sounds [r];
		gameObject.GetComponent<AudioSource> ().PlayOneShot(sounds [r]);
	}

	public void playBGSound(){
		gameObject.GetComponent<AudioSource> ().clip = sounds [4];
		gameObject.GetComponent<AudioSource> ().loop = true;
		gameObject.GetComponent<AudioSource> ().Play();
	}
}
