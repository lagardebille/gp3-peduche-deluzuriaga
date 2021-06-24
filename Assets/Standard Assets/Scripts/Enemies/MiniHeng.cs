using UnityEngine;
using System.Collections;

public class MiniHeng : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			transform.Translate (Vector3.down * Time.deltaTime * GetComponent<EnemySpeed> ().speed);
			if (transform.position.y < -18f)
				Destroy (gameObject);
		}
	}
}
