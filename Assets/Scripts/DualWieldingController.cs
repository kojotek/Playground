using UnityEngine;
using System.Collections;

public class DualWieldingController : MonoBehaviour {

    public GameObject LeftGun;
    public GameObject RightGun;
    private RevolverController _leftGunController;
    private RevolverController _rightGunController;
    public CameraController WorldCamera;
    //public CameraController CharacterCamera;
    bool state = true;
    bool lr = true;

    // Use this for initialization
    void Awake () {
        _leftGunController = LeftGun.GetComponent<RevolverController>();
        _rightGunController = RightGun.GetComponent<RevolverController>();

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
                _leftGunController.SingleShot(WorldCamera.GetAimedPointInWorld(10000.0f), time*2);
            }
            else {
                _rightGunController.SingleShot(WorldCamera.GetAimedPointInWorld(10000.0f), time * 2);
            }
            lr = !lr;
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator CoroutineSpinShot() {
        float time = 0.1f;
        for (float i = 0.0f; i < 8.0f; i += 1.0f) {
            _rightGunController.SpinShot(WorldCamera.GetAimedPointInWorld(10000.0f, i/50.0f, i/40.0f), time);
            yield return new WaitForSeconds(time);
        }
    }
}