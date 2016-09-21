using UnityEngine;
using System.Collections;
using System;

public abstract class Projectile : INotifyHit {
    protected Vector3 StartPosition;
    protected Ray AimingRay;
    protected DamageTable damageTable;
    public IDamageDealer Owner { get; set; }

    public Projectile(IDamageDealer owner, Vector3 position, Ray ray) {
        Owner = owner;
        StartPosition = position;
        AimingRay = ray;
    }

    public abstract void Init();
    public abstract void OnHitTarget(HitBoxController enemyHitBox);

    public abstract void NotifyAboutHit();
}


public class DamageTable {
    float Damage;
    float CriticalDamage;
    float StunChance;
}


public abstract class WeaponProjectile : Projectile{
    protected WeaponController _weapon;
    public WeaponProjectile(WeaponController weapon, Vector3 position, Ray ray) : base(weapon.owner.owner, position, ray) {
        _weapon = weapon;
        //StartPosition = position;
        //AimingRay = ray;
    }
}