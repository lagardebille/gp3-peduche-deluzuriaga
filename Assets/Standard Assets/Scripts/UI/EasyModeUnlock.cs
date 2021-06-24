using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyModeUnlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetInt ("C1Finished") == 1 && PlayerPrefs.GetInt ("C2Finished") == 1 && PlayerPrefs.GetInt ("C3Finished") == 1)
			GetComponent<Button> ().interactable = true;
	}
}
