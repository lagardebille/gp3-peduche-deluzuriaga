using UnityEngine;
using System.Collections;

public class SpawnBurner2 : MonoBehaviour {
	
	int whereX, whereY;
	int burner1Count;
	GameObject snake;
	public GameObject burner;
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
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && snake.GetComponent<DataHolder> ().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
				if (gameObj.name.StartsWith ("Burner2")) {
					burner1Count++;
				}
			}
			if (burner1Count <= 0) {
				rand = (int)Random.Range (0, 2);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://right
					x = -4;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner2Behaviour> ().rot = 0;
					clone.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://left
					x = 44;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner2Behaviour> ().rot = 180;
					clone.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
			} /*else if (burner1Count == 0) {
				rand = (int)Random.Range (0, 2);
				rand2 = (int)Random.Range (0, 2);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://right
					x = -4;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner2Behaviour> ().rot = 0;
					clone.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://left
					x = 44;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner2Behaviour> ().rot = 180;
					clone.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
				switch (rand2) {
				case 0://right
					x = -4;
					y = snake.transform.position.y;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.y = y;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner2Behaviour> ().rot = 0;
					clone2.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://left
					x = 44;
					y = snake.transform.position.y;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.y = y;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner2Behaviour> ().rot = 180;
					clone2.GetComponent<Burner2Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
			} */
		}
	}
}
