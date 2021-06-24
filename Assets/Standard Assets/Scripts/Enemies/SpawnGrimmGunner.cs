using UnityEngine;
using System.Collections;

public class SpawnGrimmGunner : MonoBehaviour {

	public GameObject grimmGun;
	GameObject snake;
	int gunCount;
	GameObject clone;
	Vector3 pos;
	float x, y;
	int rotY, rotZ;
	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("Spawn", 10f, 10f);
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			gunCount = 0;
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("GrimmGun")) {
					gunCount++;
				}
			}
			if (gunCount == 0) {
				int rand = (int)Random.Range(0,4);
				switch(rand) {
					case 0:
						//rot z 90
						//rot y = 0
						//y = 0.5f
						//x = 1,38
						rotZ = 90;
						rotY = 0;
						y = 19.5f;
						x = (int)Random.Range(1,39);
						break;
					case 1:
						//rot z 270
						//rot y = 0
						//y = 18.5f
						//x = 1,38
						rotZ = 270;
						rotY = 0;
						y = -1f;
						x = (int)Random.Range(1,39);
						break;
					case 2:
						//rot z 0
						//rot y = 0
						//y = 1,17
						//x = 18.5f,
						rotZ = 0;
						rotY = 0;
						y = (int)Random.Range(1,18);
						x = 39.5f;
						break;
					case 3:
						//rot z 0
						//rot y = 180
						//y = 1,17
						//x = 20.5f,
						rotZ = 0;
						rotY = 180;
						y = (int)Random.Range(1,18);
						x = -0.5f;
						break;
				}
				pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
				clone = Instantiate(grimmGun, pos, Quaternion.identity) as GameObject;
				clone.GetComponent<GrimmGunBehaviour>().rotZ = rotZ;
				clone.GetComponent<GrimmGunBehaviour>().rotY = rotY;
			}
		}
	}
}
