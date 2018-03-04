using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public AudioSource source;

	public AudioClip sfx_shot;
	public AudioClip sfx_move;
	public AudioClip sfx_death;
	public AudioClip sfx_reload;
	public AudioClip sfx_noammo;

	void Start () {
		instance = this;
	}

	public void Play(AudioClip clip) {
		source.PlayOneShot(clip);
	}
}
