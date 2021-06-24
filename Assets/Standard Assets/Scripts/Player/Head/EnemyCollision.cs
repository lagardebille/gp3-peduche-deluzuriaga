using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

	public GameObject gameOver;
	public AudioClip enemySfx, tmaggSfx, spikeSfx;
	bool isPhantom = false;
	AudioSource audio;
	GameObject runtimeUI;
	// Use this for initialization
	void Start () {
		audio = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource> ();
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
		if (PlayerPrefs.GetInt ("CharActive") == 1)
			isPhantom = true;
	}

	void Update() {
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D coll) {
		if (GetComponent<DataHolder> ().invuTimer <= 0 && !GetComponent<DataHolder> ().isCollided) {
			if (coll.name.StartsWith ("Jacque360") && !GetComponent<DataHolder> ().isCollided) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom){
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count < 2 && GetComponent<Snake> ().segments.Count > 0) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
									GameObject g = GetComponent<Snake> ().segments [i].gameObject;
									g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							
							StartCoroutine (DestroyMiniRemoveAll ());
						} else if (GetComponent<Snake> ().segments.Count >= 2) {
							for (int i = 0; i<3; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniRange (2));
						} else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 2){
							GetComponent<DataHolder>().lifes-=2;
							GetComponent<DataHolder>().score -=300;
							GetComponent<DataHolder>().miniCount-=2;
							StartCoroutine(DelayHit());
						} else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 2) {
							int diff = 2 - GetComponent<DataHolder>().lifes;
							int inv = 2 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					}
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			} if (coll.name.StartsWith ("Mastur")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count < 2 && GetComponent<Snake> ().segments.Count > 0) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							
							StartCoroutine (DestroyMiniRemoveAll ());
						} else if (GetComponent<Snake> ().segments.Count >= 2) {
							for (int i = 0; i<2; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniRange (2));
						} else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 2){
							GetComponent<DataHolder>().lifes-=2;
							GetComponent<DataHolder>().score -=200;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 2) {
							int diff = 2 - GetComponent<DataHolder>().lifes;
							int inv = 2 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					}
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			} else if (coll.name.StartsWith ("JacqueNormal")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count == 1) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (0));
						} else if (GetComponent<Snake> ().segments.Count > 1) {
							for (int i = 0; i<1; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						} 
						else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 1){
							GetComponent<DataHolder>().lifes--;
							GetComponent<DataHolder>().score -=100;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 1) {
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100);
							GetComponent<DataHolder>().miniCount--;
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						}
					} 
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			} else if (coll.name.StartsWith ("Spike_1") || coll.name.StartsWith ("Spike_2")) {
				audio.PlayOneShot(spikeSfx, PlayerPrefs.GetInt ("Sound"));
				coll.gameObject.SetActive(false);
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count == 1) {
						for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
							GameObject g = GetComponent<Snake> ().segments [i].gameObject;
							g.GetComponent<Animator> ().SetTrigger ("Dead");
						}
						StartCoroutine (DestroyMiniSpecifiedIndex (0));
						} else if (GetComponent<Snake> ().segments.Count > 1) {
							for (int i = 0; i<1; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						} 
						else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 1){
							GetComponent<DataHolder>().lifes--;
							GetComponent<DataHolder>().score -=100;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 1) {
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100);
							GetComponent<DataHolder>().miniCount--;
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						}
					} 
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			}else if (coll.name.StartsWith ("SpikeBig")) {
				audio.PlayOneShot(spikeSfx, PlayerPrefs.GetInt ("Sound"));
				Destroy(coll.gameObject);
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count == 1) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						} else if (GetComponent<Snake> ().segments.Count > 1) {
							for (int i = 0; i<1; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniRange (2));
						} 
						else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >=2){
							GetComponent<DataHolder>().lifes--;
							GetComponent<DataHolder>().score -=100;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 2) {
							int diff = 2 - GetComponent<DataHolder>().lifes;
							int inv = 2 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					} 
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			} else if (coll.name.StartsWith ("Artillery")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count < 5 && GetComponent<Snake> ().segments.Count > 0) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
						
							StartCoroutine (DestroyMiniRemoveAll ());
						} else if (GetComponent<Snake> ().segments.Count >= 5) {
							for (int i = 0; i<5; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniRange (5));
						} else if (GetComponent<Snake>().segments.Count <= 0){
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 5){
							GetComponent<DataHolder>().lifes -=5;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 5) {
							int diff = 5 - GetComponent<DataHolder>().lifes;
							int inv = 5 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					} 
					coll.enabled = false;
				} else if (isPhantom) isPhantom = false;
			} else if (coll.name.StartsWith ("Burner1") ) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count < 2 && GetComponent<Snake> ().segments.Count > 0) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							
							StartCoroutine (DestroyMiniRemoveAll ());
						} else if (GetComponent<Snake> ().segments.Count >= 2) {
							for (int i = 0; i<2; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniRange (2));
						} else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 2){
							GetComponent<DataHolder>().lifes-=2;
							GetComponent<DataHolder>().score -=200;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 2) {
							int diff = 2 - GetComponent<DataHolder>().lifes;
							int inv = 2 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					}
				} else if (isPhantom) isPhantom = false;
			} else if (coll.name.StartsWith ("Burner2") ) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count == 1) {
							for (int i = 0; i<GetComponent<Snake>().segments.Count; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (0));
						} else if (GetComponent<Snake> ().segments.Count > 1) {
							for (int i = 0; i<1; i++) {
								GameObject g = GetComponent<Snake> ().segments [i].gameObject;
								g.GetComponent<Animator> ().SetTrigger ("Dead");
							}
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						} 
						else if (GetComponent<Snake> ().segments.Count <= 0) {
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 1){
							GetComponent<DataHolder>().lifes--;
							GetComponent<DataHolder>().score -=100;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 1) {
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100);
							GetComponent<DataHolder>().miniCount--;
							StartCoroutine (DestroyMiniSpecifiedIndex (1));
						}
					} 
				} else if (isPhantom) isPhantom = false;
				coll.enabled = false;
			} else if (coll.name.StartsWith ("Burner3")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count > 0){
							GetComponent<DataHolder> ().isCollided = true;
							GameObject g = GetComponent<Snake> ().segments [0].gameObject;
							g.GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyMiniRange(3));
						}  else if (GetComponent<Snake>().segments.Count <= 0){
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 3){
							GetComponent<DataHolder>().score -= 300;
							GetComponent<DataHolder>().lifes -= 3;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 3) {
							int diff = 3 - GetComponent<DataHolder>().lifes;
							int inv = 3 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().miniCount-=inv;
							GetComponent<DataHolder>().score -=(100*inv);
							StartCoroutine (DestroyMiniRange (diff));
						}
					} 
				} else if (isPhantom) isPhantom = false;
			} else if (coll.name.StartsWith ("MegaBurner")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count > 0){
							GetComponent<DataHolder> ().isCollided = true;
							GameObject g = GetComponent<Snake> ().segments [0].gameObject;
							g.GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyMiniRange(2));
						} else if (GetComponent<Snake>().segments.Count <= 0){
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isMoving = false;
							GetComponent<Snake> ().isPlaying = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 2){
							GetComponent<DataHolder>().lifes -=2;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 2) {
							int diff = 2 - GetComponent<DataHolder>().lifes;
							int inv = 2 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					} 
				} else if (isPhantom) isPhantom = false;
			} else if (coll.name.StartsWith ("TMag")) {
				audio.PlayOneShot(tmaggSfx, PlayerPrefs.GetInt ("Sound"));
				if (!isPhantom) {
					GetComponent<DataHolder> ().isCollided = true;
					if (!GetComponent<DataHolder>().lifeActive || 
					    (GetComponent<DataHolder>().lifeActive && GetComponent<DataHolder>().lifes <= 0 )) { 
						if (GetComponent<Snake> ().segments.Count > 0){
							GetComponent<DataHolder> ().isCollided = true;
							GameObject g = GetComponent<Snake> ().segments [0].gameObject;
							g.GetComponent<Animator> ().SetTrigger ("Dead");
							StartCoroutine (DestroyMiniRemoveAll());
						} else if (GetComponent<Snake>().segments.Count <= 0){
							GetComponent<Animator> ().SetTrigger ("Die");
							GetComponent<Snake> ().currentSpeed = 0;
							GetComponent<Snake> ().isPlaying = false;
							GetComponent<Snake> ().isMoving = false;
							StartCoroutine (ShowScreen ());
						}
					} else if (GetComponent<DataHolder>().lifeActive){
						if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes >= 8){
							GetComponent<DataHolder>().score -=(100*GetComponent<DataHolder>().lifes);
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder> ().isCollided = false;
						} 
						else if (GetComponent<Snake> ().segments.Count >= 8 && GetComponent<DataHolder>().lifes < 8) {
							int diff = 8 - GetComponent<DataHolder>().lifes;
							int inv = 8 - diff;
							GetComponent<DataHolder>().lifes = 0;
							GetComponent<DataHolder>().score -=(100*inv);
							GetComponent<DataHolder>().miniCount-=inv;
							StartCoroutine (DestroyMiniRange (diff));
						}
					} 
				} else if (isPhantom) isPhantom = false;
			} else if (coll.name.StartsWith ("SJB") || coll.name.StartsWith ("Heng") || coll.name.StartsWith ("MiniHeng") || coll.name.StartsWith("Grimm") || coll.name.StartsWith("DoppelArtillery") || coll.name.StartsWith("SpikeBig") || coll.name.StartsWith("Alpha") || coll.name.StartsWith("Spike3") || coll.name.StartsWith("DoppelShocker")) {
				audio.PlayOneShot(enemySfx, PlayerPrefs.GetInt ("Sound"));
				GetComponent<DataHolder> ().isCollided = true;
				GetComponent<Animator> ().SetTrigger ("Die");
				GetComponent<Snake> ().isMoving = false;
				GetComponent<Snake> ().currentSpeed = 0;
				GetComponent<Snake> ().isPlaying = false;
				StartCoroutine (ShowScreen ());
			}
		}
	}

	IEnumerator DestroyMini(int c) {
		yield return new WaitForSeconds (1f);
		for (int i = 0; i<c; i++) {
			Destroy(GetComponent<Snake>().segments[i].gameObject);
		}
		if (c < GetComponent<Snake> ().segments.Count) {
			GetComponent<Snake> ().segments.Clear ();
			GetComponent<DataHolder> ().miniCount = 0;

		} else {
			GetComponent<Snake> ().segments.RemoveRange (0, c);
			GetComponent<DataHolder> ().miniCount -=c;
		}
		GetComponent<DataHolder> ().isCollided = false;
	}

	IEnumerator DestroyMiniRange(int r) {
		yield return new WaitForSeconds (1f);
		GetComponent<RemoveCertainBody> ().RemoveRange (0, r);
		GetComponent<DataHolder> ().isCollided = false;
	}

	IEnumerator DestroyMiniSpecifiedIndex(int c) {
		yield return new WaitForSeconds (1f);
		GetComponent<RemoveCertainBody> ().RemoveOnIndex (c);
		GetComponent<DataHolder> ().isCollided = false;
	}

	IEnumerator DestroyMiniRemoveAll() {
		yield return new WaitForSeconds (1f);
		GetComponent<RemoveCertainBody> ().RemoveAll ();
		GetComponent<DataHolder> ().isCollided = false;
	}
	
	IEnumerator ShowScreen() {
		yield return new WaitForSeconds (2f);
		Camera.main.GetComponent<LevelSounds> ().PlayGameOver ();
		GetComponent<DataHolder> ().isCollided = false;
		if (PlayerPrefs.GetInt ("goScreen") >= 9) {
			PlayerPrefs.SetInt ("goScreen", 1);
		} else
			PlayerPrefs.SetInt ("goScreen", PlayerPrefs.GetInt ("goScreen") + 1);
		gameOver.SetActive (true);
		StartCoroutine (DelayMenu ());
	}

	IEnumerator DelayMenu() {
		yield return new WaitForSeconds(20f);
		runtimeUI.GetComponent<GameplayUI> ().RestartLevel ("MainMenu");
	}

	IEnumerator DelayHit() {
		yield return new WaitForSeconds (1f);
		GetComponent<DataHolder> ().isCollided = false;
	}
}
