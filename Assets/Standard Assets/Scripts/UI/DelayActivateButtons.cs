using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DelayActivateButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().interactable = false;
		StartCoroutine (DelayBtn ());
	}
	
	// Update is called once per frame
	IEnumerator DelayBtn() {
		yield return new WaitForSeconds (0.5f);
		GetComponent<Button> ().interactable = true;
	}
}
