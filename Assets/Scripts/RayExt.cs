using UnityEngine;
using System.Collections;

public static class RayExt {
    
    public static Ray RandomizeDirectionSpherically(this Ray ray, float angle, Vector3 up) {
        Ray newRay = ray;
        //newRay.direction = Quaternion.Euler(Random.insideUnitCircle.normalized * range) * newRay.direction;
        newRay.direction = Quaternion.AngleAxis(angle, up) * Quaternion.AngleAxis(angle, Vector3.Cross(up, newRay.direction)) * newRay.direction;

        //newRay.direction += Random.insideUnitSphere * range;
        return newRay;
    }
}
