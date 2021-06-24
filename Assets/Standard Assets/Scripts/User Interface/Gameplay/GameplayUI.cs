using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {

	public GameObject pause, settings;
	public GameObject tap, swipe;
	public Button continue_after_fail;
	public GameObject gameoverScreen;
	public Sprite[] continue_after_fail_sprite;
	public int cNum, lNum;
	int caf_use = 0;
	float timer;

	GameObject snakeHead;
	GameObject runtimeUI;
	// Use this for initialization
	void Start () {
		snakeHead = GameObject.FindGameObjectWithTag ("Player");
		timer = snakeHead.GetComponent<DataHolder> ().timer;
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
	}
	
	// Update is called once per frame
	void Update () {
		runtimeUI = GameObject.FindGameObjectWithTag("RuntimeUI");
		snakeHead = GameObject.FindGameObjectWithTag ("Player");

		if (PlayerPrefs.GetInt ("Continue") > 0) {
			continue_after_fail.GetComponent<Image> ().sprite = continue_after_fail_sprite [1];
			continue_after_fail.interactable = true;
		} else {
			continue_after_fail.GetComponent<Image> ().sprite = continue_after_fail_sprite [0];
			continue_after_fail.interactable = false;
		}
	}

	public void ShowPause(bool b) {
		if (b)
			Time.timeScale = 0;
		else if (!b)
			Time.timeScale = 1;
		if (!snakeHead.GetComponent<DataHolder>().isSwiping)tap.SetActive (!b);
		else if (snakeHead.GetComponent<DataHolder>().isSwiping)swipe.SetActive (!b);
		pause.SetActive (b);
	}

	public void ShowSettings(bool b) {
		settings.SetActive (b);
		pause.SetActive (!b);
	}

	public void RestartLevel(string s) {
		Application.LoadLevel (s);
		if (snakeHead.GetComponent<DataHolder> ().starCount == 3)
			PlayerPrefs.SetInt ("perfect_plays", PlayerPrefs.GetInt ("perfect_plays") + 1);
		PlayerPrefs.SetString ("ActivePane", "chapter" + cNum.ToString ());
	}

	public void BuyAPerk() {
		Application.LoadLevel ("MainMenu");
		PlayerPrefs.SetString("ActivePane", "iap");
	}
	

	public void LoadIAP(string s) {
		PlayerPrefs.SetString("NextLevel", s);
		PlayerPrefs.SetInt ("ActiveChapter", cNum);
		PlayerPrefs.SetInt ("Chapter" + cNum.ToString () + "Level" + lNum.ToString () + "Stars", snakeHead.GetComponent<DataHolder> ().starCount);
		if (snakeHead.GetComponent<DataHolder> ().starCount == 3)
			PlayerPrefs.SetInt ("perfect_plays", PlayerPrefs.GetInt ("perfect_plays") + 1);
		PlayerPrefs.SetString ("ActivePane", "chapter" + cNum.ToString ());
		Application.LoadLevel("MainMenu");
	}

	public void LoadNextChapter(string s) {
		PlayerPrefs.SetString("NextLevel", s);
		PlayerPrefs.SetInt ("ActiveChapter", cNum+1);
		PlayerPrefs.SetString ("ActivePane", "chapter" + (cNum+1).ToString ());
		Application.LoadLevel("MainMenu");
	}

	public void LoadMenu(string s) {
		Application.LoadLevel("MainMenu");
		PlayerPrefs.SetInt ("Chapter" + cNum.ToString () + "Level" + lNum.ToString () + "Stars", snakeHead.GetComponent<DataHolder> ().starCount);
		if (snakeHead.GetComponent<DataHolder> ().starCount == 3)
			PlayerPrefs.SetInt ("perfect_plays", PlayerPrefs.GetInt ("perfect_plays") + 1);
		//PlayerPrefs.SetString ("ActivePane", s);
	}

	public void ContinueAfterFail() {
		if (caf_use < 3) {
			PlayerPrefs.SetInt ("Continue", PlayerPrefs.GetInt ("Continue") - 1);
			snakeHead.GetComponent<Snake> ().isPlaying = true;
			snakeHead.GetComponent<Snake> ().currentSpeed = PlayerPrefs.GetFloat ("speed");
			snakeHead.GetComponent<Snake> ().isMoving = false;
			gameoverScreen.SetActive (false);
			snakeHead.GetComponent<Animator> ().SetTrigger ("Idle");
			snakeHead.GetComponent<DataHolder> ().timer = timer;
			caf_use++;
		}
	}

	public void Activate(GameObject g) {
		int i = g.GetComponent<PowerupBehaviour> ().whatPowerup;
		if (g.GetComponent<PowerupBehaviour> ().numUse > 0) {
			g.GetComponent<PowerupBehaviour> ().numUse--;
			switch (i) {
			case 1:
				if (PlayerPrefs.GetInt ("pow_1") > 0){
					PlayerPrefs.SetInt ("pow_1", PlayerPrefs.GetInt ("pow_1") - 1);//invu
					if (PlayerPrefs.GetInt ("CharActive") == 1) snakeHead.GetComponent<DataHolder> ().radiusTimer += 17;
					else snakeHead.GetComponent<DataHolder> ().radiusTimer += 15;
					g.GetComponent<PowerupBehaviour> ().timer +=15;
				} break;
			case 2:
				if (PlayerPrefs.GetInt ("pow_2") > 0){
					PlayerPrefs.SetInt ("pow_2", PlayerPrefs.GetInt ("pow_2") - 1);
					if (PlayerPrefs.GetInt ("CharActive") == 1) snakeHead.GetComponent<DataHolder> ().freezeTimer += 17;
					else snakeHead.GetComponent<DataHolder> ().freezeTimer += 15;
					g.GetComponent<PowerupBehaviour> ().timer +=15;
				} break;
			case 3:
				if (PlayerPrefs.GetInt ("pow_3") > 0){
					PlayerPrefs.SetInt ("pow_3", PlayerPrefs.GetInt ("pow_3") - 1);
					if (PlayerPrefs.GetInt ("CharActive") == 1) snakeHead.GetComponent<DataHolder> ().invuTimer += 17;
					else snakeHead.GetComponent<DataHolder> ().invuTimer += 15;
					g.GetComponent<PowerupBehaviour> ().timer +=15;
				} break;
			case 4:
				if (PlayerPrefs.GetInt ("pow_4") > 0){
					PlayerPrefs.SetInt ("pow_4", PlayerPrefs.GetInt ("pow_4") - 1);
					snakeHead.GetComponent<DataHolder> ().lifeActive =  true;
				} break;
			case 5:
				if (PlayerPrefs.GetInt ("pow_5") > 0){
					PlayerPrefs.SetInt ("pow_5", PlayerPrefs.GetInt ("pow_5") - 1);
					if (PlayerPrefs.GetInt ("CharActive") == 1) snakeHead.GetComponent<DataHolder> ().passTimer += 17;
					else snakeHead.GetComponent<DataHolder> ().passTimer += 15;
					g.GetComponent<PowerupBehaviour> ().timer +=15;
				}break;
			default:
				g.GetComponent<PowerupBehaviour> ().whatPowerup = 0;
				break;
			}
		}
		else if (g.GetComponent<PowerupBehaviour> ().numUse == 0) g.GetComponent<PowerupBehaviour> ().whatPowerup = 0;
	}
}
