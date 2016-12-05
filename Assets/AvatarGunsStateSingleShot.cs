using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AvatarGunsStateSingleShot : AvatarGunsState {


    bool fire2Pressed = false;
    float cycleTime;
    float timePassed = 0.0f;
    WhichGun _whichGun;
    DamageTable damageTab = new DamageTable(6.0f, true, 12.0f, 1.25f);


    public AvatarGunsStateSingleShot(GunWieldingController controller, WhichGun whichGun = WhichGun.right) : base(controller) {
        _whichGun = whichGun;
    }


    public override void Enter() {
        Debug.Log("Single");
        cycleTime = 0.45f;
        if (_whichGun == WhichGun.right) {
            _controller.rightGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorld(), damageTab, cycleTime * 2);
            Debug.Log("Right Shot");
        }
        else {
            _controller.leftGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorld(), damageTab, cycleTime * 2);
            Debug.Log("Left Shot");
        }
        _whichGun = _whichGun.Another();
    }

    public override AvatarGunsState Proceed() {
        Debug.Log(_whichGun);
        timePassed += Time.deltaTime;

        if (Input.GetButtonDown("Fire2")) {
            fire2Pressed = true;
        }

        if (timePassed > cycleTime/2 && fire2Pressed) {
            return new AvatarGunsStateSpinShot(_controller, _whichGun.Another());
        }

        while (timePassed > cycleTime) {
            timePassed -= cycleTime;


            if (Input.GetButton("Fire1")) {

                if (_whichGun == WhichGun.right) {
                    Debug.Log("Right Shot");
                    _controller.rightGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorld(), damageTab, cycleTime * 2);
                    _whichGun = WhichGun.left;
                }
                else {
                    Debug.Log("Left Shot");
                    _controller.leftGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorld(), damageTab, cycleTime * 2);
                    _whichGun = WhichGun.right;
                }
            }
            else {
                return new AvatarGunsStateIdle(_controller, _whichGun);
            }
        }
        return null;
    }

    public override void Leave() {
    }
}