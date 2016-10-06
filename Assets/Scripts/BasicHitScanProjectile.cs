using UnityEngine;
using System.Collections;
using System;
/*
public class BasicWeaponHitScanProjectile : WeaponHitScanProjectile {

    public BasicWeaponHitScanProjectile(WeaponController weapon, Vector3 position, Ray ray, DamageTable table): base(weapon, position, ray, table) {}

    public override void Init() {
        ShotResult result = new ShotResult(AimingRay, 3000.0f, LayermaskRepository.Instance.PlayerBullet);
        if (result.hit) {
            DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
            HitBoxController hitbox = result.collider.gameObject.GetComponent<HitBoxController>();
            OnHitTarget(hitbox);
        }
    }

    public override void OnHitTarget(HitBoxController hitBox) {
        hitBox.TakeDamage(damageTable);
        AttackResult res = new AttackResult(damageTable, hitBox.CriticalSpot, )
        NotifyAboutHit();
    }


    public override void NotifyAboutHit(AttackResult effect)
    {
        Owner.OnAttackHitTarget();
    }
}


    */