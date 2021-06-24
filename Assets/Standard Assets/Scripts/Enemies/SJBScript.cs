using UnityEngine;
using System.Collections;

public class SJBScript : MonoBehaviour {

	private int rand;
	public GameObject[] artillery = new GameObject[3];
	public AudioClip sfx;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		Invoke ("PlayColor", 2.5f);
		audio = Camera.main.GetComponent<AudioSource> ();
	}

	void Update() {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void PlayColor () {
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			rand = Random.Range (0, 3);
			switch (rand) {
			case 0:
				GetComponent<Animator> ().SetTrigger ("Yellow");
				break;
			case 1:
				GetComponent<Animator> ().SetTrigger ("Red");
				break;
			case 2:
				GetComponent<Animator> ().SetTrigger ("Green");
				break;
			}
			StartCoroutine (des ());
		}
	}

	IEnumerator des() {
		yield return new WaitForSeconds (3f);
		int randX = Random.Range (2, 37);
		int randY = Random.Range (2, 18);
		int randX2 = Random.Range (2, 37);
		int randY2 = Random.Range (2, 18);
		Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3((randX+ 0.5f) / 40f, (randY+ 0.85f) / 22.5f, 3f));;
		Vector3 pos2 = Camera.main.ViewportToWorldPoint(new Vector3((randX2+ 0.5f) / 40f, (randY2+ 0.85f) / 22.5f, 3f));;
		GameObject clone = Instantiate (artillery [rand], pos, Quaternion.identity) as GameObject;
		GameObject clone2 = Instantiate (artillery [rand], pos2, Quaternion.identity) as GameObject;
		audio.PlayOneShot (sfx, PlayerPrefs.GetInt ("Sound"));
		Destroy (gameObject);
	}
}
