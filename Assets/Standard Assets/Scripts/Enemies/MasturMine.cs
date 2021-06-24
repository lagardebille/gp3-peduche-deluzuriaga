using UnityEngine;
using System.Collections;

public class MasturMine : MonoBehaviour {

	public GameObject player;
	float time = 4f;
	bool hasEnemy = false;
	public bool shouldDest = false;
	AudioSource audio;
	bool audioPlaying = false;
	public AudioClip detonate;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		audio = GetComponent<AudioSource> ();
	}
		
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null && GetComponent<EnemySpeed> ().isTraitEnabled) {
			if (!hasEnemy) {
				time -= Time.deltaTime;
				if (time <= 0) {
					GetComponent<Animator> ().SetTrigger ("Exit");
					StartCoroutine (DelayDeath (0.5f));

					hasEnemy = true;
				}
			}
			if (Vector3.Distance(transform.position, player.transform.position) < 6 && !hasEnemy) {
				GetComponent<Animator> ().SetTrigger ("PlayDeath");
				if (!audioPlaying) {
					audio.PlayOneShot (detonate, PlayerPrefs.GetInt ("Sound"));
					audioPlaying = true;
				}
				hasEnemy = true;
			}
		}

		if (shouldDest)
			Destroy (gameObject);
	}
		
	IEnumerator DelayDeath(float f) {
		yield return new WaitForSeconds (f);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag.Equals ("Enemy")) {
			Destroy(gameObject);
		}
	}
}
