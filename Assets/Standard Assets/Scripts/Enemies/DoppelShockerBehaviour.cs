using UnityEngine;
using System.Collections;

public class DoppelShockerBehaviour : MonoBehaviour {

	public float idleTime = 0f;
	public float shootFullTime = 0f;
	public float exitTime = 0f;
	public float rotZ;
	bool animIdle, animFull;
	
	// Use this for initialization
	void Start () {
		animIdle = false;
		animFull = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("diff") <= 1)
			Destroy (gameObject);
		if (GetComponent<EnemySpeed> ().isTraitEnabled) {
			transform.rotation = Quaternion.Euler (0, 0, rotZ);
			idleTime -= Time.deltaTime;
			if (idleTime <= 0) {
				idleTime = 0;
				if (!animFull) {
					GetComponent<Animator> ().SetTrigger ("Open");
					animFull = true;
				}
				shootFullTime -= Time.deltaTime;
				if (shootFullTime <= 0) {
					shootFullTime = 0;
					if (!animIdle) {
						GetComponent<Animator> ().SetTrigger ("Exit");
						animIdle = true;
					}
					exitTime -= Time.deltaTime;
					if (exitTime <= 0)
						Destroy (gameObject);
				}
			}
		}
	}
}
