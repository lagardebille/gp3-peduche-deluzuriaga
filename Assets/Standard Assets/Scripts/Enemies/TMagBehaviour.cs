using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class TMagBehaviour : MonoBehaviour {

	GameObject snakeHead;
	public int dir;
	int axis;
	Vector3 magDir;
	float borderLine;
	public AudioClip sfx;
	AudioSource fx;
	// Use this for initialization
	void Start () {
		fx = GetComponent<AudioSource> ();
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		switch (dir) {
		case 1:
			magDir = Vector3.up;
			axis = 0;
			break;
		case 2:
			magDir = Vector3.down;
			axis = 0;
			break;
		case 3:
			magDir = Vector3.right;
			axis = 1;
			break;
		case 4:
			magDir = Vector3.left;
			axis = 1;
			break;
		default:
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			gameObject.SetActive (false);
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		if (GetComponent<EnemySpeed> ().isTraitEnabled && snakeHead != null) {
			transform.Translate (magDir * Time.deltaTime * PlayerPrefs.GetFloat("speed"));
			if (axis == 0) {
				if (magDir == Vector3.up) {
					if (transform.position.y > 12) gameObject.SetActive (false);
				}
				else if (magDir == Vector3.down) {
					if (transform.position.y < -14) gameObject.SetActive (false);
				}
			} else if (axis == 1) {
				if (magDir == Vector3.right) {
					if (transform.position.x > 22) gameObject.SetActive (false);
				}
				else if (magDir == Vector3.left) {
					if (transform.position.x < -22) gameObject.SetActive (false);
				}
			}
			/*float distance = Vector3.Distance (transform.position, snakeHead.transform.position);

			if (distance <= 4) {
				GetComponent<EnemySpeed>().speed = 0;
				GetComponent<Animator> ().SetTrigger ("Dead");
				StartCoroutine (DelayDeath ());
			}
			if (transform.position.y <= -14f) {
				Destroy (gameObject);
			}*/
		}
	}

	public void EnableCollider() {
		GetComponent<BoxCollider2D> ().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Body") {
			GetComponent<EnemySpeed>().speed = 0;
			GetComponent<Animator>().SetTrigger("Die");
			fx.PlayOneShot(sfx,PlayerPrefs.GetInt ("Sound"));
			StartCoroutine(DelayDeath());
		}
	}

	IEnumerator DelayDeath() {
		yield return new WaitForSeconds (0.5f);
		gameObject.SetActive (false);
	}
}
