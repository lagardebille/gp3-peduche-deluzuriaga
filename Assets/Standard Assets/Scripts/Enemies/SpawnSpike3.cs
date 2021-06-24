using UnityEngine;
using System.Collections;

public class SpawnSpike3 : MonoBehaviour {

	public GameObject spike3;
	float[] coordXTable = new float[4]{25.5f,-25.5f,17f,-17f};//17,-17
	float[] coordYTable = new float[4]{4.75f,-9.25f,18f,-18f};//18,-18
	float coordX, coordY;
	int[] rots = new int[4]{0,90,180,270};
	int spikeCount = 0;
	int spawnWhere;
	int dir;
	GameObject snake;
	GameObject clone;
	Vector3 rot;

	// Use this for initialization
	void Start () {
		float ratio = (float)Screen.width / (float)Screen.height;
		Debug.Log (ratio);
		if (ratio >= 1.77) {//169
			coordXTable = new float[4]{25.5f,-25.5f,17f,-17f};//17,-17
		} else if (ratio < 1.77 && ratio >= 1.59f) { //1610
			coordXTable = new float[4]{23.5f,-23.5f,15f,-15f};//17,-17
		} else if (ratio < 1.59f && ratio >= 1.49f) { //32
			coordXTable = new float[4]{22.5f,-22.5f,14f,-14f};//17,-17
		} else if (ratio < 1.49f) { //43
			coordXTable = new float[4]{20.5f,-20.5f,12f,-12f};//17,-17
		} 
		snake = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("Spawn", 10f, 10f);
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		if (snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			spikeCount = 0;
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("Spike3")) {
					spikeCount++;
				}
			}
			if (spikeCount == 0) {
				spawnWhere = (int)Random.Range (0, 8);
				if (spawnWhere == 0) {
					coordX = coordXTable [1];
					coordY = coordYTable [0];
					dir = 2;
					rot = new Vector3 (0, 0, 180);//
				} else if (spawnWhere == 1) {
					coordX = coordXTable [1];
					coordY = coordYTable [1];
					dir = 1;
					rot = new Vector3 (0, 0, 0);//asdasd
				} else if (spawnWhere == 2) {
					coordX = coordXTable [0];
					coordY = coordYTable [0];
					dir = 1;
					rot = new Vector3 (0, 0, 180);//
				} else if (spawnWhere == 3) {
					coordX = coordXTable [0];
					coordY = coordYTable [1];
					dir = 2;
					rot = new Vector3 (0, 0, 0);//asdasd
				} else if (spawnWhere == 4) {//new
					coordX = coordXTable [3];
					coordY = coordYTable [2];
					dir = 1;
					rot = new Vector3 (0, 0, 270);
				} else if (spawnWhere == 5) {
					coordX = coordXTable [3];
					coordY = coordYTable [3];
					dir = 2;
					rot = new Vector3 (0, 0, 270);
				} else if (spawnWhere == 6) {
					coordX = coordXTable [2];
					coordY = coordYTable [2];
					dir = 2;
					rot = new Vector3 (0, 0, 90);
				} else if (spawnWhere == 7) {
					coordX = coordXTable [2];
					coordY = coordYTable [3];
					dir = 1;
					rot = new Vector3 (0, 0, 90);
				}
				clone = Instantiate (spike3, new Vector3 (coordX, coordY, -6.36499f), Quaternion.Euler (rot)) as GameObject;
				int willRev = (int)Random.Range (0, 2);
				clone.GetComponent<Spike3Behaviour> ().dir = dir;
				if (willRev == 0)
					clone.GetComponent<Spike3Behaviour> ().willReverse = false;
				else if (willRev == 1)
					clone.GetComponent<Spike3Behaviour> ().willReverse = true;
			}
		}
	}
}