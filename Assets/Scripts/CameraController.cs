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

    public Ray GetRayInWorldWithRecoil(float minRecoil, float maxRecoil) {
        return _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)).RandomizeDirectionSpherically(Random.Range(minRecoil, maxRecoil));
    }

    public Vector3 GetAimedPointInWorld(float maxDist, int layerMask, float minRecoil = 0.0f, float maxRecoil = 0.0f) {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        ray = ray.RandomizeDirectionSpherically(Random.Range(minRecoil, maxRecoil));

        RaycastHit hit;
        var contact = Physics.Raycast(ray, out hit, maxDist, layerMask);
        if (contact) {
            return hit.point;
        }
        else {
            return transform.position + ray.direction * maxDist;
        }
    }
}
