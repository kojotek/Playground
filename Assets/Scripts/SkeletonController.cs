using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour {

    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void StunAnimation() {
        _animator.SetTrigger("stun");
    }
}
