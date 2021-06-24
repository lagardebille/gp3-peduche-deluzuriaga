using UnityEngine;
using System.Collections;

public class JacqueBehaviour : MonoBehaviour {

	GameObject snakeHead;
	public AudioClip detonate;
	AudioSource audio;
	public bool audioPlaying = false, audioPlaying2 = false;
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			gameObject.SetActive (false);
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		if (GetComponent<EnemySpeed> ().isTraitEnabled && snakeHead != null) {
			transform.Translate (Vector3.down * Time.deltaTime * GetComponent<EnemySpeed>().speed);
			float distance = Vector3.Distance (transform.position, snakeHead.transform.position);
			if (distance <= 4) {
				GetComponent<EnemySpeed>().speed = 0;
				GetComponent<Animator> ().SetTrigger ("Dead");
				StartCoroutine (DelayDeath ());
				if (!audioPlaying) {
					audio.PlayOneShot (detonate, PlayerPrefs.GetInt ("Sound"));
					audioPlaying = true;
				}
			}
			if (transform.position.y <= -14f) {
				gameObject.SetActive(false);
			}
		}
	}

	IEnumerator DelayDeath() {
		yield return new WaitForSeconds(2f);
	
		yield return new WaitForSeconds(0.4f);
		gameObject.SetActive (false);
	}
}
