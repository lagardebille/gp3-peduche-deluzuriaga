using UnityEngine;
using System.Collections;

public class GrimmGunBehaviour : MonoBehaviour {

	public float idleTime = 0f;
	public float shootMidTime = 0f;
	public float shootFullTime = 0f;
	public float exitTime = 0f;
	public int rotY, rotZ;
	public AudioClip sfx;
	AudioSource audio;
	bool animIdle, animMid, animFull;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, rotY, rotZ);
		audio = GetComponent<AudioSource> ();
		animIdle = false;
		animMid = false;
		animFull = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			idleTime -= Time.deltaTime;
			if (idleTime <= 0) {
				idleTime = 0;
				if (!animMid) {
					GetComponent<Animator> ().SetTrigger ("Mid");
					audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
					animMid = true;
				}
				shootMidTime -= Time.deltaTime;
				if (shootMidTime <= 0) {
					shootMidTime = 0;
					if (!animFull) {
						GetComponent<Animator> ().SetTrigger ("Full");
						animFull = true;
					}
					shootFullTime -= Time.deltaTime;
					if (shootFullTime <= 0) {
						shootFullTime = 0;
						if (!animIdle) {
							GetComponent<Animator> ().SetTrigger ("Idle");
							animIdle = true;
						}
						exitTime -= Time.deltaTime;
						if (exitTime <= 0)
							Destroy (gameObject);
					}
				}
			}
		}
	}
}
