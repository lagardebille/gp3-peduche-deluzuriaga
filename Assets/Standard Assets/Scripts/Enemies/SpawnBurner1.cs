using UnityEngine;
using System.Collections;

public class SpawnBurner1 : MonoBehaviour {

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
		InvokeRepeating ("Spawn", 0f, 1f);
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
				if (gameObj.name.StartsWith ("Burner1")) {
					burner1Count++;
				}
			}
			if (burner1Count == 0) {
				rand = (int)Random.Range (0, 1);
				Debug.Log(rand);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://up
					x = snake.transform.position.x;
					y = -4;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.x = x;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "y";
					clone.GetComponent<Burner1Behaviour> ().rot = 0;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://down
					x = snake.transform.position.x;
					y = 24;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.x = x;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "y";
					clone.GetComponent<Burner1Behaviour> ().rot = 180;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 2://right
					x = -4;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "x";
					clone.GetComponent<Burner1Behaviour> ().rot = 0;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 3://left
					x = 44;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "x";
					clone.GetComponent<Burner1Behaviour> ().rot = 180;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
			} /*else if (burner1Count == 0) {
				rand = (int)Random.Range (0, 4);
				rand2 = (int)Random.Range (0, 4);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
				switch (rand) {
				case 0://up
					x = snake.transform.position.x;
					y = -4;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.x = x;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "y";
					clone.GetComponent<Burner1Behaviour> ().rot = 0;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://down
					x = snake.transform.position.x;
					y = 24;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.x = x;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "y";
					clone.GetComponent<Burner1Behaviour> ().rot = 180;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 2://right
					x = -4;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "x";
					clone.GetComponent<Burner1Behaviour> ().rot = 0;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 3://left
					x = 44;
					y = snake.transform.position.y;
					pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos.y = y;
					clone = Instantiate (burner, pos, Quaternion.identity) as GameObject;
					clone.GetComponent<Burner1Behaviour> ().axis = "x";
					clone.GetComponent<Burner1Behaviour> ().rot = 180;
					clone.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
				switch (rand2) {
				case 0://up
					x = snake.transform.position.x;
					y = -4;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.x = x;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner1Behaviour> ().axis = "y";
					clone2.GetComponent<Burner1Behaviour> ().rot = 0;
					clone2.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone2.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 1://down
					x = snake.transform.position.x;
					y = 24;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.x = x;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner1Behaviour> ().axis = "y";
					clone2.GetComponent<Burner1Behaviour> ().rot = 180;
					clone2.GetComponent<Burner1Behaviour> ().rotZ = 90;
					clone2.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 2://right
					x = -4;
					y = snake.transform.position.y;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.y = y;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner1Behaviour> ().axis = "x";
					clone2.GetComponent<Burner1Behaviour> ().rot = 0;
					clone2.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone2.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				case 3://left
					x = 44;
					y = snake.transform.position.y;
					pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
					pos2.y = y;
					clone2 = Instantiate (burner, pos2, Quaternion.identity) as GameObject;
					clone2.GetComponent<Burner1Behaviour> ().axis = "x";
					clone2.GetComponent<Burner1Behaviour> ().rot = 180;
					clone2.GetComponent<Burner1Behaviour> ().rotZ = 0;
					clone2.GetComponent<Burner1Behaviour> ().loops = (int)Random.Range (2, 8);
					break;
				}
			} */
		}
	}
}
