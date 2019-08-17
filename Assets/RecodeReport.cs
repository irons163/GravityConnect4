using UnityEngine;
using System.Collections;

public class RecodeReport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void showRecode(int score){
		MyDatabase.insert(score);
	}
}
