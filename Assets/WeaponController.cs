using UnityEngine;
using System.Collections;

public interface IDamageDealer {
    void OnAttackHitTarget();
}

public interface INotifyHit{
    [HideInInspector]
    IDamageDealer Owner { get; set; }
    void NotifyAboutHit();
}



public abstract class WeaponController : MonoBehaviour {

    [HideInInspector]
    public GunWieldingController owner;
    public GunTipController gunTip;
    public abstract void PrimaryShot(Ray rayFromCamera, float time);
    public abstract void SecondaryShot(Ray rayFromCamera, float time);
}