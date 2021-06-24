using UnityEngine;
using System.Collections;

public class ChangePrompt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("controls") == 1) {
			transform.GetChild(0).GetComponent<Animator>().SetTrigger("Tap");
		} else if (PlayerPrefs.GetInt ("controls") == 2) {
			transform.GetChild(0).GetComponent<Animator>().SetTrigger("Swipe");
		}
	}
}
