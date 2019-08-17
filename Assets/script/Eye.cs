using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {
	public GameObject eye	;
	public Vector3 upvector;
	public float xvector	;

	public Material bomb;

	public const int InitBallMinNumPerThrow = 3;
	public const int InitBallMaxNumPerThrow = 3;
	public const float InitBallFrequenceTimeBySeconds = 3.0f;
	public const float InitBallFrequenceIncreaseFactor = 0.998f;

	private float nt		;
	private float bt		;
	private GameObject objcopy	;
	private bool isEnable;
	private int ballMinNumPerThrow = InitBallMinNumPerThrow;
	private int ballMaxNumPerThrow = InitBallMaxNumPerThrow;
	private float ballFrequenceTimeBySeconds = InitBallFrequenceTimeBySeconds;
	private float ballFrequenceIncreaseFactor = InitBallFrequenceIncreaseFactor;
	private int throwBallCount; 

	void Start () {
		nt=Time.time;
		bt=nt;
		//isEnable = true;
	}

	void Update () {
		nt=Time.time;

		if((nt-bt)>=ballFrequenceTimeBySeconds && isEnable)
		{
			bt=nt;
			bool hasLightBall = false;
			int ballNum = Random.Range (ballMinNumPerThrow, ballMaxNumPerThrow+1);
			for(int i = 0; i < ballNum; i++){
				/*
				int mean = 0;
				int stdDev = 1;
				System.Random rand = new System.Random ();
				double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
				double u2 = rand.NextDouble();
				double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
					System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
				double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
					*/

				int r = Random.Range (0, 100);
				if(r < 40){
					xvector=Random.Range(-2.5f,-1.5f);
					upvector.x=Random.Range(-20f,160f);
				}else if(r > 60){
					xvector=Random.Range(1.5f,2.5f);
					upvector.x=Random.Range(-160f,20f);
				}else{
					xvector=Random.Range(-1.5f,1.5f);
					upvector.x=Random.Range(-80f,80f);
				}
					

				objcopy=(GameObject)GameObject.Instantiate(eye,new Vector3(xvector,-3.6f,-1.5f),new Quaternion(0,0,0,0));
				objcopy.GetComponent<Rigidbody>().isKinematic=false;

				upvector.y=Random.Range(600f,640f);

				if (!hasLightBall && Random.Range (0, 10) < 1) { //10%
					hasLightBall = true;
					objcopy.GetComponent<Light> ().enabled = true;
					objcopy.GetComponent<Renderer> ().material = bomb;
					upvector.y=Random.Range(520f,580f);
				}


				upvector.z=Random.Range(-60f,60f);
				objcopy.GetComponent<Rigidbody>().AddForce(upvector);
				objcopy.GetComponent<Rigidbody>().AddTorque(Random.Range(-100f,100f),Random.Range(-100f,100f),Random.Range(-100f,100f));



				//objcopy.GetComponent<Collider> ().isTrigger = true;

			}

			throwBallCount++;
			//ballMinNumPerThrow = throwBallCount / 10 + ballMinNumPerThrow;
			//ballMaxNumPerThrow = throwBallCount / 5 + ballMaxNumPerThrow;
			ballMinNumPerThrow = throwBallCount / 15 + 3;
			ballMaxNumPerThrow = throwBallCount / 8 + 3;
			ballFrequenceTimeBySeconds *= ballFrequenceIncreaseFactor;
		}
	}

	void LoseLive(){
		isEnable = false;
		Debug.Log("LoseLive, receive");
	}

	void StartGame(){
		isEnable = true;
		ballMinNumPerThrow = InitBallMinNumPerThrow;
		ballMaxNumPerThrow = InitBallMaxNumPerThrow;
		ballFrequenceTimeBySeconds = InitBallFrequenceTimeBySeconds;
		ballFrequenceIncreaseFactor = InitBallFrequenceIncreaseFactor;
		throwBallCount = 0; 
		Debug.Log("StartGame, receive");
	}

}