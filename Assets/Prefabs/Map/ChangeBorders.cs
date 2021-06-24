using UnityEngine;
using System.Collections;
using System;

public class ChangeBorders : MonoBehaviour {

	public GameObject[] borders;
	//169 1.778
	//1610 1.6
	//32 1.5
	//43 1.33
	// Use this for initialization
	void Start () {
		Vector3 newV = new Vector3 (-20f, 9f, 3f);
		float ratio = (float)Screen.width / (float)Screen.height;
		if (ratio >= 1.77) {
			//borders [0].SetActive (true);
			GameObject g = Instantiate(borders[0], newV, Quaternion.identity) as GameObject;
		} else if (ratio < 1.77 && ratio >= 1.59f) {
			//borders [1].SetActive (true);
			GameObject g = Instantiate(borders[1], newV, Quaternion.identity) as GameObject;
		} else if (ratio < 1.59f && ratio >= 1.49f) {
			//borders [2].SetActive (true);
			GameObject g = Instantiate(borders[2], newV, Quaternion.identity) as GameObject;
		} else if (ratio < 1.49f) {
			//borders [3].SetActive (true);
			GameObject g = Instantiate(borders[3], newV, Quaternion.identity) as GameObject;
		} 
	}

}
