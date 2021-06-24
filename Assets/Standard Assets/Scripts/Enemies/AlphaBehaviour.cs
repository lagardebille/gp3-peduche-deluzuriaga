using UnityEngine;
using System.Collections;

public class AlphaBehaviour : MonoBehaviour {
	
	public int loops = 0;
	public int rot;
	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, 0, rot);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			transform.Translate (Vector3.right * Time.deltaTime * GetComponent<EnemySpeed> ().speed);
			transform.rotation = Quaternion.Euler (0, rot, 0);
			if (loops > 0) {
				if (transform.position.x > 40f && rot == 0) {
					rot = 180;
					loops--;
				} else if (transform.position.x < -40f && rot == 180) {
					rot = 0;
					loops--;
				}
			} else {
				if (transform.position.x > 42f && rot == 0) {
					Destroy (gameObject);
				} else if (transform.position.x < -42f && rot == 180) {
					Destroy (gameObject);
				}
			}
		}
	}
}
