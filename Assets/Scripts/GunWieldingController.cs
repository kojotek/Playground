using UnityEngine;
using System.Collections;

public class GunWieldingController : MonoBehaviour {

    //Almost everything in this class is temporary and not suitable for use in future. Things will be changed when I'll design actual game mechanisms
    public IDamageDealer owner;
    public WeaponController leftGunController;
    public WeaponController rightGunController;
    public CameraController WorldCamera;

    bool state = true;
    bool lr = true;

    void Awake () {
        leftGunController.owner = this;
        rightGunController.owner = this;
        owner = GetComponent<IDamageDealer>();
    }

    private void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            StartCoroutine(CoroutineShot());
        }

        if (Input.GetMouseButtonUp(0)) {
            state = false;
        }

        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(CoroutineSpinShot());
        }

    }

    public IEnumerator CoroutineShot() {
        float time = 0.45f;
        state = true;
        while (state) {
            if (lr) {
                leftGunController.PrimaryShot(WorldCamera.GetRayInWorld(), new DamageTable(25.01f, 50.01f, 1.25f), time*2);
            }
            else {
                //_rightGunController.SingleShot(WorldCamera.GetAimedPointInWorld(10000.0f, LayermaskRepository.Instance.PlayerBullet), time * 2);
                rightGunController.PrimaryShot(WorldCamera.GetRayInWorld(), new DamageTable(25.01f, 50.01f, 1.25f), time * 2);
            }
            lr = !lr;
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator CoroutineSpinShot() {
        float time = 0.1f;
        for (float i = 0.0f; i < 8.0f; i += 1.0f) {
            rightGunController.SecondaryShot(WorldCamera.GetRayInWorldWithRecoil(i /50.0f, i/40.0f), new DamageTable(15.01f, 25.01f, 1.1f), time);
            yield return new WaitForSeconds(time);
        }
    }
}