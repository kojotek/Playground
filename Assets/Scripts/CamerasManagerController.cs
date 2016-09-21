using UnityEngine;
using System.Collections;

public class CamerasManagerController : MonoBehaviour {

    public Camera WorldCamera;
    public Camera WeaponCamera;

    private static CamerasManagerController _controller;

    public static CamerasManagerController Instance {
        get { return _controller; }
    }

    void Awake() {
        _controller = GetComponent<CamerasManagerController>();
    }

    public Vector3 WeaponToWorld(Vector3 point) {
        return WorldCamera.ViewportToWorldPoint(WeaponCamera.WorldToViewportPoint(point));
    }

    public Vector3 WeaponToWorld(Vector3 point, float z) {
        Vector3 position = WeaponCamera.WorldToViewportPoint(point);
        position.z = z;
        return WorldCamera.ViewportToWorldPoint(position);
        
    }

    public Vector3 WorldToWeapon(Vector3 point) {
        return WeaponCamera.ViewportToWorldPoint(WorldCamera.WorldToViewportPoint(point));
    }

    public Vector3 WorldToWeapon(Vector3 point, float z) {
        Vector3 position = WorldCamera.WorldToViewportPoint(point);
        position.z = z;
        return WeaponCamera.ViewportToWorldPoint(position);
    }
}
