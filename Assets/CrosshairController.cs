using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CrosshairController : MonoBehaviour, IResizeElement {

    private Image _outline;
    private float _size;
    public float size { get { return _size; } }
    private float inGameScale;

    void Awake() {
        _outline = GetComponent<Image>();
    }

    public void SetSizeInPixels(int pixels) {
        _size = pixels;
        _outline.rectTransform.sizeDelta = Vector2.one * pixels;
    }

    public void AddSizeInPixels(int pixels) {
        _size += pixels;
        _outline.rectTransform.sizeDelta += Vector2.one * pixels;
    }

    public void SetScale(float scale) {
        inGameScale = scale;
        _outline.rectTransform.sizeDelta = Vector2.one * _size * scale;
    }
}
