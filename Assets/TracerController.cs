using UnityEngine;
using System.Collections;

public class TracerController : MonoBehaviour {

    public Vector3 From;
    public Vector3 To;
    public AnimationCurve _fadingCurve;
    private float _elapsedTime = 0.0f;

    private LineRenderer _line;

    void Awake() {
        _line = GetComponent<LineRenderer>();
        _line.SetPositions(new Vector3[] {To, (To+From)/2.0f,  From });
    }

	void FixedUpdate () {
        var fade = _fadingCurve.Evaluate(_elapsedTime);
        _line.SetColors(new Color(0, 1, 1, fade), new Color(0, 1, 1, fade/2.0f));
        _elapsedTime += Time.deltaTime;

        if (fade < Mathf.Epsilon) {
            Destroy(gameObject, 0.1f);
        }
    }
}
