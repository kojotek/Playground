using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraController))]
public class HUDController : MonoBehaviour {

    [SerializeField]
    public CurvedBarManager sharpshooterBar;
    [SerializeField]
    public CurvedBarManager coldbloodBar;
    [SerializeField]
    public TargetHealthBarController targetHealth;
    [SerializeField]
    public CrosshairController crosshair;
    private CameraController _camera;

    private 

    void Awake() {
        _camera = GetComponent<CameraController>();
    }

    void Start() {
        SetSharpshooterValue(0.0f);
        SetColdbloodValue(0.0f);
    }

	void SetSharpshooterValue(float value) {
        sharpshooterBar.SetValue(value);
    }

    void SetColdbloodValue(float value) {
        coldbloodBar.SetValue(value);
    }

    void SetCrosshairScale(float value) {
        crosshair.SetScale(value);
    }

    void Update()
    {


        /**************************************************/
        /**************************************************/


        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            sharpshooterBar.AddSizeInPixels(10);
            coldbloodBar.AddSizeInPixels(10);
            targetHealth.AddSizeInPixels(10);
            crosshair.AddSizeInPixels(10);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            sharpshooterBar.AddSizeInPixels(-10);
            coldbloodBar.AddSizeInPixels(-10);
            targetHealth.AddSizeInPixels(-10);
            crosshair.AddSizeInPixels(-10);
        }
    }
}