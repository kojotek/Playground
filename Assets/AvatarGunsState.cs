using UnityEngine;
using System.Collections;
using System;


public abstract class AvatarGunsState {

    protected GunWieldingController _controller;

    public AvatarGunsState(GunWieldingController controller) {
        _controller = controller;
    }

    public abstract AvatarGunsState Proceed();
    public abstract void Enter();
    public abstract void Leave();
}

public enum WhichGun { left, right }

public static class WhichGunExtension {

    public static WhichGun Another(this WhichGun which) {
        if (which == WhichGun.left)
            return WhichGun.right;
        else
            return WhichGun.left;
    }

}