using UnityEngine;
using System.Collections;

public class SpawnBigSpike : MonoBehaviour {
	
	public GameObject borders;
	public GameObject[] enemy = new GameObject[2];
	private int dir = 1;
	int spikeCount;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating ("SpawnEnemy", 10f, 10f);
	}
	
	// Update is called once per frame
	void SpawnEnemy () {
		player = GameObject.FindGameObjectWithTag("Player");
		spikeCount = 0;
		if ( player!= null && player.GetComponent<Snake> ().isPlaying && player.GetComponent<DataHolder>().freezeTimer <= 0 && PlayerPrefs.GetInt("diff") > 1) {
			
			foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()){
				if(gameObj.name.StartsWith("SpikeBig")){
					spikeCount++;
				}
			}
			
			int x = Random.Range (41,43);
			int x2 = Random.Range (-4, -1);
			int y = Random.Range (1, 19);
			int y2 = Random.Range (1, 19);
			
			GameObject clone, clone2;
			Vector3 pos, pos2;
			
			if (spikeCount == 1) {
				pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
				clone = Instantiate (enemy[0], pos, Quaternion.identity) as GameObject;
				if (y < 9)
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, 1) * 2f;
				else
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, -1) * 2f;
				borders.SetActive (false);
				StartCoroutine(DelayWall());
			} else if (spikeCount == 0) {
				pos = Camera.main.ViewportToWorldPoint (new Vector3 ((x + 0.5f) / 40f, (y + 0.85f) / 22.5f, 3f));
				pos2 = Camera.main.ViewportToWorldPoint (new Vector3 ((x2 + 0.5f) / 40f, (y2 + 0.85f) / 22.5f, 3f));
				
				clone = Instantiate (enemy[0], pos, Quaternion.identity) as GameObject;
				clone2 = Instantiate (enemy[1], pos2, Quaternion.identity) as GameObject;
				if (y < 9)
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, 1) * 4f;
				else
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1, -1) * 4f;
				if (y2 < 9)
					clone2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, 1) * 4f;
				else
					clone2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, -1) * 4f;
				borders.SetActive (false);
				StartCoroutine(DelayWall());
			}
		}
	}
	
	IEnumerator DelayWall() {
		yield return new WaitForSeconds (1f);
		borders.SetActive (true);
	}
}
