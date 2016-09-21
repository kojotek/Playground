using UnityEngine;
using System.Collections;
using System;

public class AvatarController : MonoBehaviour, IDamageDealer {

    [SerializeField]
    private UIController _ui;

    private float _sharpshooter;
    public float Sharpshooter {
        set {
            _sharpshooter = value;
            _ui.SharpshooterBar.SetValue(value);
        }
        get { return _sharpshooter; }
    }

    private float _coldblood;
    public float Coldblood {
        set {
            _coldblood = value;
            _ui.ColdbloodBar.SetValue(value);
        }
        get { return _coldblood; }
    }

    public void OnAttackHitTarget() {
        Debug.Log("TRAFIENIE!");
        Sharpshooter += 0.02f;
        
    }

}
