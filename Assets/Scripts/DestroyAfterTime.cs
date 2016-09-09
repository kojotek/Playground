using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float LifeTime = 10.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, LifeTime);
	}
}
