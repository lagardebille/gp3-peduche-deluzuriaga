using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heng : MonoBehaviour {

	public float timerIn = 20f;
	public float timerGoing = 5f;
	public GameObject hengMini;
	bool hasInstantiated;
	public float[] coords = new float[2];
	public GameObject bg;
	public Sprite[] img = new Sprite[2];
	public GameObject[] blocks;
	GameObject snake;
	public AudioClip sfx;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		snake = GameObject.FindGameObjectWithTag ("Player");
		blocks = GameObject.FindGameObjectsWithTag("Blocks");
		audio = Camera.main.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		snake = GameObject.FindGameObjectWithTag ("Player");
		if (snake != null && snake.GetComponent<Snake> ().isPlaying && GetComponent<EnemySpeed> ().isTraitEnabled) {
			if (timerIn > 0) {
				GetComponent<Animator> ().SetTrigger ("EyeClose");
				timerIn -= Time.deltaTime;
				hasInstantiated = false;
				bg.GetComponent<SpriteRenderer>().sprite = img[0];
			}
			if (timerIn <= 0) {
				GetComponent<Animator> ().SetTrigger ("EyeOpen");
				if (!hasInstantiated) {
					AddMiniHeng ();
					audio.PlayOneShot(sfx, PlayerPrefs.GetInt ("Sound"));
					hasInstantiated = true;
					int i = (int) Random.Range(2, blocks.Length-2);
					if (!blocks[i].GetComponent<BlockBehaviour>().isStoned) blocks[i].GetComponent<BlockBehaviour>().isStoned = true;
					else {
						if (i+2 < blocks.Length) blocks[i+2].GetComponent<BlockBehaviour>().isStoned = true;
						else if (i-2 >= 0) blocks[i-2].GetComponent<BlockBehaviour>().isStoned = true;
					}

					GameObject g = GameObject.FindGameObjectWithTag("Food");
					g.GetComponent<FoodBehaviour>().isStoned = true;
					/*foreach (GameObject g in blocks) {
						g.GetComponent<BlockBehaviour>().isStoned = true;
					}*/
				}
				if (timerGoing > 0) {
					timerGoing -= Time.deltaTime;
					bg.GetComponent<SpriteRenderer>().sprite = img[1];
				}
				if (timerGoing <= 0) {
					timerIn = 5f;
					timerGoing = 5f;
					/*foreach (GameObject g in blocks) {
						g.GetComponent<BlockBehaviour>().isStoned = false;
					}*/
					GameObject g = GameObject.FindGameObjectWithTag("Food");
					g.GetComponent<FoodBehaviour>().isStoned = false;
				}
			}
		}
	}

	void AddMiniHeng() {
		int where = (int)Random.Range (1, 5);
		int x = (int)Random.Range (8, 32);
		int y = 25;
		Vector3 pos;
		pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
		GameObject clone = Instantiate (hengMini, pos, Quaternion.identity) as GameObject;
	}
}
