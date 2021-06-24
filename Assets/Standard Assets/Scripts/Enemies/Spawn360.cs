using UnityEngine;
using System.Collections;

public class Spawn360 : MonoBehaviour {

	public GameObject jacque360;
	GameObject player;
	public int count = 0;
	bool switched = false;
	AudioSource audio;
	public AudioClip sfx;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnEnemy", 15f, 5f);
		player = GameObject.FindGameObjectWithTag("Player");
		audio = Camera.main.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void SpawnEnemy() {
		player = GameObject.FindGameObjectWithTag("Player");
		if (!switched)
			switched = true;
		else if (switched)
			switched = false;
		if (player != null && count <= 2) {
			if (player.GetComponent<Snake> ().isPlaying && PlayerPrefs.GetInt("diff") > 1 && player.GetComponent<DataHolder>().freezeTimer <= 0) {
				audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));

				float x = 0, x2 = 0;
				float y = (int)Random.Range (-13, 11);
				float y2 = (int)Random.Range (-13, 11);

				if (y > 0) y += 0.25f;
				else if (y <= 0)y -= 0.75f;
				else if (y2 > 0) y2 += 0.25f;
				else if (y2 <= 0)y2 -= 0.75f;

				float ratio = (float)Screen.width / (float)Screen.height;
				Debug.Log (ratio);

				if (ratio >= 1.77) {//169
					x = (int)Random.Range(-23,-20) + 0.5f;
					x2 = (int)Random.Range(22,24) + 0.5f;
				} else if (ratio < 1.77 && ratio >= 1.59f) { //1610
					x = (int)Random.Range(-20,-18) + 0.5f;
					x2 = (int)Random.Range(19,21) + 0.5f;
				} else if (ratio < 1.59f && ratio >= 1.49f) { //32
					x = (int)Random.Range(-19,-17) + 0.5f;
					x2 = (int)Random.Range(19,21) + 0.5f;
				} else if (ratio < 1.49f) { //43
					x = (int)Random.Range(-17,-15) + 0.5f;
					x2 = (int)Random.Range(17,19) + 0.5f;
				} 
				
				Vector3 pos = new Vector3(x,y,-6.36499f);
				Vector3 pos2 = new Vector3(x2,y2,-6.36499f);
				
				GameObject clone, clone2;
				if (switched)clone = Instantiate (jacque360, pos, Quaternion.identity) as GameObject;
				else if (!switched) clone2 = Instantiate (jacque360, pos2, Quaternion.identity) as GameObject;
				count++;			}
		}
	}

}
