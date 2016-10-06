using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TargetHealthBarController : MonoBehaviour, IFadeElement {

    [SerializeField]
    private Image _outline;
    [SerializeField]
    private Image _fill;

	void Start () {
        _outline.CrossFadeAlpha(0.0f, 0.0f, true);
        _fill.CrossFadeAlpha(0.0f, 0.0f, true);
	}
	
    /*
    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            ShowHealthBar();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            HideHealthBar();
        }
    }
    */

    public void ShowElement(float time) {
        _outline.CrossFadeAlpha(1.0f, time, true);
        _fill.CrossFadeAlpha(1.0f, time, true);
    }

    public void HideElement(float time) {
        _outline.CrossFadeAlpha(0.0f, time, true);
        _fill.CrossFadeAlpha(0.0f, time, true);
    }

    public void SetValue(float value) {
        _fill.fillAmount = Mathf.Clamp(0.0f, value, 1.0f);
    }

    public void SetSizeInPixels(int pixels) {
        _fill.rectTransform.sizeDelta = Vector2.one * pixels;
        _outline.rectTransform.sizeDelta = Vector2.one * pixels;
    }

    public void AddSizeInPixels(int pixels) {
        _fill.rectTransform.sizeDelta += Vector2.one * pixels;
        _outline.rectTransform.sizeDelta += Vector2.one * pixels;
    }
}
