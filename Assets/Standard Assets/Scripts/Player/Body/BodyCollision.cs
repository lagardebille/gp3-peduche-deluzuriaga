using UnityEngine;
using System.Collections;

public class BodyCollision : MonoBehaviour {

	public int index = 0;
	GameObject snake;
	bool isHit = false;
	public AudioClip sfx, tmaggSfx, spikeSfx;
	AudioSource audio;
	public bool isPhantom = false;

	void Start() {
		snake = GameObject.FindGameObjectWithTag("Player");

		audio = Camera.main.GetComponent<AudioSource> ();
		if (PlayerPrefs.GetInt ("CharActive") == 1)
			isPhantom = true;
	}

	void Update() {
		snake = GameObject.FindGameObjectWithTag("Player");
		index = snake.GetComponent<Snake> ().segments.IndexOf (transform);
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (snake.GetComponent<DataHolder> ().invuTimer <= 0) {
			if ((coll.name.StartsWith ("Jacque360")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<DataHolder>().lifes <= 0 )) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 2));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						if (snake.GetComponent<DataHolder>().lifes >= 2) {
							snake.GetComponent<DataHolder> ().lifes -=2;
							snake.GetComponent<DataHolder>().score-=200;
							snake.GetComponent<DataHolder>().miniCount -=2;
						} else if (snake.GetComponent<DataHolder>().lifes < 2) {
							int difference = 2 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 2 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}

						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("JacqueNormal")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBody(index));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 1) {
							snake.GetComponent<DataHolder>().miniCount -= 1;
							snake.GetComponent<DataHolder> ().lifes -= 1;
							snake.GetComponent<DataHolder>().score-=100;
							StartCoroutine(DelayHit());
						} else if (snake.GetComponent<DataHolder>().lifes < 1) {
							snake.GetComponent<DataHolder> ().lifes  = 0;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBody(index));
						}		
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("Mastur")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 2));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 2) {
							snake.GetComponent<DataHolder> ().lifes -=2;
							snake.GetComponent<DataHolder>().score-=200;
							snake.GetComponent<DataHolder>().miniCount -=2;
						} else if (snake.GetComponent<DataHolder>().lifes < 2) {
							int difference = 2 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 2 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("Artillery")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 5));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 5) {
							snake.GetComponent<DataHolder> ().lifes -=5;
							snake.GetComponent<DataHolder>().score-=500;
							snake.GetComponent<DataHolder>().miniCount -=5;
						} else if (snake.GetComponent<DataHolder>().lifes < 5) {
							int difference = 5 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 5 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("Burner1")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 2));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 2) {
							snake.GetComponent<DataHolder> ().lifes -=2;
							snake.GetComponent<DataHolder>().score-=200;
							snake.GetComponent<DataHolder>().miniCount -=2;
						} else if (snake.GetComponent<DataHolder>().lifes < 2) {
							int difference = 2 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 2 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("Burner2")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBody(index));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 1) {
							snake.GetComponent<DataHolder>().miniCount -= 1;
							snake.GetComponent<DataHolder> ().lifes -= 1;
							snake.GetComponent<DataHolder>().score-=100;
							StartCoroutine(DelayHit());
						} else if (snake.GetComponent<DataHolder>().lifes < 1) {
							snake.GetComponent<DataHolder> ().lifes  = 0;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBody(index));
						}		
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("Burner3")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0,3));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 3) {
							snake.GetComponent<DataHolder> ().lifes -=3;
							snake.GetComponent<DataHolder>().score-=300;
							snake.GetComponent<DataHolder>().miniCount -=3;
						} else if (snake.GetComponent<DataHolder>().lifes < 3) {
							int difference = 3 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 3 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange (index));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if ((coll.name.StartsWith ("MegaBurner")) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 2));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 2) {
							snake.GetComponent<DataHolder> ().lifes -=2;
							snake.GetComponent<DataHolder>().score-=200;
							snake.GetComponent<DataHolder>().miniCount -=2;
						} else if (snake.GetComponent<DataHolder>().lifes < 2) {
							int difference = 2 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 2 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if (coll.name.StartsWith ("TMag")) {
				audio.PlayOneShot (tmaggSfx, PlayerPrefs.GetInt ("Sound"));
				coll.enabled = false;
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange(index));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 8) {
							snake.GetComponent<DataHolder>().score-=(100*snake.GetComponent<DataHolder>().lifes);
							snake.GetComponent<DataHolder>().miniCount -= snake.GetComponent<DataHolder> ().lifes;
							snake.GetComponent<DataHolder> ().lifes = 0;
						} else if (snake.GetComponent<DataHolder>().lifes < 8) {
							int difference = 8 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 8 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange (index));
						}
						
						StartCoroutine(DelayHit());
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if (coll.name.StartsWith ("Spike_1") || coll.name.StartsWith ("Spike_2")) {
				audio.PlayOneShot (spikeSfx, PlayerPrefs.GetInt ("Sound"));
				coll.gameObject.SetActive(false);
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBody(index));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 1) {
							snake.GetComponent<DataHolder>().miniCount -= 1;
							snake.GetComponent<DataHolder> ().lifes -= 1;
							snake.GetComponent<DataHolder>().score-=100;
							StartCoroutine(DelayHit());
						} else if (snake.GetComponent<DataHolder>().lifes < 1) {
							snake.GetComponent<DataHolder> ().lifes  = 0;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBody(index));
						}		
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if (coll.name.StartsWith ("SpikeBig")) {
				audio.PlayOneShot (spikeSfx, PlayerPrefs.GetInt ("Sound"));
				if (!snake.GetComponent<DataHolder>().lifeActive || 
				    (snake.GetComponent<DataHolder>().lifeActive && snake.GetComponent<Snake> ().segments.Count < 8)) {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						GetComponent<Animator> ().SetTrigger ("Dead");
						StartCoroutine (DestroyBodyRange2 (0, 2));
					} else if (isPhantom)
						isPhantom = false;
				} else {
					if (!isPhantom) {
						isHit = true;
						snake.GetComponent<DataHolder> ().isCollided = true;
						
						if (snake.GetComponent<DataHolder>().lifes >= 2) {
							snake.GetComponent<DataHolder> ().lifes -=2;
							snake.GetComponent<DataHolder>().score-=200;
							snake.GetComponent<DataHolder>().miniCount -=2;
							StartCoroutine(DelayHit());
						} else if (snake.GetComponent<DataHolder>().lifes < 2) {
							int difference = 2 - snake.GetComponent<DataHolder> ().lifes;
							int inverteDiff = 2 - difference;
							snake.GetComponent<DataHolder> ().lifes = 0;
							snake.GetComponent<DataHolder>().score-=(100*inverteDiff);
							snake.GetComponent<DataHolder>().miniCount -=inverteDiff;
							GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyBodyRange2 (0, difference));
						}
					} else if (isPhantom)
						isPhantom = false;
				}
			} else if (((coll.name.StartsWith ("DoppelArtillery")) || (coll.name.StartsWith ("Alpha")) || (coll.name.StartsWith ("Spike3"))) && !isHit && !snake.GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					isHit = true;
					snake.GetComponent<DataHolder> ().isCollided = true;

					for (int i = 0; i<snake.GetComponent<Snake>().segments.Count; i++) {
						snake.GetComponent<Snake> ().segments [i].GetComponent<Animator> ().SetTrigger ("Dead");
					}
					snake.GetComponent<DataHolder>().lifes = 0;
					StartCoroutine (DestroyAllBody ());
				} else if (isPhantom)
					isPhantom = false;
			}
		}
	}

	IEnumerator DestroyBody(int i) {
		yield return new WaitForSeconds (1f);
		if (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes > 0)
			snake.GetComponent<DataHolder> ().lifes --;
		else if (!snake.GetComponent<DataHolder> ().lifeActive ||
		         (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes <= 0))
			snake.GetComponent<RemoveCertainBody> ().RemoveOnIndex (i);
	}
	IEnumerator DestroyBodyRange(int i) {
		yield return new WaitForSeconds (1f);	
		if (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes > 0) {
			int range = snake.GetComponent<Snake> ().segments.Count - i;
			snake.GetComponent<DataHolder> ().lifes -=range;
		} else if (!snake.GetComponent<DataHolder> ().lifeActive ||
		         (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes <= 0))
			snake.GetComponent<RemoveCertainBody> ().RemoveFrom (i);
	}
	IEnumerator DestroyBodyRange2(int i, int r) {
		yield return new WaitForSeconds (1f);	
		if (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes > 0) {
			snake.GetComponent<DataHolder> ().lifes -=r;
		} else if (!snake.GetComponent<DataHolder> ().lifeActive ||
		           (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes <= 0))
			snake.GetComponent<RemoveCertainBody> ().RemoveRange (i, r);
	}
	IEnumerator DestroyAllBody() {
		yield return new WaitForSeconds (1f);	
		if (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes > 0) {
			snake.GetComponent<DataHolder> ().lifes = 0;
		} else if (!snake.GetComponent<DataHolder> ().lifeActive ||
		           (snake.GetComponent<DataHolder> ().lifeActive && snake.GetComponent<DataHolder> ().lifes <= 0))
			snake.GetComponent<RemoveCertainBody> ().RemoveAll ();
	}

	IEnumerator DelayHit() {
		yield return new WaitForSeconds (1f);
		isHit = false;
		snake.GetComponent<DataHolder> ().isCollided = false;
	}

}
