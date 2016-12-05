using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Camera _camera;
	
	void Awake () {
        _camera = GetComponent<Camera>();
	}

    public Ray GetRayInWorld() {
        return _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    }

    public Ray GetRayInWorldWithSpread(Vector2 angles) {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        ray.direction = Quaternion.AngleAxis(angles.x, transform.up) * Quaternion.AngleAxis(angles.y, Vector3.Cross(transform.up, transform.forward)) * transform.forward;
        return ray;
    }

    public Ray GetRayInWorldWithRandomDirection(float angle) {
        return GetRayInWorldWithSpread(Random.insideUnitCircle.normalized * angle);
    }

    /*
    public Vector3 GetAimedPointInWorld(float maxDist, int layerMask, float minRecoil = 0.0f, float maxRecoil = 0.0f) {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        ray = ray.RandomizeDirectionSpherically(Random.Range(minRecoil, maxRecoil), transform.up);

        RaycastHit hit;
        var contact = Physics.Raycast(ray, out hit, maxDist, layerMask);
        if (contact) {
            return hit.point;
        }
        else {
            return transform.position + ray.direction * maxDist;
        }
    }*/
}
