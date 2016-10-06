using UnityEngine;
using System.Collections;
using System;

public abstract class DamageReceiver : MonoBehaviour {

    protected ActorController _actor;
    public ActorController actor{
        get { return _actor; }
    }
    public abstract void ReceiveDamage(DamageTable table, bool critical);
}
