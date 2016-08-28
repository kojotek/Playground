using UnityEngine;
using System.Collections;

public class GraphicsController : MonoBehaviour {

    void Awake() {
        Screen.SetResolution(640, 480, true, 60);
        Cursor.visible = false;
    }
}
