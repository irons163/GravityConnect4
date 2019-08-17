using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour {
	public static int comboCount;
	public TextMesh comboText;

	private float nt;
	private float bt;
	// Use this for initialization
	void Start () {
		nt=Time.time;
		bt=nt;
		comboText.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if ((nt - bt) >= 2.0f) {
			bt = nt;
			comboText.GetComponent<Renderer> ().enabled = false;
			comboCount=0;
		}
	}

	void ComboIncrease(){
		Debug.Log("Combo, receive");
		bt = nt;
		comboCount++;
		comboText.text = "Combo X"+comboCount;
		comboText.GetComponent<Renderer> ().enabled = true;
	}
}
