using UnityEngine;
using System.Collections;

public class mousePOS : MonoBehaviour {
	public GameObject ball;
	public TextMesh comboText;
	public TextMesh scoreText;

	private RaycastHit[] hits;
	private RaycastHit hit;
	private Ray ray;
	private float check;	
	private bool mouseButtonEnable;

	// Use this for initialization
	void Start () {
		mouseButtonEnable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && mouseButtonEnable)
		{
			POS();
		}	
		else if(Input.GetMouseButtonUp(0) && mouseButtonEnable){
			Combo.comboCount = 0;
		}
	}

	void POS()
	{
		ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(Camera.main.transform.position,ray.direction,new Color(0,255,0,255));
		hits=Physics.RaycastAll(Camera.main.transform.position,ray.direction,10);
		for (var i = 0;i < hits.Length; i++) 
		{
			hit= hits[i];
			if(hit.collider.tag=="screen")
			{
				ball.transform.position=hit.point;
			}else if(hit.collider.tag=="eye"){
				//Combo.comboCount++;
				comboText.SendMessage("ComboIncrease", SendMessageOptions.RequireReceiver);
				scoreText.SendMessage("ScoreIncrease", SendMessageOptions.RequireReceiver);
				hit.collider.gameObject.SendMessage("StartExplosion", SendMessageOptions.RequireReceiver);
			}
		}
	}

	void LoseLive(){
		mouseButtonEnable = false;
		Combo.comboCount = 0;
		Debug.Log("LoseLive, receive");
	}

	void StartGame(){
		mouseButtonEnable = true;
		Debug.Log("StartGame, receive");
	}
}
