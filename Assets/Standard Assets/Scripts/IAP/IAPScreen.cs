using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IAPScreen : MonoBehaviour {

	public Sprite[] bg = new Sprite[6];
	// Use this for initialization
	void Start () {
	//	PlayerPrefs.SetInt ("ActiveChapter", 1);
	//	PlayerPrefs.SetString("NextLevel", "Level1");
		int activeLevel = PlayerPrefs.GetInt("ActiveChapter")-1;
		GetComponent<Image> ().sprite = bg [activeLevel];
	}

	void Update() {
		//Debug.Log(PlayerPrefs.GetString("NextLevel"));
	}

	public void LoadNextLevel() {
		Application.LoadLevel(PlayerPrefs.GetString("NextLevel"));
	}

	public void LoadMenu() {
		Application.LoadLevel("MainMenu");
	}

	public void LoadIAP() {
		Application.LoadLevel ("MainMenu");
		PlayerPrefs.SetString("ActivePane", "iap");
	}
}
