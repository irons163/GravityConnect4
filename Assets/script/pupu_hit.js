#pragma strict

private var check:float;
//private var child:	Component[];

public var openEye:GameObject;
public var red:GameObject;

function Start () {
	//child=gameObject.GetComponentsInChildren(Transform);
}

function Update () {


	/*
	if(child[1].transform.position.y<=-10)
	{
		Destroy(gameObject);	
	}*/
	if(Input.GetMouseButton(0))
	{
		check=1;
	}
	if(Input.GetMouseButtonUp(0))
	{
		check=0;
	}
}


function OnTriggerEnter (other : Collider) 
{
	if(check==1)
	{
		if(other.tag=="ball")
		{

	    	gameObject.GetComponent.<Rigidbody>().isKinematic=true;
	    	gameObject.GetComponent.<Collider>().isTrigger=true;
	    /*	
	    	child[1].GetComponent.<Collider>().isTrigger=false;
	    	child[2].GetComponent.<Collider>().isTrigger=false;
	    	child[1].GetComponent.<Rigidbody>().isKinematic=false;
	    	child[2].GetComponent.<Rigidbody>().isKinematic=false;
	    	
	    	child[1].GetComponent.<Rigidbody>().AddRelativeForce(Random.Range(-1,1),Random.Range(-1,1),Random.Range(-20,0));
	    	child[2].GetComponent.<Rigidbody>().AddRelativeForce(Random.Range(-1,1),Random.Range(-1,1),Random.Range(0,20));
	    	*/

	    	StartExplosion();
		}
	}
}

function StartExplosion() {
		BroadcastMessage("Explode");

		if(gameObject.GetComponent.<Light>().enabled){
			red.SendMessage("RedLight");
		}else{
				var objcopy=GameObject.Instantiate(openEye,gameObject.transform.position,new Quaternion(0,0,0,0));
			objcopy.GetComponent.<Rigidbody>().isKinematic=false;
			objcopy.GetComponent.<Rigidbody>().AddTorque(Random.Range(-100f,100f),Random.Range(-100f,100f),Random.Range(-100f,100f));
			objcopy.GetComponent.<Rigidbody>().AddForce(new Vector3(0,150.0f,0));
			//openEye.SendMessage("GenerateEye");
		}

		GameObject.Destroy(gameObject);
}
