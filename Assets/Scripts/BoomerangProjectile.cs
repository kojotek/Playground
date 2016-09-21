using UnityEngine;
using System.Collections;
using System;


//Currenty unused
/*
public class BoomerangProjectile : WeaponHitScanProjectile {

    private DamageSourceController weapon;

    public BoomerangProjectile(DamageSourceController owner, Vector3 position, Ray ray) : base(owner.owner, position, ray) {
        weapon = owner;
    }

    public override void Init() {
        ShotResult result = new ShotResult(AimingRay, 50.0f, LayermaskRepository.Instance.PlayerBoomerangBullet);
        if (result.hit) {
            DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
            HitBoxController hitbox = result.collider.gameObject.GetComponent<HitBoxController>();
            OnHitTarget(hitbox);
        }
    }

    public override void OnHitTarget(HitBoxController hitBox) {
        hitBox.TakeDamage();
    }
}
*/