using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DamageTable
{
    public DamageTable(float _damage, bool _canDoCriticalDamage, float _criticalDamage, float _stunChance) {
        damage = _damage;
        canDoCriticalDamage = _canDoCriticalDamage;
        criticalDamage = _criticalDamage;
        stunChance = _stunChance;
    }

    public bool canDoCriticalDamage;
    public float damage;
    public float criticalDamage;
    public float stunChance;
}