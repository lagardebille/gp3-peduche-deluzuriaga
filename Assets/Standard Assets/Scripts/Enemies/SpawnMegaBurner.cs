using UnityEngine;
using System.Collections;

public class SpawnMegaBurner : MonoBehaviour {
	
	int whereX, whereY;
	int burner1Count;
	public GameObject[] burners = new GameObject[2];
	GameObject snake;
	public AudioClip sfx;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
		InvokeRepeating ("Spawn", 10f, 15f);
	}
	
	// Update is called once per frame
	void Spawn () {
		snake = GameObject.FindGameObjectWithTag("Player");
		burner1Count = 0;
		int rand, rand2;
		GameObject clone, clone2;
		Vector3 pos, pos2;
		float x, y, x2, y2;
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("MegaBurner")) {
					burner1Count++;
				}
			}
			if (burner1Count == 0) {
				rand = (int)Random.Range (0, 2);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://right
					x = -4;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burners [(int)Random.Range (0, 2)], pos, Quaternion.identity) as GameObject;
					clone.GetComponent<MegaBurnerBehaviour> ().rot = 0;
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				case 1://left
					x = 44;
					y = Random.Range(4,17);
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					clone = Instantiate (burners [(int)Random.Range (0, 2)], pos, Quaternion.identity) as GameObject;
					clone.GetComponent<MegaBurnerBehaviour> ().rot = 180;
					clone.GetComponent<EnemySpeed> ().speed = (int)Random.Range (5, 7);
					break;
				}
			}
		}
	}
}
