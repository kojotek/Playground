using UnityEngine;
using System.Collections;
using System;

public class AvatarController : ActorController, IDamageDealer {

    [SerializeField]
    private HUDController _ui;
    private SkillSpawnerController _skillSpawner;
    private GunWieldingController _gunWielding;

    private float _sharpshooter;
    public float sharpshooter {
        set {
            _sharpshooter = value;
            _ui.sharpshooterBar.SetValue(value);
        }
        get { return _sharpshooter; }
    }

    private float _coldblood;
    public float coldblood {
        set {
            _coldblood = value;
            _ui.coldbloodBar.SetValue(value);
        }
        get { return _coldblood; }
    }

    void Awake() {
        _skillSpawner = GetComponentInChildren<SkillSpawnerController>();
        _gunWielding = GetComponentInChildren<GunWieldingController>();
    }

    void Start() {
        InitActor(100.0f);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E)){
            _skillSpawner.SpawnBoomerang(_gunWielding.WorldCamera.GetRayInWorld(), new DamageTable(5.0f, false, 5.0f, 10.0f));
        }
    }

    public void OnAttackHitTarget(AttackResult result) {
        if (result.actor.alive) {

            switch (result.type) {

                case (AttackType.SingleShot):
                    if (result.criticalSpot) {
                        sharpshooter += 0.1f;
                    }
                    else {
                        sharpshooter += 0.05f;
                    }
                    break;

                case (AttackType.SpinShot):
                    sharpshooter += 0.05f;
                    break;

                case (AttackType.Boomerang):
                    sharpshooter += 0.01f;
                    break;
            }
        }
    }




}
