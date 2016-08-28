using UnityEngine;
using System.Collections;

public class DualWieldingController : MonoBehaviour {

    public GameObject _leftGun;
    public GameObject _rightGun;
    private RevolverController _l;
    private RevolverController _r;
    bool state = true;
    bool lr = true;

    // Use this for initialization
    void Start () {
        _l = _leftGun.GetComponent<RevolverController>();
        _r = _rightGun.GetComponent<RevolverController>();

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
        float time = 0.35f;
        state = true;
        while (state) {
            if (lr) {
                _l.SingleShot(time*2);
            }
            else {
                _r.SingleShot(time*2);
            }
            lr = !lr;
            yield return new WaitForSeconds(time);
        }
    }

    public IEnumerator CoroutineSpinShot() {
        float time = 0.15f;
        for (int i = 0; i < 6; i++) {
            _r.SpinShot(time);
            yield return new WaitForSeconds(time);
            //_r.SpinShot(time);
            //yield return new WaitForSeconds(time/2);
        }
    }

}
