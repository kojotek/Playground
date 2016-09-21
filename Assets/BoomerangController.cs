using UnityEngine;
using System.Collections;
using System;

public class BoomerangController : MonoBehaviour, INotifyHit {

    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public BoomernagParticleSystemController _particleSystem;
    [HideInInspector]
    public RevolverTrailController _trail;
    [HideInInspector]
    public RevolverSoundSourceController _soundSource;
    [HideInInspector]
    public TracerGeneratorController _tracerGenerator;
    [HideInInspector]
    public RedHotEffectController _redHotEffect;
    [HideInInspector]
    public RotateAroundObject _movementController;
    public Transform RotationAxis;

    public float RotationSpeed = 250.0f;
    public float FireRate = 2.0f;

    public IDamageDealer Owner { get; set; }

    private void Awake() {
        //
        Owner = GameObject.Find("Avatar").GetComponent<IDamageDealer>();
        //
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponentInChildren<BoomernagParticleSystemController>();
        _soundSource = GetComponentInChildren<RevolverSoundSourceController>();
        _trail = GetComponentInChildren<RevolverTrailController>();
        _tracerGenerator = GetComponentInChildren<TracerGeneratorController>();
        _redHotEffect = GetComponentInChildren<RedHotEffectController>();
        //_gunTip = GetComponentInChildren<GunTipController>();
        _movementController = GetComponentInParent<RotateAroundObject>();
    }

    private void Start() {
        StartCoroutine(ShotCoroutine());
    }

    private void FixedUpdate() {
        RotationAxis.Rotate(-RotationSpeed * 60.0f * Time.deltaTime, 0.0f, 0.0f);
    }

    IEnumerator ShotCoroutine() {
        while (true) {
            Shot();
            yield return new WaitForSeconds(1.0f / FireRate);
        }
    }

    void Shot() {
        if (_movementController.targetInRange) {
            //BoomerangProjectile bshp = new BoomerangProjectile (this, _gunTip.transform.position, new Ray(_gunTip.transform.position, (_movementController.targetCenter - _gunTip.transform.position).normalized));
            //bshp.Init();
            var targetDamageReceiver = _movementController.Target.gameObject.GetComponent<SkeletonDamageReceiver>();

            //_soundSource.PlaySingleShotSound();
            //_particleSystem.BurstParticles();

            if (targetDamageReceiver != null) {
                targetDamageReceiver.ReceiveDamage();
                NotifyAboutHit();

                RaycastHit hit;
                if (Physics.Raycast(transform.position, 
                    _movementController.targetCenter - transform.position,
                    out hit,
                    _movementController.radius * 2.0f, 
                    LayermaskRepository.Instance.PlayerBoomerangBullet)){
                        DecalsManagerController.Instance.CreateShotEffect(hit);
                }

               
            }
        }
        
    }

    public void NotifyAboutHit() {
        Owner.OnAttackHitTarget();
    }
}
