using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurvedBarManager : MonoBehaviour {

    private Image _bar;

	void Awake () {
        _bar = GetComponent<Image>();
        _bar.fillAmount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        _bar.fillAmount = _bar.fillAmount + 0.01f;


        if (_bar.fillAmount >= 1) {
            _bar.fillAmount = 0.0f;
        }
	}
}
