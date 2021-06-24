using UnityEngine;
using System.Collections;

public class SpawnMasturmine : MonoBehaviour {

	public GameObject enemy;
	GameObject player;
	AudioSource audio;
	public AudioClip sfx;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnEnemy", 10f, 10f);
		player = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
	}
	
	void SpawnEnemy() {
		if (player == null )player = GameObject.FindGameObjectWithTag("Player");
		if (player != null && player.GetComponent<Snake> ().isPlaying && player.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			int randX = Random.Range (2, 20);
			int randY = Random.Range (2, 18);
			int randX2 = Random.Range (25, 37);
			int randY2 = Random.Range (2, 18);
			Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3((randX+ 0.5f) / 40f, (randY+ 0.85f) / 22.5f, 3f));;
			Vector3 pos2 = Camera.main.ViewportToWorldPoint(new Vector3((randX2+ 0.5f) / 40f, (randY2+ 0.85f) / 22.5f, 3f));;
			GameObject clone = Instantiate (enemy, pos, Quaternion.identity) as GameObject;
			GameObject clone2 = Instantiate (enemy, pos2, Quaternion.identity) as GameObject;
			audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
		}
	}
}
