using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class CharacterControls : MonoBehaviour {

    //public Transform LookTransform;
    public Vector3 Gravity = Vector3.down * 9.81f;
    //public float RotationRate = 0.1f;
    public float Velocity = 8;
    public float SprintVelocity = 8;
    public float GroundControl = 1.0f;
    public float AirControl = 0.2f;
    public float JumpVelocity = 5;
    //public float GroundHeight = 1.1f;
    private bool jump;
    private float currentVelocity;
    private bool _grounded;
    private Rigidbody _rigidbody;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
    }

    void OnEnable() {
        GroundContactController gcc = GetComponentInChildren<GroundContactController>();
        gcc.GroundContactChanged += OnGroundContactChange;
    }

    void OnDisable() {
        GroundContactController gcc = GetComponentInChildren<GroundContactController>();
        gcc.GroundContactChanged -= OnGroundContactChange;
    }

    void OnGroundContactChange(object source, GroundContactEventArgs args) {
        _grounded = args.Grounded;
        //Debug.Log(_grounded);
    }

    void Update() {
        jump = jump || Input.GetAxis("Jump") > 0.05f;
    }

    void FixedUpdate() {
        // Cast a ray towards the ground to see if the Walker is grounded
        //bool grounded = Physics.Raycast(transform.position, Gravity.normalized, GroundHeight);

        // Rotate the body to stay upright
        //Vector3 gravityForward = Vector3.Cross(Gravity, transform.right);
        //Quaternion targetRotation = Quaternion.LookRotation(gravityForward, -Gravity);
        //_rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, RotationRate);

        if (_grounded) {
            currentVelocity = Input.GetButton("Sprint") ? SprintVelocity : Velocity;
        }

        // Add velocity change for movement on the local horizontal plane
        Vector3 forward = Vector3.Cross(transform.up, -transform.right).normalized;
        Vector3 right = Vector3.Cross(transform.up, transform.forward).normalized;
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