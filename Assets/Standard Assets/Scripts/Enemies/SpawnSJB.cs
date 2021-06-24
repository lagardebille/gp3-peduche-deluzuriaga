using UnityEngine;
using System.Collections;

public class SpawnSJB : MonoBehaviour {

	public GameObject sjb, overlay;
	private float[] coordsX = new float[9]{-15.5f, -10.5f, -6.5f, -3.5f, 0.5f, 3.5f, 6.5f, 10.5f, 15.5f};
	//-10.5, -6.5, -3.5, 0.5, 3.5, 6.5, 10.5
	//-5.75, -1.75, 1.25

	private float[] coordsY = new float[3]{-5.75f, -1.75f, 1.25f};
	GameObject player;
	public AudioClip sfx;
	AudioSource audio;
	float timer = 0; 
	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 15f, 15f);
		player = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
		overlay = GameObject.FindGameObjectWithTag("Overlay");

	}

	void Update() {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			overlay.transform.GetChild(0).gameObject.SetActive (false);
		}
	}
	// Update is called once per frame
	void Spawn () {
		player = GameObject.FindGameObjectWithTag("Player");
		if (player.GetComponent<Snake> ().isPlaying && PlayerPrefs.GetInt ("diff") > 1 && player.GetComponent<DataHolder>().freezeTimer <= 0) {
			if (GameObject.FindGameObjectWithTag ("sjb") == null && player.GetComponent<Snake> ().isPlaying) {
				timer = 7.5f;
				int rand = Random.Range (0, 10);
				int rand2 = Random.Range (0, 4);
				Vector3 pos = new Vector3(coordsX[rand],coordsY[rand2],-6.36499f);
				GameObject clone = Instantiate (sjb, pos, Quaternion.identity) as GameObject;
				overlay.transform.GetChild(0).gameObject.SetActive (true);
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
			}
		}
	}
}