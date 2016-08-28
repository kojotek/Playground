using UnityEngine;
using System.Collections;

public class RevolverParticleSystemController : MonoBehaviour {

    public GameObject SingleShotMuzzleFlashObject;
    private ParticleSystem _singleShotMuzzleFlash;

    public GameObject SpinShotMuzzleFlashObject;
    private ParticleSystem _spinShotMuzzleFlash;

    private void Awake() {
        _singleShotMuzzleFlash = SingleShotMuzzleFlashObject.GetComponent<ParticleSystem>();
        _spinShotMuzzleFlash = SpinShotMuzzleFlashObject.GetComponent<ParticleSystem>();
    }
	
	public void BurstSingleShotParticles() {
        _singleShotMuzzleFlash.Emit(30);
	}

    public void BurstSpinShotParticles() {
        _spinShotMuzzleFlash.Emit(17);
    }
}
