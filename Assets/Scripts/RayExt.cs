using UnityEngine;
using System.Collections;

public static class RayExt {
    
    public static Ray RandomizeDirectionSpherically(this Ray ray, float range) {
        Ray newRay = ray;
        newRay.direction += Random.insideUnitSphere * range;
        return newRay;
    }
}
