using UnityEngine;
using System.Collections;

public class RedHotEffectController : MonoBehaviour {

    private Renderer _renderer;
    private Material _material;

    void Awake() {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    public void SetEmissionIntensity(float intensity) {
        _material.SetColor ("_EmissionColor", new Color(intensity, 0.0f, 0.0f));
    }
}
