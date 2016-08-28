using UnityEngine;
using System.Collections;

public class RevolverTrailController : MonoBehaviour {

    private Xft.XWeaponTrail _trail;

	void Awake () {
        _trail = GetComponent<Xft.XWeaponTrail>();
	}

    public void SetTrailLength(int length) {
        if (_trail.isActiveAndEnabled && length == _trail.MaxFrame) {
            //DO NOTHING
        }
        else {
            _trail.Deactivate();
            if (length > 0) {
                _trail.MaxFrame = length;
                _trail.Activate();
            }
        }
    }

    public void FadeInTime(float time) {
        _trail.StopSmoothly(time);
    }
}
