using UnityEngine;
using System.Collections;
using System;

public class BasicHitScanTracerWeaponProjectile : WeaponHitScanProjectile {

    public BasicHitScanTracerWeaponProjectile(WeaponController weapon, Vector3 position, Ray ray): base(weapon, position, ray) {}

    public override void Init() {
        ShotResult result = new ShotResult(AimingRay, 3000.0f, LayermaskRepository.Instance.PlayerBullet);
        if (result.hit) {
            TracerGeneratorController.Instance.GenerateSingleShotTracer(StartPosition, result.point);
            DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
            HitBoxController hitbox = result.collider.gameObject.GetComponent<HitBoxController>();
            if (hitbox != null) {
                OnHitTarget(hitbox);
            }
            
        }
        else {
            TracerGeneratorController.Instance.GenerateSingleShotTracer(StartPosition, AimingRay.origin + AimingRay.direction * 1000.0f);
        }
    }

    public override void OnHitTarget(HitBoxController hitBox) {
        hitBox.TakeDamage();
        NotifyAboutHit();
    }

    public override void NotifyAboutHit() {
        Owner.OnAttackHitTarget();
    }
}