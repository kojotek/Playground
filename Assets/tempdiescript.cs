using UnityEngine;
using System.Collections;

public class tempdiescript : MonoBehaviour {

    private Animator _animator;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.F)) {
            _animator.SetFloat("hp", -1.0f);
        }
	}
}
