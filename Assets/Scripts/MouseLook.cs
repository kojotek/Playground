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
    private Vector3 rotation = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private Vector3 veloc;

    void Update() {

        switch (axes) {
            case (RotationAxes.MouseXAndY):
                destination.x = rotation.x + Input.GetAxis("Mouse X") * sensitivityX;
                destination.y += Input.GetAxis("Mouse Y") * sensitivityY;
                destination.y = Mathf.Clamp(rotation.y, minimumY, maximumY);
                //transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                break;

            case (RotationAxes.MouseX):
                //transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                destination.x = rotation.x + Input.GetAxis("Mouse X") * sensitivityX;
                break;

            case (RotationAxes.MouseY):
                destination.y += Input.GetAxis("Mouse Y") * sensitivityY;
                destination.y = Mathf.Clamp(destination.y, minimumY, maximumY);

                //transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                break;
        }
    }

    void FixedUpdate() {
        rotation = Vector3.SmoothDamp(rotation, destination, ref veloc, 0.05f);
        //rotation = Vector3.Lerp(rotation, destination, 0.6f);
        transform.localEulerAngles = new Vector3(-rotation.y, 2.0f * rotation.x, 0);
    }

}
