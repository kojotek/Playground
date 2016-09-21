using UnityEngine;
using System.Collections;
using System;

public class SkeletonDamageReceiver : DamageReceiver {

    public SkeletonController _skeleton;

    public override void ReceiveDamage() {
        _skeleton.StunAnimation();
    }

}
