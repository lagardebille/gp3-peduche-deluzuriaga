using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSpike : MonoBehaviour {

	public GameObject borders;
	public GameObject enemy;
	private int dir = 1;
	int spikeCount;
	GameObject player;
	List<GameObject> poolObject;
	int poolAmount = 2;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("SpawnEnemy", 10f, 10f);
		Pool ();
	}
	
	// Update is called once per frame
	void SpawnEnemy () {
		if (player == null)player = GameObject.FindGameObjectWithTag("Player");
		spikeCount = 0;
		if ( player!= null && player.GetComponent<Snake> ().isPlaying && player.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {

			foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()){
				if(gameObj.name.StartsWith("Spike_1") && gameObj.activeInHierarchy){
					spikeCount++;
				}
			}

			int x = Random.Range (41,43);
			int x2 = Random.Range (-4, -1);
			int y = Random.Range (1, 19);
			int y2 = Random.Range (1, 19);

			GameObject clone, clone2;
			Vector3 pos, pos2;

			if (spikeCount == 1) {
				pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
			 	clone = Instantiate (enemy, pos, Quaternion.identity) as GameObject;
				clone.GetComponent<SpikeScript>().EnableColl();
				if (y < 9)
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, 1) * 2f;
				else
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, -1) * 2f;
				borders.SetActive (false);
				StartCoroutine(DelayWall());
			} else if (spikeCount == 0) {
				pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
				pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x2 + 0.5f) / 40f, (y2 + 0.85f) / 22.5f, 3f));

				//clone = Instantiate (enemy, pos, Quaternion.identity) as GameObject;
				clone = GetObj();
				clone.GetComponent<SpikeScript>().EnableColl();
				clone.SetActive(true);
				clone.transform.position = pos;
				clone.transform.rotation = Quaternion.identity;
				clone.GetComponent<SpikeScript>().timer = 10f;
				//clone2 = Instantiate (enemy, pos2, Quaternion.identity) as GameObject;
				if (y < 9)
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, 1) * 4f;
				else
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, -1) * 4f;


				clone2 = GetObj();
				clone2.SetActive(true);
				clone2.GetComponent<SpikeScript>().EnableColl();
				clone2.transform.position = pos2;
				clone2.transform.rotation = Quaternion.identity;
				clone2.GetComponent<SpikeScript>().timer = 10f;
				if (y2 < 9)
					clone2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, 1) * 4f;
				else
					clone2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, -1) * 4f;

				borders.SetActive (false);
				StartCoroutine(DelayWall());
			}
		}
	}

	IEnumerator DelayWall() {
		yield return new WaitForSeconds (1f);
		borders.SetActive (true);
	}

	void Pool() {
		poolObject = new List<GameObject> ();

		for (int i = 0; i<poolAmount; i++) {
			GameObject ojb = (GameObject)Instantiate(enemy);
			ojb.SetActive(false);
			poolObject.Add(ojb);
		}
	}

	GameObject GetObj() {
		for (int i = 0; i<poolObject.Count; i++) {
			if (!poolObject[i].activeInHierarchy) return poolObject[i];
		}
		return null;
	}
}
