using UnityEngine;
using System.Collections;

public class RevolverTracerGeneratorController : MonoBehaviour {

    [SerializeField]
    private Camera WorldCamera;
    [SerializeField]    
    private Camera CharacterCamera;
    [SerializeField]
    private GameObject SingleShotTracerObjectTemplate;

    public void GenerateSignleShotTracer(Vector3 destination) {
        var origin = WorldCamera.ViewportToWorldPoint(CharacterCamera.WorldToViewportPoint(transform.position));
        var newTrail = Instantiate(SingleShotTracerObjectTemplate);
        newTrail.GetComponent<TracerController>().From = origin;
        newTrail.GetComponent<TracerController>().To = destination;
        newTrail.SetActive(true);
    }
}
