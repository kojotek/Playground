using UnityEngine;
using System.Collections;
using System;

public class GroundContactEventArgs: EventArgs {
    public GroundContactEventArgs(bool grounded) {
        Grounded = grounded;
    }
    public bool Grounded { get; set; }
}


public class GroundContactController : MonoBehaviour {

    private bool _grounded;
    
    void OnTriggerEnter(Collider collider) {
        _grounded = true;
        OnGroundContactChanged(_grounded);
    }

    void OnTriggerExit(Collider collider) {
        _grounded = false;
        OnGroundContactChanged(_grounded);
    }

    public delegate void GroundContactChangedEventHandler(object source, GroundContactEventArgs args);
    public event EventHandler<GroundContactEventArgs> GroundContactChanged;
    protected virtual void OnGroundContactChanged(bool grounded) {
        if (GroundContactChanged != null) {
            GroundContactChanged(this, new GroundContactEventArgs(grounded));
        }
    }
}
