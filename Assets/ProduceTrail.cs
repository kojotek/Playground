using UnityEngine;
using System.Collections;

public class ProduceTrail : MonoBehaviour {

    public GameObject RevolverTracerObjectTemplate;
    public Camera FrontCamera;
    public Camera BackCamera;

    void Awake () {
	
	}

    void LateUpdate() {
        /*
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;
        var hited = Physics.Raycast(ray, out hit, 1000);
        if (!hited) {
            Debug.DrawRay(transform.position, ray.direction * 100000, Color.yellow);
        }
        else {
            Debug.DrawLine(transform.position, hit.point, Color.yellow);
        }
        */
    }

    public void Temp() {
        var origin = BackCamera.ViewportToWorldPoint(FrontCamera.WorldToViewportPoint(transform.position));
        Ray ray = FrontCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        var hited = Physics.Raycast(ray, out hit, 1000);
        if (!hited) {
            CreateTrailToThePoint(origin, ray.direction * 1000);
        }
        else {
            CreateTrailToThePoint(origin, hit.point);
        }
    }

    public void CreateTrailToThePoint(Vector3 origin, Vector3 destination) {
        var newTrail = Instantiate(RevolverTracerObjectTemplate);
        newTrail.GetComponent<TracerController>().From = origin;
        newTrail.GetComponent<TracerController>().To = destination;
        newTrail.SetActive(true);
    }
}
