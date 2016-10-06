using UnityEngine;
using System.Collections;

public abstract class WeaponController : MonoBehaviour {

    [HideInInspector]
    public GunWieldingController owner;
    public GunTipController gunTip;
    public abstract void PrimaryShot(Ray rayFromCamera, DamageTable table, float time);
    public abstract void SecondaryShot(Ray rayFromCamera, DamageTable table, float time);
}