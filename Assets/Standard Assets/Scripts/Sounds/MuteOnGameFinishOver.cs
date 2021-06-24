using UnityEngine;
using System.Collections;

public class MuteOnGameFinishOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Snake> ().isPlaying) {
			GetComponent<AudioSource> ().volume = 0f;
		} else {
			GetComponent<AudioSource> ().volume = 0f;
		}
	}
}
