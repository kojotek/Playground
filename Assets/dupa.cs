using UnityEngine;
using System.Collections;

public class dupa : MonoBehaviour {

    void OnEnable() {
        Debug.Log("Enabled");
    }

    public void EnableThisShit() {
        enabled = false;
        enabled = true;

    }
}
