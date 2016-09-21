using UnityEngine;
using System.Collections;

public class TracerGeneratorController : MonoBehaviour {

    private static TracerGeneratorController _controller;

    public static TracerGeneratorController Instance {
        get { return _controller; }
    }

    [SerializeField]
    private GameObject SingleShotTracerObjectTemplate;

    void Awake() {
        _controller = GetComponent<TracerGeneratorController>();
    }

    public void GenerateSingleShotTracer(Vector3 origin, Vector3 destination) {
        var newTrail = Instantiate(SingleShotTracerObjectTemplate);
        var newTrailController = newTrail.GetComponent<TracerController>();
        newTrailController.From = origin;
        newTrailController.To = destination;
        newTrail.SetActive(true);
    }
}
