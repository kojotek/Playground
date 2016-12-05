using UnityEngine;
using System.Collections;

public class AvatarGunsStateIdle : AvatarGunsState {

    float resetGunAfter = 1.5f;
    float timePassed = 0.0f;
    private WhichGun _whichGun;
    public AvatarGunsStateIdle(GunWieldingController controller, WhichGun whichGun=WhichGun.right) : base(controller) {
        _whichGun = whichGun;
    }

    public override void Enter() {
        Debug.Log("Idle");
    }

    public override void Leave() {
    }

    public override AvatarGunsState Proceed() {
        Debug.Log(_whichGun);
        timePassed += Time.deltaTime;
        
        if (timePassed >= resetGunAfter) {
            Debug.Log("TimePassed");
            _whichGun = WhichGun.right;
        }

        if (Input.GetButtonDown("Fire1")) {
            return new AvatarGunsStateSingleShot(_controller, _whichGun);
        }
        if (Input.GetButtonDown("Fire2")) {
            return new AvatarGunsStateSpinShot(_controller, _whichGun);
        }
        return null;
    }
}