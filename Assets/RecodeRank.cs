using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RecodeRank : MonoBehaviour {
	public TextMesh scoreText;

	// Use this for initialization
	void Start () {
		int[] scores = MyDatabase.loadAll ();
		for(int i = 0 ; i < scores.Length; i++){
			if (i >= 10)
				break;
			if (i == 0)
				scoreText.text = scores [i]+"";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene ("Launch"); }
		else if(Input.GetMouseButton(0)){
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if(hitInfo.transform.gameObject.tag=="exit"){
					SceneManager.LoadScene ("Launch");
				}
		}
	}
}
