using UnityEngine;
using System.Collections;

public class OpenEye : MonoBehaviour {
	private Vector3 lacalScale;
	private float nt;
	private float bt;
	private bool isGroupUp;

	public Material[] materials;
	// Use this for initialization
	void Start () {
		lacalScale = gameObject.transform.localScale;
		gameObject.transform.localScale = new Vector3 (0.1f,0.1f,0.1f);
		isGroupUp = true;
		Renderer render = GetComponent<Renderer> ();
		if(Combo.comboCount>=materials.Length)
			render.material = materials[0];
		else
			render.material = materials[Combo.comboCount];
	}
	
	// Update is called once per frame
	void Update () {
		nt=Time.time;
		if ((nt - bt) >= 0.05) {
			bt = nt;
			if(lacalScale.x*1.4 > gameObject.transform.localScale.x && isGroupUp){
				gameObject.transform.localScale += new Vector3 (0.1f,0.1f,0.1f);
			}else if(lacalScale.x < gameObject.transform.localScale.x){
				isGroupUp = false;
				gameObject.transform.localScale -= new Vector3 (0.1f,0.1f,0.1f);
			}
		}

	}

	void GenerateEye(){
		
	}
}
