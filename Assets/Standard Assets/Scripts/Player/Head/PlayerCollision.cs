using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollision : MonoBehaviour {

	public GameObject bodyPrefab, gameOver;
	GameObject runTime;
	public AudioClip miniSfx;
	public AudioClip nkSfx;
	public AudioClip wallSfx;
	public AudioClip frozenWallSfx;
	public AudioSource audio;
	public AudioClip [] audios = new AudioClip[4];
	Vector3 trPos;
	GameObject runtimeUI;
	public GameObject powerup1, powerup2;
	float scale;
	int cNum, lNum;

	//new on gameobject pooling
	List<GameObject> pooledBodies;
	int poolAmount = 21;
	// Use this for initialization
	void Start () {
		runTime = GameObject.FindGameObjectWithTag("Runtime");
		audio = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource> ();
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
		powerup1 = GameObject.FindGameObjectWithTag ("Slot1");
		powerup2 = GameObject.FindGameObjectWithTag ("Slot2");
		scale = transform.root.localScale.x;
		poolBody ();

		cNum = transform.root.gameObject.GetComponent<DataHolder> ().cNum;
		lNum = transform.root.gameObject.GetComponent<DataHolder> ().lNum;
	}

	void Update() {
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D coll)  {
		if (coll.gameObject.tag == "Blocks" || coll.gameObject.tag == "OuterBlocks") {
			if (PlayerPrefs.GetInt("diff") < 3) {
				if (coll.gameObject.GetComponent<BlockBehaviour>().isStoned) {
					transform.root.GetComponent<Snake>().frozenTimer = 3f; 
					audio.PlayOneShot(frozenWallSfx, 1f);
				}
				transform.root.GetComponent<Snake> ().currentSpeed = 0;
				Vector3 heading = coll.transform.position - transform.position;
				if (Mathf.Abs(heading.x) > Mathf.Abs(heading.y)) {
					if (heading.x > 0){
						/*transform.root.GetComponent<Snake> ().*/trPos = 
							new Vector3(coll.transform.position.x - scale, transform.position.y, transform.position.z);
						transform.root.position = trPos;
						transform.root.GetComponent<Snake> ().isRight = false;
						Debug.Log ("BLOCK MOVEMENTS AT RIGHT");
					} else if (heading.x < 0){
						/*transform.root.GetComponent<Snake> ().*/trPos = 
							new Vector3(coll.transform.position.x + scale, transform.position.y, transform.position.z);
						transform.root.position = trPos;
						Debug.Log ("BLOCK MOVEMENTS AT LEFT");
					}
				} else if (Mathf.Abs(heading.x) < Mathf.Abs(heading.y)) {
					if (heading.y > 0) {
						/*transform.root.GetComponent<Snake> ().*/trPos = 
							new Vector3(transform.position.x, coll.transform.position.y - scale, transform.position.z);
						transform.root.position = trPos;
						transform.root.GetComponent<Snake> ().isUp = false;
						Debug.Log("BLOCK MOVEMENTS AT UP");
					} else if (heading.y < 0){ 
						/*transform.root.GetComponent<Snake> ().*/trPos = 
							new Vector3(transform.position.x, coll.transform.position.y + scale, transform.position.z);
						transform.root.position = trPos;
						transform.root.GetComponent<Snake> ().isDown = false;
						Debug.Log("BLOCK MOVEMENTS AT DOWN");
					}
				}
			}
			else if (PlayerPrefs.GetInt("diff") == 3) {
				audio.PlayOneShot(wallSfx, PlayerPrefs.GetInt ("Sound"));
				transform.root.GetComponent<Snake> ().currentSpeed = 0;
				transform.root.GetComponent<Snake> ().isPlaying = false;
				transform.root.GetComponent<Snake> ().isMoving = false;
				transform.root.GetComponent<Animator>().SetTrigger("Die");
				StartCoroutine(ShowScreen());
			}
		}
		else if (coll.name.StartsWith ("FoodPrefab")) {
			audio.PlayOneShot(miniSfx, PlayerPrefs.GetInt ("Sound"));
			transform.root.GetComponent<DataHolder> ().score += 100;
			transform.root.GetComponent<DataHolder> ().miniCount++;
			transform.root.GetComponent<DataHolder> ().nkTemp++;
			// Get longer in next Move call
			if (!transform.root.GetComponent<DataHolder>().lifeActive || 
			    (transform.root.GetComponent<DataHolder>().lifeActive && transform.root.GetComponent<Snake> ().segments.Count < 8)){
				GameObject clone;
				//clone = Instantiate (bodyPrefab, transform.root.GetComponent<Snake> ().breadcrumbs.Last (), Quaternion.identity) as GameObject;
				clone = getBody();
				clone.transform.position = transform.root.GetComponent<Snake> ().breadcrumbs.Last ();
				clone.transform.rotation = Quaternion.identity;
				clone.SetActive(true);
				//else clone = Instantiate(bodyPrefab,breadcrumbs.Last(), Quaternion.identity) as GameObject;
				transform.root.GetComponent<Snake> ().segments.Add (clone.transform);
				transform.root.GetComponent<Snake> ().breadcrumbs.Add (clone.transform.position);
				clone.GetComponent<BodyCollision> ().index = transform.root.GetComponent<Snake> ().segments.Count;
			}
			else transform.root.GetComponent<DataHolder> ().lifes ++;

			if (transform.root.GetComponent<DataHolder> ().nkTemp == 3) {
				Debug.Log("AW");
				if (GameObject.FindGameObjectsWithTag ("NK").Length == 0) {
					runTime.GetComponent<SpawnNK> ().Spawn ();
					transform.root.GetComponent<DataHolder> ().nkTemp = 0;
				}
			}

			//transform.root.GetComponent<Animator>().SetTrigger("Idle");
			/*for (int i = 0; i<transform.root.GetComponent<Snake> ().segments.Count; i++) {
				transform.root.GetComponent<Snake> ().segments[i].GetComponent<Animator>().SetTrigger("Idle");
			}*/

			Destroy (coll.gameObject);
		} else if (coll.tag == "NK") {
			if (PlayerPrefs.GetInt ("C"+cNum.ToString()+"Level"+lNum.ToString()+"Finished") == 1)PlayerPrefs.SetInt("NKCount", PlayerPrefs.GetInt("NKCount")+1);
			audio.PlayOneShot(nkSfx, PlayerPrefs.GetInt ("Sound"));
			transform.root.GetComponent<DataHolder> ().nkCount += 1;
			Destroy (coll.gameObject);
			transform.root.GetComponent<DataHolder> ().score += 50;
		} else if (coll.tag == "Enemy") {

		} else if (coll.tag == "Radius") {
			transform.root.GetComponent<DataHolder> ().radiusTimer = 10f; 
			Destroy (coll.gameObject);
		} else if (coll.tag == "PassThru") {
			transform.root.GetComponent<DataHolder> ().passTimer = 10f; 
			Destroy (coll.gameObject);
		} else if (coll.tag == "Freeze") {
			transform.root.GetComponent<DataHolder> ().freezeTimer = 10f; 
			Destroy (coll.gameObject);
		} else if (coll.tag == "Invincible") {
			transform.root.GetComponent<DataHolder> ().invuTimer = 10f; 
			Destroy (coll.gameObject);
		} else if (coll.tag == "Chest") {
			int i = coll.gameObject.GetComponent<ChestBehaviour>().type;
			audio.PlayOneShot(nkSfx, PlayerPrefs.GetInt ("Sound"));
			if (i >= 0) {
				if (i == 0) {
					if (powerup1.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup1.GetComponent<PowerupBehaviour>().whatPowerup = 1;
						powerup1.GetComponent<PowerupBehaviour>().numUse = 1;
					} else if (powerup2.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup2.GetComponent<PowerupBehaviour>().whatPowerup = 1;
						powerup2.GetComponent<PowerupBehaviour>().numUse = 1;
					} PlayerPrefs.SetInt("pow_1", PlayerPrefs.GetInt("pow_1")+1);//radius
				} else if (i == 1) {
					if (powerup1.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup1.GetComponent<PowerupBehaviour>().whatPowerup = 2;
						powerup1.GetComponent<PowerupBehaviour>().numUse = 1;
					} else if (powerup2.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup2.GetComponent<PowerupBehaviour>().whatPowerup = 2;
						powerup2.GetComponent<PowerupBehaviour>().numUse = 1;
					} PlayerPrefs.SetInt("pow_2", PlayerPrefs.GetInt("pow_2")+1);//freeze
				} else if (i == 2){
					if (powerup1.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup1.GetComponent<PowerupBehaviour>().whatPowerup = 3;
						powerup1.GetComponent<PowerupBehaviour>().numUse = 1;
					} else if (powerup2.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup2.GetComponent<PowerupBehaviour>().whatPowerup = 3;
						powerup2.GetComponent<PowerupBehaviour>().numUse = 1;
					} PlayerPrefs.SetInt("pow_3", PlayerPrefs.GetInt("pow_3")+1);//invu
				} else if (i == 3){
					if (powerup1.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup1.GetComponent<PowerupBehaviour>().whatPowerup = 5;
						powerup1.GetComponent<PowerupBehaviour>().numUse = 1;
					} else if (powerup2.GetComponent<PowerupBehaviour>().whatPowerup == 0) {
						powerup2.GetComponent<PowerupBehaviour>().whatPowerup = 5;
						powerup2.GetComponent<PowerupBehaviour>().numUse = 1;
					} PlayerPrefs.SetInt("pow_5", PlayerPrefs.GetInt("pow_5")+1);//pass
				}

			}
			Destroy (coll.gameObject);
		} else if (coll.tag == "Body"){ 
			if (PlayerPrefs.GetInt ("CharActive") == 0) {
				transform.root.GetComponent<Snake> ().currentSpeed = 0;
				transform.root.GetComponent<Snake> ().isPlaying = false;
				transform.root.GetComponent<Snake> ().isMoving = false;
				transform.root.GetComponent<Animator>().SetTrigger("Die");
				StartCoroutine(ShowScreen());
			} else {
			}
		} else {
			transform.root.GetComponent<Snake> ().currentSpeed = 0;
			transform.root.GetComponent<Snake> ().isPlaying = false;
			transform.root.GetComponent<Snake> ().isMoving = false;
			transform.root.GetComponent<Animator>().SetTrigger("Die");
			StartCoroutine(ShowScreen());
		} 


	}
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Blocks" || coll.gameObject.tag == "OuterBlocks") {
			if (PlayerPrefs.GetInt("diff") < 3) {
				//transform.root.GetComponent<Snake> ().currentSpeed = 0;
				Vector3 heading = coll.transform.position - transform.position;
				//Debug.Log(coll.transform.position - Vector3.right);
				if (Mathf.Abs(heading.x) > Mathf.Abs(heading.y)) {
					if (heading.x > 0){
						transform.root.GetComponent<Snake> ().trPos = 
							new Vector3(coll.transform.position.x - scale, transform.position.y, transform.position.z);
						transform.root.GetComponent<Snake> ().isRight = false;
						Debug.Log ("BLOCK MOVEMENTS AT RIGHT");
					} else if (heading.x < 0){
						transform.root.GetComponent<Snake> ().trPos = 
							new Vector3(coll.transform.position.x + scale, transform.position.y, transform.position.z);
						transform.root.GetComponent<Snake> ().isLeft = false;
						Debug.Log ("BLOCK MOVEMENTS AT LEFT");
					}
				} else if (Mathf.Abs(heading.x) < Mathf.Abs(heading.y)) {
					if (heading.y > 0) {
						transform.root.GetComponent<Snake> ().trPos = 
							new Vector3(transform.position.x, coll.transform.position.y - scale, transform.position.z);
						transform.root.GetComponent<Snake> ().isUp = false;
						Debug.Log("BLOCK MOVEMENTS AT UP");
					} else if (heading.y < 0){ 
						transform.root.GetComponent<Snake> ().trPos = 
							new Vector3(transform.position.x, coll.transform.position.y + scale, transform.position.z);
						transform.root.GetComponent<Snake> ().isDown = false;
						Debug.Log("BLOCK MOVEMENTS AT DOWN");
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		transform.root.GetComponent<Snake> ().isUp = true;
		transform.root.GetComponent<Snake> ().isDown = true;
		transform.root.GetComponent<Snake> ().isLeft = true;
		transform.root.GetComponent<Snake> ().isRight = true;
	}

	void poolBody() {
		pooledBodies = new List<GameObject>();
		for (int i = 0; i<poolAmount; i++) {
			GameObject obj = (GameObject)Instantiate(bodyPrefab);
			obj.SetActive(false);
			pooledBodies.Add(obj);
		}
	}

	GameObject getBody() {
		for (int i = 0; i<pooledBodies.Count; i++) {
			if (!pooledBodies[i].activeInHierarchy) {
				//Debug.Log("damn");
				return pooledBodies[i];
			}
		}

		return null;
	}
	

	IEnumerator ShowScreen() {
		yield return new WaitForSeconds (2f);
		Camera.main.GetComponent<LevelSounds> ().PlayGameOver ();
		gameOver.SetActive (true);
		if (PlayerPrefs.GetInt ("goScreen") >= 9) {
			PlayerPrefs.SetInt ("goScreen", 1);
		} else PlayerPrefs.SetInt ("goScreen", PlayerPrefs.GetInt ("goScreen") + 1);
		StartCoroutine (DelayMenu ());
		//Destroy (transform.root.gameObject);
	}

	IEnumerator DelayMenu() {
		yield return new WaitForSeconds(20f);
		runtimeUI.GetComponent<GameplayUI> ().RestartLevel ("MainMenu");
	}
}
