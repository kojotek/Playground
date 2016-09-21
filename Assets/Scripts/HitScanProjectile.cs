using UnityEngine;
using System.Collections;
using System;

public abstract class WeaponHitScanProjectile : WeaponProjectile {
    public WeaponHitScanProjectile(WeaponController weapon, Vector3 position, Ray ray) : base(weapon, position, ray) {}
}
