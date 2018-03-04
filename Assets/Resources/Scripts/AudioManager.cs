using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public AudioSource source;

	public AudioClip sfx_shot;
	public AudioClip sfx_move;
	public AudioClip sfx_death;
	public AudioClip sfx_reload;
	public AudioClip sfx_noammo;
	public AudioClip intro_sound;

	public bool mainGameplay;
	public static AudioManager dontDestroyInstance;

	void Start () {
		if (mainGameplay) {
			if (dontDestroyInstance == null) {
				instance = this;
				SceneManager.sceneLoaded += OnLevelLoad;
				dontDestroyInstance = this;
				DontDestroyOnLoad(this.gameObject);
			} else {
				Destroy(this.gameObject);
			}
		} else {
			instance = this;
		}
	}

	void OnLevelLoad(Scene scene, LoadSceneMode mode) {
		if (scene.name == "Victory") {
			Destroy(dontDestroyInstance.gameObject);
			dontDestroyInstance = null;
		}
	}

	public void Play(AudioClip clip) {
		source.PlayOneShot(clip);
	}

	public void PlayAndLoop(AudioClip clip) {
		source.clip = clip;
		source.loop = true;
		source.Play();
	}

	public void ChangeVolume(float volume) {
		source.volume = volume;
	}
}
