using UnityEngine;
using System.Collections;

public class SpawnFood : MonoBehaviour {
	
	public GameObject foodPrefab, zrexAkPrefab;
	GameObject snakeHead;
	public AudioClip spawn;
	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		audio = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource> ();
	}  
	
	void Update() {
		snakeHead = GameObject.FindGameObjectWithTag("Player");
		if (GameObject.FindGameObjectsWithTag ("Food").Length <= 0) {
			Spawn();
			audio.PlayOneShot(spawn, PlayerPrefs.GetInt ("Sound"));
		}
	}
	
	public void Spawn() {
		float x = 0;
		float y = (int)Random.Range (-11, 8);
		if (y > 0) 
			y += 0.25f;
		else
			y -= 0.75f;
		float ratio = (float)Screen.width / (float)Screen.height;
		if (ratio >= 1.77) {//169
			x = (int)Random.Range(-20,21) + 0.5f;
		} else if (ratio < 1.77 && ratio >= 1.59f) { //1610
			x = (int)Random.Range(-18,19) + 0.5f;
		} else if (ratio < 1.59f && ratio >= 1.49f) { //32
			x = (int)Random.Range(-17,18) + 0.5f;
		} else if (ratio < 1.49f) { //43
			x = (int)Random.Range(-15,16) + 0.5f;
		} 
		Vector3 pos = new Vector3(x,y,-6.36499f);
		// Instantiate the food at (x, y)
		if (PlayerPrefs.GetInt ("CharActive") == 2)
			Instantiate (zrexAkPrefab, pos, Quaternion.identity); // default rotation
		else
			Instantiate (foodPrefab, pos, Quaternion.identity);
	}
	
}
