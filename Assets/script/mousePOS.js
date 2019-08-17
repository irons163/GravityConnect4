#pragma strict
var ball:GameObject;

private var hits	:RaycastHit[];
		var hit 	:RaycastHit;
private var ray		:Ray;
private var check	:float;	
private var mouseButtonEnable:boolean;


function Start(){
	mouseButtonEnable = true;
}

function Update () {
	if(Input.GetMouseButton(0) && mouseButtonEnable)
	{
		POS();
	}	
}



function POS()
{
	ray=Camera.main.ScreenPointToRay(Input.mousePosition);

	Debug.DrawRay(Camera.main.transform.position,ray.direction,Color(255,0,0,255));
	hits=Physics.RaycastAll(Camera.main.transform.position,ray.direction,3550);
	for (var i = 0;i < hits.Length; i++) 
	{
		hit= hits[i];
		if(hit.collider.tag=="screen")
		{
			ball.transform.position=hit.point;
		}else if(hit.collider.tag=="eye"){
			hit.collider.gameObject.SendMessage("StartExplosion", SendMessageOptions.RequireReceiver);
		}
	}
}

	function LoseLive(){
		mouseButtonEnable = false;
		Debug.Log("LoseLive, receive");
	}

	function StartGame(){
		mouseButtonEnable = true;
		Debug.Log("StartGame, receive");
	}