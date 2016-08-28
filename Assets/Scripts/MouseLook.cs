using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

    public enum RotationAxes { MouseXAndY, MouseX, MouseY }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float rotationY = 0F;

    void Update() {

        switch (axes) {
            case (RotationAxes.MouseXAndY):
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                break;

            case (RotationAxes.MouseX):
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                break;

            case (RotationAxes.MouseY):
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                break;
        }
    }

}
