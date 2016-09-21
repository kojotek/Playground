using UnityEngine;
using System.Collections;

public class TracerController : MonoBehaviour {

    [HideInInspector]
    public Vector3 From;
    [HideInInspector]
    public Vector3 To;
    public AnimationCurve _fadingCurve;
    private float _elapsedTime = 0.0f;

    private LineRenderer _line;

    void Awake() {
        _line = GetComponent<LineRenderer>();
        _line.SetPositions(new Vector3[] {To, From });
        float dist = Vector3.Distance(From, To);
        if (dist >= 180.0f ) {
            _line.SetWidth(2.5f, 2.0f);
        }
        if (dist < 180.0f && dist > 80.0f) {
            _line.SetWidth(1.7f, 2.0f);
        }
        if (dist <= 80.0f) {
            _line.SetWidth(1.2f, 2.0f);
        }
        if (dist <= 10.0f) {
            _line.SetWidth(0, 0);
        }
    }

	void FixedUpdate () {
        var fade = _fadingCurve.Evaluate(_elapsedTime);
        _line.SetColors(new Color(0, 1, 1, fade), new Color(0, 1, 1, fade/2.0f));
        _elapsedTime += Time.deltaTime;

        if (fade < 0.1f) {
            Destroy(gameObject, 0.3f);
        }
    }
}
