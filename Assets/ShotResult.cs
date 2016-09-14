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


    public ShotResult(Ray rayFromCamera, float maxDist) {
        RaycastHit cameraHit;
        Physics.Raycast(rayFromCamera, out cameraHit, maxDist, LayermaskRepository.Instance.PlayerBullet);


        if (cameraHit.collider != null) {
            origin = rayFromCamera.origin;
            raycastHit = cameraHit;
            point = cameraHit.point;
            collider = cameraHit.collider;
        }
        else {
            point = cameraHit.point;
            origin = rayFromCamera.origin;
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