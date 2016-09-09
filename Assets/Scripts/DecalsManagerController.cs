using UnityEngine;
using System.Collections;

public class DecalsManagerController : MonoBehaviour {

    [SerializeField]
    public GameObject BulletHoleTemplate;
    private static DecalsManagerController _controller;

    void Awake() {
        _controller = GetComponent<DecalsManagerController>();
    }
	
    public void CreateShotEffect(RaycastHit hit) {
        var decalReceiver = hit.collider.gameObject.GetComponent<DecalsReceiver>();
        if(decalReceiver != null) {
            int elem = Random.Range(0, decalReceiver.AcceptableDecals.Count);
            var effect = Instantiate(decalReceiver.AcceptableDecals[elem], hit.point, Quaternion.identity) as GameObject;
            effect.transform.up = hit.normal;
            effect.transform.Rotate(Vector3.down * Random.value * 360.0f);
            effect.transform.parent = hit.collider.transform;
            effect.SetActive(true);
        }

    }

    public static DecalsManagerController GetInstance() {
        return _controller;
    }

}
