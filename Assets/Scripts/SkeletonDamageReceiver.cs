using UnityEngine;
using System.Collections;

public class SkeletonDamageReceiver : DamageReceiver {

    SkeletonController _skeleton;

    void Awake() {
        _actor = GetComponent<SkeletonController>();
        _skeleton = _actor as SkeletonController;
    }

    public override void ReceiveDamage(DamageTable table, bool critical) {
        if (critical){
            _actor.hp -= table.criticalDamage;
        }
        else{
            _actor.hp -= table.damage;
        }

        if (_actor.hp <= 0.0f){
            _skeleton.DeathAnimation();
        }
        
       if (Chance.GetTrueWithChance(table.stunChance)){
            _skeleton.StunAnimation();
        }
    }

}
