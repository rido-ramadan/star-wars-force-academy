#pragma strict

public var speed : float = 1.0f;
public var maxDistance: float = 50.0f;
private var init: Vector3;
private var travel: float = 0.0f;
private var right = true;

function Start () {
	init = transform.position;
}

function Update () {
	if (travel > maxDistance){
		travel = 0.0f;
		right = !right;
	}
	if (right){
		travel += 1.0f;
		transform.position += transform.forward*speed*Time.deltaTime;
	} else {
		travel += 1.0f;
		transform.position -= transform.forward*speed*Time.deltaTime;
	}
}