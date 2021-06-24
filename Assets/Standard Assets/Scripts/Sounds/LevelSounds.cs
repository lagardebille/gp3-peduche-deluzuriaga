using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LevelSounds : MonoBehaviour {

	AudioSource audio, bgAudio;
	public AudioClip levelFailed;
	public AudioClip levelFinished;
	public AudioClip button;
	public AudioClip star;
	// Use this for initialization
	void Start () {
		audio = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource> ();
	}
	
	public void PlayButton() {
		audio.PlayOneShot (button, PlayerPrefs.GetInt ("Sound"));
	}

	public void PlayGameOver() {
		audio.loop = false;
		audio.Stop ();
		if (!audio.isPlaying)audio.PlayOneShot(levelFailed, PlayerPrefs.GetInt ("Sound"));
	}
	public void PlayGameFinished() {
		audio.loop = false;
		audio.Stop ();
		if (!audio.isPlaying)audio.PlayOneShot (levelFinished, PlayerPrefs.GetInt ("Sound"));
	}
	public void PlayStar() {
		audio.PlayOneShot (star, PlayerPrefs.GetInt ("Sound"));
	}
}
