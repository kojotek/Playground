using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraController))]
public class CrossFadeElementOnRaycast : MonoBehaviour {

    public float distance = 8000.0f;
    public bool displayOnHit = true;    //TODO: apply
    public float crossFadeTime = 1.0f;

    [SerializeField]
    private TargetHealthBarController _element; //TODO: IFadeElement
    [SerializeField]
    private CameraController _camera;
    private ActorController _currentActor;
    private bool _targetSelected = false;

    void Awake() {
        _camera = GetComponent<CameraController>();
    }


    void Update() {
        var ray = _camera.GetRayInWorld();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, LayermaskRepository.Instance.PlayerSelectEnemyBodyPart)) {
            var hitBox = hit.collider.gameObject.GetComponent<HitBoxController>();

            if (hitBox == null && _currentActor != null) {
                OnDeselectCurrentActor();
            }

            if (hitBox != null) {
                var hitedActor = hitBox.damageReceiver.actor;
                OnSelect(hitedActor);
            }

            if (hitBox != null) {
                var hitedActor = hitBox.damageReceiver.actor;
                if (hitedActor == null) {
                    OnDeselectCurrentActor();
                }
            }

        }

        if (_currentActor != null) {
            _element.SetValue(_currentActor.hpPercent);
        }
    }

    void OnSelect(ActorController actor) {
        if (actor != null) {
            if (_currentActor == null) {
                _element.ShowElement(crossFadeTime);
            }
            _currentActor = actor;
        }
    }

    void OnDeselectCurrentActor() {
        _element.HideElement(crossFadeTime);
        _currentActor = null;
    }
}