#pragma strict

public var particle1: GameObject;
public var particle2: GameObject;
public var particle3: GameObject;

public var explosionStrength : float = 500.0f;
public var bounceStrength : float = 50.0f;

function OnCollisionEnter(collision : Collision) {
	var forceVec : Vector3;
	if (collision.gameObject.tag == "Box"){
	    if (collision.rigidbody && collision.relativeVelocity.magnitude > 8 ) {
			particle1.active = true;
			particle2.active = true;
	    	forceVec = collision.rigidbody.velocity.normalized * explosionStrength;
	    	//print(collision.rigidbody.velocity.normalized);
	    	collision.rigidbody.AddForce(forceVec);
	    	
	    } else if (collision.rigidbody){
			particle3.active = true;
			forceVec = collision.rigidbody.velocity.normalized * bounceStrength;
	    	collision.rigidbody.AddForce(forceVec);
	    	yield WaitForSeconds(0.5);
			particle3.active = false;
	    }
    } else if (collision.gameObject.tag == "Barrel"){
    	if (collision.rigidbody && collision.relativeVelocity.magnitude > 8 ) {
    		collision.rigidbody.isKinematic = false;
			particle1.active = true;
			particle2.active = true;
	    	forceVec = collision.rigidbody.velocity.normalized * explosionStrength;
    		print (forceVec);
	    	//print(collision.rigidbody.velocity.normalized);
	    	collision.rigidbody.AddForce(forceVec);
	    } else if (collision.rigidbody){
	    	print ("masuk2");
    		collision.rigidbody.isKinematic = false;
			particle3.active = true;
			forceVec = collision.rigidbody.velocity.normalized * bounceStrength;
	    	collision.rigidbody.AddForce(forceVec);
	    	yield WaitForSeconds(0.5);
			particle3.active = false;
	    }
    }
}
