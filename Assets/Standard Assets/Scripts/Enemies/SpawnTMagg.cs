using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpawnTMagg : MonoBehaviour {

	int whereX, whereY;
	int tmagCount;
	GameObject snake;
	public GameObject[] tMags = new GameObject[4];
	AudioSource audio;
	List<GameObject> pool1, pool2;
	public AudioClip sfx;

	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
		InvokeRepeating ("Spawn", 10f, 8f);
		Pool ();
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		tmagCount = 0;
		int rand, rand2;
		GameObject clone, clone2;
		Vector3 pos, pos2;
		int x, y, x2, y2;
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("TMag") && gameObj.activeInHierarchy) {
					tmagCount++;
				}
			}
			if (tmagCount == 1) {
				audio.PlayOneShot(sfx,PlayerPrefs.GetInt ("Sound"));
				rand = (int)Random.Range (0, 4);
				switch (rand) {
				case 0://up
					x = (int)Random.Range (1, 39);
					y = -4;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = GetObj(1, 0);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					//clone = Instantiate (tMags [0], pos, Quaternion.identity) as GameObject;
					break;
				case 1://down
					x = (int)Random.Range (1, 39);
					y = 24;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [1], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 1);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				case 2://right
					x = -4;
					y = (int)Random.Range (1, 19);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [2], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 2);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				case 3://left
					x = 44;
					y = (int)Random.Range (1, 19);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [3], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 3);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				}
			} else if (tmagCount == 0) {
				audio.PlayOneShot(sfx,PlayerPrefs.GetInt ("Sound"));
				rand = (int)Random.Range (0, 4);
				rand2 = (int)Random.Range (0, 4);
				switch (rand) {
				case 0://up
					x = (int)Random.Range (1, 39);
					y = -4;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [0], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 0);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				case 1://down
					x = (int)Random.Range (1, 39);
					y = 24;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [1], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 1);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				case 2://right
					x = -4;
					y = (int)Random.Range (1, 19);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [2], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 2);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				case 3://left
					x = 44;
					y = (int)Random.Range (1, 19);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone = Instantiate (tMags [3], pos, Quaternion.identity) as GameObject;
					clone = GetObj(1, 3);
					clone.transform.position = pos;
					clone.transform.rotation = Quaternion.identity;
					clone.GetComponent<TMagBehaviour>().EnableCollider();
					clone.SetActive(true);
					break;
				}
				switch (rand2) {
				case 0://up
					x = (int)Random.Range (1, 39);
					y = -4;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone2 = Instantiate (tMags [0], pos2, Quaternion.identity) as GameObject;
					clone2 = GetObj(2, 0);
					clone2.transform.position = pos2;
					clone2.transform.rotation = Quaternion.identity;
					clone2.GetComponent<TMagBehaviour>().EnableCollider();
					clone2.SetActive(true);
					break;
				case 1://down
					x = (int)Random.Range (1, 39);
					y = 24;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone2 = Instantiate (tMags [1], pos2, Quaternion.identity) as GameObject;
					clone2 = GetObj(2, 1);
					clone2.transform.position = pos2;
					clone2.transform.rotation = Quaternion.identity;
					clone2.GetComponent<TMagBehaviour>().EnableCollider();
					clone2.SetActive(true);
					break;
				case 2://right
					x = -4;
					y = (int)Random.Range (1, 19);
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone2 = Instantiate (tMags [2], pos2, Quaternion.identity) as GameObject;
					clone2 = GetObj(2, 2);
					clone2.transform.position = pos2;
					clone2.transform.rotation = Quaternion.identity;
					clone2.GetComponent<TMagBehaviour>().EnableCollider();
					clone2.SetActive(true);
					break;
				case 3://left
					x = 44;
					y = (int)Random.Range (1, 19);
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					//clone2 = Instantiate (tMags [3], pos2, Quaternion.identity) as GameObject;
					clone2 = GetObj(2, 3);
					clone2.transform.position = pos2;
					clone2.transform.rotation = Quaternion.identity;
					clone2.GetComponent<TMagBehaviour>().EnableCollider();
					clone2.SetActive(true);
					break;
				}
			} 
		}
	}

	void Pool() {
		pool1  = new List<GameObject>();
		pool2  = new List<GameObject>();

		for (int i = 0; i<4; i++) {
			GameObject obj = (GameObject)Instantiate(tMags[i]);
			obj.SetActive(false);
			pool1.Add(obj);
			GameObject obj2 = (GameObject)Instantiate(tMags[i]);
			obj2.SetActive(false);
			pool2.Add(obj2);
		}
	}

	GameObject GetObj(int i, int type) {
		if (i == 1) {
			if (!pool1[type].activeInHierarchy) return pool1[type];
			else return null;
		} if (i == 2) {
			if (!pool2[type].activeInHierarchy) return pool2[type];
			else return null;
		}  return null;
	}
}
