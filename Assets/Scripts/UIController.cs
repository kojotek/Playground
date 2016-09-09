using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public CurvedBarManager SharpshooterBar;
    public CurvedBarManager ColdbloodBar;

    void Awake() {
        SetSharpshooterValue(0.0f);
        SetColdbloodValue(0.0f);
    }

	void SetSharpshooterValue(float value) {
        SharpshooterBar.SetValue(value);
    }

    void SetColdbloodValue(float value) {
        ColdbloodBar.SetValue(value);
    }
}