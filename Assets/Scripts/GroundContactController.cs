using UnityEngine;
using System.Collections;
using System;


public class GroundContactController : MonoBehaviour {

    public bool Grounded {
        get { return _groundColliders > 0.0f; }
    }

    private int _groundColliders = 0;
    
    public RaycastHit GetContactPoint() {
        if (Grounded) {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit)) {
                return hit;
            }
        }
        return new RaycastHit();
    }

    void OnTriggerEnter(Collider collider) {
        _groundColliders++;
    }

    void OnTriggerExit(Collider collider) {
        _groundColliders--;
    }

}
