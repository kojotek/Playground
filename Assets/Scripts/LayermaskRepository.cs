using UnityEngine;
using System.Collections;

public class LayermaskRepository{

    public LayerMask AllLayers;
    public LayerMask PlayerBullet;



    private static LayermaskRepository _instance = new LayermaskRepository();
    private static bool initialised = false;

    public static LayermaskRepository Instance{
        get {
            if (!initialised) {
                _instance.Init();
                initialised = true;
            }
            return _instance;
        }
    }

    private void Init() {
        _instance.AllLayers.value = int.MaxValue;
        _instance.PlayerBullet.value = 0;
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Terrain"));
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Dynamic Terrain Element"));
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Enemy Body Part"));
    }

}
