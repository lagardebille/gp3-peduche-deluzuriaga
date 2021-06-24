using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MainMenuSounds : MonoBehaviour {

	public AudioClip[] audios;
	//0 = main menu bgm
	//1 = start btn
	//2 = click btn
	//3 = select chapter
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		audio.volume = 2f;
		/*	audio.clip = audios [0];
		audio.loop = true;
		audio.Play ();*/

	}

	void Update() {
		audio.volume = PlayerPrefs.GetInt ("Sound");
	}
	public void PlayButtonSound(int i) {
		audio.PlayOneShot (audios [i], PlayerPrefs.GetInt ("Sound"));
	}
}
