using UnityEngine;
using System.Collections;

public class RevolverTracerGeneratorController : MonoBehaviour {

    [SerializeField]
    private GameObject SingleShotTracerObjectTemplate;

    public void GenerateSingleShotTracer(Vector3 destination) {
        var origin = CamerasManagerController.Instance.WeaponToWorld(transform.position);
        var newTrail = Instantiate(SingleShotTracerObjectTemplate);
        var newTrailController = newTrail.GetComponent<TracerController>();
        newTrailController.From = origin;
        newTrailController.To = destination;
        newTrail.SetActive(true);
    }

    public void GenerateSingleShotTracer(Vector3 origin, Vector3 destination) {
        var newTrail = Instantiate(SingleShotTracerObjectTemplate);
        var newTrailController = newTrail.GetComponent<TracerController>();
        newTrailController.From = origin;
        newTrailController.To = destination;
        newTrail.SetActive(true);
    }
}
