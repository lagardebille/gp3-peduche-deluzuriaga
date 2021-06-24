using UnityEngine;
using System.Collections;

public class SetVolume : MonoBehaviour {

	public GameObject snake;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (snake == null) snake = GameObject.FindGameObjectWithTag("Player");
		bool moving = false;
		if (snake != null) moving = snake.GetComponent<Snake> ().isMoving;
		if (gameObject.tag != "SoundManager") {
			if (gameObject.tag.Equals("Enemy")) {
				if (moving)GetComponent<AudioSource> ().volume = PlayerPrefs.GetInt ("Sound");
				else GetComponent<AudioSource> ().volume = 0f;
			} else GetComponent<AudioSource> ().volume = PlayerPrefs.GetInt ("Sound");
		} else {
			GetComponent<AudioSource>().volume = PlayerPrefs.GetInt ("Sound")*0.4f;
		}
	}	
}
