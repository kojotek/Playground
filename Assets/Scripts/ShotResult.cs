using UnityEngine;

public class ShotResult {

    public ShotResult(Vector3 worldSpaceOrigin, Ray rayFromCamera, float maxDist) {
        RaycastHit cameraHit;
        Physics.Raycast(rayFromCamera, out cameraHit, maxDist, LayermaskRepository.Instance.PlayerBullet);

        Vector3 cameraHitPoint;
        if (cameraHit.collider != null) {
            cameraHitPoint = cameraHit.point;
        }
        else {
            cameraHitPoint = rayFromCamera.origin + rayFromCamera.direction * maxDist;
        }

        Ray gunRay = new Ray(worldSpaceOrigin, (cameraHitPoint - worldSpaceOrigin).normalized);

        RaycastHit gunHit;
        Physics.Raycast(gunRay, out gunHit, maxDist, LayermaskRepository.Instance.PlayerBullet);


        if (gunHit.collider != null) {
            origin = worldSpaceOrigin;
            raycastHit = gunHit;
            point = gunHit.point;
            collider = gunHit.collider;
        }
        else {
            point = cameraHitPoint;
            origin = worldSpaceOrigin;
        }
    }


    public ShotResult(Ray ray, float maxDist, LayerMask mask) {
        RaycastHit cameraHit;
        Physics.Raycast(ray, out cameraHit, maxDist, mask);

        if (cameraHit.collider != null) {
            origin = ray.origin;
            raycastHit = cameraHit;
            point = cameraHit.point;
            collider = cameraHit.collider;
        }
        else {
            point = cameraHit.point;
            origin = ray.origin;
        }
    }

    public ShotResult(Ray ray, float maxDist, float radius, LayerMask mask) {
        RaycastHit cameraHit;
        Physics.CapsuleCast(ray.origin, ray.origin + ray.direction, radius, ray.direction, out cameraHit, 3000.0f, mask);

        if (cameraHit.collider != null) {
            origin = ray.origin;
            raycastHit = cameraHit;
            point = cameraHit.point;
            collider = cameraHit.collider;
        }
        else {
            point = cameraHit.point;
            origin = ray.origin;
        }
    }


    public bool hit {
        get { return (collider != null); }
    }
    public Vector3 origin;
    public Vector3 point;
    public Collider collider;
    public RaycastHit raycastHit;
}