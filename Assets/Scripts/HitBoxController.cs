using UnityEngine;
using System.Collections;

public class HitBoxController : MonoBehaviour {

    //[SerializeField]
    private DamageReceiver _damageReceiver;

    void Awake() {
        _damageReceiver = GetComponentInParent<DamageReceiver>();
    }

    public void TakeDamage() {
        _damageReceiver.ReceiveDamage();
    }
}
