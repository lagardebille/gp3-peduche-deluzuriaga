using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	AudioSource audioSource;
	public AudioClip[] sounds = new AudioClip[7];
	bool isAudioInitialized = false;
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		audioSource = GetComponent<AudioSource> ();
		audioSource.volume = 0.5f;
	}

	void OnLevelWasLoaded (int i) {
		isAudioInitialized = false;
	}
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (gameObject);
		if (!isAudioInitialized) {
			if (Application.loadedLevelName.Equals ("MainMenu") || Application.loadedLevelName.Equals ("IAP")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [0];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level1") || Application.loadedLevelName.Equals ("Level2") || 
				Application.loadedLevelName.Equals ("Level3") || Application.loadedLevelName.Equals ("Level4")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [1];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level5") || Application.loadedLevelName.Equals ("Level6") || 
				Application.loadedLevelName.Equals ("Level7") || Application.loadedLevelName.Equals ("Level8")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [2];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level9") || Application.loadedLevelName.Equals ("Level10") || 
				Application.loadedLevelName.Equals ("Level11") || Application.loadedLevelName.Equals ("Level12")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [3];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level13") || Application.loadedLevelName.Equals ("Level14") || 
				Application.loadedLevelName.Equals ("Level15") || Application.loadedLevelName.Equals ("Level16")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [4];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level17") || Application.loadedLevelName.Equals ("Level18") || 
				Application.loadedLevelName.Equals ("Level19") || Application.loadedLevelName.Equals ("Level20")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [5];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} else if (Application.loadedLevelName.Equals ("Level21") || Application.loadedLevelName.Equals ("Level22") || 
				Application.loadedLevelName.Equals ("Level23") || Application.loadedLevelName.Equals ("Level24")) {
				isAudioInitialized = true;
				audioSource.clip = sounds [6];
				audioSource.loop = true;
				if (!audioSource.isPlaying)
					audioSource.Play ();
			} 
		}
	}
}
