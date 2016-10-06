using UnityEngine;
using System.Collections;

public class LayermaskRepository{

    public LayerMask AllLayers;
    public LayerMask PlayerBullet;
    public LayerMask PlayerBoomerangBullet;
    public LayerMask PlayerSelectEnemy;
    public LayerMask PlayerSelectEnemyBodyPart;


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
        _instance.AllLayers.value = -1;

        _instance.PlayerBullet.value = 0;
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Terrain"));
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Dynamic Terrain Element"));
        _instance.PlayerBullet.value |= (1 << LayerMask.NameToLayer("Enemy Body Part"));

        _instance.PlayerBoomerangBullet.value = 0;
        _instance.PlayerBoomerangBullet.value |= (1 << LayerMask.NameToLayer("Enemy Body Part"));

        _instance.PlayerSelectEnemy.value = 0;
        _instance.PlayerSelectEnemy.value |= (1 << LayerMask.NameToLayer("Enemy"));
        _instance.PlayerSelectEnemy.value |= (1 << LayerMask.NameToLayer("Terrain"));
        _instance.PlayerSelectEnemy.value |= (1 << LayerMask.NameToLayer("Dynamic Terrain Element"));

        _instance.PlayerSelectEnemyBodyPart.value = 0;
        _instance.PlayerSelectEnemyBodyPart.value |= (1 << LayerMask.NameToLayer("Enemy Body Part"));
        _instance.PlayerSelectEnemyBodyPart.value |= (1 << LayerMask.NameToLayer("Terrain"));
        _instance.PlayerSelectEnemyBodyPart.value |= (1 << LayerMask.NameToLayer("Dynamic Terrain Element"));
    }

}
