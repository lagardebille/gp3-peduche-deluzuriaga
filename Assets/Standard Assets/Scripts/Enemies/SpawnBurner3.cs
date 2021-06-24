using UnityEngine;
using System.Collections;

public class SpawnBurner3 : MonoBehaviour {
	
	int whereX, whereY;
	int burner1Count;
	public GameObject burner;
	GameObject snake;
	public AudioClip sfx;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
		InvokeRepeating ("Spawn", 10f, 8f);
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		burner1Count = 0;
		int rand, rand2;
		GameObject clone, clone2;
		Vector3 pos, pos2;
		float x, y, x2, y2;
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1)  {
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("Burner3")) {
					burner1Count++;
				}
			}
			if (burner1Count == 1) {
				rand = (int)Random.Range (0, 2);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://right
					x = -4;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner3Behaviour> ().rot = 0;
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					clone.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					break;
				case 1://left
					x = 44;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner3Behaviour> ().rot = 180;
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					clone.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					break;
				}
			} else if (burner1Count == 0) {
				rand = (int)Random.Range (0, 2);
				rand2 = (int)Random.Range (0, 2);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://right
					x = -4;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner3Behaviour> ().rot = 0;
					clone.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				case 1://left
					x = 44;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner3Behaviour> ().rot = 180;
					clone.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				}
				switch (rand2) {
				case 0://right
					x = -4;
					y = Random.Range(4,17);
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner3Behaviour> ().rot = 0;
					clone2.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					clone2.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				case 1://left
					x = 44;
					y = Random.Range(4,17);
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner3Behaviour> ().rot = 180;
					clone2.GetComponent<Burner3Behaviour> ().loops = (int)Random.Range (1, 3);
					clone2.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				}
			}
		}
	}
}
