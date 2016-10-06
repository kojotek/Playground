using UnityEngine;
using System.Collections;

public class SkillSpawnerController : MonoBehaviour {

    [SerializeField]
    private GameObject _boomerangTemplate;
    private IDamageDealer owner;

	void Awake () {
        owner = GetComponentInParent<IDamageDealer>();
	}

    public void SpawnBoomerang(Ray cameraRay, DamageTable table){
        RaycastHit hit;
        Transform target;
        if (Physics.Raycast(cameraRay, out hit, 3000.0f, LayermaskRepository.Instance.PlayerSelectEnemy))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")){
                target = hit.transform;
                GameObject boomerang = Instantiate(_boomerangTemplate, transform.position, Quaternion.identity) as GameObject;
                boomerang.SetActive(true);
                BoomerangController controller = boomerang.GetComponentInChildren<BoomerangController>();
                controller.Init(owner, target, table);
            }
        }


    }
}
