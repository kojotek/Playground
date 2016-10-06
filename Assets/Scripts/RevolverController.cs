using UnityEngine;
using System.Collections;

public class RevolverController : WeaponController {

    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public RevolverParticleSystemController _particleSystem;
    [HideInInspector]
    public RevolverTrailController _trail;
    [HideInInspector]
    public RevolverSoundSourceController _soundSource;
    [HideInInspector]
    public RedHotEffectController _redHotEffect;
    private float _heat;

	private void Awake () {
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<RevolverParticleSystemController>();
        _soundSource = GetComponentInChildren<RevolverSoundSourceController>();
        _trail = GetComponentInChildren<RevolverTrailController>();
        _redHotEffect = GetComponentInChildren<RedHotEffectController>();
        gunTip = GetComponentInChildren<GunTipController>();
    }

    private void Start() {
        _animator.Play("idle");
    }

    private void FixedUpdate() {
        _heat = Mathf.Clamp(_heat - (0.1f * Time.fixedDeltaTime), 0.0f, 1.0f);
        _redHotEffect.SetEmissionIntensity(_heat);
    }

    public override void PrimaryShot(Ray rayFromCamera, DamageTable table, float time) {
       //ShotResult result = new ShotResult(CamerasManagerController.Instance.WeaponToWorld(_gunTip.transform.position, 3.0f), rayFromCamera, 3000.0f);
        _animator.speed = 1.01f / time;
        _animator.Play("shot", 0, 0.0f);
        SingleShotProjectile bshp = new SingleShotProjectile(this, CamerasManagerController.Instance.WeaponToWorld(gunTip.transform.position, 3.0f), rayFromCamera, table);
        bshp.Init();
        //_tracerGenerator.GenerateSingleShotTracer(result.point);
        //if (result.hit) {
        //    DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
        //}
        _heat += 0.02f;
    }

    public override void SecondaryShot(Ray rayFromCamera, DamageTable table, float time)
    {
        //ShotResult result = new ShotResult(CamerasManagerController.Instance.WeaponToWorld(_gunTip.transform.position, 3.0f), rayFromCamera, 3000.0f);
        _animator.speed = 1.0f / time;
        _animator.PlayInFixedTime("spinshot", 0, 0.0f);
        SingleShotProjectile bshp = new SingleShotProjectile(this, CamerasManagerController.Instance.WeaponToWorld(gunTip.transform.position, 3.0f), rayFromCamera, table);
        bshp.Init();
        //_tracerGenerator.GenerateSingleShotTracer(result.point);
        //if (result.hit) {
        //    DecalsManagerController.Instance.CreateShotEffect(result.raycastHit);
        //}
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