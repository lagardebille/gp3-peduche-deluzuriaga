using UnityEngine;
using System.Collections;

public class SpawnShocker : MonoBehaviour {

	public GameObject shocker, artillery;
	GameObject snake;
	int shockerCount;
	GameObject clone, clone2;
	Vector3 pos, pos2;
	float x, y, y2;
	int rotY, rotZ;
	float idleTime = 0, idleTime2 = 0;
	bool hasSpawned = true;
	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("Spawn", 10f, 15f);
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
		shockerCount = 0;
		foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			if (gameObj.name.StartsWith ("DoppelShocker")) {
				shockerCount++;
			}
		}
		if (shockerCount == 0) {
			//4,36
			//y is 1 & 18 only
			x = (int)Random.Range(4,36);
			pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (17.6f + 0.85f) / 22.5f, 3f));
			pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (0.7f + 0.85f) / 22.5f, 3f));
			clone = Instantiate(shocker, pos, Quaternion.identity) as GameObject;
			clone2 = Instantiate(shocker, pos2, Quaternion.identity) as GameObject;
			clone2.GetComponent<DoppelShockerBehaviour>().rotZ = 180;

			idleTime = (int)Random.Range(2,4);
			idleTime2 = idleTime+0.7f;
			hasSpawned = false;
			clone2.GetComponent<DoppelShockerBehaviour>().idleTime = idleTime;
			clone.GetComponent<DoppelShockerBehaviour>().idleTime = idleTime;
		}
		}
	}

	void Update() {
		if (idleTime2 > 0) {
			idleTime2-=Time.deltaTime;
		}
		if (idleTime2 <= 0 && !hasSpawned) {
			Vector3 p = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (9.25f + 0.85f) / 22.5f, 3f));
			GameObject g = Instantiate(artillery, p, Quaternion.identity) as GameObject;
			hasSpawned = true;
		}
	}
}
