#pragma strict

public var speed : float = 1.0f;
public var maxDistance: float = 100.0f;
private var init: Vector3;
private var travel: float = 0.0f;

function Start () {
	 init = transform.position;
}

function Update () {
		if (travel > maxDistance){
			travel = 0.0f;
			transform.position = init;
		} else {
			travel += 1.0f;
			transform.position += transform.forward*speed*Time.deltaTime;
		}
}