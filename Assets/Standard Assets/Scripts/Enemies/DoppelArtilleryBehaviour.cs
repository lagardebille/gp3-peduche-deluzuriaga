using UnityEngine;
using System.Collections;

public class DoppelArtilleryBehaviour : MonoBehaviour {
	
	public float idleTime;
	public float shootFullTime;
	public float exitTime;
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
			idleTime -= Time.deltaTime;
			if (idleTime <= 0) {
				idleTime = 0;
				if (!animFull) {
					GetComponent<Animator> ().SetTrigger ("Full");
					animFull = true;
				}
				//Debug.Log (shootFullTime);
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
