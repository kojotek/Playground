﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurvedBarManager : MonoBehaviour {

    private Image _bar;
    [SerializeField]
    private Image _outline;


    void Awake () {
        _bar = GetComponent<Image>();
        _bar.fillAmount = 0.0f;
	}
	
    public void SetValue(float value) {
        _bar.fillAmount = Mathf.Clamp(0.0f, value, 1.0f);
    }

    public void SetSizeInPixels(int pixels){
        _bar.rectTransform.sizeDelta = Vector2.one * pixels;
        _outline.rectTransform.sizeDelta = Vector2.one * pixels;
    }

    public void AddSizeInPixels(int pixels){
        _bar.rectTransform.sizeDelta += Vector2.one * pixels;
        _outline.rectTransform.sizeDelta += Vector2.one * pixels;
    }
}
