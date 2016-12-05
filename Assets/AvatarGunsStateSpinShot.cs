using UnityEngine;
using System.Collections;

public class AvatarGunsStateSpinShot : AvatarGunsState {

    private WhichGun _whichGun;
    float cycleTime;
    float timePassed;
    int shots;
    int shotNumber;
    bool fire1Pressed = false;
    float coolDown = 5.33f;
    float coolDownPassed = 0;

    public AvatarGunsStateSpinShot(GunWieldingController controller, WhichGun whichGun = WhichGun.right) : base(controller) {
        _whichGun = whichGun;
    }

    public override void Enter() {
        Debug.Log("Spin");
        cycleTime = 0.1f;
        timePassed = 0.0f;
        shots = 8;
        shotNumber = 1;
        var damageTab = new DamageTable(3.0f, false, 0.0f, 1.0f);
        if (_whichGun == WhichGun.right)
            _controller.rightGunController.SecondaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(0.0f), damageTab, cycleTime);
        else
            _controller.leftGunController.SecondaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(0.0f), damageTab, cycleTime);
    }

    public override void Leave() {
    }

    public override AvatarGunsState Proceed() {

        timePassed += Time.deltaTime;

        if (shotNumber >= 3) {
            if (Input.GetButton("Fire1") || Input.GetButtonDown("Fire1")) {
                fire1Pressed = true;
            }
        }

        if (shotNumber <= shots) {

            while (timePassed > cycleTime) {

                if (shotNumber < shots) {

                    timePassed -= cycleTime;
                    shotNumber++;
                    var damageTab = new DamageTable(3.0f, false, 0.01f, 0.0f);
                    float spread = Mathf.Clamp((float)shotNumber + Random.value, 0.0f, 5.0f);

                    float animationTime = cycleTime - timePassed;
                    while (animationTime <= 0.0f) {
                        animationTime += cycleTime;
                    }

                    if (_whichGun == WhichGun.right) {
                        _controller.rightGunController.SecondaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(spread), damageTab, animationTime);
                    }
                    else {
                        _controller.leftGunController.SecondaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(spread), damageTab, animationTime);
                    }

                }
                else if (shotNumber == shots) {

                    var damageTab = new DamageTable(3.0f, false, 0.0f, 1.0f);
                    float spread = Mathf.Clamp((float)shotNumber + Random.value, 0.0f, 5.0f);

                    if (_whichGun == WhichGun.right) {
                        _controller.rightGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(spread), damageTab, cycleTime * 10.0f);
                    }
                    else {
                        _controller.leftGunController.PrimaryShot(_controller.WorldCamera.GetRayInWorldWithRandomDirection(spread), damageTab, cycleTime * 10.0f);
                    }

                    shotNumber++;

                }
            }
        }
        else {
            coolDownPassed += Time.deltaTime;

            //if (fire1Pressed) {
            //    return new AvatarGunsStateSingleShot(_controller, _whichGun);
            //}
            //else {
            if (coolDownPassed >= coolDown)
                return new AvatarGunsStateIdle(_controller, _whichGun.Another());
            // }

        }
        
        return null;
    }
}
