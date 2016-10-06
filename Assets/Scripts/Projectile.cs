using UnityEngine;
using System.Collections;
using System;

public abstract class Projectile : INotifyHit {
    protected Vector3 StartPosition;
    protected Ray AimingRay;
    protected DamageTable damageTable;
    public IDamageDealer Owner { get; set; }

    public Projectile(IDamageDealer owner, Vector3 position, Ray ray, DamageTable table) {
        Owner = owner;
        StartPosition = position;
        AimingRay = ray;
        damageTable = table;
    }

    public abstract void Init();
    public abstract void OnHitTarget(HitBoxController enemyHitBox);
    public abstract void NotifyAboutHit(AttackResult effect);
}
