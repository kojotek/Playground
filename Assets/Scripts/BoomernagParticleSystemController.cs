using UnityEngine;
using System.Collections;

public class BoomernagParticleSystemController : MonoBehaviour {

    public ParticleSystem BoomerangExplosionParticle;
    public ParticleSystem BoltsParticle;
    public ParticleSystem GearsParticle;

    public void BurstExplosionParticles() {
        BoomerangExplosionParticle.Emit(300);
        BoltsParticle.Emit(12);
        GearsParticle.Emit(12);
    }
}
