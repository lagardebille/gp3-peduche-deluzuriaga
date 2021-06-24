using UnityEngine;
using System.Collections;

public class PowerupSound : MonoBehaviour {

	public AudioClip[] sfx = new AudioClip[5];
	AudioSource audio;
	//0 cap 1 freeze 2 invin 3 pak 4 pass
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	} 
	
	// Update is called once per frame
	public void PlaySFX(GameObject g) {
		int i = g.GetComponent<PowerupBehaviour> ().whatPowerup - 1;
		if (i >= 0) {
			if (i == 2) {
				AudioSource playerAud = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
				playerAud.clip = sfx [i];
				playerAud.loop = true;
				playerAud.Play();
			}
			else audio.PlayOneShot (sfx [i], PlayerPrefs.GetInt ("Sound"));
		}
	}
}
