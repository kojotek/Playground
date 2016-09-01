using UnityEngine;
using System.Collections;

public class RevolverSoundSourceController : MonoBehaviour {

    private AudioSource _audio;

	private void Awake () {
        _audio = GetComponent<AudioSource>();
	}
	
    public void PlaySingleShotSound() {
        _audio.pitch = Random.Range(0.95f, 1.05f);
        _audio.Play();
    }

    public void PlaySpinShotSound() {
        _audio.pitch = Random.Range(1.05f, 1.15f);
        _audio.Play();
    }
}
