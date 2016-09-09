using UnityEngine;
using System.Collections;

public class SingleEmissionParticleController : MonoBehaviour {

    public int Min = 10;
    public int Max = 20;

    private ParticleSystem _system;

	void Awake () {
        _system = GetComponent<ParticleSystem>();
	}
	
    void Start() {
        _system.Emit(Random.Range(Min, Max));
    }
}