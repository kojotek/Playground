using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour {

    [SerializeField]
    private Transform _target;
    public Transform Target {
        get {
            return _target;
        }
        set {
            _target = value;
            _targetCollider = _target.gameObject.GetComponent<Collider>();
            targetInRange = false;
        }
    }
    public Vector3 targetCenter {
        get { return _targetCollider.bounds.center + PositionModifier; }
    }
    private Collider _targetCollider;
    private float currentRadius;
    public float radius = 20.0f;
    public float speed = 5.0f;
    [HideInInspector]
    public bool targetInRange = false;
    private Vector3 previous;
    private Vector3 targetPosition;
    public Vector3 PositionModifier;
    private float time;

    void Awake() {
        _targetCollider = Target.gameObject.GetComponent<Collider>();
    }

    void Update() {
        previous = targetPosition;
        targetPosition = _targetCollider.bounds.center + PositionModifier;
        Vector3 positionDelta = targetPosition - transform.position;
        if (!targetInRange) {
            Vector3 point = Quaternion.Euler(0.0f, 90.0f, 0.0f) * positionDelta;
            point = point.normalized * radius;
            point += targetPosition;
            transform.LookAt(point);
            transform.position += transform.forward * speed * Time.deltaTime * 60.0f;
            transform.rotation = Quaternion.identity;
            if (Vector3.Distance(transform.position, targetPosition) <= radius * 1.1f) {
                targetInRange = true;
            }
        }
        else {
            time += Time.deltaTime * 0.7f;
            currentRadius = Vector3.Distance(targetPosition, transform.position);
            transform.position += targetPosition - previous;
            var axis = new Vector3(Mathf.Sin(time) / 5.0f, 1.0f, 0.0f);
            var rotation = -Time.deltaTime * speed * 60.0f * 360.0f / (2 * Mathf.PI * currentRadius);
            transform.RotateAround(targetPosition, axis, rotation);
            //transform. Rot//rotation = Quaternion.AngleAxis(15.0f, new Vector3(Mathf.Sin(time) / 5.0f, 1.0f, Mathf.Cos(time) / 5.0f));
            transform.position = Vector3.Lerp(  targetPosition + (transform.position - targetPosition).normalized * currentRadius,
                                                targetPosition + (transform.position-targetPosition).normalized * radius,
                                                0.03f);
        }
    }
}


