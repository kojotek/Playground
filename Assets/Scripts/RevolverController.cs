using UnityEngine;
using System.Collections;

public class RevolverController : MonoBehaviour {

    public Animator _animator;
    public RevolverParticleSystemController _particleSystem;
    public RevolverTrailController _trail;
    public RevolverSoundSourceController _soundSource;
    public RevolverTracerGeneratorController _tracerGenerator;
    public RevolverGunTipController _gunTip;
    public RedHotEffectController _redHotEffect;
    private float _heat;

	private void Awake () {
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<RevolverParticleSystemController>();
        _soundSource = GetComponentInChildren<RevolverSoundSourceController>();
        _trail = GetComponentInChildren<RevolverTrailController>();
        _tracerGenerator = GetComponentInChildren<RevolverTracerGeneratorController>();
        _redHotEffect = GetComponentInChildren<RedHotEffectController>();
        _gunTip = GetComponentInChildren<RevolverGunTipController>();
    }

    private void Start() {
        _animator.Play("idle");
    }

    private void FixedUpdate() {
        _heat = Mathf.Clamp(_heat - (0.1f * Time.fixedDeltaTime), 0.0f, 1.0f);
        _redHotEffect.SetEmissionIntensity(_heat);
    }

    public void SingleShot(Ray rayFromCamera, float time) {
        ShotResult result = new ShotResult(CamerasManagerController.Instance.WeaponToWorld(_gunTip.transform.position, 3.0f), rayFromCamera, 3000.0f);
        _animator.speed = 1.01f / time;
        _animator.Play("shot", 0, 0.0f);
        _tracerGenerator.GenerateSingleShotTracer(result.point);
        if (result.hit) {
            DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
        }
        _heat += 0.02f;
    }

    public void SpinShot(Ray rayFromCamera, float time) {
        ShotResult result = new ShotResult(CamerasManagerController.Instance.WeaponToWorld(_gunTip.transform.position, 3.0f), rayFromCamera, 3000.0f);
        _animator.speed = 1.0f / time;
        _animator.PlayInFixedTime("spinshot", 0, 0.0f);
        _tracerGenerator.GenerateSingleShotTracer(result.point);
        if (result.hit) {
            DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
        }
        _heat += 0.06f;
    }


    //Animation functions

    private void BurstSingleShotParticles() {
        _particleSystem.BurstSingleShotParticles();
    }

    private void BurstSpinShotParticles() {
        _particleSystem.BurstSpinShotParticles();
    }

    private void SetTrailLength(int length) {
        _trail.SetTrailLength(length);
    }

    private void FadeTrailInTime(float time) {
        _trail.FadeInTime(time);
    }

    private void PlaySingleShotSound() {
        _soundSource.PlaySingleShotSound();
    }

    private void PlaySpinShotSound() {
        _soundSource.PlaySpinShotSound();
    }

    private void ResetAnimationSpeed() {
        _animator.speed = 1.0f;
    }

}
