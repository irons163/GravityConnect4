
#pragma strict
		var pupu	:GameObject;
		var upvector:Vector3;
		var xvector	:float;

private var nt		:float;
private var bt		:float;
private var objcopy	:GameObject;
private var isEnable:boolean;

function Start () {
	nt=Time.time;
	bt=nt;
	isEnable = true;
}

function Update () {
	nt=Time.time;
	
	if((nt-bt)>=0.7 && isEnable)
	{
		bt=nt;
		xvector=Random.Range(-1.5f,1.5f);
		objcopy=GameObject.Instantiate(pupu,Vector3(xvector,-3.6f,-1),Quaternion(0,0,0,0));
		objcopy.GetComponent.<Rigidbody>().isKinematic=false;
		upvector.x=Random.Range(-60f,170f);
		upvector.y=Random.Range(380f,680f);
		upvector.z=Random.Range(-60f,60f);
		objcopy.GetComponent.<Rigidbody>().AddForce(upvector);
		objcopy.GetComponent.<Rigidbody>().AddTorque(Random.Range(-100f,100f),Random.Range(-100f,100f),Random.Range(-100f,100f));
	}
}

function LoseLive(){
		isEnable = false;
		Debug.Log("LoseLive, receive");
	}

	function StartGame(){
		isEnable = true;
		Debug.Log("StartGame, receive");
	}