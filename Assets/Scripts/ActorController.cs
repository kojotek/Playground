using UnityEngine;

public abstract class ActorController : MonoBehaviour {

    public float hp {
        get { return _hp; }
        set { _hp = Mathf.Clamp(value, 0.0f, _maxHp); }
    }
    public float maxHp {
        get { return _maxHp; }
        set {
            _maxHp = Mathf.Max(0.0f, value);
            hp = _hp;
        }
    }
    public float hpPercent { get { return _hp / _maxHp; } }
    public bool alive { get { return _alive; } }



    protected bool _alive = true;
    protected float _hp;
    protected float _maxHp;
    protected void InitActor(float maxHealth){
        InitActor(maxHealth, maxHealth);
    }
    protected void InitActor(float maxHealth, float currentHealth){
        _maxHp = maxHealth;
        _hp = maxHealth;
    }
}