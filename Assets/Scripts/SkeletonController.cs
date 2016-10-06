using UnityEngine;
using System.Collections;

public class SkeletonController : ActorController {

    private Animator _animator;
    private Transform follow;
    private Rigidbody _rigidbody;
    public float speed = 2.0f;

    void Awake() {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        follow = GameObject.Find("Avatar").transform;
    }

    void Start() {
        InitActor(100.0f);
    }

    public void StunAnimation() {
        if (_alive)
        _animator.SetTrigger("stun");
    }

    public void DeathAnimation()
    {
        if (alive)
        {
            _animator.SetTrigger("death");
            gameObject.layer = LayerMask.NameToLayer("Dead Body");
            _alive = false;
        }

    }

    void Update()
    {
        Vector3 direction = follow.position - transform.position;
        if (!_alive || _animator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            direction.y = 0.0f;
            direction.Normalize();
            _rigidbody.velocity = direction * speed;
            Vector3 lookAtVector = follow.position;
            lookAtVector.y = transform.position.y;
            transform.LookAt(lookAtVector);
            _animator.SetFloat("speed", _rigidbody.velocity.magnitude);
        }
    }
}
