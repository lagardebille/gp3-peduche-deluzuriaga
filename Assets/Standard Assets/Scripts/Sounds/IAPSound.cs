using UnityEngine;
using System.Collections;

public class IAPSound : MonoBehaviour {

	public AudioClip[] sfx = new AudioClip[6];
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	public void PlayBtn (int i) {
		audio.PlayOneShot(sfx[i], PlayerPrefs.GetInt("Sound"));
	}
}
