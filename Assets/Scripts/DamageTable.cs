using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DamageTable
{
    public DamageTable(float dmg, float critdmg, float stn) {
        damage = dmg;
        criticalDamage = critdmg;
        stunChance = stn;
    }

    public float damage;
    public float criticalDamage;
    public float stunChance;
}