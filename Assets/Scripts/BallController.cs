using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    private Rigidbody _rigidbody;
    public float GravityMultiplier = 1.0f;
    private Vector3 _lastVelocity;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate() {
        _rigidbody.AddForce(Physics.gravity * _rigidbody.mass * GravityMultiplier);
        _lastVelocity = _rigidbody.velocity;
    }

    void OnCollisionEnter(Collision collisionInfo) {
        _rigidbody.velocity = Vector3.Reflect(_lastVelocity, collisionInfo.contacts[0].normal);
    }

}
