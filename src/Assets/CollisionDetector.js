#pragma strict
@script RequireComponent(AudioSource)

public var particle1: GameObject;
public var particle2: GameObject;
public var particle3: GameObject;

public var explosion: AudioClip;

public var explosionStrength : float = 500.0f;
public var bounceStrength : float = 50.0f;
public var durability : float = 8.0f;

function OnTriggerEnter(other: Collider){
    if (other.gameObject.tag == "MedExplosive" || other.gameObject.tag == "Explosive"){
        print("Triggered" + other);
        other.rigidbody.useGravity = !other.rigidbody.useGravity;
    }
}

function OnCollisionEnter(collision : Collision) {
    var forceVec : Vector3;
    if (collision.gameObject.tag == "Explosive"){
        if (collision.rigidbody) {
            collision.rigidbody.isKinematic = false;
            particle1.active = true;
            particle2.active = true;
            forceVec = -collision.rigidbody.velocity.normalized * explosionStrength;
            collision.rigidbody.AddForce(forceVec);
            audio.PlayOneShot(explosion);
        } 
    } else if (collision.gameObject.tag == "MedExplosive"){
        if (collision.rigidbody && collision.relativeVelocity.magnitude > durability ) {
            collision.rigidbody.isKinematic = false;
            particle1.active = true;
            particle2.active = true;
            forceVec = collision.rigidbody.velocity.normalized * explosionStrength;

            GameObject.Find("Score Manager").SendMessage("AddScore", explosionStrength);

            collision.rigidbody.AddForce(forceVec);
            audio.PlayOneShot(explosion, 0.7);
        } else if (collision.rigidbody){
            collision.rigidbody.isKinematic = false;
            particle3.active = true;
            forceVec = collision.rigidbody.velocity.normalized * bounceStrength;

            GameObject.Find("Score Manager").SendMessage("AddScore", bounceStrength);

            collision.rigidbody.AddForce(forceVec);
            yield WaitForSeconds(0.5);
            particle3.active = false;
        }
    } else if (collision.gameObject.tag == "NonExplosive"){
        if (collision.rigidbody) {
            collision.rigidbody.isKinematic = false;
            particle3.active = true;
            forceVec = collision.rigidbody.velocity.normalized * bounceStrength;
            collision.rigidbody.AddForce(forceVec);
            yield WaitForSeconds(0.5);
            particle3.active = false;
        } 
    }
}
