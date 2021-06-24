using UnityEngine;
using System.Collections;

public class Jacque360 : MonoBehaviour {

	Vector3 magnitude = Vector3.right;
	public Vector3 pos;
	Quaternion rotation;
	public float startSwitch = 5f;
	public float speed = 5f;
	GameObject player;
	float timer = 8f;

	GameObject runtime;
	public AudioClip detonate;
	AudioSource audio;
	public bool deton = false;
	bool audioPlaying = false, audioPlaying2 = false;

	void Start() {
		pos = transform.position;
		rotation = transform.rotation;
		player = GameObject.FindGameObjectWithTag("Player");
		runtime = GameObject.FindGameObjectWithTag("Runtime");
		audio = GetComponent<AudioSource> ();
	}

	void Update() {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		startSwitch -= Time.deltaTime;
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null && GetComponent<EnemySpeed> ().isTraitEnabled) {

			if (!deton && timer > 0) {
				timer-=Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed * player.GetComponent<Snake> ().scale);
				if (transform.position == pos) {
					pos += magnitude;
					transform.rotation = rotation;
				}
				if (startSwitch < 0f) {
					SwitchX ();
					startSwitch = Random.Range (0f, 2f);
				}
				if (Vector3.Distance (player.transform.position, transform.position) < 6) {
					timer = 10f;
					speed = 0f;
					GetComponent<Animator> ().SetTrigger ("PlayDeath");
					StartCoroutine (DelayDeath ());
					if (!audioPlaying) {
						audio.PlayOneShot (detonate, PlayerPrefs.GetInt ("Sound"));
						audioPlaying = true;
					}
				} 
			} else if (deton && timer > 0) {
				runtime.GetComponent<Spawn360> ().count--;
				Destroy (gameObject);
			} else if (timer <= 0) {
				runtime.GetComponent<Spawn360> ().count--;
				Destroy (gameObject);
			}
		}
	}

	void SwitchX() {		
		if (magnitude == Vector3.right || magnitude == Vector3.left) {
			float distUp = 9f - transform.position.y;
			float distDown = transform.position.y + 9f;

			if (distUp > distDown) {
				magnitude = Vector3.up;
				rotation = Quaternion.Euler (0, 0, 270);
			}
			else {
				magnitude = Vector3.down;
				rotation = Quaternion.Euler (0, 0, 90);
			}
		}
		else if (magnitude == Vector3.up || magnitude == Vector3.down) {
			float distRight = 18f - transform.position.x;
			float distLeft= transform.position.x + 18f;
			
			if (distRight > distLeft) {
				magnitude = Vector3.right;
				rotation = Quaternion.Euler (0, 0, 180);
			}
			else {
				magnitude = Vector3.left;
				rotation = Quaternion.Euler (0, 0, 0);
			}
		}
	}
	IEnumerator DelayDeath() {
		yield return new WaitForSeconds (2.25f);

	}

}