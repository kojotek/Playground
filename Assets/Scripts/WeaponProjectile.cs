using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class WeaponProjectile : Projectile
{
    protected WeaponController _weapon;
    public WeaponProjectile(WeaponController weapon, Vector3 position, Ray ray, DamageTable table) : base(weapon.owner.owner, position, ray, table){
        _weapon = weapon;
    }
}