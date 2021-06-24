using UnityEngine;
using System.Collections;

public class DestroyArtillery : MonoBehaviour {

	public AudioClip detonate;
	AudioSource audio;
	public GameObject overlay;
	bool isPlaying = false;
	float time = 2f;
	// Use this for initialization
	void Start () {
		overlay = GameObject.FindGameObjectWithTag("Overlay");
		audio = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource> ();
		audio.PlayOneShot (detonate, PlayerPrefs.GetInt ("Sound"));
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
	}

	IEnumerator dest() {
		yield return new WaitForSeconds(2f);
		overlay.transform.GetChild(0).gameObject.SetActive (false);
		Destroy (gameObject);
	}
}
