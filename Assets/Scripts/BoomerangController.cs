using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class BoomerangController : MonoBehaviour, INotifyHit {

    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public RevolverTrailController _trail;
    [HideInInspector]
    public RevolverSoundSourceController _soundSource;
    //[HideInInspector]
    //public TracerGeneratorController _tracerGenerator;
    [HideInInspector]
    public RedHotEffectController _redHotEffect;
    [HideInInspector]
    private BoomerangMovementController _movementController;

    public GameObject DestroyParticlesObject;
    public GameObject RootBoomerangObject;

    private DamageReceiver _targetDamageReceiver;
    private ActorController _targetEnemyController;

    public float FireRate = 2.0f;
    private DamageTable _damageTable;
    private float _timePassed;

    public IDamageDealer Owner { get; set; }


    public void Init(IDamageDealer owner, Transform target, DamageTable table){
        Owner = owner;
        _damageTable = table;
        SetTarget(target);
    }


    private void Awake() {
        _animator = GetComponent<Animator>();
        _soundSource = GetComponentInChildren<RevolverSoundSourceController>();
        _trail = GetComponentInChildren<RevolverTrailController>();
        //_tracerGenerator = GetComponentInChildren<TracerGeneratorController>();
        _redHotEffect = GetComponentInChildren<RedHotEffectController>();
        _movementController = GetComponentInParent<BoomerangMovementController>();
    }


    void Update() {
        if (!_targetEnemyController.alive){
            var nextEnemy = GetClosestLivingEnemy();
            if (nextEnemy != null){
                SetTarget(nextEnemy.transform);
            }
            else{
                var particles = Instantiate(DestroyParticlesObject, transform.position, Quaternion.identity) as GameObject;
                particles.SetActive(true);
                Destroy(RootBoomerangObject);
            }
        }

        if (_movementController.targetInRange) {
            _timePassed += Time.deltaTime;
            while(_timePassed >= (1.0f / FireRate))
            {
                _timePassed -= (1.0f / FireRate);
                //_soundSource.PlaySingleShotSound();
                //_particleSystem.BurstParticles();
                if (_targetDamageReceiver != null)
                {
                    _targetDamageReceiver.ReceiveDamage(_damageTable, false);
                    AttackResult result = new AttackResult(_targetEnemyController,_damageTable, false, AttackType.Boomerang);
                    NotifyAboutHit(result);

                    RaycastHit hit;
                    if (Physics.Raycast(transform.position,
                        _movementController.targetCenter - transform.position,
                        out hit,
                        _movementController.radius * 2.0f,
                        LayermaskRepository.Instance.PlayerBoomerangBullet))
                    {
                        DecalsManagerController.Instance.CreateShotEffect(hit);
                    }
                }
            }
            
        }   
    }

    public void NotifyAboutHit(AttackResult effect)
    {
        Owner.OnAttackHitTarget(effect);
    }

    private void SetTarget(Transform newTarget)
    {
        _movementController.target = newTarget;
        _targetDamageReceiver = _movementController.target.gameObject.GetComponent<DamageReceiver>();
        _targetEnemyController = _movementController.target.gameObject.GetComponent<ActorController>();
    }

    private GameObject GetClosestLivingEnemy()
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");

        var alives = objectsWithTag
            .Where(x => x.GetComponentInChildren<ActorController>() != null)
            .Where(x => x.GetComponentInChildren<ActorController>().alive)
            .ToList();

        if (alives.Count == 0){
            return null;
        }

        return alives.OrderByDescending(x => Vector3.Distance(x.transform.position, transform.position))
            .First();
    }
}
