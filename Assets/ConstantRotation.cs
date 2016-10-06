using UnityEngine;
using System.Collections;

public class ConstantRotation : MonoBehaviour {

    public float rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(-rotationSpeed * 60.0f * Time.deltaTime, 0.0f, 0.0f);
    }

}
