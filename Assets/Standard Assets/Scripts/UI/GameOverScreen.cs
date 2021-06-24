using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

	
	public Sprite[] gameOverScreen = new Sprite[9];
	// Use this for initialization
	void Start () {
		GetComponent<Image> ().sprite = gameOverScreen [PlayerPrefs.GetInt ("goScreen") - 1];
	}

}
