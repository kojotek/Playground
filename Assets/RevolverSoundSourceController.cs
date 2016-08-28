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
}
