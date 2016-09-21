using UnityEngine;
using System.Collections;

public class BoomernagParticleSystemController : MonoBehaviour {

    public ParticleSystem BoomerangMuzzleFlashParticle;

    public void BurstParticles() {
        BoomerangMuzzleFlashParticle.Emit(30);
    }
}
