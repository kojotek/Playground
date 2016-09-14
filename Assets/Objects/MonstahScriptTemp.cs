using UnityEngine;
using System.Collections;

public class MonstahScriptTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().speed = 2.0f;

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, -65.0f), ForceMode.VelocityChange);
    }
}
