using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterControls : MonoBehaviour {

    //public Transform LookTransform;
    public GroundContactController GroundContactController;
    public Vector3 Gravity = Vector3.down * 9.81f;
    //public float RotationRate = 0.1f;
    public float Velocity = 8;
    public float SprintVelocity = 8;
    public float GroundControl = 1.0f;
    public float AirControl = 0.2f;
    public float JumpVelocity = 5;
    //public float GroundHeight = 1.1f;
    private bool jump;
    private bool dash;
    private float currentVelocity;
    private bool _grounded {
        get { return GroundContactController.Grounded; }
    }
    private Rigidbody _rigidbody;

    //For now it's good, but if more movement possibilities will occur, I'll have to implement state machine

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
    }

    void Update() {
        jump = jump || Input.GetAxis("Jump") > 0.5f;
        if (Input.GetButtonDown("Dash") && !dash) {
            StartCoroutine(DashCorutine());
        }
    }

    void FixedUpdate() {

        if (!dash) {
            if (_grounded) {
                currentVelocity = Input.GetButton("Sprint") ? SprintVelocity : Velocity;
            }

            // Add velocity change for movement on the local horizontal plane
            //Vector3 forward = Vector3.Cross(transform.up, -transform.right).normalized;
            //Vector3 right = Vector3.Cross(transform.up, transform.forward).normalized;

            Vector3 forward = transform.forward.normalized;
            Vector3 right = transform.right.normalized;


            Vector3 targetVelocity = (forward * Input.GetAxisRaw("FrontBack") + right * Input.GetAxisRaw("RightLeft")).normalized * currentVelocity;
            Vector3 localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
            Vector3 velocityChange = transform.InverseTransformDirection(targetVelocity) - localVelocity;

            // The velocity change is clamped to the control velocity
            // The vertical component is either removed or set to result in the absolute jump velocity
            velocityChange = Vector3.ClampMagnitude(velocityChange, _grounded ? GroundControl : AirControl);
            velocityChange.y = jump && _grounded ? -localVelocity.y + JumpVelocity : 0;
            velocityChange = transform.TransformDirection(velocityChange);

            _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Add gravity
            _rigidbody.AddForce(Gravity * _rigidbody.mass);
            jump = false;
        }
        
    }

    IEnumerator DashCorutine() {
        dash = true;
        Vector3 target = (transform.forward * Input.GetAxisRaw("FrontBack") + transform.right * Input.GetAxisRaw("RightLeft")).normalized;
        if (target.magnitude == 0.0f) {
            target = transform.forward;
        }
        Vector3 location = transform.position;
        for (float t = 0.0f; t < 0.1f; t+=Time.deltaTime){
            _rigidbody.velocity = target * 1000f;
            yield return null;
        }
        _rigidbody.velocity = target * Velocity;
        dash = false;
        Debug.Log(Vector3.Distance(location, transform.position));
    }

}