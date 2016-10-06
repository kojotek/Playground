using UnityEngine;
using System.Collections;

public class HitBoxController : MonoBehaviour {

    private DamageReceiver _damageReceiver;
    public DamageReceiver damageReceiver{
        get { return _damageReceiver; }
    }

    [SerializeField]
    private bool _criticalSpot;
    public bool CriticalSpot {
        get { return _criticalSpot; }
    }

    private void Awake() {
        _damageReceiver = GetComponentInParent<DamageReceiver>();
    }

    public void TakeDamage(DamageTable table) {
        _damageReceiver.ReceiveDamage(table, _criticalSpot);
    }
}
