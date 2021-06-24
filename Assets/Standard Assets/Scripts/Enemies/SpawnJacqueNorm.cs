using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnJacqueNorm : MonoBehaviour {

	// Use this for initialization
	public GameObject jacqueNorm;
	public AudioClip sfx;
	GameObject player;
	AudioSource audio;
	List<GameObject> pooledObjects;
	int poolAmount = 4;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnEnemy", 10f, 10);
		player = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
		poolObject ();
	}
	
	// Update is called once per frame
	void SpawnEnemy() {
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			if (player.GetComponent<Snake> ().isPlaying && PlayerPrefs.GetInt("diff") > 1 && player.GetComponent<DataHolder>().freezeTimer <= 0) {
				float x = player.transform.position.x +2;
				float x2 = player.transform.position.x -2;
				int y = (int)Random.Range (9, 13);
				int y2 = (int)Random.Range (9, 13);

				Vector3 pos = new Vector3(x,y,-6.36499f);
				Vector3 pos2 = new Vector3(x2,y2,-6.36499f);

				GameObject clone, clone2;
				clone = GetObj();
				clone.SetActive(true);
				clone.GetComponent<JacqueBehaviour>().audioPlaying = false;
				clone2 = GetObj();
				clone2.SetActive(true);
				clone2.GetComponent<JacqueBehaviour>().audioPlaying = false;
				clone.transform.position = pos;
				clone2.transform.position = pos2;
				clone.transform.rotation = Quaternion.identity;
				clone2.transform.rotation = Quaternion.identity;
				clone.GetComponent<EnemySpeed>().speed = 2;
				clone2.GetComponent<EnemySpeed>().speed = 2;
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
			}
		}
	}

	void poolObject() {
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i<poolAmount; i++) {
			GameObject obj = (GameObject)Instantiate(jacqueNorm);
			pooledObjects.Add(obj);
			obj.SetActive(false);
		}
	}

	GameObject GetObj() {
		for (int i = 0; i<pooledObjects.Count; i++) {
			if (!pooledObjects[i].activeInHierarchy) 
				return pooledObjects[i];
		}
		return null;
	}
}
