using UnityEngine;
using System.Collections;

public class Burner1Behaviour : MonoBehaviour {

	public int loops = 0;
	public string axis;
	public int rot, rotZ;
	// Use this for, rot initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, rot, rotZ);
		//22,-22 x
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			transform.Translate (Vector3.right * Time.deltaTime * GetComponent<EnemySpeed> ().speed);
			transform.rotation = Quaternion.Euler (0, rot, rotZ);
			if (loops > 0) {
				if (axis.Equals ("x")) {
					if (transform.position.x > 17f && rot == 0) {
						rot = 180;
						loops--;
					} else if (transform.position.x < -17f && rot == 180) {
						rot = 0;
						loops--;
					}
				} else if (axis.Equals ("y")) {
					if (transform.position.y > 7f && rotZ == 90) {
						rotZ = 270;
						loops--;
					} else if (transform.position.y < -9f && rotZ == 270) {
						rotZ = 90;
						loops--;
					}
				}
			} else {
				if (axis.Equals ("x")) {
					if (transform.position.x > 24f && rot == 0) {
						Destroy (gameObject);
					} else if (transform.position.x < -24f && rot == 180) {
						Destroy (gameObject);
					}
				} else if (axis.Equals ("y")) {
					if (transform.position.y > 15f && rotZ == 90) {
						Destroy (gameObject);
					} else if (transform.position.y < -15f && rotZ == 270) {
						Destroy (gameObject);
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
	}
}
