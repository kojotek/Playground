using UnityEngine;
using System.Collections;

public class GunWieldingController : MonoBehaviour {

    //Almost everything in this class is temporary and not suitable for use in future. Things will be changed when I'll design actual game mechanisms
    public IDamageDealer owner;
    public WeaponController leftGunController;
    public WeaponController rightGunController;
    public CameraController WorldCamera;
    private AvatarGunsState _gunState;

    bool state = true;
    bool lr = true;

    void Awake () {
        leftGunController.owner = this;
        rightGunController.owner = this;
        owner = GetComponent<IDamageDealer>();
    }

    void Start() {
        _gunState = new AvatarGunsStateIdle(this);
    }

    private void Update () {
        var newState = _gunState.Proceed();
        if (newState != null) {
            _gunState.Leave();
            _gunState = newState;
            _gunState.Enter();
        }
    }

    public IEnumerator CoroutineShot() {
        var damageTab = new DamageTable(6.0f, true, 12.0f, 1.25f);
        float time = 0.45f;
        state = true;
        while (state) {
            if (lr) {
                leftGunController.PrimaryShot(WorldCamera.GetRayInWorld(), damageTab, time*2);
            }
            else {
                //_rightGunController.SingleShot(WorldCamera.GetAimedPointInWorld(10000.0f, LayermaskRepository.Instance.PlayerBullet), time * 2);
                rightGunController.PrimaryShot(WorldCamera.GetRayInWorld(), damageTab, time * 2);
            }
            lr = !lr;
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator CoroutineSpinShot() {
        float time = 0.1f;
        for (float i = 0.0f; i < 8.0f; i += 1.0f) {
            var damageTab = new DamageTable(3.0f, false, 0.01f, (int)i % 7 == 0 ? 1.0f : 0.0f);
            float spread = Mathf.Clamp(i + Random.value, 0.0f, 5.0f);
            rightGunController.SecondaryShot(WorldCamera.GetRayInWorldWithRandomDirection(spread), damageTab, time);
            yield return new WaitForSeconds(time);
        }
    }
}